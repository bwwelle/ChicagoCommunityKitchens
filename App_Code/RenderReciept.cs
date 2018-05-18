using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

/// <summary>
/// Summary description for RenderReciept
/// </summary>
public class RenderReciept
{
	public string m_SQL;
	public string[] m_Headers = new string[] { "SiteName" };
	public static string m_ReportDataSetName = "DeliveryData";
	public static string m_ReportName = "Test";
	public static string m_ReportHeaderText = "Site Name";
	public static string m_ReportQueryText = "SELECT * from Site Where SiteID in (30, 34)";
	public static string m_Author = "Bradley Weller";
	private DataSet _data = new DataSet("Default");
	public string[] m_ServingWeekdaysArray;
	public string[] m_DeliveryWeekdaysArray;
	public string[] m_MealCountsArray;
	public string m_MealCount;
	public string m_BreadcaseCount;
	public string m_MilkCrateCount;
	public string m_SingleMilkCount;
	public string m_BreadLoafCount;
	public DataSet deliveryDataSet;
	public DataSet mealDataSet;
	public string m_ServingDay;
	public Boolean firstRecord = true;
	public string m_SiteId;
	public double m_Top;
	public static string DeliveryDate;

	public static MemoryStream GetRdlcStream(string m_DeliveryDataSQL)
	{
        byte[] rdlBytes = Encoding.UTF8.GetBytes(GetRdlcString(m_DeliveryDataSQL));

		return new MemoryStream(rdlBytes);
	}

	public DataSet Data
	{
		get
		{
			return _data;
		}
	}

