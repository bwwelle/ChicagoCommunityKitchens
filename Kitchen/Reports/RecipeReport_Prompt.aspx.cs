using System;
using Microsoft.Reporting.WebForms;

public partial class RecipeReport_Prompt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if(!IsPostBack)
		{
			ReportViewer1.ProcessingMode = ProcessingMode.Remote;
			ServerReport serverReport = ReportViewer1.ServerReport;

			serverReport.ReportServerUrl = new Uri("http://gcfd-intranet/reportserver");
			serverReport.ReportPath = "/CCKReports/RecipeReport_NamePrompt";

			ReportViewer1.ServerReport.Refresh();
		}
    }

	protected void BackToReportsButton_Click(object sender, EventArgs e)
	{
		Response.Redirect("CCKReports.aspx", false);
	}
}
