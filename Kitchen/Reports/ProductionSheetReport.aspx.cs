using System;
using GCFDGlobalsNamespace;
using System.Data;

public partial class ProductionSheetReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
	{
        string reportType = Request.QueryString.Get("ReportType");
        string reportStartDate = Request.QueryString.Get("ReportStartDate");

        if (reportType == "Excel")
        {
            ProcessExcelReport report = new ProcessExcelReport();

            report.StartDate = reportStartDate;
            report.Name = "ProductionSheet";
            report.Response = Response;

            string m_SQL = "EXEC spProductionReportInformation '" + reportStartDate + "'";
            DataSet LabelCreation = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            report.RenderReport();
        }
        else
        {
            ProcessReport report = new ProcessReport();

            report.StartDate = reportStartDate;
            report.Name = "ProductionSheet";
            report.Response = Response;

            string m_SQL = "EXEC spProductionReportInformation '" + reportStartDate + "'";
            DataSet LabelCreation = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            report.RenderReport();
        }
	}
}
