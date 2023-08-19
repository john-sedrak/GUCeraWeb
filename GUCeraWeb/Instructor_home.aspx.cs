using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GUCeraWeb
{
    public partial class Instructor_home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["user"] = 1; //TODO: pls remove this

            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GUCera"].ToString());

            SqlCommand query = new SqlCommand("SELECT firstName FROM Users WHERE id = " + Session["user"], conn);

            conn.Open();
            SqlDataReader rdr = query.ExecuteReader();

            string name = "";
            if(rdr.Read() && !rdr.IsDBNull(0))
            {
                name = rdr.GetString(0);
            }

            conn.Close();

            Label1.Text = name;
        }

        protected void defineAssign_Click(object sender, EventArgs e)
        {
            Response.Redirect("Define_assignment.aspx");
        }

        protected void addCourse_Click(object sender, EventArgs e)
        {
            Response.Redirect("Add_course.aspx");
        }

        protected void viewAndGrade_Click(object sender, EventArgs e)
        {
            Response.Redirect("View_assignments.aspx");
        }

        protected void viewFeedback_Click(object sender, EventArgs e)
        {
            Response.Redirect("View_feedback.aspx");
        }

        protected void issueCert_Click(object sender, EventArgs e)
        {
            Response.Redirect("Issue_certificate.aspx");
        }
    }
}