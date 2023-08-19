using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace GUCeraWeb
{
    public partial class Define_assignment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["user"] = 1; //TODO: remove this pls
            
            
            DataTable dt = getCourses();

            StringBuilder str = new StringBuilder("<h3>Available Courses:</h3>");

            str.Append("<table border='1' name='Available Courses'>");

            str.Append("<tr>");

                str.Append("<th>");
                str.Append("ID");
                str.Append("</th>");

                str.Append("<th>");
                str.Append("Course Name");
                str.Append("</th>");

                str.Append("<th>");
                str.Append("Credit Hours");
                str.Append("</th>");

            str.Append("</tr>");

            for(int i = 0;i<dt.Rows.Count;i++)
            {
                str.Append("<tr>");
                DataRow row = dt.Rows[i];
                for(int j = 0;j<dt.Columns.Count;j++)
                {
                    str.Append("<td>");
                    str.Append(row[j]);
                    str.Append("</td>");
                }

                str.Append("</tr>");
                
            }
            str.Append("</table>");

            if (dt.Rows.Count == 0)
            {
                str.Clear();
                str.Append("<p>You do not teach any accepted courses, so you cannot add any assignments to any course.</p>");
            }

            Placeholder1.Controls.Add(new Literal { Text = str.ToString() });

            if(dt.Rows.Count>0)
            {
                type.Attributes.Remove("Disabled");
                fullGrade.Attributes.Remove("Disabled");
                weight.Attributes.Remove("Disabled");
                deadline.Attributes.Remove("Disabled");
                time.Attributes.Remove("Disabled");
                content.Attributes.Remove("Disabled");
                Button1.Attributes.Remove("Disabled");
                
            }
        }

        private DataTable getCourses()
        {
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand command = new SqlCommand("InstructorViewAcceptedCoursesByAdmin", conn);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@instrId", Session["user"]);

            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            sda.Fill(dt);

            conn.Close();

            return dt;
        }

        protected void DefineAssignment(object sender,EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            SqlConnection conn = new SqlConnection(connStr);


            SqlCommand query = new SqlCommand("SELECT max(number) FROM Assignment WHERE cid = "+Int16.Parse(cid.Text)+" and type = '"+type.Text+"'",conn);


            conn.Open();
            SqlDataReader rdr = query.ExecuteReader();
            int num=1;
            if (rdr.Read() && !rdr.IsDBNull(0))
            {
                num = rdr.GetInt32(0)+1;
            }
            conn.Close();
            rdr.Close();

            string date = deadline.Text + " " + time.Text;

            SqlCommand proc = new SqlCommand("DefineAssignmentOfCourseOfCertianType", conn);
            proc.CommandType = CommandType.StoredProcedure;

            proc.Parameters.AddWithValue("@instId", Session["user"]);
            proc.Parameters.AddWithValue("@cid", Int16.Parse(cid.Text));
            proc.Parameters.AddWithValue("@number", num);
            proc.Parameters.AddWithValue("@type", type.Text);
            proc.Parameters.AddWithValue("@fullGrade", Int16.Parse(fullGrade.Text));
            proc.Parameters.AddWithValue("@weight", Double.Parse(weight.Text));
            proc.Parameters.AddWithValue("@deadline", date);
            proc.Parameters.AddWithValue("@content", content.Text);

            conn.Open();
            proc.ExecuteNonQuery();
            conn.Close();

            Label1.Text = type.Text + " " + num + " created successfully.";
            Label1.Attributes.Add("style", "color:green");

            type.ClearSelection();
            fullGrade.Text = "";
            deadline.Text = "";
            weight.Text = "";
            time.Text = "";
            content.Text = "";

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Instructor_home.aspx");
        }

    }
}