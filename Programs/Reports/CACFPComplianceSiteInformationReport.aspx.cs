using System;

public partial class CACFPComplianceSiteInformationReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string reportType = Request.QueryString.Get("ReportType");

        if (reportType == "Excel")
        {
            ProcessExcelReport report = new ProcessExcelReport();

            report.Name = "CACFPComplianceSiteInformationReport";
            report.Response = Response;

            report.RenderReport();
        }
        else
        {
            ProcessReport report = new ProcessReport();

            report.Name = "CACFPComplianceSiteInformationReport";
            report.Response = Response;

            report.RenderReport();        
        }
    }
}
