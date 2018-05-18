using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            //RegisterClientScriptBlock("", "<script>top.window.moveTo(0,0); top.window.resizeTo(screen.availWidth,screen.availHeight);</script>");

            Menu myMasterMenu = (Menu)Master.FindControl("NavigationMenu");

            myMasterMenu.Items[1].Enabled = false;
            myMasterMenu.Items[2].Enabled = false;
            myMasterMenu.Items[3].Enabled = false;
        }
        
        HyperLink1.NavigateUrl = "PasswordRecovery.aspx";
    }
}
