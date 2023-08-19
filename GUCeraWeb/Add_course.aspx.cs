using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace GUCeraWeb
{
    public partial class Add_course : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TODO: verify the user is logged in and is an instructor
        }

        protected void CreateCourse(object sender, EventArgs e)
        {
            Session["user"] = 1;  //TODO: remove this line; i'm only using it to test

            string res = "";
            res += name_TextChanged(name.Text);
            res += hours_TextChanged(hours.Text);
            res += price_TextChanged(price.Text);

            if (String.IsNullOrEmpty(res))
            {
                string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
                SqlConnection conn = new SqlConnection(connStr);

                int creditHours = Int16.Parse(hours.Text);
                string cName = name.Text;
                decimal cPrice = Decimal.Parse(price.Text);

                SqlCommand addProc = new SqlCommand("InstAddCourse", conn);
                addProc.CommandType = System.Data.CommandType.StoredProcedure;

                addProc.Parameters.AddWithValue("@creditHours", creditHours);
                addProc.Parameters.AddWithValue("@name", cName);
                addProc.Parameters.AddWithValue("@price", cPrice);
                addProc.Parameters.AddWithValue("@instructorId", Session["user"]);


                conn.Open();
                addProc.ExecuteNonQuery();
                conn.Close();

                Label1.Text="You have successfully added " + cName +".";
                Label1.ForeColor = System.Drawing.Color.Green;

                SqlCommand findid = new SqlCommand("SELECT id FROM Course WHERE name = '" + cName + "'", conn);
                   
                conn.Open();
                SqlDataReader rdr = findid.ExecuteReader();
                int id = -1;
                if(rdr.Read() && !rdr.IsDBNull(0))
                {
                   id = rdr.GetInt32(0);
                }
                conn.Close();
                  
                if (id != -1)
                {
                    if(String.IsNullOrWhiteSpace(content.Text))
                    {
                        content.Text = "Not available";
                    }

                    if (String.IsNullOrWhiteSpace(desc.Text))
                    {
                        desc.Text = "Not available";
                    }
                    SqlCommand updateContent = new SqlCommand("UpdateCourseContent", conn);
                    updateContent.CommandType = System.Data.CommandType.StoredProcedure;

                    updateContent.Parameters.AddWithValue("@instrId", Session["user"]);
                    updateContent.Parameters.AddWithValue("@courseId", id);
                    updateContent.Parameters.AddWithValue("@content", content.Text);

                    SqlCommand updateDesc = new SqlCommand("UpdateCourseDescription", conn);
                    updateDesc.CommandType = System.Data.CommandType.StoredProcedure;

                    updateDesc.Parameters.AddWithValue("@instrId", Session["user"]);
                    updateDesc.Parameters.AddWithValue("@courseId", id);
                    updateDesc.Parameters.AddWithValue("@courseDescription", desc.Text);

                    conn.Open();
                    updateContent.ExecuteNonQuery();
                    updateDesc.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    Label2.Text = "Course Content and Description could not be updated due to an internal issue";
                    Label2.Attributes.Add("style", "color:red");
                }



                name.Text = "";
                hours.Text = "";
                price.Text = "";
                content.Text = "";
                desc.Text = "";
            }
            else
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = res;

            }
            
        }

        protected static string hours_TextChanged(string hours)
        {
            string res = "";

            if (String.IsNullOrWhiteSpace(hours))
                res += "Credit hours should not be left blank.\n";

            return res;
            

        }

        protected static string price_TextChanged(string price)
        {
            string res = "";

            if (String.IsNullOrEmpty(price)) 
                res += "Price should not be left blank.\n";

            return res;
            

        }

        protected static string name_TextChanged(string name)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand query = new SqlCommand("SELECT name FROM Course", conn);

            String res = "";
            conn.Open();
            SqlDataReader reader = query.ExecuteReader();

            while (reader.Read())
            {
                if (name.Equals(reader.GetString(0)))
                {
                    res = "A course with the same name already exists in the database.\n";
                }
            }
            conn.Close();

            if (String.IsNullOrWhiteSpace(name))
                res += "Course name should not be left blank.\n";

            return res;

        }

        protected void CheckInputs(object sender, EventArgs e)
        {
            string res = "";
            res += name_TextChanged(name.Text);
            res += hours_TextChanged(hours.Text);
            res += price_TextChanged(price.Text);

            if (String.IsNullOrEmpty(res))
            {
                Button1.Enabled = true;
            }
            else
            {
                HttpContext.Current.Response.Write(res);
                Button1.Enabled = false;
            }
           
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Instructor_home.aspx");
        }
    }
}