using System;
using System.Data;
using GCFDGlobalsNamespace;

public partial class DeliveryReport : System.Web.UI.Page
{
    private string m_SQL;

    protected void Page_Load(object sender, EventArgs e)
    {
        string reportStartDate = Request.QueryString.Get("ReportStartDate");
        string reportType = Request.QueryString.Get("ReportType");

        if (reportType == "Excel")
        {
            ProcessExcelReport report = new ProcessExcelReport();

            report.StartDate = reportStartDate;
            report.Name = "DeliveryReport_ForExcel";
            report.Response = Response;

            report.RenderReport();
        }
        else
        {
            ProcessReport report = new ProcessReport();

            report.StartDate = reportStartDate;
            report.Name = "DeliveryReport";
            report.Response = Response;

            report.RenderReport();
        }
    }
}
