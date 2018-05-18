using System;
using GCFDGlobalsNamespace;
using System.Data;
using System.Web;

public partial class Default : System.Web.UI.Page
{
    public static GCFDGlobals m_GlobalVariables;
    public string m_SQL = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.Identity.IsAuthenticated)
        {            
            Response.Redirect("Account/Login.aspx", false);
        }
        else
        {
            Session["SessionID"] = Session.SessionID;

            //RegisterClientScriptBlock("","<script>top.window.moveTo(0,0); top.window.resizeTo(screen.availWidth,screen.availHeight);</script>");

            string m_strConnectString = GCFDGlobals.GetConnectString();

            if (m_strConnectString == null)
                return;

            m_GlobalVariables = new GCFDGlobals(m_strConnectString);

            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.ClearSessionVariables();

            if (User.IsInRole("Kitchen-Staff"))
            {
                MealPlannerLinkButton.Enabled = true;
                MealPlannerLinkButton.ForeColor = System.Drawing.Color.White;

                BreakfastPlannerLinkButton.Enabled = true;
                BreakfastPlannerLinkButton.ForeColor = System.Drawing.Color.White;

                RecipesLinkButton.Enabled = true;
                RecipesLinkButton.ForeColor = System.Drawing.Color.White;

                if(User.IsInRole("Kitchen-Admin"))
                {
                    FoodItemInventoryLinkButton.Enabled = true;
                    FoodItemInventoryLinkButton.ForeColor = System.Drawing.Color.White;

                    NonFoodItemInventoryLinkButton.Enabled = true;
                    NonFoodItemInventoryLinkButton.ForeColor = System.Drawing.Color.White;
                }
            }

            if (User.IsInRole("Programs-Admin"))
            {
                MealDeliveryTypesLinkButton.Enabled = true;
                MealDeliveryTypesLinkButton.ForeColor = System.Drawing.Color.White;
            }

            if(User.IsInRole("Programs"))
            {
                MealPlannerLinkButton.Enabled = true;
                MealPlannerLinkButton.ForeColor = System.Drawing.Color.White;

                BreakfastPlannerLinkButton.Enabled = true;
                BreakfastPlannerLinkButton.ForeColor = System.Drawing.Color.White;

                DeliveryPlannerLinkButton.Enabled = true;
                DeliveryPlannerLinkButton.ForeColor = System.Drawing.Color.White;

                DeliverySitesLinkButton.Enabled = true;
                DeliverySitesLinkButton.ForeColor = System.Drawing.Color.White;

                SiteRouteLinkButton.Enabled = true;
                SiteRouteLinkButton.ForeColor = System.Drawing.Color.White;

                MilkOrderingCalculatorLinkButton.Enabled = true;
                MilkOrderingCalculatorLinkButton.ForeColor = System.Drawing.Color.White;

                SiteDeliveryCountsLinkButton.Enabled = true;
                SiteDeliveryCountsLinkButton.ForeColor = System.Drawing.Color.White;
            }
            
            if (User.IsInRole("Transportation"))
            {
                SiteRouteLinkButton.Enabled = true;
                SiteRouteLinkButton.ForeColor = System.Drawing.Color.White;
            }

            if (User.IsInRole("Compliance"))
            {
                DeliverySitesLinkButton.Enabled = true;
                DeliverySitesLinkButton.ForeColor = System.Drawing.Color.White;

                SiteDeliveryCountsLinkButton.Enabled = true;
                SiteDeliveryCountsLinkButton.ForeColor = System.Drawing.Color.White;
            }
        }
    }
    
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("kitchen/ManageIngredients_Specific.aspx");
    }

	protected void ReportsLinkButton_Click(object sender, EventArgs e)
	{
		Response.Redirect("CCKReports.aspx");
	}

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Response.Redirect("kitchen/ManageMeasurements.aspx");
    }

    protected void MenuPlannerLinkButton_Click(object sender, EventArgs e)
    {
        Session["MealMode"] = "PageOpened";

        Response.Redirect("kitchen/mealcalendar.aspx");

        //Server.Transfer("kitchen/Wait.aspx?Page=kitchen/mealcalendar.aspx", true);
    }

    protected void BreakfastPlannerLinkButton_Click(object sender, EventArgs e)
    {
        Session["MealMode"] = "PageOpened";

        Response.Redirect("kitchen/breakfastcalendar.aspx");

        //Server.Transfer("kitchen/Wait.aspx?Page=kitchen/breakfastcalendar.aspx", true);
    }

    protected void SiteDeliveryCounts_Click(object sender, EventArgs e)
    {
        Response.Redirect("Programs/SiteDeliveryCount.aspx");
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("kitchen/Recipes.aspx");
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Response.Redirect("kitchen/FoodItemInventory.aspx");
    }

    protected void LinkButton69_Click(object sender, EventArgs e)
    {
        Response.Redirect("kitchen/NonFoodItemInventory.aspx");
    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Response.Redirect("Programs/Sites.aspx");
    }

    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Response.Redirect("Programs/DeliveryCalendar.aspx");
    }

	protected void LinkButton2_Click1(object sender, EventArgs e)
	{
        Response.Redirect("Programs/SiteRouteOrder.aspx");
	}

    protected void MilkOrderingCalculator_Click(object sender, EventArgs e)
    {
        Response.Redirect("Programs/MilkOrderingCalculator.aspx");
    }

    protected void MealDeliveryType_Click(object sender, EventArgs e)
    {
        Response.Redirect("Programs/MealDeliveryType.aspx");
    }

    protected void ViewChangeLogLinkButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("CCKDevelopmentChanges.aspx");
    }
    protected void UserActionLogButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("CCKUserActionLog.aspx");
    }
}
