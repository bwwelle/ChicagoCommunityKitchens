using System;
using Microsoft.Reporting.WebForms;

public partial class RecipeReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		ReportViewer1.ShowPromptAreaButton = false;
    	ReportViewer1.PromptAreaCollapsed = true;
		ReportViewer1.ProcessingMode = ProcessingMode.Remote;
		ServerReport serverReport = ReportViewer1.ServerReport;

		serverReport.ReportServerUrl = new Uri("http://gcfd-intranet/reportserver");
		serverReport.ReportPath = "/CCKReports/RecipeReport";

		ReportViewer1.ServerReport.Refresh();
		ReportParameter parameter = new ReportParameter();
		parameter.Name = "RecipeID";
		parameter.Values.Add(Session["RecipeID"].ToString());

		ReportViewer1.ServerReport.SetParameters(
			new ReportParameter[] { parameter });  
    }

	protected void BackToRecipeButton_Click(object sender, EventArgs e)
	{
		Response.Redirect("RecipeDetails.aspx", false);	
	}
}
