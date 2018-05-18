using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using GCFDGlobalsNamespace;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using AjaxControlToolkit;

public partial class SiteRouteOrder : System.Web.UI.Page
{
    private string[] SouthRouteOrderItems
    {
        get
        {
            //We assume the array of items will be small, so we use viewstate
            //  If the array were big you may need to use session, the cache API, 
            //  or even a database or filesystem to store the items between postbacks.

            object items = ViewState["SouthRouteOrderItems"];

            if (items == null) // items are not in viewstate, read from data store
            {
                items = GetSouthRouteOrderItemsFromDb(); //get values from the data store

                ViewState["SouthRouteOrderItems"] = items;//shove into viewstate
            }

            return (string[])items;
        }
        set
        {
            ViewState["SouthRouteOrderItems"] = value;
        }
    }

    private string[] SouthOrderNumberItems
    {
        get
        {
            //We assume the array of items will be small, so we use viewstate
            //  If the array were big you may need to use session, the cache API, 
            //  or even a database or filesystem to store the items between postbacks.

            object items = ViewState["SouthOrderNumberItems"];

            if (items == null) // items are not in viewstate, read from data store
            {
                items = GetSouthOrderNumberItemsFromDb(); //get values from the data store

                ViewState["SouthOrderNumberItems"] = items;//shove into viewstate
            }

            return (string[])items;
        }
        set
        {
            ViewState["SouthRouteOrderItems"] = value;
        }
    }

    private string[] NorthOrderNumberItems
    {
        get
        {
            //We assume the array of items will be small, so we use viewstate
            //  If the array were big you may need to use session, the cache API, 
            //  or even a database or filesystem to store the items between postbacks.

            object items = ViewState["NorthOrderNumberItems"];

            if (items == null) // items are not in viewstate, read from data store
            {
                items = GetNorthOrderNumberItemsFromDb(); //get values from the data store

                ViewState["NorthOrderNumberItems"] = items;//shove into viewstate
            }

            return (string[])items;
        }
        set
        {
            ViewState["NorthRouteOrderItems"] = value;
        }
    }

    private string[] GetSouthOrderNumberItemsFromDb()
    {
        string m_SQL = "SELECT SiteName, SiteRoute, SiteRouteOrder FROM Site WHERE SiteRoute = 'South' ORDER BY SiteRouteOrder";
        DataSet siteInfoDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        string[] ret = new string[siteInfoDataSet.Tables[0].Rows.Count];

        int i = 0;

        foreach (DataRow SiteDataRow in siteInfoDataSet.Tables[0].Rows)
        {
            ret[i] = i + 1 + ". ";

            i++;
        }

        return ret;
    }

    private string[] GetNorthOrderNumberItemsFromDb()
    {
        string m_SQL = "SELECT SiteName, SiteRoute, SiteRouteOrder FROM Site WHERE SiteRoute = 'North' ORDER BY SiteRouteOrder";
        DataSet siteInfoDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        string[] ret = new string[siteInfoDataSet.Tables[0].Rows.Count];

        int i = 0;

        foreach (DataRow SiteDataRow in siteInfoDataSet.Tables[0].Rows)
        {
            ret[i] = i + 1 + ". ";

            i++;
        }

        return ret;
    }

    private string[] GetSouthRouteOrderItemsFromDb()
    {
        string m_SQL = "SELECT SiteName, SiteRoute, SiteRouteOrder FROM Site WHERE SiteRoute = 'South' ORDER BY SiteRouteOrder";
        DataSet siteInfoDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        string[] ret = new string[siteInfoDataSet.Tables[0].Rows.Count];

        int i = 0;

        foreach (DataRow SiteDataRow in siteInfoDataSet.Tables[0].Rows)
        {
            ret[i] = GCFDGlobals.dbGetValue(SiteDataRow, "SiteName");

            i++;
        }

        return ret;
    }

