using System;

public partial class SFSPProgramsLabelReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string labelReportText = Request.QueryString.Get("LabelReportText");
        
        ProcessReport report = new ProcessReport();

        report.LabelText = labelReportText;
        report.Name = "SFSPProgramsLabelReport";
        report.Response = Response;

        report.RenderReport();     
    }
}
