using System;
using Microsoft.Reporting.WebForms;
using System.Data;
using GCFDGlobalsNamespace;

public partial class ColdMealSchedule : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
    {
        string deliverySiteName;
        string m_SQL;
        string reportStartDate = Request.QueryString.Get("ReportStartDate");
        string reportType = Request.QueryString.Get("ReportType");

        m_SQL = "DELETE FROM DailyCountReport";
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

        m_SQL = "SELECT DISTINCT SiteName FROM vwDelivery WHERE ServingDate BETWEEN '" + reportStartDate + "' AND DATEADD(d, 6, '" + reportStartDate + "') AND DeliveryTypeID IN (1,2) AND DeliveryDate <> '01/01/1900' AND MealTypeID = 2";
        DataSet deliveryDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        m_SQL =
            "DECLARE @FirstDay smalldatetime, @NumberOfMonths int SELECT @FirstDay = '" + Convert.ToDateTime(reportStartDate).ToString("yyyyMMdd") + "';WITH Days AS (SELECT @FirstDay as CalendarDay UNION ALL SELECT DATEADD(d, 1, CalendarDay) as CalendarDay FROM Days WHERE DATEADD(d, 1, CalendarDay) < DATEADD(d, 6, '" + reportStartDate + "')) SELECT CONVERT(varchar(10), CalendarDay, 101) AS WeekdayDate FROM Days";
        DataSet daysOfTheWeekDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        foreach (DataRow deliveryDataRow in deliveryDataSet.Tables[0].Rows)
        {
            deliverySiteName = GCFDGlobals.dbGetValue(deliveryDataRow, "SiteName");

            foreach (DataRow weekdayDataRow in daysOfTheWeekDataSet.Tables[0].Rows)
            {
                m_SQL = "SELECT DISTINCT SiteName, LAHSiteCode, ServingDate, DATENAME(dw, ServingDate) AS WeekdayName, MealCount, MealTypeName FROM vwDelivery WHERE MealTypeID = 2 AND ((DeliveryTypeName = 'Scheduled') OR (DeliveryTypeName='Rescheduled' AND CONVERT(varchar(10), DeliveryDate, 101) <> '01/01/1900')) AND ServingDate = '" + GCFDGlobals.dbGetValue(weekdayDataRow, "WeekdayDate") + "' AND SiteName = '" + deliverySiteName + "'";
                DataSet dailyCountDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                if (dailyCountDataSet.Tables[0].Rows.Count > 0)
                {
                    m_SQL =
                        "INSERT INTO DailyCountReport(ServingDate, SiteName, Sent, MealTypeName) VALUES('" +
                        Convert.ToDateTime(GCFDGlobals.dbGetValue(weekdayDataRow, "WeekdayDate")).ToString("MM/dd/yyyy") + "', '" + deliverySiteName + "', " +
                        GCFDGlobals.dbGetValue(dailyCountDataSet.Tables[0].Rows[0], "MealCount") + ", '" + GCFDGlobals.dbGetValue(dailyCountDataSet.Tables[0].Rows[0], "MealTypeName") + "')";
                    GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
                }
                else
                {
                    m_SQL =
                        "INSERT INTO DailyCountReport(ServingDate, SiteName, Sent, MealTypeName) VALUES('" +
                        Convert.ToDateTime(GCFDGlobals.dbGetValue(weekdayDataRow, "WeekdayDate")).ToString("MM/dd/yyyy") + "', '" + deliverySiteName + "', 0, 'Cold')";
                    GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
                }
            }
        }

        m_SQL = "SELECT DISTINCT SiteName FROM vwDelivery WHERE ServingDate BETWEEN '" + reportStartDate + "' AND DATEADD(d, 6, '" + reportStartDate + "') AND DeliveryTypeID IN (1,2) AND DeliveryDate <> '01/01/1900' AND MealTypeID = 8";
        deliveryDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        m_SQL =
            "DECLARE @FirstDay smalldatetime, @NumberOfMonths int SELECT @FirstDay = '" + Convert.ToDateTime(reportStartDate).ToString("yyyyMMdd") + "';WITH Days AS (SELECT @FirstDay as CalendarDay UNION ALL SELECT DATEADD(d, 1, CalendarDay) as CalendarDay FROM Days WHERE DATEADD(d, 1, CalendarDay) < DATEADD(d, 6, '" + reportStartDate + "')) SELECT CONVERT(varchar(10), CalendarDay, 101) AS WeekdayDate FROM Days";
        daysOfTheWeekDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        foreach (DataRow deliveryDataRow in deliveryDataSet.Tables[0].Rows)
        {
            deliverySiteName = GCFDGlobals.dbGetValue(deliveryDataRow, "SiteName");

            foreach (DataRow weekdayDataRow in daysOfTheWeekDataSet.Tables[0].Rows)
            {
                m_SQL = "SELECT DISTINCT SiteName, LAHSiteCode, ServingDate, DATENAME(dw, ServingDate) AS WeekdayName, MealCount, MealTypeName FROM vwDelivery WHERE MealTypeID = 8 AND ((DeliveryTypeName = 'Scheduled') OR (DeliveryTypeName='Rescheduled' AND CONVERT(varchar(10), DeliveryDate, 101) <> '01/01/1900')) AND ServingDate = '" + GCFDGlobals.dbGetValue(weekdayDataRow, "WeekdayDate") + "' AND SiteName = '" + deliverySiteName + "'";
                DataSet dailyCountDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);
                
                if (dailyCountDataSet.Tables[0].Rows.Count > 0)
                {
                    m_SQL =
                        "INSERT INTO DailyCountReport(ServingDate, SiteName, Sent, MealTypeName) VALUES('" +
                        Convert.ToDateTime(GCFDGlobals.dbGetValue(weekdayDataRow, "WeekdayDate")).ToString("MM/dd/yyyy") + "', '" + deliverySiteName + "', " +
                        GCFDGlobals.dbGetValue(dailyCountDataSet.Tables[0].Rows[0], "MealCount") + ", '" + GCFDGlobals.dbGetValue(dailyCountDataSet.Tables[0].Rows[0], "MealTypeName") + "')";
                    GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
                }
                else
                {
                    m_SQL =
                        "INSERT INTO DailyCountReport(ServingDate, SiteName, Sent, MealTypeName) VALUES('" +
                        Convert.ToDateTime(GCFDGlobals.dbGetValue(weekdayDataRow, "WeekdayDate")).ToString("MM/dd/yyyy") + "', '" + deliverySiteName + "', 0, 'Cold Breakfast (LAH)')";
                    GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
                }
            }
        }

        if (reportType == "Excel")
        {
            ProcessExcelReport report = new ProcessExcelReport();

            report.StartDate = reportStartDate;
            report.Name = "ColdMealScheduleReport";
            report.Response = Response;

            report.RenderReport();
        }
        else
        {
            ProcessReport report = new ProcessReport();

            report.StartDate = reportStartDate;
            report.Name = "ColdMealScheduleReport";
            report.Response = Response;

            report.RenderReport();
        }        
    }
}
