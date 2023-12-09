using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineJobPortal.User
{
    public partial class Contact : System.Web.UI.Page
    {
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        SqlConnection connection;
        SqlCommand command;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(str);
                string query = @"INSERT INTO Contacts VALUES(@Name, @Email, @Subject, @Message)";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name.Value.Trim());
                command.Parameters.AddWithValue("@Email", email.Value.Trim());
                command.Parameters.AddWithValue("@Subject", subject.Value.Trim());
                command.Parameters.AddWithValue("@Message", message.Value.Trim());
                connection.Open();

                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Thank you for reaching out. We will get back to you very shortly!";
                    lblMsg.CssClass = "alert alert-success";
                    Clear();
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Something went wrong. Please try again some other time.";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                Message(ex.Message);
                ex.Data.Clear();

                //lblMsg.Visible = true;
                //lblMsg.Text = "Something went wrong. Please try again some other time.";
                //lblMsg.CssClass = "alert alert-danger";
            }
            finally { connection.Close(); }
        }

        private void Clear()
        {
            name.Value = string.Empty;
            email.Value = string.Empty;
            subject.Value = string.Empty;
            message.Value = string.Empty;
        }

        public void Message(string message)
        {
            string strScript = "<script>alert('" + message + "')</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }
    }
}