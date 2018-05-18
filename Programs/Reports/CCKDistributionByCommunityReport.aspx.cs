using System;
using Microsoft.Reporting.WebForms;
using System.Data;
using GCFDGlobalsNamespace;

public partial class CCKDistributionByCommunityReport : System.Web.UI.Page
{
    private string m_SQL;

    protected void Page_Load(object sender, EventArgs e)
    {
        string reportID;
        string reportStartDate = Request.QueryString.Get("ReportStartDate");
        string reportEndDate = Request.QueryString.Get("ReportEndDate");
        string reportType = Request.QueryString.Get("ReportType");

        m_SQL = "DECLARE @ReportID int EXEC spInsertCCKDistributionByCommunity '" + reportStartDate + "', '" + reportEndDate + "', '" + User.Identity.Name + "', @ReportID = @ReportID OUTPUT SELECT @ReportID AS 'ReportID'";
        DataSet dailyCountReportCreation = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        reportID = GCFDGlobals.dbGetValue(dailyCountReportCreation.Tables[0].Rows[0], "ReportID");

        if (reportType == "Excel")
        {
            ProcessExcelReport report = new ProcessExcelReport();

            report.Name = "CCKDistributionByCommunityReport";
            report.StartDate = reportStartDate;
            report.EndDate = reportEndDate;
            report.ReportID = reportID;
            report.Response = Response;

            report.RenderReport();
        }
        else
        {
            ProcessReport report = new ProcessReport();

            report.Name = "CCKDistributionByCommunityReport";
            report.StartDate = reportStartDate;
            report.EndDate = reportEndDate;
            report.ReportID = reportID;
            report.Response = Response;

            report.RenderReport();
        }
    }
}
