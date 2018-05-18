using System;
using System.Data;
using GCFDGlobalsNamespace;

public partial class SFSPDeliveryReport : System.Web.UI.Page
{
    private string m_SQL;

    protected void Page_Load(object sender, EventArgs e)
    {
        string reportStartDate = Request.QueryString.Get("ReportStartDate");
        string reportType = Request.QueryString.Get("ReportType");
        string bagCount = Request.QueryString.Get("BagCount");
        string sliceCount = Request.QueryString.Get("SliceCount");
        string loafCount = Request.QueryString.Get("LoafCount");
        string bunCount = Request.QueryString.Get("BunCount");

        if (reportType == "Excel")
        {
            ProcessExcelReport report = new ProcessExcelReport();

            report.StartDate = reportStartDate;
            report.BagCount = bagCount;
            report.SliceCount = sliceCount;
            report.LoafCount = loafCount;
            report.BunCount = bunCount;
            report.Name = "SFSPDeliveryReport_ForExcel";
            report.Response = Response;

            report.RenderReport();
        }
        else
        {
            ProcessReport report = new ProcessReport();

            report.StartDate = reportStartDate;
            report.BagCount = bagCount;
            report.SliceCount = sliceCount;
            report.LoafCount = loafCount;
            report.BunCount = bunCount;
            report.Name = "SFSPDeliveryReport";
            report.Response = Response;

            report.RenderReport();
        }        
    }
}
