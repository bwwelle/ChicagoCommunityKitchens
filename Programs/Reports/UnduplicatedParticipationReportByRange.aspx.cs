using System;

public partial class UnduplicatedParticipationReportByRange : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string reportType = Request.QueryString.Get("ReportType");
        string reportStartDate = Request.QueryString.Get("ReportStartDate");
        string reportEndDate = Request.QueryString.Get("ReportEndDate");

        if (reportType == "Excel")
        {
            ProcessExcelReport report = new ProcessExcelReport();

            report.Name = "UnduplicatedParticipationReportByRange";
            report.StartDate = reportStartDate;
            report.EndDate = reportEndDate;
            report.Response = Response;

            report.RenderReport();
        }
        else
        {
            ProcessReport report = new ProcessReport();

            report.Name = "UnduplicatedParticipationReportByRange";
            report.StartDate = reportStartDate;
            report.EndDate = reportEndDate;
            report.Response = Response;

            report.RenderReport();
        }	
    }
}
