using System;
using System.Data;
using System.Data.OleDb;
using GCFDGlobalsNamespace;
using aejw.Network;

public partial class InventoryOrderReport : System.Web.UI.Page
{
    private string m_SQL;

    protected void Page_Load(object sender, EventArgs e)
    {
        string filePath = @"C:\InventoryReports\InventoryReport.xls";
        //string filePath = @"\\gcfd-fpmain\site$\inventoryreport.xls";
        string item = "";
        string quantity = "";
        string reportStartDate = Request.QueryString.Get("ReportStartDate");
        string reportEndDate = Request.QueryString.Get("ReportEndDate");
        string reportType = Request.QueryString.Get("ReportType");

        NetworkDrive oNetDrive = new aejw.Network.NetworkDrive();
        
        try
        {
            //oNetDrive.LocalDrive = "K:";
            //oNetDrive.ShareName = @"\\10.99.1.36\shareCK$";
            //oNetDrive.Persistent = false;
            //oNetDrive.MapDrive("admin.em", "arcgis@123");

            //oNetDrive = null;
            OleDbConnection oconn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + "; Extended Properties=Excel 8.0");//OledbConnection and 

            //OleDbConnection oconn = new OleDbConnection(@"Driver=ODBCDriver;server=GCFD-INTRANET;providerName=inventory");//OledbConnection and 
            // connectionstring to connect to the Excel Sheet
            try
            {
                m_SQL = "DELETE FROM ItemOrder";
                GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                //After connecting to the Excel sheet here we are selecting the data 
                //using select statement from the Excel sheet
                OleDbCommand ocmd = new OleDbCommand("SELECT * FROM [InventoryReport$]", oconn);

                oconn.Open();  //Here [Sheet1$] is the name of the sheet 
                //in the Excel file where the data is present

                OleDbDataReader odr = ocmd.ExecuteReader();

                while (odr.Read())
                {
                    item = valid(odr, 1);//Here we are calling the valid method

                    quantity = valid(odr, 2);

                    //Here using this method we are inserting the data into the database
                    insertdataintosql(item, quantity);
                }

                oconn.Close();
            }
            catch (DataException ee)
            {
                //lblmsg.Text = ee.Message;
                //lblmsg.ForeColor = System.Drawing.Color.Red;
            }

            m_SQL = "DELETE FROM MealRecipe";
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

            m_SQL = "INSERT INTO MealRecipe (RecipeName, MealID) SELECT DISTINCT RecipeName, MealID FROM vwMealDelivery WHERE (DeliveryDate BETWEEN '" + reportStartDate + "' AND '" + reportEndDate + "') AND DeliveryTypeName <> 'Cancelled' AND MealTypeName IN('Hot','Breakfast') AND RecipeTypeID <> 11";
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

            if (reportType == "Excel")
            {
                ProcessExcelReport report = new ProcessExcelReport();

                report.Name = "InventoryOrderReport";
                report.Response = Response;

                report.RenderReport();
            }
            else
            {
                ProcessReport report = new ProcessReport();

                report.Name = "InventoryOrderReport";
                report.Response = Response;

                report.RenderReport();
            }

            oNetDrive.LocalDrive = "M:";
            oNetDrive.ShareName = @"\\10.99.1.36\shareCK$";
            oNetDrive.UnMapDrive();
        }
        catch (Exception err)
        {
            MessageBox.Show("Error: " + err.Message);
        }
    }

    protected string valid(OleDbDataReader myreader, int stval)//if any columns are 
    //found null then they are replaced by zero
    {
        object val = myreader[stval];

        if (val != DBNull.Value)
            return val.ToString();
        else
            return Convert.ToString(0);
    }

    public void insertdataintosql(string item, string quantity)
    {
        //inserting data into the Sql Server
        m_SQL = "INSERT INTO ItemOrder(Item,quantity) VALUES('" + item + "', '" + quantity + "')";
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
    }
}