    public static string GetRdlcString(string m_DeliveryDataSQL)
	{
		StringBuilder result = new StringBuilder();
		StringWriter writer = new StringWriter(result);
		XmlTextWriter _rdl = new XmlTextWriter(writer);
		_rdl.Formatting = Formatting.Indented;

		_rdl.Indentation = 3;
		_rdl.Namespaces = true;

		#region Header
		_rdl.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");

		_rdl.WriteStartElement("Report");
		_rdl.WriteAttributeString("xmlns", null,
								  "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
		//Writing namespace
		_rdl.WriteAttributeString("xmlns", "rd", null,
								  "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner");
		//Writing namespace

		_rdl.WriteElementString("InteractiveWidth", "11in");
		_rdl.WriteElementString("InteractiveHeight", "8.5in");
		_rdl.WriteElementString("Description", m_ReportName);
		_rdl.WriteElementString("rd:DrawGrid", "true");
		_rdl.WriteElementString("rd:SnapToGrid", "true");
		_rdl.WriteElementString("RightMargin", "0in");
		_rdl.WriteElementString("LeftMargin", "0in");
		_rdl.WriteElementString("PageWidth", "11in");
		_rdl.WriteElementString("PageHeight", "8.5in");
		_rdl.WriteElementString("Width", "11in");
		_rdl.WriteElementString("TopMargin", "0in");
		_rdl.WriteElementString("BottomMargin", "0in");
		_rdl.WriteStartElement("DataSources");
		_rdl.WriteStartElement("DataSource");
		_rdl.WriteAttributeString("Name", null, m_ReportDataSetName);
		_rdl.WriteStartElement("ConnectionProperties");
		_rdl.WriteElementString("DataProvider", "System.Data.SqlClient");
		_rdl.WriteElementString("ConnectString", "gcfdkitchenConnectionString");
		_rdl.WriteElementString("IntegratedSecurity", "true");
		_rdl.WriteEndElement(); //End ConnectionProperties
		_rdl.WriteEndElement(); //End DataSource
		_rdl.WriteEndElement(); //End DataSources
		_rdl.WriteStartElement("DataSets");
		_rdl.WriteStartElement("DataSet");
		_rdl.WriteAttributeString("Name", null, m_ReportDataSetName);
		_rdl.WriteStartElement("Fields");
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "SiteRoute");
		_rdl.WriteElementString("DataField", "SiteRoute");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "SiteName");
		_rdl.WriteElementString("DataField", "SiteName");
		_rdl.WriteEndElement(); // End Field
        _rdl.WriteStartElement("Field");
        _rdl.WriteAttributeString("Name", null, "Cambros");
        _rdl.WriteElementString("DataField", "Cambros");
        _rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "SiteAddress1");
		_rdl.WriteElementString("DataField", "SiteAddress1");
		_rdl.WriteEndElement(); // End Field
        _rdl.WriteStartElement("Field");
        _rdl.WriteAttributeString("Name", null, "Notes");
        _rdl.WriteElementString("DataField", "Notes");
        _rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "ExtendedAddress");
		_rdl.WriteElementString("DataField", "ExtendedAddress");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "ExtendedName");
		_rdl.WriteElementString("DataField", "ExtendedName");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "SiteServingDays");
		_rdl.WriteElementString("DataField", "SiteServingDays");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "SiteDeliveryDays");
		_rdl.WriteElementString("DataField", "SiteDeliveryDays");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "MealDate");
		_rdl.WriteElementString("DataField", "MealDate");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "MealCounts");
		_rdl.WriteElementString("DataField", "MealCounts");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "ProteinRecipeName");
		_rdl.WriteElementString("DataField", "ProteinRecipeName");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "ProteinServingDetail");
		_rdl.WriteElementString("DataField", "ProteinServingDetail");
		_rdl.WriteEndElement(); // End Field
        _rdl.WriteStartElement("Field");
        _rdl.WriteAttributeString("Name", null, "ProteinNotes");
        _rdl.WriteElementString("DataField", "ProteinNotes");
        _rdl.WriteEndElement(); // End Field
        _rdl.WriteStartElement("Field");
        _rdl.WriteAttributeString("Name", null, "VegetableNotes");
        _rdl.WriteElementString("DataField", "VegetableNotes");
        _rdl.WriteEndElement(); // End Field
        _rdl.WriteStartElement("Field");
        _rdl.WriteAttributeString("Name", null, "FruitNotes");
        _rdl.WriteElementString("DataField", "FruitNotes");
        _rdl.WriteEndElement(); // End Field
        _rdl.WriteStartElement("Field");
        _rdl.WriteAttributeString("Name", null, "Other1Notes");
        _rdl.WriteElementString("DataField", "Other1Notes");
        _rdl.WriteEndElement(); // End Field
        _rdl.WriteStartElement("Field");
        _rdl.WriteAttributeString("Name", null, "Other2Notes");
        _rdl.WriteElementString("DataField", "Other2Notes");
        _rdl.WriteEndElement(); // End Field
        _rdl.WriteStartElement("Field");
        _rdl.WriteAttributeString("Name", null, "Other3Notes");
        _rdl.WriteElementString("DataField", "Other3Notes");
        _rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "VegetableRecipeName");
		_rdl.WriteElementString("DataField", "VegetableRecipeName");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "VegetableServingDetail");
		_rdl.WriteElementString("DataField", "VegetableServingDetail");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "FruitRecipeName");
		_rdl.WriteElementString("DataField", "FruitRecipeName");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "FruitServingDetail");
		_rdl.WriteElementString("DataField", "FruitServingDetail");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "Other1RecipeName");
		_rdl.WriteElementString("DataField", "Other1RecipeName");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "Other1ServingDetail");
		_rdl.WriteElementString("DataField", "Other1ServingDetail");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "Other2RecipeName");
		_rdl.WriteElementString("DataField", "Other2RecipeName");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "Other2ServingDetail");
		_rdl.WriteElementString("DataField", "Other2ServingDetail");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "Other3RecipeName");
		_rdl.WriteElementString("DataField", "Other3RecipeName");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "Other3ServingDetail");
		_rdl.WriteElementString("DataField", "Other3ServingDetail");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "ProteinPanCount");
		_rdl.WriteElementString("DataField", "ProteinPanCount");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "VegetablePanCount");
		_rdl.WriteElementString("DataField", "VegetablePanCount");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "FruitPanCount");
		_rdl.WriteElementString("DataField", "FruitPanCount");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "Other1PanCount");
		_rdl.WriteElementString("DataField", "Other1PanCount");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "Other2PanCount");
		_rdl.WriteElementString("DataField", "Other2PanCount");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "Other3PanCount");
		_rdl.WriteElementString("DataField", "Other3PanCount");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "ServingDays");
		_rdl.WriteElementString("DataField", "ServingDays");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteStartElement("Field");
		_rdl.WriteAttributeString("Name", null, "BreakfastCounts");
		_rdl.WriteElementString("DataField", "BreakfastCounts");
		_rdl.WriteEndElement(); // End Field
		_rdl.WriteEndElement(); // End Fields
		_rdl.WriteStartElement("Query");
		_rdl.WriteElementString("DataSourceName", m_ReportDataSetName);
		_rdl.WriteElementString("CommandText", m_DeliveryDataSQL);
		_rdl.WriteEndElement(); // End Query
		_rdl.WriteEndElement(); // End DataSet
		_rdl.WriteEndElement(); // End DataSets
		_rdl.WriteStartElement("", "Body", null);
		_rdl.WriteElementString("Height", "0in");
		_rdl.WriteStartElement("", "ReportItems", null);
		_rdl.WriteStartElement("", "List", null);
		_rdl.WriteAttributeString("Name", null, "list1");
		_rdl.WriteElementString("DataSetName", m_ReportDataSetName);
		_rdl.WriteStartElement("", "ReportItems", null);

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "Title");
		_rdl.WriteElementString("rd:DefaultName", "Title");
		_rdl.WriteElementString("Width", "4in");
		_rdl.WriteElementString("Top", "0in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("FontWeight", "700");
		_rdl.WriteElementString("FontSize", "18pt");
		_rdl.WriteElementString("TextAlign", "Center");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "3.7in");
		_rdl.WriteElementString("Height", "0in");
		_rdl.WriteElementString("Value", "Chicago's Community Kitchen Kids Cafe Delivery Receipt");
		_rdl.WriteEndElement(); //End Textbox
		#endregion

		#region Delivery Notes
        _rdl.WriteStartElement("", "Line", null);
        _rdl.WriteAttributeString("Name", null, "line3");
        _rdl.WriteElementString("rd:DefaultName", "line3");
        _rdl.WriteElementString("Top", "7.65in");
        _rdl.WriteElementString("Width", "1.625in");
        _rdl.WriteStartElement("", "Style", null);
        _rdl.WriteStartElement("", "BorderStyle", null);
        _rdl.WriteElementString("Default", "Solid");
        _rdl.WriteEndElement(); //End BorderStyle
        _rdl.WriteEndElement(); //End Style
        _rdl.WriteElementString("ZIndex", "11");
        _rdl.WriteElementString("Left", "8.3in");
        _rdl.WriteElementString("Height", "0in");
        _rdl.WriteEndElement(); //End Line

        _rdl.WriteStartElement("", "Line", null);
        _rdl.WriteAttributeString("Name", null, "line2");
        _rdl.WriteElementString("rd:DefaultName", "line2");
        _rdl.WriteElementString("Top", "7.65in");
        _rdl.WriteElementString("Width", "1.625in");
        _rdl.WriteStartElement("", "Style", null);
        _rdl.WriteStartElement("", "BorderStyle", null);
        _rdl.WriteElementString("Default", "Solid");
        _rdl.WriteEndElement(); //End BorderStyle
        _rdl.WriteEndElement(); //End Style
        _rdl.WriteElementString("ZIndex", "10");
        _rdl.WriteElementString("Left", "4.65in");
        _rdl.WriteElementString("Height", "0in");
        _rdl.WriteEndElement(); //End Line

        _rdl.WriteStartElement("", "Line", null);
        _rdl.WriteAttributeString("Name", null, "line1");
        _rdl.WriteElementString("rd:DefaultName", "line1");
        _rdl.WriteElementString("Top", "7.65in");
        _rdl.WriteElementString("Width", "1.625in");
        _rdl.WriteStartElement("", "Style", null);
        _rdl.WriteStartElement("", "BorderStyle", null);
        _rdl.WriteElementString("Default", "Solid");
        _rdl.WriteEndElement(); //End BorderStyle
        _rdl.WriteEndElement(); //End Style
        _rdl.WriteElementString("ZIndex", "9");
        _rdl.WriteElementString("Left", "1.3in");
        _rdl.WriteElementString("Height", "0in");
        _rdl.WriteEndElement(); //End Line

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox3");
		_rdl.WriteElementString("rd:DefaultName", "textbox3");
		_rdl.WriteElementString("Top", "7.7in");
		_rdl.WriteElementString("Width", "1.5in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("FontWeight", "700");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "8");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "1.5in");
		_rdl.WriteElementString("Height", "0in");
		_rdl.WriteElementString("Value", "Driver's Signature");
		_rdl.WriteEndElement(); //End Textbox

        _rdl.WriteStartElement("", "Textbox", null);
        _rdl.WriteAttributeString("Name", null, "textbox2");
        _rdl.WriteElementString("rd:DefaultName", "textbox2");
        _rdl.WriteElementString("Top", "7.7in");
        _rdl.WriteElementString("Width", "1.5in");
        _rdl.WriteStartElement("", "Style", null);
        _rdl.WriteElementString("FontWeight", "700");
        _rdl.WriteElementString("PaddingLeft", "2pt");
        _rdl.WriteElementString("PaddingRight", "2pt");
        _rdl.WriteElementString("PaddingTop", "2pt");
        _rdl.WriteElementString("PaddingBottom", "2pt");
        _rdl.WriteEndElement(); //End Style
        _rdl.WriteElementString("ZIndex", "8");
        _rdl.WriteElementString("CanGrow", "true");
        _rdl.WriteElementString("Left", "4.75in");
        _rdl.WriteElementString("Height", "0in");
		_rdl.WriteElementString("Value", "Recipient's Signature");
		_rdl.WriteEndElement(); //End Textbox

        _rdl.WriteStartElement("", "Textbox", null);
        _rdl.WriteAttributeString("Name", null, "textbox1");
        _rdl.WriteElementString("rd:DefaultName", "textbox1");
        _rdl.WriteElementString("Top", "7.7in");
        _rdl.WriteElementString("Width", "1.5in");
        _rdl.WriteStartElement("", "Style", null);
        _rdl.WriteElementString("FontWeight", "700");
        _rdl.WriteElementString("PaddingLeft", "2pt");
        _rdl.WriteElementString("PaddingRight", "2pt");
        _rdl.WriteElementString("PaddingTop", "2pt");
        _rdl.WriteElementString("PaddingBottom", "2pt");
        _rdl.WriteEndElement(); //End Style
        _rdl.WriteElementString("ZIndex", "8");
        _rdl.WriteElementString("CanGrow", "true");
        _rdl.WriteElementString("Left", "8.5in");
        _rdl.WriteElementString("Height", "0in");
		_rdl.WriteElementString("Value", "Time of Delivery");
		_rdl.WriteEndElement(); //End Textbox

        _rdl.WriteStartElement("", "Textbox", null);
        _rdl.WriteAttributeString("Name", null, "textbox35");
        _rdl.WriteElementString("rd:DefaultName", "textbox35");
        _rdl.WriteElementString("Top", "6.25in");
        _rdl.WriteElementString("Width", "6in");
        _rdl.WriteStartElement("", "Style", null);
        _rdl.WriteElementString("PaddingLeft", "2pt");
        _rdl.WriteElementString("PaddingRight", "2pt");
        _rdl.WriteElementString("PaddingTop", "2pt");
        _rdl.WriteElementString("PaddingBottom", "2pt");
        _rdl.WriteEndElement(); //End Style
        _rdl.WriteElementString("ZIndex", "37");
        _rdl.WriteElementString("CanGrow", "true");
        _rdl.WriteElementString("Left", "2.75in");
        _rdl.WriteElementString("Height", "2in");
        _rdl.WriteElementString("Value",
                                "=Fields!Notes.Value");
        _rdl.WriteEndElement(); //End Textbox

        _rdl.WriteStartElement("", "Textbox", null);
        _rdl.WriteAttributeString("Name", null, "textbox34");
        _rdl.WriteElementString("rd:DefaultName", "textbox34");
        _rdl.WriteElementString("Top", "6.25in");
        _rdl.WriteElementString("Width", "4in");
        _rdl.WriteStartElement("", "Style", null);
        _rdl.WriteElementString("FontWeight", "700");
        _rdl.WriteElementString("PaddingLeft", "2pt");
        _rdl.WriteElementString("PaddingRight", "2pt");
        _rdl.WriteElementString("PaddingTop", "2pt");
        _rdl.WriteElementString("PaddingBottom", "2pt");
        _rdl.WriteEndElement(); //End Style
        _rdl.WriteElementString("ZIndex", "36");
        _rdl.WriteElementString("CanGrow", "true");
        _rdl.WriteElementString("Left", "0.375in");
        _rdl.WriteElementString("Height", "0.25in");
        _rdl.WriteElementString("Value", "Important Food Safety Information:");
        _rdl.WriteEndElement(); //End Textbox

        _rdl.WriteStartElement("", "Textbox", null);
        _rdl.WriteAttributeString("Name", null, "cambros1");
        _rdl.WriteElementString("rd:DefaultName", "cambros1");
        _rdl.WriteElementString("Top", "6in");
        _rdl.WriteElementString("Width", "0.9in");
        _rdl.WriteStartElement("", "Style", null);
        _rdl.WriteElementString("TextAlign", "Center");
        _rdl.WriteEndElement(); //End Style
        _rdl.WriteElementString("ZIndex", "37");
        _rdl.WriteElementString("CanGrow", "true");
        _rdl.WriteElementString("Left", "8.85in");
        _rdl.WriteElementString("Height", "0.75in");
        _rdl.WriteElementString("Value",
                                "Cambros left overnight");
        _rdl.WriteEndElement(); //End Textbox

        _rdl.WriteStartElement("", "Rectangle", null);
        _rdl.WriteAttributeString("Name", null, "rectangle1");
        _rdl.WriteElementString("Left", "9in");
        _rdl.WriteElementString("Top", "6.325in");
        _rdl.WriteElementString("Width", "0.625in");
        _rdl.WriteElementString("Height", "0.35in");
        _rdl.WriteStartElement("", "Style", null);
        _rdl.WriteStartElement("", "BorderStyle", null);
        _rdl.WriteElementString("Default", "Solid");
        _rdl.WriteEndElement(); //End BorderStyle
        _rdl.WriteEndElement();	//End Style
        _rdl.WriteEndElement(); //End Rectangle

        _rdl.WriteStartElement("", "Textbox", null);
        _rdl.WriteAttributeString("Name", null, "cambros2");
        _rdl.WriteElementString("rd:DefaultName", "cambros2");
        _rdl.WriteElementString("Top", "6in");
        _rdl.WriteElementString("Width", "0.75in");
        _rdl.WriteStartElement("", "Style", null);
        _rdl.WriteElementString("TextAlign", "Center");
        _rdl.WriteEndElement(); //End Style
        _rdl.WriteElementString("ZIndex", "37");
        _rdl.WriteElementString("CanGrow", "true");
        _rdl.WriteElementString("Left", "9.95in");
        _rdl.WriteElementString("Height", "0.25in");
        _rdl.WriteElementString("Value",
                                "Cambros picked up");
        _rdl.WriteEndElement(); //End Textbox

        _rdl.WriteStartElement("", "Rectangle", null);
        _rdl.WriteAttributeString("Name", null, "rectangle2");
        _rdl.WriteElementString("Left", "10in");
        _rdl.WriteElementString("Top", "6.325in");
        _rdl.WriteElementString("Width", "0.625in");
        _rdl.WriteElementString("Height", "0.35in");
        _rdl.WriteStartElement("", "Style", null);
        _rdl.WriteStartElement("", "BorderStyle", null);
        _rdl.WriteElementString("Default", "Solid");
        _rdl.WriteEndElement(); //End BorderStyle
        _rdl.WriteEndElement();	//End Style
        _rdl.WriteEndElement(); //End Rectangle

        _rdl.WriteStartElement("", "Textbox", null);
        _rdl.WriteAttributeString("Name", null, "clean");
        _rdl.WriteElementString("rd:DefaultName", "clean");
        _rdl.WriteElementString("Top", "6.7in");
        _rdl.WriteElementString("Width", "0.75in");
        _rdl.WriteStartElement("", "Style", null);
        _rdl.WriteElementString("TextAlign", "Center");
        _rdl.WriteEndElement(); //End Style
        _rdl.WriteElementString("ZIndex", "37");
        _rdl.WriteElementString("CanGrow", "true");
        _rdl.WriteElementString("Left", "9in");
        _rdl.WriteElementString("Height", "0in");
        _rdl.WriteElementString("Value",
                                "Clean?");
        _rdl.WriteEndElement(); //End Textbox

        _rdl.WriteStartElement("", "Rectangle", null);
        _rdl.WriteAttributeString("Name", null, "rectangle3");
        _rdl.WriteElementString("Left", "9.76in");
        _rdl.WriteElementString("Top", "6.8in");
        _rdl.WriteElementString("Width", "0.1in");
        _rdl.WriteElementString("Height", "0.1in");
        _rdl.WriteStartElement("", "Style", null);
        _rdl.WriteStartElement("", "BorderStyle", null);
        _rdl.WriteElementString("Default", "Solid");
        _rdl.WriteEndElement(); //End BorderStyle
        _rdl.WriteEndElement();	//End Style
        _rdl.WriteEndElement(); //End Rectangle

        _rdl.WriteStartElement("", "Textbox", null);
        _rdl.WriteAttributeString("Name", null, "cleany");
        _rdl.WriteElementString("rd:DefaultName", "cleany");
        _rdl.WriteElementString("Top", "6.77in");
        _rdl.WriteElementString("Width", "0.4in");
        _rdl.WriteStartElement("", "Style", null);
        _rdl.WriteElementString("TextAlign", "Center");
        _rdl.WriteEndElement(); //End Style
        _rdl.WriteElementString("ZIndex", "37");
        _rdl.WriteElementString("CanGrow", "true");
        _rdl.WriteElementString("Left", "9.75in");
        _rdl.WriteElementString("Height", "0in");
        _rdl.WriteElementString("Value",
                                "Y");
        _rdl.WriteEndElement(); //End Textbox

        _rdl.WriteStartElement("", "Rectangle", null);
        _rdl.WriteAttributeString("Name", null, "rectangle4");
        _rdl.WriteElementString("Left", "10.1in");
        _rdl.WriteElementString("Top", "6.8in");
        _rdl.WriteElementString("Width", "0.1in");
        _rdl.WriteElementString("Height", "0.1in");
        _rdl.WriteStartElement("", "Style", null);
        _rdl.WriteStartElement("", "BorderStyle", null);
        _rdl.WriteElementString("Default", "Solid");
        _rdl.WriteEndElement(); //End BorderStyle
        _rdl.WriteEndElement();	//End Style
        _rdl.WriteEndElement(); //End Rectangle

        _rdl.WriteStartElement("", "Textbox", null);
        _rdl.WriteAttributeString("Name", null, "cleann");
        _rdl.WriteElementString("rd:DefaultName", "cleann");
        _rdl.WriteElementString("Top", "6.77in");
        _rdl.WriteElementString("Width", "0.4in");
        _rdl.WriteStartElement("", "Style", null);
        _rdl.WriteElementString("TextAlign", "Center");
        _rdl.WriteEndElement(); //End Style
        _rdl.WriteElementString("ZIndex", "37");
        _rdl.WriteElementString("CanGrow", "true");
        _rdl.WriteElementString("Left", "10.1in");
        _rdl.WriteElementString("Height", "0in");
        _rdl.WriteElementString("Value",
                                "N");
        _rdl.WriteEndElement(); //End Textbox
		#endregion

		#region Milk Information
		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox100");
		_rdl.WriteElementString("rd:DefaultName", "textbox100");
		_rdl.WriteElementString("Top", "3.75in");
		_rdl.WriteElementString("Width", "1.875in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "35");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "8.5in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox121");
		_rdl.WriteElementString("rd:DefaultName", "textbox121");
		_rdl.WriteElementString("Top", "3.75in");
		_rdl.WriteElementString("Width", "2.25in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "34");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "6in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "1 carton");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox311");
		_rdl.WriteElementString("rd:DefaultName", "textbox311");
		_rdl.WriteElementString("Top", "3.75in");
		_rdl.WriteElementString("Width", "4in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "33");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "2in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "Milk");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox301");
		_rdl.WriteElementString("rd:DefaultName", "textbox301");
		_rdl.WriteElementString("Top", "3.75in");
		_rdl.WriteElementString("Width", "1in");
		_rdl.WriteStartElement("", "Style", null);  
		_rdl.WriteElementString("TextAlign", "Left");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "32");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.375in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!MealCounts.Value");
		_rdl.WriteEndElement(); //End Textbox
		#endregion

		#region Breakfast Milk Information
		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "breakfastmilknotes");
		_rdl.WriteElementString("rd:DefaultName", "BreakfastMilknotes");
		_rdl.WriteElementString("Top", "3.95in");
		_rdl.WriteElementString("Width", "1.875in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "35");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "8.5in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "breakfastmilkservingamount");
		_rdl.WriteElementString("rd:DefaultName", "breakfastmilkservingamount");
		_rdl.WriteElementString("Top", "3.95in");
		_rdl.WriteElementString("Width", "2.25in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "34");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "6in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "1 carton");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "breakfastmilktype");
		_rdl.WriteElementString("rd:DefaultName", "breakfastmilktype");
		_rdl.WriteElementString("Top", "3.95in");
		_rdl.WriteElementString("Width", "4in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "33");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "2in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "Breakfast Milk");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "breakfastmilkquantity");
		_rdl.WriteElementString("rd:DefaultName", "breakfastmilkquantity");
		_rdl.WriteElementString("Top", "3.95in");
		_rdl.WriteElementString("Width", "1in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("TextAlign", "Left");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "32");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.375in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "0");
		_rdl.WriteEndElement(); //End Textbox
		#endregion

		#region	Bread Information
		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "BreadNotes");
		_rdl.WriteElementString("rd:DefaultName", "BreadNotes");
		_rdl.WriteElementString("Top", "4.2in");
		_rdl.WriteElementString("Width", "1.875in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "35");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "8.5in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "BreadServingDescription");
		_rdl.WriteElementString("rd:DefaultName", "BreadServingDescription");
		_rdl.WriteElementString("Top", "4.2in");
		_rdl.WriteElementString("Width", "2.25in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "34");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "6in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "1 Slice");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "BreadName");
		_rdl.WriteElementString("rd:DefaultName", "BreadName");
		_rdl.WriteElementString("Top", "4.2in");
		_rdl.WriteElementString("Width", "4in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "33");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "2in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "Bread");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "BreadQuantity");
		_rdl.WriteElementString("rd:DefaultName", "BreadQuantity");
		_rdl.WriteElementString("Top", "4.2in");
		_rdl.WriteElementString("Width", "1in");
		_rdl.WriteStartElement("", "Style", null); 
		_rdl.WriteElementString("TextAlign", "Left");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "32");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.375in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", @"=Fields!MealCounts.Value");
		_rdl.WriteEndElement(); //End Textbox
		#endregion

		#region	Breakfast Information
		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "BreakfastNotes");
		_rdl.WriteElementString("rd:DefaultName", "BreakfastNotes");
		_rdl.WriteElementString("Top", "4.45in");
		_rdl.WriteElementString("Width", "1.875in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "35");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "8.5in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "BreakfastServingDescription");
		_rdl.WriteElementString("rd:DefaultName", "BreakfastServingDescription");
		_rdl.WriteElementString("Top", "4.45in");
		_rdl.WriteElementString("Width", "2.25in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "34");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "6in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "1 box");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "BreakfastName");
		_rdl.WriteElementString("rd:DefaultName", "BreakfastName");
		_rdl.WriteElementString("Top", "4.45in");
		_rdl.WriteElementString("Width", "4in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "33");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "2in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "Breakfast Box");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "BreakfastQuantity");
		_rdl.WriteElementString("rd:DefaultName", "BreakfastQuantity");
		_rdl.WriteElementString("Top", "4.45in");
		_rdl.WriteElementString("Width", "1in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("TextAlign", "Left");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "32");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.375in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "0");
		_rdl.WriteEndElement(); //End Textbox
		#endregion

		#region	Protein Information
		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "ProteinNotes");
		_rdl.WriteElementString("rd:DefaultName", "ProteinNotes");
		_rdl.WriteElementString("Top", "4.7in");
		_rdl.WriteElementString("Width", "1.875in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "35");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "8.5in");
		_rdl.WriteElementString("Height", "0.25in");
        _rdl.WriteElementString("Value", "=Fields!ProteinNotes.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "ProteinServingDescription");
		_rdl.WriteElementString("rd:DefaultName", "ProteinServingDescription");
		_rdl.WriteElementString("Top", "4.7in");
		_rdl.WriteElementString("Width", "2.25in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "34");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "6in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!ProteinServingDetail.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "ProteinRecipeName");
		_rdl.WriteElementString("rd:DefaultName", "ProteinRecipeName");
		_rdl.WriteElementString("Top", "4.7in");
		_rdl.WriteElementString("Width", "4in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "33");
		//_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "2in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!ProteinRecipeName.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "ProteinQuantity");
		_rdl.WriteElementString("rd:DefaultName", "ProteinQuantity");
		_rdl.WriteElementString("Top", "4.7in");
		_rdl.WriteElementString("Width", "2in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("TextAlign", "Left");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "32");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.375in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!ProteinPanCount.Value");
		_rdl.WriteEndElement(); //End Textbox
		#endregion

		#region	Vegetable Information
		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "VegetableNotes");
		_rdl.WriteElementString("rd:DefaultName", "VegetableNotes");
		_rdl.WriteElementString("Top", "4.95in");
		_rdl.WriteElementString("Width", "1.875in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "35");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "8.5in");
		_rdl.WriteElementString("Height", "2.0in");
        _rdl.WriteElementString("Value", "=Fields!VegetableNotes.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "VegetableServingDescription");
		_rdl.WriteElementString("rd:DefaultName", "VegetableServingDescription");
		_rdl.WriteElementString("Top", "4.95in");
		_rdl.WriteElementString("Width", "2.25in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "34");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "6in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!VegetableServingDetail.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "VegetableRecipeName");
		_rdl.WriteElementString("rd:DefaultName", "VegetableRecipeName");
		_rdl.WriteElementString("Top", "4.95in");
		_rdl.WriteElementString("Width", "4in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "33");
		//_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "2in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!VegetableRecipeName.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "VegetableQuantity");
		_rdl.WriteElementString("rd:DefaultName", "VegetableQuantity");
		_rdl.WriteElementString("Top", "4.95in");
		_rdl.WriteElementString("Width", "2in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("TextAlign", "Left");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "32");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.375in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!VegetablePanCount.Value");
		_rdl.WriteEndElement(); //End Textbox
		#endregion

		#region	Fruit Information
		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "FruitNotes");
		_rdl.WriteElementString("rd:DefaultName", "FruitNotes");
		_rdl.WriteElementString("Top", "5.2in");
		_rdl.WriteElementString("Width", "1.875in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "35");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "8.5in");
		_rdl.WriteElementString("Height", "0.25in");
        _rdl.WriteElementString("Value", "=Fields!FruitNotes.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "FruitServingDescription");
		_rdl.WriteElementString("rd:DefaultName", "FruitServingDescription");
		_rdl.WriteElementString("Top", "5.2in");
		_rdl.WriteElementString("Width", "2.25in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "34");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "6in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!FruitServingDetail.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "FruitRecipeName");
		_rdl.WriteElementString("rd:DefaultName", "FruitRecipeName");
		_rdl.WriteElementString("Top", "5.2in");
		_rdl.WriteElementString("Width", "4in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "33");
		//_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "2in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!FruitRecipeName.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "FruitQuantity");
		_rdl.WriteElementString("rd:DefaultName", "FruitQuantity");
		_rdl.WriteElementString("Top", "5.2in");
		_rdl.WriteElementString("Width", "2in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("TextAlign", "Left");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "32");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.375in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!FruitPanCount.Value");
		_rdl.WriteEndElement(); //End Textbox
		#endregion

		#region	Other1 Information
		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "Other1Notes");
		_rdl.WriteElementString("rd:DefaultName", "Other1Notes");
		_rdl.WriteElementString("Top", "5.45in");
		_rdl.WriteElementString("Width", "1.875in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "35");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "8.5in");
		_rdl.WriteElementString("Height", "0.25in");
        _rdl.WriteElementString("Value", "=Fields!Other1Notes.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "Other1ServingDescription");
		_rdl.WriteElementString("rd:DefaultName", "Other1ServingDescription");
		_rdl.WriteElementString("Top", "5.45in");
		_rdl.WriteElementString("Width", "2.25in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "34");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "6in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!Other1ServingDetail.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "Other1RecipeName");
		_rdl.WriteElementString("rd:DefaultName", "Other1RecipeName");
		_rdl.WriteElementString("Top", "5.45in");
		_rdl.WriteElementString("Width", "4in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "33");
		//_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "2in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!Other1RecipeName.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "Other1Quantity");
		_rdl.WriteElementString("rd:DefaultName", "Other1Quantity");
		_rdl.WriteElementString("Top", "5.45in");
		_rdl.WriteElementString("Width", "2in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("TextAlign", "Left");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "32");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.375in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!Other1PanCount.Value");
		_rdl.WriteEndElement(); //End Textbox
		#endregion

		#region	Other2 Information
		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "Other2Notes");
		_rdl.WriteElementString("rd:DefaultName", "Other2Notes");
		_rdl.WriteElementString("Top", "5.7in");
		_rdl.WriteElementString("Width", "1.875in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "35");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "8.5in");
		_rdl.WriteElementString("Height", "0.25in");
        _rdl.WriteElementString("Value", "=Fields!Other2Notes.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "Other2ServingDescription");
		_rdl.WriteElementString("rd:DefaultName", "Other2ServingDescription");
		_rdl.WriteElementString("Top", "5.7in");
		_rdl.WriteElementString("Width", "2.25in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "34");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "6in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!Other2ServingDetail.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "Other2RecipeName");
		_rdl.WriteElementString("rd:DefaultName", "Other2RecipeName");
		_rdl.WriteElementString("Top", "5.7in");
		_rdl.WriteElementString("Width", "4in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "33");
		//_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "2in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!Other2RecipeName.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "Other2Quantity");
		_rdl.WriteElementString("rd:DefaultName", "Other2Quantity");
		_rdl.WriteElementString("Top", "5.7in");
		_rdl.WriteElementString("Width", "2in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("TextAlign", "Left");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "32");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.375in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!Other2PanCount.Value");
		_rdl.WriteEndElement(); //End Textbox
		#endregion

		#region	Other3 Information
		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "Other3Notes");
		_rdl.WriteElementString("rd:DefaultName", "Other3Notes");
		_rdl.WriteElementString("Top", "5.95in");
		_rdl.WriteElementString("Width", "1.875in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "35");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "8.5in");
		_rdl.WriteElementString("Height", "0.25in");
        _rdl.WriteElementString("Value", "=Fields!Other3Notes.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "Other3ServingDescription");
		_rdl.WriteElementString("rd:DefaultName", "Other3ServingDescription");
		_rdl.WriteElementString("Top", "5.95in");
		_rdl.WriteElementString("Width", "2.25in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "34");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "6in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!Other3ServingDetail.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "Other3RecipeName");
		_rdl.WriteElementString("rd:DefaultName", "Other3RecipeName");
		_rdl.WriteElementString("Top", "5.95in");
		_rdl.WriteElementString("Width", "4in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "33");
		//_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "2in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!Other3RecipeName.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "Other3Quantity");
		_rdl.WriteElementString("rd:DefaultName", "Other3Quantity");
		_rdl.WriteElementString("Top", "5.95in");
		_rdl.WriteElementString("Width", "2in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("TextAlign", "Left");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "32");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.375in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!Other3PanCount.Value");
		_rdl.WriteEndElement(); //End Textbox
		#endregion

		#region Meal Columns Header Information
		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox29");
		_rdl.WriteElementString("rd:DefaultName", "textbox29");
		_rdl.WriteElementString("Top", "3.5in");
		_rdl.WriteElementString("Width", "2in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("FontWeight", "700");
		_rdl.WriteElementString("TextDecoration", "Underline");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "31");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "8.5in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "NOTES");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox28");
		_rdl.WriteElementString("rd:DefaultName", "textbox28");
		_rdl.WriteElementString("Top", "3.5in");
		_rdl.WriteElementString("Width", "4in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("FontWeight", "700");
		_rdl.WriteElementString("TextDecoration", "Underline");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "30");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "6in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "ITEM SERVING DESCRIPTION");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox27");
		_rdl.WriteElementString("rd:DefaultName", "textbox27");
		_rdl.WriteElementString("Top", "3.5in");
		_rdl.WriteElementString("Width", "0.75in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("FontWeight", "700");
		_rdl.WriteElementString("TextDecoration", "Underline");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "29");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "2in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "ITEM");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox26");
		_rdl.WriteElementString("rd:DefaultName", "textbox26");
		_rdl.WriteElementString("Top", "3.5in");
		_rdl.WriteElementString("Width", "1in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("FontWeight", "700");
		_rdl.WriteElementString("TextDecoration", "Underline");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "28");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.375in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "QUANTITY");
		_rdl.WriteEndElement(); //End Textbox
		#endregion

		#region Site Information
		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox23");
		_rdl.WriteElementString("rd:DefaultName", "textbox23");
		_rdl.WriteElementString("Top", "3.125in");
		_rdl.WriteElementString("Width", "4in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "25");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "1.375in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!ServingDays.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox22");
		_rdl.WriteElementString("rd:DefaultName", "textbox22");
		_rdl.WriteElementString("Top", "3.125in");
		_rdl.WriteElementString("Width", "1in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("FontWeight", "700");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "24");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.375in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "Serving Day:");
		_rdl.WriteEndElement(); //End Textbox

        _rdl.WriteStartElement("", "Textbox", null);
        _rdl.WriteAttributeString("Name", null, "cambrostextbox");
        _rdl.WriteElementString("rd:DefaultName", "cambrostextbox");
        _rdl.WriteElementString("Top", "3.375in");
        _rdl.WriteElementString("Width", "1in");
        _rdl.WriteStartElement("", "Style", null);
        _rdl.WriteElementString("PaddingLeft", "2pt");
        _rdl.WriteElementString("PaddingRight", "2pt");
        _rdl.WriteElementString("PaddingTop", "2pt");
        _rdl.WriteElementString("PaddingBottom", "2pt");
        _rdl.WriteEndElement(); //End Style
        _rdl.WriteElementString("ZIndex", "25");
        _rdl.WriteElementString("CanGrow", "true");
        _rdl.WriteElementString("Left", "1.7in");
        _rdl.WriteElementString("Height", "0.25in");
        _rdl.WriteElementString("Value", "=Fields!Cambros.Value");
        _rdl.WriteEndElement(); //End Textbox

        _rdl.WriteStartElement("", "Textbox", null);
        _rdl.WriteAttributeString("Name", null, "cambroslabel");
        _rdl.WriteElementString("rd:DefaultName", "cambroslabel");
        _rdl.WriteElementString("Top", "3.375in");
        _rdl.WriteElementString("Width", "2.5in");
        _rdl.WriteStartElement("", "Style", null);
        _rdl.WriteElementString("FontWeight", "700");
        _rdl.WriteElementString("PaddingLeft", "2pt");
        _rdl.WriteElementString("PaddingRight", "2pt");
        _rdl.WriteElementString("PaddingTop", "2pt");
        _rdl.WriteElementString("PaddingBottom", "2pt");
        _rdl.WriteEndElement(); //End Style
        _rdl.WriteElementString("ZIndex", "24");
        _rdl.WriteElementString("CanGrow", "true");
        _rdl.WriteElementString("Left", "0.375in");
        _rdl.WriteElementString("Height", "0.25in");
        _rdl.WriteElementString("Value", "# of Cambros To Be Delivered:");
        _rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox21");
		_rdl.WriteElementString("rd:DefaultName", "textbox21");
		_rdl.WriteElementString("Top", "2.875in");
		_rdl.WriteElementString("Width", "2in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("TextAlign", "Left");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "23");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "2.625in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!MealCounts.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox20");
		_rdl.WriteElementString("rd:DefaultName", "textbox20");
		_rdl.WriteElementString("Top", "2.875in");
		_rdl.WriteElementString("Width", "2.25in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("FontWeight", "700");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "22");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.375in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "Total Number of Meals per Day:");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox19");
		_rdl.WriteElementString("rd:DefaultName", "textbox19");
		_rdl.WriteElementString("Top", "2.375in");
		_rdl.WriteElementString("Width", "3.125in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "21");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.875in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!SiteRoute.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox18");
		_rdl.WriteElementString("rd:DefaultName", "textbox18");
		_rdl.WriteElementString("Top", "2.375in");
		_rdl.WriteElementString("Width", "3.125in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("FontWeight", "700");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "20");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.375in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "Route:");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox17");
		_rdl.WriteElementString("rd:DefaultName", "textbox17");
		_rdl.WriteElementString("Top", "2.125in");
		_rdl.WriteElementString("Width", "3.125in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "19");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "1in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=Fields!ExtendedName.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox16");
		_rdl.WriteElementString("rd:DefaultName", "textbox16");
		_rdl.WriteElementString("Top", "2.125in");
		_rdl.WriteElementString("Width", "0.625in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("FontWeight", "700");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "18");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.375in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "Contact:");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox15");
		_rdl.WriteElementString("rd:DefaultName", "textbox15");
		_rdl.WriteElementString("Top", "1.875in");
		_rdl.WriteElementString("Width", "3.875in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "0pt");
		_rdl.WriteElementString("PaddingRight", "0pt");
		_rdl.WriteElementString("PaddingTop", "0pt");
		_rdl.WriteElementString("PaddingBottom", "0pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "17");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.375in");
		_rdl.WriteElementString("Height", "0in");
		_rdl.WriteElementString("Value", "=Fields!ExtendedAddress.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox14");
		_rdl.WriteElementString("rd:DefaultName", "textbox14");
		_rdl.WriteElementString("Top", "1.625in");
		_rdl.WriteElementString("Width", "4.25in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "0pt");
		_rdl.WriteElementString("PaddingRight", "0pt");
		_rdl.WriteElementString("PaddingTop", "0pt");
		_rdl.WriteElementString("PaddingBottom", "0pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "16");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.375in");
		_rdl.WriteElementString("Height", "0in");
		_rdl.WriteElementString("Value", "=Fields!SiteAddress1.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox13");
		_rdl.WriteElementString("rd:DefaultName", "textbox13");
		_rdl.WriteElementString("Top", "1.375in");
		_rdl.WriteElementString("Width", "5.50in");
		_rdl.WriteStartElement("", "Style", null);      
		_rdl.WriteElementString("FontSize", "15pt");
		_rdl.WriteElementString("PaddingLeft", "0pt");
		_rdl.WriteElementString("PaddingRight", "0pt");
		_rdl.WriteElementString("PaddingTop", "0pt");
		_rdl.WriteElementString("PaddingBottom", "0pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "15");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.375in");
		_rdl.WriteElementString("Height", "0in");
		_rdl.WriteElementString("Value", "=Fields!SiteName.Value");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox12");
		_rdl.WriteElementString("rd:DefaultName", "textbox12");
		_rdl.WriteElementString("Top", "1.125in");
		_rdl.WriteElementString("Width", "0.875in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("FontWeight", "700");
		_rdl.WriteElementString("PaddingLeft", "0pt");
		_rdl.WriteElementString("PaddingRight", "0pt");
		_rdl.WriteElementString("PaddingTop", "0pt");
		_rdl.WriteElementString("PaddingBottom", "0pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "14");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.375in");
		_rdl.WriteElementString("Height", "0in");
		_rdl.WriteElementString("Value", "SHIP TO:");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox11");
		_rdl.WriteElementString("rd:DefaultName", "textbox11");
		_rdl.WriteElementString("Top", "0.75in");
		_rdl.WriteElementString("Width", "3.5in");
		_rdl.WriteStartElement("", "Style", null);  
		_rdl.WriteElementString("FontSize", "12pt");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "13");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "1.6in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "=FormatDateTime(Fields!MealDate.Value,1)");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox10");
		_rdl.WriteElementString("rd:DefaultName", "textbox10");
		_rdl.WriteElementString("Top", "0.75in");
		_rdl.WriteElementString("Width", "2in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("FontWeight", "700");
		_rdl.WriteElementString("FontSize", "12pt");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "12");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "0.375in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "Delivery Date:");
		_rdl.WriteEndElement(); //End Textbox  

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox9");
		_rdl.WriteElementString("rd:DefaultName", "textbox9");
		_rdl.WriteElementString("Top", "2.375in");
		_rdl.WriteElementString("Width", "2.5in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("FontWeight", "700");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "5");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "7.25in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "Food Questions-Phone:");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox8");
		_rdl.WriteElementString("rd:DefaultName", "textbox8");
		_rdl.WriteElementString("Top", "2.125in");
		_rdl.WriteElementString("Width", "2.5in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("FontWeight", "700");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "4");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "7.25in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "Food Questions-Contact:");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox25");
		_rdl.WriteElementString("rd:DefaultName", "textbox25");
		_rdl.WriteElementString("Top", "2.375in");
		_rdl.WriteElementString("Width", "1.125in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "27");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "9in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "773-843-2601");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox24");
		_rdl.WriteElementString("rd:DefaultName", "textbox24");
		_rdl.WriteElementString("Top", "2.125in");
		_rdl.WriteElementString("Width", "1.125in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "26");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "9in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "Chef Anna Jones");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "schedulingnumberlabel");
		_rdl.WriteElementString("rd:DefaultName", "schedulingnumberlabel");
		_rdl.WriteElementString("Top", "3.125in");
		_rdl.WriteElementString("Width", "2.5in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("FontWeight", "700");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "5");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "7.25in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "Delivery Questions-Phone:");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "schedulingcontactlabel");
		_rdl.WriteElementString("rd:DefaultName", "schedulingcontactlabel");
		_rdl.WriteElementString("Top", "2.875in");
		_rdl.WriteElementString("Width", "2.5in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("FontWeight", "700");
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "4");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "7.25in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "Delivery Questions-Contact:");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "schedulingnumber");
		_rdl.WriteElementString("rd:DefaultName", "schedulingnumber");
		_rdl.WriteElementString("Top", "3.125in");
		_rdl.WriteElementString("Width", "1.125in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "27");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "9.2in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "773-843-2608");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "schedulingcontact");
		_rdl.WriteElementString("rd:DefaultName", "schedulingcontact");
		_rdl.WriteElementString("Top", "2.875in");
		_rdl.WriteElementString("Width", "1.125in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "2pt");
		_rdl.WriteElementString("PaddingRight", "2pt");
		_rdl.WriteElementString("PaddingTop", "2pt");
		_rdl.WriteElementString("PaddingBottom", "2pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "26");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "9.2in");
		_rdl.WriteElementString("Height", "0.25in");
		_rdl.WriteElementString("Value", "Valeri Chow");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox7");
		_rdl.WriteElementString("rd:DefaultName", "textbox7");
		_rdl.WriteElementString("Top", "1.875in");
		_rdl.WriteElementString("Width", "2.375in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "0pt");
		_rdl.WriteElementString("PaddingRight", "0pt");
		_rdl.WriteElementString("PaddingTop", "0pt");
		_rdl.WriteElementString("PaddingBottom", "0pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "3");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "7.25in");
		_rdl.WriteElementString("Height", "0in");
		_rdl.WriteElementString("Value", "Chicago, IL 60632");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox6");
		_rdl.WriteElementString("rd:DefaultName", "textbox6");
		_rdl.WriteElementString("Top", "1.625in");
		_rdl.WriteElementString("Width", "2.375in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "0pt");
		_rdl.WriteElementString("PaddingRight", "0pt");
		_rdl.WriteElementString("PaddingTop", "0pt");
		_rdl.WriteElementString("PaddingBottom", "0pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "2");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "7.25in");
		_rdl.WriteElementString("Height", "0in");
		_rdl.WriteElementString("Value", "4100 W. Ann Lurie Place");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox5");
		_rdl.WriteElementString("rd:DefaultName", "textbox5");
		_rdl.WriteElementString("Top", "1.375in");
		_rdl.WriteElementString("Width", "2.375in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("PaddingLeft", "0pt");
		_rdl.WriteElementString("PaddingRight", "0pt");
		_rdl.WriteElementString("PaddingTop", "0pt");
		_rdl.WriteElementString("PaddingBottom", "0pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "1");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "7.25in");
		_rdl.WriteElementString("Height", "0in");
		_rdl.WriteElementString("Value", "Greater Chicago Food Depository");
		_rdl.WriteEndElement(); //End Textbox

		_rdl.WriteStartElement("", "Textbox", null);
		_rdl.WriteAttributeString("Name", null, "textbox4");
		_rdl.WriteElementString("rd:DefaultName", "textbox4");
		_rdl.WriteElementString("Top", "1.125in");
		_rdl.WriteElementString("Width", "0.875in");
		_rdl.WriteStartElement("", "Style", null);
		_rdl.WriteElementString("FontWeight", "700");
		_rdl.WriteElementString("PaddingLeft", "0pt");
		_rdl.WriteElementString("PaddingRight", "0pt");
		_rdl.WriteElementString("PaddingTop", "0pt");
		_rdl.WriteElementString("PaddingBottom", "0pt");
		_rdl.WriteEndElement(); //End Style
		_rdl.WriteElementString("ZIndex", "1");
		_rdl.WriteElementString("CanGrow", "true");
		_rdl.WriteElementString("Left", "7.25in");
		_rdl.WriteElementString("Height", "0in");
		_rdl.WriteElementString("Value", "SHIP FROM:");
		_rdl.WriteEndElement(); //End Textbox
		#endregion

		_rdl.WriteEndElement(); //End ReportItems
		_rdl.WriteEndElement(); //End List
		_rdl.WriteEndElement(); //End ReportItems
		_rdl.WriteEndElement(); //End Body
		_rdl.WriteEndElement(); //End Report
		//write the document to the memory stream
		_rdl.Flush();

		//write the document to the memory stream
		_rdl.Flush();

		return result.ToString();
	}
}
