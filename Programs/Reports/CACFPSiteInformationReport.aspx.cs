using System;

public partial class CACFPSiteInformationReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string reportType = Request.QueryString.Get("ReportType");

        if (reportType == "Excel")
        {
            ProcessExcelReport report = new ProcessExcelReport();

            report.Name = "CACFPSiteInformationReport";
            report.Response = Response;

            report.RenderReport();
        }
        else
        {
            ProcessReport report = new ProcessReport();
            
            report.Name = "CACFPSiteInformationReport";
            report.Response = Response;

            report.RenderReport();
        }        
    }
}
