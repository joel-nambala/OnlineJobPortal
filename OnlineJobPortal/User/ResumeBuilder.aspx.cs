using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineJobPortal.User
{
    public partial class ResumeBuilder : System.Web.UI.Page
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        string query;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
                if (Request.QueryString["id"] != null)
                {
                    LoadCountries();
                    ShowUserInfo();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
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

        private void ShowUserInfo()
        {
            try
            {
                connection = new SqlConnection(str);
                string query = @"SELECT * FROM Users WHERE UserId=@userId";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", Request.QueryString["id"].ToString());
                connection.Open();

                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        txtUsername.Text = reader["Username"].ToString();
                        txtFullName.Text = reader["Name"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtMobile.Text = reader["Mobile"].ToString();
                        txtTenth.Text = reader["TenthGrade"].ToString();
                        txtTwefth.Text = reader["TwelvthGrade"].ToString();
                        txtGraduation.Text = reader["GraduationGrade"].ToString();
                        txtPostGraduation.Text = reader["PostGraduationGrade"].ToString();
                        txtPhd.Text = reader["Phd"].ToString();
                        txtWork.Text = reader["WorkOn"].ToString();
                        txtExperience.Text = reader["Experience"].ToString();
                        txtAddress.Text = reader["Address"].ToString();
                        ddlCountry.SelectedValue = reader["Country"].ToString();
                    }
                    reader.Close();
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "User not found";
                    lblMsg.CssClass = "alert alert-danger";
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["id"] != null)
                {
                    string concatQuery = "", filePath = string.Empty;
                    //bool isValidToExecute = false;
                    bool IsValid = false;
                    connection = new SqlConnection(str);
                    if (fuResume.HasFile)
                    {
                        if (IsValidExtension(fuResume.FileName))
                        {
                            concatQuery = "Resume=@Resume";
                            IsValid = true;
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Please select.doc, .docx and .pdf file extensions only!";
                            lblMsg.CssClass = "alert alert-danger";
                            //isValidToExecute = false;
                        }
                    }
                    else
                    {
                        concatQuery = string.Empty;
                    }

                    query = @"UPDATE Users SET Username=@Username, Name=@Name, Email=@Email, Mobile=@Mobile, TenthGrade=@TenthGrade, TwelvthGrade=@TwelvthGrade, GraduationGrade=@GraduatioonGrade, 
                                PostGraduationGrade=@PostGraduationGrade, Phd=@Phd, WorkOn=@WorkOn, Experience=@Experience, " + concatQuery + ", Address=@Address, Country=@Country WHERE UserId=@UserId";
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    command.Parameters.AddWithValue("@Name", txtFullName.Text.Trim());
                    command.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    command.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
                    command.Parameters.AddWithValue("@TenthGrade", txtTenth.Text.Trim());
                    command.Parameters.AddWithValue("@TwelvthGrade", txtTwefth.Text.Trim());
                    command.Parameters.AddWithValue("@GraduatioonGrade", txtGraduation.Text.Trim());
                    command.Parameters.AddWithValue("@PostGraduationGrade", txtPostGraduation.Text.Trim());
                    command.Parameters.AddWithValue("@Phd", txtPhd.Text.Trim());
                    command.Parameters.AddWithValue("@WorkOn", txtWork.Text.Trim());
                    command.Parameters.AddWithValue("@Experience", txtExperience.Text.Trim());
                    command.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    command.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
                    command.Parameters.AddWithValue("@UserId", Request.QueryString["id"]);
                    if (IsValid)
                    {
                        Guid obj = Guid.NewGuid();
                        filePath = "Resumes/" + obj.ToString() + fuResume.FileName;
                        fuResume.PostedFile.SaveAs(Server.MapPath("~/Resumes/") + obj.ToString() + fuResume.FileName);
                        command.Parameters.AddWithValue("@Resume", filePath);
                    }
                    else
                    {
                        IsValid = true;
                    }

                    if (IsValid)
                    {
                        connection.Open();
                        int r = command.ExecuteNonQuery();
                        if (r > 0)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Resume details updated succesiful..!";
                            lblMsg.CssClass = "alert alert-success";
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Something went wrong!";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Something went wrong!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        private bool IsValidExtension(string fileName)
        {
            bool isValid = false;
            string[] fileExtension = { ".doc", ".docx", ".pdf" };
            for (int i = 0; i < fileExtension.Length; i++)
            {
                if (fileName.Contains(fileExtension[i]))
                {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }
    }
}