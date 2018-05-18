using System;

public partial class InventoryReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ProcessExcelReport report = new ProcessExcelReport();

		report.Name = "InventoryReport";
		report.Response = Response;

		report.RenderReport();
    }
}
