using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace GUCeraWeb
{
    public partial class View_assignments : System.Web.UI.Page
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

            if (dt.Rows.Count==0)
            {
                str.Clear();
                str.Append("<p>You do not teach any accepted courses, so you cannot see the assignments of any courses.</p>");
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
                PlaceHolder3.Controls.Add(new Literal { Text = "<p style='color:red'>You do not teach any accepted courses, so you cannot see the assignments of any course.</p>" });
                return false;
            }

            return true;
        }

        protected DataTable GetAssignments()
        {
            
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand command = new SqlCommand("InstructorViewAssignmentsStudents", conn);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@instrId", Session["user"]);
            command.Parameters.AddWithValue("@cid", Int16.Parse(cid.Text));

            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            sda.Fill(dt);

            conn.Close();

            return dt;

        }

        protected void ViewAssignments(object sender, EventArgs e)
        {

            if (!ValidateCourse())
            {
                return;
            }
            DataTable dt = GetAssignments();

            StringBuilder str = new StringBuilder("<h3>Assignments:</h3>");

            str.Append("<table border='1' name='Assignments'>");

            str.Append("<tr>");


            str.Append("<th>");
            str.Append("Student ID");
            str.Append("</th>");

            str.Append("<th>");
            str.Append("Course ID");
            str.Append("</th>");

            str.Append("<th>");
            str.Append("Assignment Number");
            str.Append("</th>");

            str.Append("<th>");
            str.Append("Assignment Type");
            str.Append("</th>");

            str.Append("<th>");
            str.Append("Grade");
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

            if (dt.Rows.Count>0)
            {
                Button2.Attributes.Remove("Disabled");
                sid.Attributes.Remove("Disabled");
                type.Attributes.Remove("Disabled");
                number.Attributes.Remove("Disabled");
                grade.Attributes.Remove("Disabled");

            }


        }

        protected void GradeAssignment(object sender, EventArgs e)
        {

            if(String.IsNullOrEmpty(grade.Text))
            {
                Label1.Text = " Grade cannot be left empty.";
                Label1.Attributes.Add("style", "color:red");
            }

            ViewAssignments(sender, e);
            if (String.IsNullOrEmpty(grade.Text))
            {
                Label1.Text = "Please enter a grade.";
                Label1.Attributes.Add("style", "color:red");
                return;
            }
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand gradeProc = new SqlCommand("InstructorgradeAssignmentOfAStudent", conn);
            gradeProc.CommandType = CommandType.StoredProcedure;

            gradeProc.Parameters.AddWithValue("@instrId", Session["user"]);
            gradeProc.Parameters.AddWithValue("@sid", Int16.Parse(sid.Text));
            gradeProc.Parameters.AddWithValue("@cid", Int16.Parse(cid.Text));
            gradeProc.Parameters.AddWithValue("@assignmentNumber", Int16.Parse(number.Text));
            gradeProc.Parameters.AddWithValue("@type", type.Text);
            gradeProc.Parameters.AddWithValue("@grade", Decimal.Parse(grade.Text));

            Label1.Text = "Assignment graded successfully";
            Label1.Attributes.Remove("Style");
            Label1.Attributes.Add("style", "color:green");

            grade.Text = "";

            conn.Open();
            gradeProc.ExecuteNonQuery();
            conn.Close();

            PlaceHolder3.Controls.Clear();
            ViewAssignments(sender, e);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Instructor_home.aspx");
        }
    }

}