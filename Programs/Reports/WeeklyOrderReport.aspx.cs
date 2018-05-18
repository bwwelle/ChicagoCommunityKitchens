using System;

public partial class WeeklyOrderReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string reportStartDate = Request.QueryString.Get("ReportStartDate");
        string reportType = Request.QueryString.Get("ReportType");

        if (reportType == "Excel")
        {
            ProcessExcelReport report = new ProcessExcelReport();

            report.StartDate = reportStartDate;
            report.Name = "WeeklyOrderReport";
            report.Response = Response;

            report.RenderReport();
        }
        else
        {
            ProcessReport report = new ProcessReport();

            report.StartDate = reportStartDate;
            report.Name = "WeeklyOrderReport";
            report.Response = Response;

            report.RenderReport();
        }        
    }
}
