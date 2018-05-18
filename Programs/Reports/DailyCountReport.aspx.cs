using System;
using Microsoft.Reporting.WebForms;
using System.Data;
using GCFDGlobalsNamespace;

public partial class DailyCountReport : System.Web.UI.Page
{
	private string m_SQL;

    protected void Page_Load(object sender, EventArgs e)
    {
        string createdMeal;
        string reportStartDate = Request.QueryString.Get("ReportStartDate");
        string reportEndDate = Request.QueryString.Get("ReportEndDate");
        string mealType = Request.QueryString.Get("MealType");
        string scheduleType = Request.QueryString.Get("ScheduleType");
        string communityArea = Request.QueryString.Get("CommunityArea");
        string siteName = Request.QueryString.Get("SiteName");
        string reportType = Request.QueryString.Get("ReportType");

        m_SQL = "DECLARE @CreatedMeal varchar(50) EXEC spInsertDailyCountReportInformation '" + reportStartDate + "', '" + reportEndDate + "', " + mealType + ", " + scheduleType + ",'" + communityArea + "', '" + siteName + "', '" + User.Identity.Name + "', @CreatedMeal = @CreatedMeal OUTPUT SELECT @CreatedMeal as 'CreatedMeal'";
        DataSet dailyCountReportCreation = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        createdMeal = GCFDGlobals.dbGetValue(dailyCountReportCreation.Tables[0].Rows[0], "CreatedMeal");

        if (createdMeal != "-1")
        {
            if (siteName == "-1" && mealType != "-1" && communityArea == "-1" && scheduleType == "-1")
            {
                m_SQL = "SELECT MealTypeName FROM MealTypeDict WHERE MealTypeID = " + mealType;
                DataSet MealTypeIDDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                //m_SQL = "SELECT ScheduleTypeName FROM ScheduleTypeDict WHERE ScheduleTypeID = " + scheduleType;
                //DataSet ScheduleTypeIDDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                if (reportType == "Excel")
                {
                    ProcessExcelReport report = new ProcessExcelReport();

                    report.MealType = GCFDGlobals.dbGetValue(MealTypeIDDataSet.Tables[0].Rows[0], "MealTypeName");
                    //report.ScheduleType = GCFDGlobals.dbGetValue(ScheduleTypeIDDataSet.Tables[0].Rows[0], "ScheduleTypeName"); 
                    report.ReportID = createdMeal;
                    report.StartDate = reportStartDate;
                    report.EndDate = reportEndDate;
                    report.Name = "NewDailyCountReport";
                    report.Response = Response;
                    report.RenderReport();
                }
                else
                {
                    ProcessReport report = new ProcessReport();

                    report.MealType = GCFDGlobals.dbGetValue(MealTypeIDDataSet.Tables[0].Rows[0], "MealTypeName");
                    //report.ScheduleType = GCFDGlobals.dbGetValue(ScheduleTypeIDDataSet.Tables[0].Rows[0], "ScheduleTypeName"); 
                    report.ReportID = createdMeal;
                    report.StartDate = reportStartDate;
                    report.EndDate = reportEndDate;
                    report.Name = "NewDailyCountReport";
                    report.Response = Response;
                    report.RenderReport();
                }                      
            }
            else if (siteName == "-1" && mealType != "-1" && communityArea == "-1" && scheduleType != "-1")
            {
                m_SQL = "SELECT MealTypeName FROM MealTypeDict WHERE MealTypeID = " + mealType;
                DataSet MealTypeIDDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                m_SQL = "SELECT ScheduleTypeName FROM ScheduleTypeDict WHERE ScheduleTypeID = " + scheduleType;
                DataSet ScheduleTypeIDDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                if (reportType == "Excel")
                {
                    ProcessExcelReport report = new ProcessExcelReport();

                    report.MealType = GCFDGlobals.dbGetValue(MealTypeIDDataSet.Tables[0].Rows[0], "MealTypeName");
                    report.ScheduleType = GCFDGlobals.dbGetValue(ScheduleTypeIDDataSet.Tables[0].Rows[0], "ScheduleTypeName"); 
                    report.ReportID = createdMeal;
                    report.StartDate = reportStartDate;
                    report.EndDate = reportEndDate;
                    report.Name = "NewDailyCountReportByScheduleType";
                    report.Response = Response;
                    report.RenderReport();
                }
                else
                {
                    ProcessReport report = new ProcessReport();

                    report.MealType = GCFDGlobals.dbGetValue(MealTypeIDDataSet.Tables[0].Rows[0], "MealTypeName");
                    report.ScheduleType = GCFDGlobals.dbGetValue(ScheduleTypeIDDataSet.Tables[0].Rows[0], "ScheduleTypeName"); 
                    report.ReportID = createdMeal;
                    report.StartDate = reportStartDate;
                    report.EndDate = reportEndDate;
                    report.Name = "NewDailyCountReportByScheduleType";
                    report.Response = Response;
                    report.RenderReport();
                }
            }
            else if(siteName == "-1" && mealType != "-1" && communityArea != "-1" && scheduleType == "-1")
            {
                m_SQL = "SELECT MealTypeName FROM MealTypeDict WHERE MealTypeID = " + mealType;
                DataSet MealTypeIDDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                if (reportType == "Excel")
                {
                    ProcessExcelReport report = new ProcessExcelReport();

                    report.MealType = GCFDGlobals.dbGetValue(MealTypeIDDataSet.Tables[0].Rows[0], "MealTypeName");
                    report.CommunityArea = communityArea;
                    report.ReportID = createdMeal;
                    report.StartDate = reportStartDate;
                    report.EndDate = reportEndDate;
                    report.Name = "NewDailyCountReportByCommunityAreaMealType";
                    report.Response = Response;
                    report.RenderReport();
                }
                else
                {
                    ProcessReport report = new ProcessReport();

                    report.MealType = GCFDGlobals.dbGetValue(MealTypeIDDataSet.Tables[0].Rows[0], "MealTypeName");
                    report.CommunityArea = communityArea;
                    report.ReportID = createdMeal;
                    report.StartDate = reportStartDate;
                    report.EndDate = reportEndDate;
                    report.Name = "NewDailyCountReportByCommunityAreaMealType";
                    report.Response = Response;
                    report.RenderReport();
                }      
            }
            else if (siteName == "-1" && mealType != "-1" && communityArea != "-1" && scheduleType != "-1")
            {
                m_SQL = "SELECT MealTypeName FROM MealTypeDict WHERE MealTypeID = " + mealType;
                DataSet MealTypeIDDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                m_SQL = "SELECT ScheduleTypeName FROM ScheduleTypeDict WHERE ScheduleTypeID = " + scheduleType;
                DataSet ScheduleTypeIDDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                if (reportType == "Excel")
                {
                    ProcessExcelReport report = new ProcessExcelReport();

                    report.MealType = GCFDGlobals.dbGetValue(MealTypeIDDataSet.Tables[0].Rows[0], "MealTypeName");
                    report.ScheduleType = GCFDGlobals.dbGetValue(ScheduleTypeIDDataSet.Tables[0].Rows[0], "ScheduleTypeName"); 
                    report.CommunityArea = communityArea;
                    report.ReportID = createdMeal;
                    report.StartDate = reportStartDate;
                    report.EndDate = reportEndDate;
                    report.Name = "NewDailyCountReportByCommunityAreaMealTypeScheduleType";
                    report.Response = Response;
                    report.RenderReport();
                }
                else
                {
                    ProcessReport report = new ProcessReport();

                    report.MealType = GCFDGlobals.dbGetValue(MealTypeIDDataSet.Tables[0].Rows[0], "MealTypeName");
                    report.ScheduleType = GCFDGlobals.dbGetValue(ScheduleTypeIDDataSet.Tables[0].Rows[0], "ScheduleTypeName"); 
                    report.CommunityArea = communityArea;
                    report.ReportID = createdMeal;
                    report.StartDate = reportStartDate;
                    report.EndDate = reportEndDate;
                    report.Name = "NewDailyCountReportByCommunityAreaMealTypeScheduleType";
                    report.Response = Response;
                    report.RenderReport();
                }
            }
            else if (siteName == "-1" && mealType == "-1" && communityArea == "-1" && scheduleType == "-1")
            {
                if (reportType == "Excel")
                {
                    ProcessExcelReport report = new ProcessExcelReport();

                    report.ReportID = createdMeal;
                    report.StartDate = reportStartDate;
                    report.EndDate = reportEndDate;
                    report.Name = "NewDailyCountReportOrderedBySite";
                    report.Response = Response;
                    report.RenderReport();
                }
                else
                {
                    ProcessReport report = new ProcessReport();

                    report.ReportID = createdMeal;
                    report.StartDate = reportStartDate;
                    report.EndDate = reportEndDate;
                    report.Name = "NewDailyCountReportOrderedBySite";
                    report.Response = Response;
                    report.RenderReport();
                }                      
            }
            else if (siteName == "-1" && mealType == "-1" && communityArea == "-1" && scheduleType != "-1")
            {
                m_SQL = "SELECT ScheduleTypeName FROM ScheduleTypeDict WHERE ScheduleTypeID = " + scheduleType;
                DataSet ScheduleTypeIDDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                if (reportType == "Excel")
                {
                    ProcessExcelReport report = new ProcessExcelReport();

                    report.ReportID = createdMeal;
                    report.StartDate = reportStartDate;
                    report.ScheduleType = GCFDGlobals.dbGetValue(ScheduleTypeIDDataSet.Tables[0].Rows[0], "ScheduleTypeName"); 
                    report.EndDate = reportEndDate;
                    report.Name = "NewDailyCountReportOrderedBySiteScheduleType";
                    report.Response = Response;
                    report.RenderReport();
                }
                else
                {
                    ProcessReport report = new ProcessReport();

                    report.ReportID = createdMeal;
                    report.StartDate = reportStartDate;
                    report.ScheduleType = GCFDGlobals.dbGetValue(ScheduleTypeIDDataSet.Tables[0].Rows[0], "ScheduleTypeName"); 
                    report.EndDate = reportEndDate;
                    report.Name = "NewDailyCountReportOrderedBySiteScheduleType";
                    report.Response = Response;
                    report.RenderReport();
                }
            }
            else if (siteName != "-1" && mealType == "-1" && communityArea == "-1" && scheduleType == "-1")
            {
                if (reportType == "Excel")
                {
                    ProcessExcelReport report = new ProcessExcelReport();

                    report.ReportID = createdMeal;
                    report.SiteName = siteName;
                    report.StartDate = reportStartDate;
                    report.EndDate = reportEndDate;
                    report.Name = "NewDailyCountReportBySite";
                    report.Response = Response;
                    report.RenderReport();
                }
                else
                {
                    ProcessReport report = new ProcessReport();
                    
                    report.ReportID = createdMeal;
                    report.SiteName = siteName;
                    report.StartDate = reportStartDate;
                    report.EndDate = reportEndDate;
                    report.Name = "NewDailyCountReportBySite";
                    report.Response = Response;
                    report.RenderReport();
                }                     
            }
            else if (siteName != "-1" && mealType == "-1" && communityArea == "-1" && scheduleType != "-1")
            {
                m_SQL = "SELECT ScheduleTypeName FROM ScheduleTypeDict WHERE ScheduleTypeID = " + scheduleType;
                DataSet ScheduleTypeIDDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                if (reportType == "Excel")
                {
                    ProcessExcelReport report = new ProcessExcelReport();

                    report.ReportID = createdMeal;
                    report.ScheduleType = GCFDGlobals.dbGetValue(ScheduleTypeIDDataSet.Tables[0].Rows[0], "ScheduleTypeName"); 
                    report.SiteName = siteName;
                    report.StartDate = reportStartDate;
                    report.EndDate = reportEndDate;
                    report.Name = "NewDailyCountReportBySite";
                    report.Response = Response;
                    report.RenderReport();
                }
                else
                {
                    ProcessReport report = new ProcessReport();

                    report.ReportID = createdMeal;
                    report.ScheduleType = GCFDGlobals.dbGetValue(ScheduleTypeIDDataSet.Tables[0].Rows[0], "ScheduleTypeName"); 
                    report.SiteName = siteName;
                    report.StartDate = reportStartDate;
                    report.EndDate = reportEndDate;
                    report.Name = "NewDailyCountReportBySite";
                    report.Response = Response;
                    report.RenderReport();
                }
            }
            else if (siteName == "-1" && mealType == "-1" && communityArea != "-1" && scheduleType == "-1")
            {
                if (reportType == "Excel")
                {
                    ProcessExcelReport report = new ProcessExcelReport();

                    report.ReportID = createdMeal;
                    report.CommunityArea = communityArea;
                    report.StartDate = reportStartDate;
                    report.EndDate = reportEndDate;
                    report.Name = "NewDailyCountReportByCommunityArea";
                    report.Response = Response;

                    report.RenderReport();
                }
                else
                {
                    ProcessReport report = new ProcessReport();

                    report.ReportID = createdMeal;
                    report.CommunityArea = communityArea;
                    report.StartDate = reportStartDate;
                    report.EndDate = reportEndDate;
                    report.Name = "NewDailyCountReportByCommunityArea";
                    report.Response = Response;

                    report.RenderReport();
                } 
            }
            else if (siteName == "-1" && mealType == "-1" && communityArea != "-1" && scheduleType != "-1")
            {
                m_SQL = "SELECT ScheduleTypeName FROM ScheduleTypeDict WHERE ScheduleTypeID = " + scheduleType;
                DataSet ScheduleTypeIDDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                if (reportType == "Excel")
                {
                    ProcessExcelReport report = new ProcessExcelReport();

                    report.ReportID = createdMeal;
                    report.ScheduleType = GCFDGlobals.dbGetValue(ScheduleTypeIDDataSet.Tables[0].Rows[0], "ScheduleTypeName"); 
                    report.CommunityArea = communityArea;
                    report.StartDate = reportStartDate;
                    report.EndDate = reportEndDate;
                    report.Name = "NewDailyCountReportByCommunityArea";
                    report.Response = Response;

                    report.RenderReport();
                }
                else
                {
                    ProcessReport report = new ProcessReport();

                    report.ReportID = createdMeal;
                    report.ScheduleType = GCFDGlobals.dbGetValue(ScheduleTypeIDDataSet.Tables[0].Rows[0], "ScheduleTypeName"); 
                    report.CommunityArea = communityArea;
                    report.StartDate = reportStartDate;
                    report.EndDate = reportEndDate;
                    report.Name = "NewDailyCountReportByCommunityArea";
                    report.Response = Response;

                    report.RenderReport();
                }
            }  
        }
        else
        {
            RegisterClientScriptBlock("",
                      "<script>{ alert('Error creating Daily Count Report.');window.close();}</script>");
        }        
    }
}
