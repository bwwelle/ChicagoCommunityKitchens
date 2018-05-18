using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.IsInRole("Kitchen-Staff"))
        {
            NavigationMenu.Items[2].ChildItems[0].Selectable = true;
            NavigationMenu.Items[2].ChildItems[1].Selectable = true;
            NavigationMenu.Items[2].ChildItems[3].Selectable = true;

            if (HttpContext.Current.User.IsInRole("Kitchen-Admin"))
            {
                NavigationMenu.Items[2].ChildItems[4].Selectable = true;
                NavigationMenu.Items[2].ChildItems[5].Selectable = true;
            }
        }

        if (HttpContext.Current.User.IsInRole("Programs-Admin"))
        {
            NavigationMenu.Items[1].ChildItems[5].Selectable = true;
        }
        
        if (HttpContext.Current.User.IsInRole("Programs"))
        {
            NavigationMenu.Items[2].ChildItems[0].Selectable = true;
            NavigationMenu.Items[2].ChildItems[1].Selectable = true;

            NavigationMenu.Items[1].ChildItems[0].Selectable = true;
            NavigationMenu.Items[1].ChildItems[1].Selectable = true;
            NavigationMenu.Items[1].ChildItems[3].Selectable = true;
            NavigationMenu.Items[1].ChildItems[4].Selectable = true;
        }

        if (HttpContext.Current.User.IsInRole("Compliance"))
        {
            NavigationMenu.Items[1].ChildItems[0].Selectable = true;
            NavigationMenu.Items[1].ChildItems[4].Selectable = true;
        }

        if (HttpContext.Current.User.IsInRole("Transportation"))
        {
            NavigationMenu.Items[1].ChildItems[2].Selectable = true;
        }
    }
}
