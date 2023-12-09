using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineJobPortal.Layouts
{
    public partial class User : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] != null)
                {
                    lbtnRegisterOrProfile.Text = "Profile";
                    lbtnLoginOrLogout.Text = "Logout";
                }
                else
                {
                    lbtnRegisterOrProfile.Text = "Register";
                    lbtnLoginOrLogout.Text = "Login";
                }
            }
        }

        protected void lbtnRegisterOrProfile_Click(object sender, EventArgs e)
        {
            if (lbtnRegisterOrProfile.Text == "Profile")
            {
                Response.Redirect("Profile.aspx");
            }
            else
            {
                Response.Redirect("Register.aspx");
            }
        }

        protected void lbtnLoginOrLogout_Click(object sender, EventArgs e)
        {
            if (lbtnRegisterOrProfile.Text == "Login")
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Session.Abandon();
                Session.RemoveAll();
                Response.Redirect("Login.aspx");
            }
        }
    }
}