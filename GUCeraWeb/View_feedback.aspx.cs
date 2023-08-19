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
    public partial class View_feedback : System.Web.UI.Page
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

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str.Append("<tr>");
                DataRow row = dt.Rows[i];
                for (int j = 0; j < dt.Columns.Count; j++)
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
                str.Append("<p>You do not teach any accepted courses, so you cannot view feedback of any course.</p>");
            }

            Placeholder1.Controls.Add(new Literal { Text = str.ToString() });
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

        protected bool ValidateCourse()
        {

            if (String.IsNullOrWhiteSpace(cid.Text))
            {
                PlaceHolder3.Controls.Add(new Literal { Text = "<p style='color:red'>You do not teach any accepted courses, so you cannot see the assignments of any courses.</p>" });
                return false;
            }
            return true;

        }

        protected void ViewFeedback(object sender, EventArgs e)
        {
            if(!ValidateCourse())
            {
                return;
            }
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand feedbackProc = new SqlCommand("ViewFeedbacksAddedByStudentsOnMyCourse", conn);
            feedbackProc.CommandType = CommandType.StoredProcedure;

            feedbackProc.Parameters.AddWithValue("@instrId", Session["user"]);
            feedbackProc.Parameters.AddWithValue("@cid", Int16.Parse(cid.Text));

            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(feedbackProc);

            DataTable dt = new DataTable();
            sda.Fill(dt);

            conn.Close();

            StringBuilder str = new StringBuilder("<h3>Feedback</h3>");

            str.Append("<table border='1' name='Available Courses'>");

            str.Append("<tr>");

            str.Append("<th>");
            str.Append("Feedback Number");
            str.Append("</th>");

            str.Append("<th>");
            str.Append("Comment");
            str.Append("</th>");

            str.Append("<th>");
            str.Append("Number of Likes");
            str.Append("</th>");

            str.Append("</tr>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str.Append("<tr>");
                DataRow row = dt.Rows[i];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    str.Append("<td>");
                    str.Append(row[j]);
                    str.Append("</td>");
                }

                str.Append("</tr>");

            }
            str.Append("</table>");

            PlaceHolder3.Controls.Add(new Literal { Text = str.ToString() });
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Instructor_home.aspx");
        }
    }
}