using System;

public partial class AdjustedRecipesReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string reportStartDate = Request.QueryString.Get("ReportStartDate");

		ProcessReport report = new ProcessReport();

		report.StartDate = reportStartDate;
		report.Name = "AdjustedRecipeReport";
		report.Response = Response;

		report.RenderReport();
    }
}
