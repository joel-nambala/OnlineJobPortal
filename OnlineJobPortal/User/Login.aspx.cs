using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineJobPortal.User
{
    public partial class Login : System.Web.UI.Page
    {
        SqlCommand command;
        SqlConnection connecion;
        SqlDataReader reader;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        string username = string.Empty;
        string password = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (ddlLoginType.SelectedIndex == 0)
            {
                lblLogin.Text = "Login type cannot be empty";
                ddlLoginType.Focus();
                return;
            }
            try
            {
                if (ddlLoginType.SelectedValue == "Admin")
                {
                    username = ConfigurationManager.AppSettings["username"];
                    password = ConfigurationManager.AppSettings["password"];

                    if (username == txtUsername.Text.Trim() && password == txtPassword.Text.Trim())
                    {
                        Session["admin"] = username;
                        Response.Redirect("../Admin/Dashboard.aspx");
                    }
                    else
                    {
                        ShowErrorMessage("Admin");
                    }
                }
                else
                {
                    connecion = new SqlConnection(str);
                    string query = @"SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
                    command = new SqlCommand(query, connecion);
                    command.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    command.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                    connecion.Open();

                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Session["username"] = reader["Username"].ToString();
                        Session["userid"] = reader["UserId"].ToString();
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        ShowErrorMessage("User");
                        //ErrorMessage($"User {txtUsername.Text.Trim()} does not exist. Please try again!");
                    }
                    connecion.Close();
                }
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        private void Message(string message)
        {
            string strScript = "<script>alert('" + message + "')</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }

        private void ShowErrorMessage(string userType)
        {
            lblMsg.Visible = true;
            lblMsg.Text = $"<strong>{userType}</strong> credentials are incorrect";
            lblMsg.CssClass = "alert alert-danger";
        }

        private void ErrorMessage(string message)
        {
            string myPage = "Register.aspx";
            string strScript = $"<script>alert('{message}');window.location={myPage}</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }
    }
}