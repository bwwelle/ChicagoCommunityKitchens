using System;
using System.Data;
using GCFDGlobalsNamespace;

public partial class LabelReport : System.Web.UI.Page
{
	private string m_SQL;

    protected void Page_Load(object sender, EventArgs e)
    {
        string createdMeal;
        string reportStartDate = Request.QueryString.Get("ReportStartDate");
        string reportType = Request.QueryString.Get("ReportType");

        m_SQL = "DECLARE @CreatedMeal varchar(50) EXEC spInsertLabelInformationHot '" + reportStartDate + "', @CreatedMeal = @CreatedMeal OUTPUT SELECT @CreatedMeal as 'CreatedMeal'"; ;
        DataSet LabelCreation = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        createdMeal = GCFDGlobals.dbGetValue(LabelCreation.Tables[0].Rows[0], "CreatedMeal");

        if (createdMeal != "-1")
        {      
            if (reportType == "Excel")
            {
                ProcessExcelReport report = new ProcessExcelReport();

                report.ReportID = createdMeal;
                report.Name = "LabelReport";
                report.Response = Response;

                report.RenderReport();
            }
            else
            {
                ProcessReport report = new ProcessReport();

                report.ReportID = createdMeal;
                report.Name = "LabelReport";
                report.Response = Response;

                report.RenderReport();
            }

            
        }
        else
        {
            RegisterClientScriptBlock("",
                      "<script>{ alert('No Meal Created For This Date. Please Choose Another Date.');window.close();}</script>");
        }
    }
}
