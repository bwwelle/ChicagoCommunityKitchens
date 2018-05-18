using System;
using System.Data;
using System.Web.UI.WebControls;
using GCFDGlobalsNamespace;
using System.Web.UI;

public partial class CCKUserActionLog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FillUserActionsDataGridView();
    }

    public void FillUserActionsDataGridView()
    {
        DataSet userActionsDataSet =
           GCFDGlobals.m_GCFDPlannerDatabaseLibrary.GetUserActions();

        try
        {
            //if (userActionsDataSet.Tables[0].Rows.Count == 0)
            //{
            //    AddDummyDirectionsData();

            //    int columnCount = UserActionsGridView.Rows[0].Cells.Count;

            //    UserActionsGridView.Rows[0].Cells.Clear();

            //    UserActionsGridView.Rows[0].Cells.Add(new TableCell());

            //    UserActionsGridView.Rows[0].Cells[0].ColumnSpan = columnCount;
            //}
            //else
            //{
                UserActionsGridView.DataMember = userActionsDataSet.Tables[0].TableName;

                UserActionsGridView.DataSource = userActionsDataSet;

                UserActionsGridView.DataBind();
            //}
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving user actions information - " + ex.Message);
        }
    }

    //private void AddDummyDirectionsData()
    //{
    //    try
    //    {
    //        DataTable directionsDataTable = new DataTable("DummyDirectionsTable");

    //        directionsDataTable.Columns.Add("Directions");

    //        DataRow newRow = directionsDataTable.NewRow();

    //        directionsDataTable.Rows.Add(newRow);

    //        DirectionsGridView.DataSource = directionsDataTable;

    //        DirectionsGridView.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show("Error retrieving directions information - " + ex.Message);
    //    }
    //}
}