    private string[] NorthRouteOrderItems
    {
        get
        {
            //We assume the array of items will be small, so we use viewstate
            //  If the array were big you may need to use session, the cache API, 
            //  or even a database or filesystem to store the items between postbacks.

            object items = ViewState["NorthRouteOrderItems"];

            if (items == null) // items are not in viewstate, read from data store
            {
                items = GetNorthRouteOrderItemsFromDb(); //get values from the data store

                ViewState["NorthRouteOrderItems"] = items;//shove into viewstate
            }

            return (string[])items;
        }
        set
        {
            ViewState["NorthRouteOrderItems"] = value;
        }
    }

    private string[] GetNorthRouteOrderItemsFromDb()
    {
        string m_SQL = "SELECT SiteName, SiteRoute, SiteRouteOrder FROM Site WHERE SiteRoute = 'North' ORDER BY SiteRouteOrder";
        DataSet siteInfoDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        string[] ret = new string[siteInfoDataSet.Tables[0].Rows.Count];

        int i = 0;

        foreach (DataRow SiteDataRow in siteInfoDataSet.Tables[0].Rows)
        {
            ret[i] = GCFDGlobals.dbGetValue(SiteDataRow, "SiteName");

            i++;
        }

        return ret;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated && User.IsInRole("Transportation"))
        {
            if (Session["SessionID"] == null)
            {
                System.Web.Security.FormsAuthentication.SignOut();

                Response.Redirect("~/Account/Login.aspx", false);

                Response.End();
            }
            else
            {
            }
        }
        else
        {
            Response.Redirect("Default.aspx", false);

            Response.End();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        // doing this at PreRender so we don't have to worry about when/if 
        //   we should bind based on if it's a postback or callback and what not.
        SouthRouteOrderList.DataSource = SouthRouteOrderItems;

        SouthOrderNumberList.DataSource = SouthOrderNumberItems;

        SouthOrderNumberList.DataBind();

        SouthRouteOrderList.DataBind();

        NorthRouteOrderList.DataSource = NorthRouteOrderItems;

        NorthRouteOrderList.DataBind();

        NorthOrderNumberList.DataSource = NorthOrderNumberItems;

        NorthOrderNumberList.DataBind();
    }
    
    protected void SaveOrder_Click(object sender, EventArgs e)
    {
        int i = 1;

        //normally you'd save to reordered list to the DB or whatever
        foreach (string s in SouthRouteOrderItems)
        {
            string m_SQL = "UPDATE Site SET SiteRouteOrder = " + i + " WHERE SiteName = '" + s + "'";
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
            //Message.Text = Message.Text + s + "<br />";// output the re-ordered values to page

            i++;
        }

        i = 1;
        //normally you'd save to reordered list to the DB or whatever
        foreach (string s in NorthRouteOrderItems)
        {
            string m_SQL = "UPDATE Site SET SiteRouteOrder = " + i + " WHERE SiteName = '" + s + "'";
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
            //Message.Text = Message.Text + s + "<br />";// output the re-ordered values to page

            i++;
        }

        MessageBox.Show("Site Route Order Changes Have Been Saved");
    }

    protected void SouthRouteOrderItems_ItemReorder(object sender, ReorderListItemReorderEventArgs e)
    {
        string[] items = SouthRouteOrderItems;

        List<string> list = new List<string>(SouthRouteOrderItems);//using a list for the reordering (convienience)

        string itemToMove = list[e.OldIndex];

        list.Remove(itemToMove);

        list.Insert(e.NewIndex, itemToMove);

        SouthRouteOrderItems = list.ToArray();
        //you could save this to the DB now, but this example uses a save button to batch up changes
    }

    protected void NorthRouteOrderItems_ItemReorder(object sender, ReorderListItemReorderEventArgs e)
    {
        string[] items = NorthRouteOrderItems;
        List<string> list = new List<string>(NorthRouteOrderItems);//using a list for the reordering (convienience)
        string itemToMove = list[e.OldIndex];
        list.Remove(itemToMove);
        list.Insert(e.NewIndex, itemToMove);
        NorthRouteOrderItems = list.ToArray();
        //you could save this to the DB now, but this example uses a save button to batch up changes
    }
}
