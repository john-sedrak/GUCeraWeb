using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GUCera
{
    public partial class studentregister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void sregister(object sender, EventArgs e)
        {

            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String firstn = firstname.Text;
            String lastn = lastname.Text;
            String pass = password.Text;
            String emaill = email.Text;
            bool g = gender.SelectedValue.Equals("1");

            if (CheckEmail(emaill))
            {
                Label5.Text = "email taken";
                Label5.Attributes.Add("style", "color:red");
            }

            else
            {
                String add = address.Text;
                SqlCommand studentproc = new SqlCommand("studentRegister", conn);
                studentproc.CommandType = CommandType.StoredProcedure;

                studentproc.Parameters.Add(new SqlParameter("@first_name", firstn));
                studentproc.Parameters.Add(new SqlParameter("@last_name", lastn));
                studentproc.Parameters.Add(new SqlParameter("@password", pass));
                studentproc.Parameters.Add(new SqlParameter("@email", emaill));
                studentproc.Parameters.Add(new SqlParameter("@gender", g));
                studentproc.Parameters.Add(new SqlParameter("@address", add));


                conn.Open();
                studentproc.ExecuteNonQuery();
                conn.Close();
                Label5.Text = "You're Registered! Your user id is " + return_id();
                Label5.Attributes.Add("style", "color:green");

                firstname.Text = "";
                lastname.Text = "";
                password.Text = "";
                email.Text = "";
                gender.ClearSelection();
                address.Text = "";
            }
        }

        protected void iregister(object sender, EventArgs e)
        {

            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String firstn = firstname.Text;
            String lastn = lastname.Text;
            String pass = password.Text;
            String emaill = email.Text;
            bool g = gender.SelectedValue.Equals("1");

            if (CheckEmail(emaill))
            {
                Label5.Text="email taken";
                Label5.Attributes.Add("style", "color:red");
            }

            else
            {
                String add = address.Text;
                SqlCommand studentproc = new SqlCommand("instructorRegister", conn);
                studentproc.CommandType = CommandType.StoredProcedure;

                studentproc.Parameters.Add(new SqlParameter("@first_name", firstn));
                studentproc.Parameters.Add(new SqlParameter("@last_name", lastn));
                studentproc.Parameters.Add(new SqlParameter("@password", pass));
                studentproc.Parameters.Add(new SqlParameter("@email", emaill));
                studentproc.Parameters.Add(new SqlParameter("@gender", g));
                studentproc.Parameters.Add(new SqlParameter("@address", add));


                conn.Open();
                studentproc.ExecuteNonQuery();
                conn.Close();
                Label5.Text = "You're Registered! Your user id is " + return_id();
                Label5.Attributes.Add("style", "color:green");

                firstname.Text = "";
                lastname.Text = "";
                password.Text = "";
                email.Text = "";
                gender.ClearSelection();
                address.Text = "";
            }
        }


        private bool CheckEmail(String email)
        {

            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            string cmdText = "SELECT COUNT(*) FROM Users WHERE email = @email";

            using (conn)
            {
                conn.Open(); // Open DB connection.

                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email); // Add the SQL parameter.

                    int count = (int)cmd.ExecuteScalar();

                    // True (> 0) when the username exists, false (= 0) when the username does not exist.
                    return (count > 0);
                }
            }
        }

        private int return_id()
        {
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            string cmdText = "SELECT id FROM Users WHERE email = @email";

            using (conn)
            {
                conn.Open(); // Open DB connection.

                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email.Text); // Add the SQL parameter.

                    int id = (int)cmd.ExecuteScalar();

                    // True (> 0) when the username exists, false (= 0) when the username does not exist.
                    return id;
                }
            }
        }

            protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}