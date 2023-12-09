using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineJobPortal.User
{
    public partial class Register : System.Web.UI.Page
    {
        SqlConnection connection;
        SqlCommand command;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCountries();
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
                {
                    lblPass.Text = "Passwords do not match";
                    return;
                }

                if (ddlCountry.SelectedValue == "0")
                {
                    Message("Country cannot be null");
                    ddlCountry.Focus();
                    return;
                }

                //if (Components.IsValidCharaters(txtUsername.Text.ToString()) == false)
                //{
                //    Message("Full name accepts only characters");
                //    return;
                //}

                connection = new SqlConnection(str);
                string query = @"INSERT INTO Users (Username, Password, Name, Address, Mobile, Email, Country)
                       VALUES (@Username, @Password, @Name, @Address, @Mobile, @Email, @Country)";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", txtUsername.Text.ToString());
                command.Parameters.AddWithValue("@Password", txtPassword.Text.ToString());
                command.Parameters.AddWithValue("@Name", txtFullName.Text.ToString());
                command.Parameters.AddWithValue("@Address", txtAddress.Text.ToString());
                command.Parameters.AddWithValue("@Mobile", txtMobile.Text.ToString());
                command.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
                command.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
                connection.Open();

                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    //lblMsg.Visible = true;
                    //lblMsg.Text = "Registration successful!";
                    //lblMsg.CssClass = "alert alert-success";
                    SuccessMessage("Registration successful!");
                    Clear();
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Something went wrong";
                    lblMsg.CssClass = "alert alert-danger";
                }
                connection.Close();
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = $"<b>{txtUsername.Text.Trim()}</b> already exists. Please use a new one!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    Message(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        private void LoadCountries()
        {
            try
            {
                ddlCountry.Items.Clear();
                using (connection = new SqlConnection(str))
                {
                    string sqlStmnt = "spGetCountries";
                    command = new SqlCommand();
                    command.CommandText = sqlStmnt;
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    ListItem li = new ListItem("--Select--", "0");
                    ddlCountry.Items.Add(li);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                li = new ListItem(reader["NAME"].ToString(), reader["NAME"].ToString());
                                ddlCountry.Items.Add(li);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        private void Clear()
        {
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtAddress.Text = string.Empty;
            ddlCountry.ClearSelection();
        }

        public void Message(string message)
        {
            string strScript = "<script>alert('" + message + "')</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }

        public void SuccessMessage(string message)
        {
            string myPage = "Login.aspx";
            string strScript = "<script>alert('" + message + "');window.location='" + myPage + "'</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }
    }
}