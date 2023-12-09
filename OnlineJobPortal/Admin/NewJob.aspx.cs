using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Microsoft.SqlServer.Server;

namespace OnlineJobPortal.Admin
{
    public partial class NewJob : System.Web.UI.Page
    {
        SqlConnection connection;
        SqlCommand command;
        string query;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                    return;
                }
                Session["title"] = "Add Job";
                LoadCountries();
                FillData();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string imagePath = string.Empty;
                string concatQuery = string.Empty;
                string type = string.Empty;
                bool IsValidToExecute = false;

                if (Request.QueryString["id"] != null)
                {
                    string id = Request.QueryString["id"].ToString();
                    if (fuCompanyLogo.HasFile)
                    {
                        if (IsValidExtension(fuCompanyLogo.FileName))
                        {
                            concatQuery = "CompanyImage = @CompanyImage,";
                        }
                        else
                        {
                            concatQuery = string.Empty;
                        }
                    }
                    else
                    {
                        concatQuery = string.Empty;
                    }
                    connection = new SqlConnection(str);
                    query = @"UPDATE Jobs SET Title=@Title, NoOfPosts=@NoOfPosts, Description=@Description, Qualification=@Qualification, Experience=@Experience, Specialization=@Specialization, LastDateToApply=@LastDateToApply
                        ,Salary=@Salary, JobType=@JobType, CompanyName=@CompanyName, " + concatQuery + @"Website=@Website, Email=@Email, Address=@Address, Country=@Country, State=@State WHERE JobId=@id";
                    type = "updated";
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Title", txtJobTitle.Text.Trim());
                    command.Parameters.AddWithValue("@NoOfPosts", txtNoOfPosts.Text.Trim());
                    command.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                    command.Parameters.AddWithValue("@Qualification", txtQualification.Text.Trim());
                    command.Parameters.AddWithValue("@Experience", txtExperience.Text.Trim());
                    command.Parameters.AddWithValue("@Specialization", txtSpecialization.Text.Trim());
                    command.Parameters.AddWithValue("@LastDateToApply", txtLastDate.Text.Trim());
                    command.Parameters.AddWithValue("@Salary", txtSalary.Text.Trim());
                    command.Parameters.AddWithValue("@JobType", ddlJobType.SelectedValue);
                    command.Parameters.AddWithValue("@CompanyName", txtCompany.Text.Trim());
                    command.Parameters.AddWithValue("@Website", txtWebsite.Text.Trim());
                    command.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    command.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    command.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
                    command.Parameters.AddWithValue("@State", txtState.Text.Trim());
                    command.Parameters.AddWithValue("@id", id);

                    if (fuCompanyLogo.HasFile)
                    {
                        if (IsValidExtension(fuCompanyLogo.FileName))
                        {
                            Guid obj = Guid.NewGuid();
                            imagePath = "Images/" + obj.ToString() + fuCompanyLogo.FileName;
                            fuCompanyLogo.PostedFile.SaveAs(Server.MapPath("~/Images/") + obj.ToString() + fuCompanyLogo.FileName);
                            command.Parameters.AddWithValue("@CompanyImage", imagePath);
                            IsValidToExecute = true;
                        }
                        else
                        {
                            lblMsg.Text = "Please select an image with .jpg, .png and .jpeg extensions for the compant logo!";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                    else
                    {
                        IsValidToExecute = true;
                    }
                }
                else
                {
                    connection = new SqlConnection(str);
                    query = @"INSERT INTO Jobs VALUES(@Title, @NoOfPosts, @Description, @Qualification, @Experience, @Specialization, @LastDateToApply
                        ,@Salary, @JobType, @CompanyName, @CompanyImage, @Website, @Email, @Address, @Country, @State, @CreatedDate)";
                    type = "saved";
                    DateTime time = DateTime.Now;
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Title", txtJobTitle.Text.Trim());
                    command.Parameters.AddWithValue("@NoOfPosts", txtNoOfPosts.Text.Trim());
                    command.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                    command.Parameters.AddWithValue("@Qualification", txtQualification.Text.Trim());
                    command.Parameters.AddWithValue("@Experience", txtExperience.Text.Trim());
                    command.Parameters.AddWithValue("@Specialization", txtSpecialization.Text.Trim());
                    command.Parameters.AddWithValue("@LastDateToApply", txtLastDate.Text.Trim());
                    command.Parameters.AddWithValue("@Salary", txtSalary.Text.Trim());
                    command.Parameters.AddWithValue("@JobType", ddlJobType.SelectedValue);
                    command.Parameters.AddWithValue("@CompanyName", txtCompany.Text.Trim());
                    command.Parameters.AddWithValue("@Website", txtWebsite.Text.Trim());
                    command.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    command.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    command.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
                    command.Parameters.AddWithValue("@State", txtState.Text.Trim());
                    command.Parameters.AddWithValue("@CreatedDate", time.ToString("yyyy-MM-dd HH:mm:ss"));

                    if (fuCompanyLogo.HasFile)
                    {
                        if (IsValidExtension(fuCompanyLogo.FileName))
                        {
                            Guid obj = Guid.NewGuid();
                            imagePath = "Images/" + obj.ToString() + fuCompanyLogo.FileName;
                            fuCompanyLogo.PostedFile.SaveAs(Server.MapPath("~/Images/") + obj.ToString() + fuCompanyLogo.FileName);
                            command.Parameters.AddWithValue("@CompanyImage", imagePath);
                            IsValidToExecute = true;
                        }
                        else
                        {
                            lblMsg.Text = "Please select an image with .jpg, .png and .jpeg extensions for the compant logo!";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@CompanyImage", imagePath);
                        IsValidToExecute = true;
                    }
                }
                if (IsValidToExecute)
                {
                    connection.Open();
                    int res = command.ExecuteNonQuery();
                    if (res > 0)
                    {
                        lblMsg.Text = $"Job {type} successifully...!";
                        lblMsg.CssClass = "alert alert-success";
                        Clear();
                        if(type == "updated")
                        {
                            Response.Redirect("~/Admin/JobList.aspx");
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Something went wrong!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Message(ex.Message);
                ex.Data.Clear();
            }
        }

        private void FillData()
        {
            if (Request.QueryString["id"] != null)
            {
                string id = Request.QueryString["id"].ToString();
                connection = new SqlConnection(str);
                query = @"SELECT * FROM Jobs WHERE JobId = '" + id + "'";
                command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtJobTitle.Text = reader["Title"].ToString();
                        txtNoOfPosts.Text = reader["NoOfPosts"].ToString();
                        txtDescription.Text = reader["Description"].ToString();
                        txtQualification.Text = reader["Qualification"].ToString();
                        txtExperience.Text = reader["Experience"].ToString();
                        txtSpecialization.Text = reader["Specialization"].ToString();
                        txtLastDate.Text = Convert.ToDateTime(reader["LastDateToApply"]).ToString("yyyy-MM-dd");
                        txtSalary.Text = reader["Salary"].ToString();
                        ddlJobType.SelectedValue = reader["JobType"].ToString();
                        txtCompany.Text = reader["CompanyName"].ToString();
                        txtWebsite.Text = reader["Website"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        ddlCountry.SelectedValue = reader["Country"].ToString();
                        txtAddress.Text = reader["Address"].ToString();
                        txtState.Text = reader["State"].ToString();
                        btnAdd.Text = "Update";
                        hlBack.Visible = true;
                        Session["title"] = "Edit Job";
                    }
                }
                else
                {
                    lblMsg.Text = "Job not found..!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                reader.Close();
                connection.Close();
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

        private bool IsValidExtension(string fileName)
        {
            bool isValid = false;
            try
            {
                string[] fileExtension = { ".png", ".jpeg", ".jpg" };

                for (int i = 0; i < fileExtension.Length; i++)
                {
                    if (fileName.Contains(fileExtension[i]))
                    {
                        isValid = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return isValid;
        }

        private void Clear()
        {
            txtJobTitle.Text = string.Empty;
            txtNoOfPosts.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtQualification.Text = string.Empty;
            txtExperience.Text = string.Empty;
            txtSpecialization.Text = string.Empty;
            txtLastDate.Text = string.Empty;
            txtSalary.Text = string.Empty;
            ddlJobType.ClearSelection();
            txtCompany.Text = string.Empty;
            txtWebsite.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            ddlCountry.ClearSelection();
            txtState.Text = string.Empty;
        }

        private void Message(string message)
        {
            string strScript = "<script>alert('" + message + "')</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }
    }
}