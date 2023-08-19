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
    public partial class Issue_certificate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["user"] = 1; // TODO: pls remove this

            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GUCera"].ToString());

            SqlCommand viable = new SqlCommand( "SELECT s.cid, c.name, s.sid " +
                                                "FROM Studenttakecourse s " +
                                                "INNER JOIN Course c ON c.id = s.cid " +
                                                "WHERE s.insid = "+Session["user"]+" AND s.grade > 2.0 " +
                                                "EXCEPT " +
                                                "SELECT s.cid, c.name, s.sid " +
                                                "FROM StudentTakeCourse s " +
                                                "INNER JOIN Course c ON c.id = s.cid " +
                                                "INNER JOIN StudentCertifyCourse sc ON sc.sid = s.sid " +
                                                "WHERE s.insid = " + Session["user"] + " AND s.grade >2.0 AND sc.cid = c.id ", conn);

            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(viable);

            DataTable dt = new DataTable();
            sda.Fill(dt);
            conn.Close();

            StringBuilder str = new StringBuilder("<h3>Qualified Students:</h3>");

            str.Append("<table border='1' name='Qualified Students'>");

            str.Append("<tr>");

            str.Append("<th>");
            str.Append("Course ID");
            str.Append("</th>");

            str.Append("<th>");
            str.Append("Course Name");
            str.Append("</th>");

            str.Append("<th>");
            str.Append("Student ID");
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

            DataTable temp = new DataTable();

            SqlCommand command = new SqlCommand("InstructorViewAcceptedCoursesByAdmin", conn);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@instrId", Session["user"]);
            
            conn.Open();
            sda.SelectCommand = command;
            sda.Fill(temp);
            conn.Close();


            if (temp.Rows.Count == 0)
            {
                str.Clear();
                str.Append("<p>You do not teach any accepted courses, so you cannot issue certificates to students of any courses.</p>");
            }
            PlaceHolder1.Controls.Add(new Literal { Text = str.ToString() });
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cid.Text))
            {
                Label1.Text = "There are no courses with students qualified for a certificate.";
                Label1.Attributes.Add("style", "color:red");
                return;
            }
            if(String.IsNullOrEmpty(sid.Text))
            {
                Label1.Text = "There are no students qualified for a certificate in the selected course";
                Label1.Attributes.Add("style", "color:red");
                return;
            }

            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GUCera"].ToString());

            SqlCommand issue = new SqlCommand("InstructorIssueCertificateToStudent", conn);
            issue.CommandType = CommandType.StoredProcedure;

            issue.Parameters.AddWithValue("@cid", Int16.Parse(cid.Text));
            issue.Parameters.AddWithValue("@sid", Int16.Parse(sid.Text));
            issue.Parameters.AddWithValue("@insId", Session["user"]);
            issue.Parameters.AddWithValue("@issueDate", DateTime.Now.ToString());

            conn.Open();
            issue.ExecuteNonQuery();
            conn.Close();

            PlaceHolder1.Controls.Clear();

            Page_Load(sender, e);
            cid.DataBind();
            sid.DataBind();

            Label1.Text = "Certificate successfully issued for student " + sid.Text + " in course " + cid.Text + " with date " + DateTime.Now;
            Label1.Attributes.Add("style", "color:green");

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Instructor_home.aspx");
        }
    }
}