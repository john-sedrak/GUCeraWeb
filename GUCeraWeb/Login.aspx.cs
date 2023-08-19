using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void loginfunc(object sender, EventArgs e)
        {
            try
            {
                String conStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
                SqlConnection conn = new SqlConnection(conStr);
                int id = Int16.Parse(username.Text);
                String pass = password.Text;

                SqlCommand loginproc = new SqlCommand("userLogin", conn);
                loginproc.CommandType = System.Data.CommandType.StoredProcedure;
                loginproc.Parameters.Add(new SqlParameter("@id", id));
                loginproc.Parameters.Add(new SqlParameter("@password", pass));
                SqlParameter success = loginproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
                SqlParameter type = loginproc.Parameters.Add("@type", System.Data.SqlDbType.Int);

                success.Direction = System.Data.ParameterDirection.Output;
                type.Direction = System.Data.ParameterDirection.Output;

                conn.Open();
                loginproc.ExecuteNonQuery();
                conn.Close();

                if (success.Value.ToString() == "1")
                {
                    Session["user"] = id;
                    Response.Write("Logging in");
                    if (type.Value.ToString() == "0")
                    {
                        Response.Redirect("Instructor_home.aspx");
                    }

                    else if (type.Value.ToString() == "1")
                    {
                        Response.Redirect("AdminPanel.aspx");
                    }
                    else
                    {
                        Response.Redirect("Student.aspx");
                    }

                }
                else
                {
                    Response.Write("Invalid Credentials.Please try again");
                }



            }
            catch (FormatException ex)
            {
                Response.Write("ID should be a number only.Please try again");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

        }
    }
}