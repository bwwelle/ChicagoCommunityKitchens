using System;

public partial class CACFPProgramsLabelReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string labelReportText = Request.QueryString.Get("LabelReportText");

        ProcessReport report = new ProcessReport();

        report.LabelText = labelReportText;
        report.Name = "CACFPProgramsLabelReport";
        report.Response = Response;

        report.RenderReport();     
    }
}
