using System;
using Microsoft.Reporting.WebForms;
using System.Data;
using GCFDGlobalsNamespace;

public partial class CACFPWeeklyOrderReportCold : System.Web.UI.Page
{
	private string m_SQL;

    protected void Page_Load(object sender, EventArgs e)
    {
        string createdMeal;
        string reportStartDate = Request.QueryString.Get("ReportStartDate");
        string reportEndDate = Request.QueryString.Get("ReportEndDate");
        string reportType = Request.QueryString.Get("ReportType");
        string bagCount = Request.QueryString.Get("BagCount");
        string sliceCount = Request.QueryString.Get("SliceCount");
        string loafCount = Request.QueryString.Get("LoafCount");
        string bunCount = Request.QueryString.Get("BunCount");

        m_SQL = "DECLARE @CreatedMeal varchar(50) EXEC spInsertWeeklyOrderReportInformationCold " + sliceCount + ", " + loafCount + ", " + bunCount + ", " + bagCount + ", '" + reportStartDate + "', '" + reportEndDate + "', '" + User.Identity.Name + "', @CreatedMeal = @CreatedMeal OUTPUT SELECT @CreatedMeal as 'CreatedMeal'";
        DataSet dailyCountReportCreation = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        createdMeal = GCFDGlobals.dbGetValue(dailyCountReportCreation.Tables[0].Rows[0], "CreatedMeal");

        if (createdMeal != "-1")
        {
            if (reportType == "Excel")
            {
                ProcessExcelReport report = new ProcessExcelReport();

                report.ReportID = createdMeal;
                report.StartDate = reportStartDate;
                report.EndDate = reportEndDate;
                report.Name = "CACFPWeeklyOrderReportCold";
                report.Response = Response;

                report.RenderReport();
            }
            else
            {
                ProcessReport report = new ProcessReport();

                report.ReportID = createdMeal;
                report.StartDate = reportStartDate;
                report.EndDate = reportEndDate;
                report.Name = "CACFPWeeklyOrderReportCold";
                report.Response = Response;

                report.RenderReport();
            }            
        }
        else
        {
            RegisterClientScriptBlock("",
                      "<script>{ alert('Error creating CACFP Weekly Order Report.');window.close();}</script>");
        }        
    }
}
