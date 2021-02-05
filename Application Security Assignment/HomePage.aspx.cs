using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Application_Security_Assignment
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] != null && Session["AuthenticationToken"] != null && Request.Cookies["AuthenticationToken"] != null )
            {
                if (!Session["AuthenticationToken"].ToString().Equals(Request.Cookies["AuthenticationToken"].Value))
                {
                    Response.Redirect("Login.aspx", false);
                }

                else
                {
                    lbl_message.Text = "Congratulations!, you are logged in";
                    lbl_message.ForeColor = Color.Green;
                    btn_logout.Visible = true;
                }

            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            Response.Redirect("Login.aspx", false);

            if (Request.Cookies["ASP.NET_SessionId"] != null )
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }

            if (Request.Cookies["AuthenticationToken"] != null )
            {
                Response.Cookies["AuthenticationToken"].Value = string.Empty;
                Response.Cookies["AuthenticationToken"].Expires = DateTime.Now.AddMonths(-20);
            }

        }
    }
}