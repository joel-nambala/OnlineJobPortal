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
    public partial class Profile : System.Web.UI.Page
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter sda;
        DataTable dt;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
                ShowUserProfile();
            }
        }

        private void ShowUserProfile()
        {
            try
            {
                connection = new SqlConnection(str);
                string query = @"SELECT UserId, Username, Name, Email, Mobile, Address, Resume, Country from Users WHERE Username=@username";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", Session["username"].ToString());
                sda = new SqlDataAdapter(command);
                dt = new DataTable();
                sda.Fill(dt);
                dlProfile.DataSource = dt;
                dlProfile.DataBind();
            }
            catch(Exception ex)
            {
                ex.Data.Clear();
            }
        }

        protected void dlProfile_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if(e.CommandName == "EditUserProfile")
            {
                Response.Redirect($"ResumeBuilder.aspx?id={e.CommandArgument}");
            }
        }
    }
}