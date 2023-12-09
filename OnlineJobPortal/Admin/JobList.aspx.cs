using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineJobPortal.Admin
{
    public partial class JobList : System.Web.UI.Page
    {
        SqlConnection connection;
        SqlCommand command;
        DataTable dt;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                    return;
                }
                ShowJob();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ShowJob();
        }

        private void ShowJob()
        {
            try
            {
                connection = new SqlConnection(str);
                string query = @"SELECT Row_Number() over(Order by (Select 1)) as [Sr. No], JobId, Title, NoOfPosts, Qualification,
                        Experience, LastDateToApply, CompanyName, Country, State, CreatedDate from Jobs";
                command = new SqlCommand(query, connection);
                SqlDataAdapter sda = new SqlDataAdapter(command);
                dt = new DataTable();
                sda.Fill(dt);
                gvJobList.DataSource = dt;
                gvJobList.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        protected void gvJobList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvJobList.PageIndex = e.NewPageIndex;
            ShowJob();
        }

        protected void gvJobList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = gvJobList.Rows[e.RowIndex];
                int jobId = Convert.ToInt32(gvJobList.DataKeys[e.RowIndex].Values[0]);
                connection = new SqlConnection(str);
                command = new SqlCommand("Delete from Jobs where JobId = @id", connection);
                command.Parameters.AddWithValue("@id", jobId);
                connection.Open();

                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    lblMsg.Text = "Job has been deleted successifully!";
                    lblMsg.CssClass = "alert alert-success";
                }
                else
                {
                    lblMsg.Text = "Cannot delete this record...!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                connection.Close();
                gvJobList.EditIndex = -1;
                ShowJob();
            }
            catch (Exception ex)
            {
                Message(ex.Message);
                ex.Data.Clear();
            }
        }

        public void Message(string message)
        {
            string strScript = "<script>alert('" + message + "')</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }

        protected void gvJobList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "EditJob")
            {
                Response.Redirect($"NewJob.aspx?id={e.CommandArgument}");
            }
        }
    }
}