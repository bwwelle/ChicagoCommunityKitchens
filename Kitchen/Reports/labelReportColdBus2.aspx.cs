using System;
using System.Data;
using GCFDGlobalsNamespace;
using Microsoft.Reporting.WebForms;

public partial class DeliveryReceiptReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string reportStartDate = Request.QueryString.Get("ReportStartDate");
      //  int numMealsPerBox = Request.QueryString.Get("txtMealsPerBox");
        string reportType = Request.QueryString.Get("ReportType");

        string bunCount = Request.QueryString.Get("BunCount");

       if (reportType == "Excel")
        {
            ProcessExcelReport report = new ProcessExcelReport();
            report.StartDate = reportStartDate;
            report.BunCount = bunCount;
            report.Name = "LabelLunchBusCold2";
            report.Response = Response;

            report.RenderReport();
        }
        else
        {
            ProcessReport report = new ProcessReport();
            report.StartDate = reportStartDate;
            report.BunCount = bunCount;
            report.Name = "LabelLunchBusCold2";
            report.Response = Response;

            report.RenderReport();
        }
    }
}
