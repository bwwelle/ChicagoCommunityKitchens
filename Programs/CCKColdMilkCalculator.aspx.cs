using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using GCFDGlobalsNamespace;
using System.Data.SqlClient;
using System.Globalization;
using AjaxControlToolkit;
using System.Diagnostics;

public partial class MilkOrderingCalculator : System.Web.UI.Page
{
    protected Label label = null;
    protected string labelName = "";
    protected int labelCounter = 1;
    protected int milkCount = 0;
    protected int daysTotalMilkCrates = 0;
    protected Decimal totalCrates = 0;
    protected int totalWeeksMilkCrates = 0;
    DateTime startDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated && User.IsInRole("Programs"))
        {
            if (Session["SessionID"] == null)
            {
                System.Web.Security.FormsAuthentication.SignOut();

                Response.Redirect("~/Account/Login.aspx", false);

                Response.End();
            }
            else
            {
                ChocolateInventoryTextBox.Attributes["onchange"] = "javascript:updateChocolateMilkQuantities()";

                ChocolateExpirationDateTextBox.Attributes["onchange"] = "javascript:updateChocolateMilkQuantities()";

                DateTextBox.Attributes["onchange"] = "javascript:updateChocolateMilkQuantities()";

                WhiteInventoryTextBox.Attributes["onchange"] = "javascript:updateWhiteMilkQuantities()";

                WhiteExpirationDateTextBox.Attributes["onchange"] = "javascript:updateWhiteMilkQuantities()";

                DateTextBox.Attributes["onchange"] = "javascript:updateChocolateMilkQuantities();updateWhiteMilkQuantities()";
            }
        }
        else
        {
            Response.Redirect("Default.aspx", false);

            Response.End();
        }
    }

    protected void DateTextBox_TextChanged(object sender, EventArgs e)
    {
        startDate = Convert.ToDateTime(DateTextBox.Text);

        WhiteInventoryTextBox.Text = "";
        ChocolateInventoryTextBox.Text = "";
        WhiteNeededForWeekdaysBeforeLabel.Text = "N/A";
        ChocolateNeededForWeekdaysBeforeLabel.Text = "N/A";
        WhiteExpirationDateTextBox.Text = "";
        ChocolateExpirationDateTextBox.Text = "";
        QuantityOfWhiteAvailableLabel.Text = "N/A";
        QuantityOfChocolateAvailableLabel.Text = "N/A";
        WhiteAmountNeededLabel.Text = "N/A";
        ChocolateAmountNeededLabel.Text = "N/A";

        this.label = this.Master.FindControl("MainContent").FindControl("WeekDay1Label") as Label;

        this.label.Text = startDate.DayOfWeek.ToString();

        this.label = this.Master.FindControl("MainContent").FindControl("Date1Label") as Label;

        this.label.Text = startDate.ToString("MM/dd/yyyy");

        this.label = this.Master.FindControl("MainContent").FindControl("ChocolateMilkCrateCountDate1Label") as Label;

        this.label.Text = "N/A";

        this.label = this.Master.FindControl("MainContent").FindControl("WhiteMilkCrateCountDate1Label") as Label;

        this.label.Text = "N/A";

        DateTime newDate = startDate;

        for (int i = 1; i < 5; i++)
        {
            int numberToAdd = 1;
            
            if (newDate.DayOfWeek.ToString() == "Friday")
            {
                numberToAdd = 3;
            }
            else
            {
                numberToAdd = 1;
            }

            newDate = newDate.AddDays(numberToAdd);

            labelCounter = i + 1;

            labelName = "Date" + labelCounter + "Label";

            this.label = this.Master.FindControl("MainContent").FindControl(labelName) as Label;

            this.label.Text = newDate.ToString("MM/dd/yyyy");

            labelName = "WeekDay" + labelCounter + "Label";

            this.label = this.Master.FindControl("MainContent").FindControl(labelName) as Label;

            this.label.Text = newDate.DayOfWeek.ToString();

            this.label = this.Master.FindControl("MainContent").FindControl("ChocolateMilkCrateCountDate" + labelCounter + "Label") as Label;

            this.label.Text = "N/A";

            this.label = this.Master.FindControl("MainContent").FindControl("WhiteMilkCrateCountDate" + labelCounter + "Label") as Label;

            this.label.Text = "N/A";
        }

        UpdateChocolateMilkCounts();

        UpdateWhiteMilkCounts();
    }

    protected void UpdateChocolateMilkCounts()
    {
        labelCounter = 1;
        milkCount = 0;
        daysTotalMilkCrates = 0;
        totalCrates = 0;
        totalWeeksMilkCrates = 0;

        string m_SQL = "SELECT DISTINCT SiteName, SiteRoute, DeliveryDate, dbo.udf_DeliveryReportByRangeMealCount(DeliveryDate, SiteID,38,1) + dbo.udf_DeliveryReportByRangeMealCount(DeliveryDate, SiteID,38,2) AS MilkCrateCount FROM vwDelivery WHERE DeliveryDate >= '" + DateTextBox.Text + "' AND DeliveryDate <= '" + Date5Label.Text + "' AND MealTypeName = 'Cold-CCK' AND DeliveryTypeName <> 'Cancelled' GROUP BY DeliveryDate, SiteRoute, SiteName, SiteID ORDER BY DeliveryDate, siteroute, SiteName";
        DataSet m_MilkCrateDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        if (m_MilkCrateDataSet.Tables[0].Rows.Count > 0)
        {

            string previousDeliveryDate = Convert.ToDateTime(GCFDGlobals.dbGetValue(m_MilkCrateDataSet.Tables[0].Rows[0], "DeliveryDate")).ToString("MM/dd/yyyy");
            string previousSiteRoute = GCFDGlobals.dbGetValue(m_MilkCrateDataSet.Tables[0].Rows[0], "SiteRoute");

            foreach (DataRow milkCrateDataRow in m_MilkCrateDataSet.Tables[0].Rows)
            {
                if (previousDeliveryDate != Convert.ToDateTime(GCFDGlobals.dbGetValue(milkCrateDataRow, "DeliveryDate")).ToString("MM/dd/yyyy"))
                {
                    while (labelCounter < 8)
                    {
                        labelName = "Date" + labelCounter + "Label";

                        this.label = this.Master.FindControl("MainContent").FindControl(labelName) as Label;

                        if (this.label.Text != previousDeliveryDate)
                        {
                            labelCounter = labelCounter + 1;
                        }
                        else
                        {
                            break;
                        }
                    }

                    previousDeliveryDate = Convert.ToDateTime(GCFDGlobals.dbGetValue(milkCrateDataRow, "DeliveryDate")).ToString("MM/dd/yyyy");

                    previousSiteRoute = GCFDGlobals.dbGetValue(milkCrateDataRow, "SiteRoute");

                    totalCrates = Math.Ceiling(Convert.ToDecimal(milkCount) / 50);

                    daysTotalMilkCrates = daysTotalMilkCrates + Convert.ToInt32(totalCrates);

                    totalWeeksMilkCrates = totalWeeksMilkCrates + daysTotalMilkCrates;

                    labelName = "ChocolateMilkCrateCountDate" + labelCounter + "Label";

                    this.label = this.Master.FindControl("MainContent").FindControl(labelName) as Label;

                    this.label.Text = daysTotalMilkCrates.ToString();

                    labelCounter = labelCounter + 1;

                    daysTotalMilkCrates = 0;

                    milkCount = Convert.ToInt32(GCFDGlobals.dbGetValue(milkCrateDataRow, "MilkCrateCount"));

                    previousSiteRoute = GCFDGlobals.dbGetValue(milkCrateDataRow, "SiteRoute");
                }
                else if (previousSiteRoute != GCFDGlobals.dbGetValue(milkCrateDataRow, "SiteRoute"))
                {
                    totalCrates = Math.Ceiling(Convert.ToDecimal(milkCount) / 50);

                    daysTotalMilkCrates = daysTotalMilkCrates + Convert.ToInt32(totalCrates);

                    milkCount = Convert.ToInt32(GCFDGlobals.dbGetValue(milkCrateDataRow, "MilkCrateCount"));

                    previousSiteRoute = GCFDGlobals.dbGetValue(milkCrateDataRow, "SiteRoute");
                }
                else
                {
                    milkCount = milkCount + Convert.ToInt32(GCFDGlobals.dbGetValue(milkCrateDataRow, "MilkCrateCount"));
                }
            }

            totalCrates = Math.Ceiling(Convert.ToDecimal(milkCount) / 50);

            daysTotalMilkCrates = daysTotalMilkCrates + Convert.ToInt32(totalCrates);

            totalWeeksMilkCrates = totalWeeksMilkCrates + daysTotalMilkCrates;

            while (labelCounter < 8)
            {
                labelName = "Date" + labelCounter + "Label";

                this.label = this.Master.FindControl("MainContent").FindControl(labelName) as Label;

                if (this.label.Text != previousDeliveryDate)
                {
                    labelCounter = labelCounter + 1;
                }
                else
                {
                    break;
                }
            }

            labelName = "ChocolateMilkCrateCountDate" + labelCounter + "Label";

            this.label = this.Master.FindControl("MainContent").FindControl(labelName) as Label;

            this.label.Text = daysTotalMilkCrates.ToString();

            ChocolateMilkCrateCountTotalNeededLabel.Text = totalWeeksMilkCrates.ToString();

            startDate = Convert.ToDateTime(DateTextBox.Text);

            DateTime twoWeekdaysBefore = startDate.AddDays(-2);
            DateTime oneWeekdayBefore;

            if (twoWeekdaysBefore.DayOfWeek.ToString() == "Saturday")
            {
                twoWeekdaysBefore = startDate.AddDays(-4);

                oneWeekdayBefore = startDate.AddDays(-3);
            }
            else if (twoWeekdaysBefore.DayOfWeek.ToString() == "Sunday")
            {
                twoWeekdaysBefore = startDate.AddDays(-4);
                oneWeekdayBefore = startDate.AddDays(-1);
            }
            else
            {
                twoWeekdaysBefore = startDate.AddDays(-2);
                oneWeekdayBefore = startDate.AddDays(-1);
            }

            WeekdaysNameBeforeLabel.Text = "Needed for " + twoWeekdaysBefore.DayOfWeek.ToString() + " and " + oneWeekdayBefore.DayOfWeek.ToString();

            m_SQL = "SELECT DISTINCT SiteName, SiteRoute, DeliveryDate, dbo.udf_DeliveryReportByRangeMealCount(DeliveryDate, SiteID,38,1) + dbo.udf_DeliveryReportByRangeMealCount(DeliveryDate, SiteID,38,2) AS MilkCrateCount FROM vwDelivery WHERE DeliveryDate = '" + twoWeekdaysBefore + "' OR  DeliveryDate = '" + oneWeekdayBefore + "' AND MealTypeName = 'Cold-CCK' AND DeliveryTypeName <> 'Cancelled' GROUP BY DeliveryDate, SiteRoute, SiteName, SiteID ORDER BY DeliveryDate, siteroute, SiteName";
            m_MilkCrateDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            milkCount = 0;
            daysTotalMilkCrates = 0;
            totalCrates = 0;
            totalWeeksMilkCrates = 0;

            foreach (DataRow milkCrateDataRow in m_MilkCrateDataSet.Tables[0].Rows)
            {
                if (previousDeliveryDate != GCFDGlobals.dbGetValue(milkCrateDataRow, "DeliveryDate"))
                {
                    previousDeliveryDate = GCFDGlobals.dbGetValue(milkCrateDataRow, "DeliveryDate");

                    totalCrates = Math.Ceiling(Convert.ToDecimal(milkCount) / 50);

                    daysTotalMilkCrates = daysTotalMilkCrates + Convert.ToInt32(totalCrates);

                    totalWeeksMilkCrates = totalWeeksMilkCrates + daysTotalMilkCrates;

                    labelCounter = labelCounter + 1;

                    daysTotalMilkCrates = 0;

                    milkCount = Convert.ToInt32(GCFDGlobals.dbGetValue(milkCrateDataRow, "MilkCrateCount"));

                    previousSiteRoute = GCFDGlobals.dbGetValue(milkCrateDataRow, "SiteRoute");
                }
                else if (previousSiteRoute != GCFDGlobals.dbGetValue(milkCrateDataRow, "SiteRoute"))
                {
                    totalCrates = Math.Ceiling(Convert.ToDecimal(milkCount) / 50);

                    daysTotalMilkCrates = daysTotalMilkCrates + Convert.ToInt32(totalCrates);

                    milkCount = Convert.ToInt32(GCFDGlobals.dbGetValue(milkCrateDataRow, "MilkCrateCount"));

                    previousSiteRoute = GCFDGlobals.dbGetValue(milkCrateDataRow, "SiteRoute");
                }
                else
                {
                    milkCount = milkCount + Convert.ToInt32(GCFDGlobals.dbGetValue(milkCrateDataRow, "MilkCrateCount"));
                }
            }

            totalCrates = Math.Ceiling(Convert.ToDecimal(milkCount) / 50);

            daysTotalMilkCrates = daysTotalMilkCrates + Convert.ToInt32(totalCrates);

            totalWeeksMilkCrates = totalWeeksMilkCrates + daysTotalMilkCrates;

            ChocolateNeededForWeekdaysBeforeLabel.Text = totalWeeksMilkCrates.ToString();
        }
    }

    protected void UpdateWhiteMilkCounts()
    {
        labelCounter = 1;
        milkCount = 0;
        daysTotalMilkCrates = 0;
        totalCrates = 0;
        totalWeeksMilkCrates = 0;

        string m_SQL = "SELECT DISTINCT SiteName, SiteRoute, DeliveryDate, dbo.udf_DeliveryReportByRangeMealCount(DeliveryDate, SiteID, 38, 1) + dbo.udf_DeliveryReportByRangeMealCount(DeliveryDate, SiteID, 38, 2) AS MilkCrateCount FROM vwDelivery WHERE DeliveryDate >= '" + DateTextBox.Text + "' AND DeliveryDate <= '" + Date5Label.Text + "' AND MealTypeName = 'Cold-CCK' AND DeliveryTypeName <> 'Cancelled' GROUP BY DeliveryDate, SiteRoute, SiteName, SiteID ORDER BY DeliveryDate, siteroute, SiteName";
        DataSet m_MilkCrateDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        if (m_MilkCrateDataSet.Tables[0].Rows.Count > 0)
        {

            string previousDeliveryDate = Convert.ToDateTime(GCFDGlobals.dbGetValue(m_MilkCrateDataSet.Tables[0].Rows[0], "DeliveryDate")).ToString("MM/dd/yyyy");
            string previousSiteRoute = GCFDGlobals.dbGetValue(m_MilkCrateDataSet.Tables[0].Rows[0], "SiteRoute");

            foreach (DataRow milkCrateDataRow in m_MilkCrateDataSet.Tables[0].Rows)
            {
                if (previousDeliveryDate != Convert.ToDateTime(GCFDGlobals.dbGetValue(milkCrateDataRow, "DeliveryDate")).ToString("MM/dd/yyyy"))
                {
                    while (labelCounter < 8)
                    {
                        labelName = "Date" + labelCounter + "Label";

                        this.label = this.Master.FindControl("MainContent").FindControl(labelName) as Label;

                        if (this.label.Text != previousDeliveryDate)
                        {
                            labelCounter = labelCounter + 1;
                        }
                        else
                        {
                            break;
                        }
                    }

                    previousDeliveryDate = Convert.ToDateTime(GCFDGlobals.dbGetValue(milkCrateDataRow, "DeliveryDate")).ToString("MM/dd/yyyy");

                    totalCrates = Math.Ceiling(Convert.ToDecimal(milkCount) / 50);

                    daysTotalMilkCrates = daysTotalMilkCrates + Convert.ToInt32(totalCrates);

                    totalWeeksMilkCrates = totalWeeksMilkCrates + daysTotalMilkCrates;

                    labelName = "WhiteMilkCrateCountDate" + labelCounter + "Label";

                    this.label = this.Master.FindControl("MainContent").FindControl(labelName) as Label;

                    this.label.Text = daysTotalMilkCrates.ToString();

                    labelCounter = labelCounter + 1;

                    daysTotalMilkCrates = 0;

                    milkCount = Convert.ToInt32(GCFDGlobals.dbGetValue(milkCrateDataRow, "MilkCrateCount"));

                    previousSiteRoute = GCFDGlobals.dbGetValue(milkCrateDataRow, "SiteRoute");
                }
                else if (previousSiteRoute != GCFDGlobals.dbGetValue(milkCrateDataRow, "SiteRoute"))
                {
                    totalCrates = Math.Ceiling(Convert.ToDecimal(milkCount) / 50);

                    daysTotalMilkCrates = daysTotalMilkCrates + Convert.ToInt32(totalCrates);

                    milkCount = Convert.ToInt32(GCFDGlobals.dbGetValue(milkCrateDataRow, "MilkCrateCount"));

                    previousSiteRoute = GCFDGlobals.dbGetValue(milkCrateDataRow, "SiteRoute");
                }
                else
                {
                    milkCount = milkCount + Convert.ToInt32(GCFDGlobals.dbGetValue(milkCrateDataRow, "MilkCrateCount"));
                }
            }

            totalCrates = Math.Ceiling(Convert.ToDecimal(milkCount) / 50);

            daysTotalMilkCrates = daysTotalMilkCrates + Convert.ToInt32(totalCrates);

            totalWeeksMilkCrates = totalWeeksMilkCrates + daysTotalMilkCrates;

            while (labelCounter < 8)
            {
                labelName = "Date" + labelCounter + "Label";

                this.label = this.Master.FindControl("MainContent").FindControl(labelName) as Label;

                if (this.label.Text != previousDeliveryDate)
                {
                    labelCounter = labelCounter + 1;
                }
                else
                {
                    break;
                }
            }

            labelName = "WhiteMilkCrateCountDate" + labelCounter + "Label";

            this.label = this.Master.FindControl("MainContent").FindControl(labelName) as Label;

            this.label.Text = daysTotalMilkCrates.ToString();

            WhiteMilkCrateCountTotalNeededLabel.Text = totalWeeksMilkCrates.ToString();

            startDate = Convert.ToDateTime(DateTextBox.Text);

            DateTime twoWeekdaysBefore = startDate.AddDays(-2);
            DateTime oneWeekdayBefore;

            if (twoWeekdaysBefore.DayOfWeek.ToString() == "Saturday")
            {
                twoWeekdaysBefore = startDate.AddDays(-4);

                oneWeekdayBefore = startDate.AddDays(-3);
            }
            else if (twoWeekdaysBefore.DayOfWeek.ToString() == "Sunday")
            {
                twoWeekdaysBefore = startDate.AddDays(-4);
                oneWeekdayBefore = startDate.AddDays(-1);
            }
            else
            {
                twoWeekdaysBefore = startDate.AddDays(-2);
                oneWeekdayBefore = startDate.AddDays(-1);
            }

            m_SQL = "SELECT DISTINCT SiteName, SiteRoute, DeliveryDate, dbo.udf_DeliveryReportByRangeMealCount(DeliveryDate, SiteID, 38, 1) + dbo.udf_DeliveryReportByRangeMealCount(DeliveryDate, SiteID, 38, 2) AS MilkCrateCount FROM vwDelivery WHERE DeliveryDate = '" + twoWeekdaysBefore + "' OR  DeliveryDate = '" + oneWeekdayBefore + "' AND MealTypeName = 'Cold-CCK' AND DeliveryTypeName <> 'Cancelled' GROUP BY DeliveryDate, SiteRoute, SiteName, SiteID ORDER BY DeliveryDate, siteroute, SiteName";
            m_MilkCrateDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            milkCount = 0;
            daysTotalMilkCrates = 0;
            totalCrates = 0;
            totalWeeksMilkCrates = 0;

            foreach (DataRow milkCrateDataRow in m_MilkCrateDataSet.Tables[0].Rows)
            {
                if (previousDeliveryDate != GCFDGlobals.dbGetValue(milkCrateDataRow, "DeliveryDate"))
                {
                    previousDeliveryDate = GCFDGlobals.dbGetValue(milkCrateDataRow, "DeliveryDate");

                    totalCrates = Math.Ceiling(Convert.ToDecimal(milkCount) / 50);

                    daysTotalMilkCrates = daysTotalMilkCrates + Convert.ToInt32(totalCrates);

                    totalWeeksMilkCrates = totalWeeksMilkCrates + daysTotalMilkCrates;

                    labelCounter = labelCounter + 1;

                    daysTotalMilkCrates = 0;

                    milkCount = Convert.ToInt32(GCFDGlobals.dbGetValue(milkCrateDataRow, "MilkCrateCount"));

                    previousSiteRoute = GCFDGlobals.dbGetValue(milkCrateDataRow, "SiteRoute");
                }
                else if (previousSiteRoute != GCFDGlobals.dbGetValue(milkCrateDataRow, "SiteRoute"))
                {
                    totalCrates = Math.Ceiling(Convert.ToDecimal(milkCount) / 50);

                    daysTotalMilkCrates = daysTotalMilkCrates + Convert.ToInt32(totalCrates);

                    milkCount = Convert.ToInt32(GCFDGlobals.dbGetValue(milkCrateDataRow, "MilkCrateCount"));

                    previousSiteRoute = GCFDGlobals.dbGetValue(milkCrateDataRow, "SiteRoute");
                }
                else
                {
                    milkCount = milkCount + Convert.ToInt32(GCFDGlobals.dbGetValue(milkCrateDataRow, "MilkCrateCount"));
                }
            }

            totalCrates = Math.Ceiling(Convert.ToDecimal(milkCount) / 50);

            daysTotalMilkCrates = daysTotalMilkCrates + Convert.ToInt32(totalCrates);

            totalWeeksMilkCrates = totalWeeksMilkCrates + daysTotalMilkCrates;

            WhiteNeededForWeekdaysBeforeLabel.Text = totalWeeksMilkCrates.ToString();
        }
    }
}