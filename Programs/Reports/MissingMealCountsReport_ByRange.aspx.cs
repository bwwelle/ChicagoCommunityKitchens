using System;

public partial class MissingMealCountsReport_ByRange: System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string reportType = Request.QueryString.Get("ReportType");
        string reportStartDate = Request.QueryString.Get("ReportStartDate");
        string reportEndDate = Request.QueryString.Get("ReportEndDate");

        if (reportType == "Excel")
        {
            ProcessExcelReport report = new ProcessExcelReport();

            report.StartDate = reportStartDate;
            report.EndDate = reportEndDate;

            report.Name = "MissingMealCountsByDateRange";
            report.Response = Response;

            report.RenderReport();
        }
        else
        {
            ProcessReport report = new ProcessReport();

            report.StartDate = reportStartDate;
            report.EndDate = reportEndDate;

            report.Name = "MissingMealCountsByDateRange";
            report.Response = Response;

            report.RenderReport();
        }        
    }
}
