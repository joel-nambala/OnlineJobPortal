using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineJobPortal.Admin
{
    public partial class ContactList : System.Web.UI.Page
    {
        SqlConnection connection;
        SqlCommand command;
        DataTable dt;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["admin"] == null)
                {
                    Response.Redirect("~/User/Default.aspx");
                    return;
                }
                ShowContact();
            }
        }

        private void ShowContact()
        {
            try
            {
                connection = new SqlConnection(str);
                string query = @"SELECT Row_Number() over(Order by (Select 1)) as [Sr. No], ContactId, Name, Email, Subject, Message from Contacts";
                command = new SqlCommand(query, connection);
                SqlDataAdapter sda = new SqlDataAdapter(command);
                dt = new DataTable();
                sda.Fill(dt);
                gvContactList.DataSource = dt;
                gvContactList.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        protected void gvContactList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvContactList.PageIndex = e.NewPageIndex;
            ShowContact();
        }

        protected void gvContactList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = gvContactList.Rows[e.RowIndex];
                int contactId = Convert.ToInt32(gvContactList.DataKeys[e.RowIndex].Values[0]);
                connection = new SqlConnection(str);
                command = new SqlCommand("Delete from Contacts where ContactId = @id", connection);
                command.Parameters.AddWithValue("@id", contactId);
                connection.Open();

                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    lblMsg.Text = "Contact has been deleted successifully!";
                    lblMsg.CssClass = "alert alert-success";
                }
                else
                {
                    lblMsg.Text = "Cannot delete this record...!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                connection.Close();
                gvContactList.EditIndex = -1;
                ShowContact();
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
    }
}