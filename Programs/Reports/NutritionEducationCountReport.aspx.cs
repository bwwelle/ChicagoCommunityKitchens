using System;

public partial class NutritionEducationCountReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string reportStartDate = Request.QueryString.Get("ReportStartDate");
        string reportEndDate = Request.QueryString.Get("ReportEndDate");
        string reportType = Request.QueryString.Get("ReportType");

        if (reportType == "Excel")
        {
            ProcessExcelReport report = new ProcessExcelReport();

            report.StartDate = reportStartDate;
            report.EndDate = reportEndDate;
            report.Name = "NutritionEducationCountReport";
            report.Response = Response;

            report.RenderReport();
        }
        else
        {
            ProcessReport report = new ProcessReport();

            report.StartDate = reportStartDate;
            report.EndDate = reportEndDate;
            report.Name = "NutritionEducationCountReport";
            report.Response = Response;

            report.RenderReport();
        }            
    }
}
