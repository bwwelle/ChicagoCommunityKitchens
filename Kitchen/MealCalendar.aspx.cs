using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using GCFDGlobalsNamespace;

public partial class MealCalendar : System.Web.UI.Page
{
    public string m_SQL;
    public DataSet mealDataSet;
    public DataSet calendarDataSet;
    public CreateMealCalendarCellTable calendarCellTable;
    protected DropDownList dropDownList = null;
    protected HiddenField hiddenField = null;
    protected Label label = null;
    protected Button button = null;
    protected TextBox textbox = null;
    protected UpdatePanel updatePanel = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated && (User.IsInRole("Kitchen-Staff") || User.IsInRole("Programs")))
        {
            if (Session["SessionID"] == null)
            {
                System.Web.Security.FormsAuthentication.SignOut();

                Response.Redirect("~/Account/Login.aspx", false);

                Response.End();
            }
            else
            {
                if (User.IsInRole("Kitchen-Admin") || User.IsInRole("Administrator"))
                {
                    SaveMealDetailButton.Enabled = true;

                    SaveCookChillYieldDetailButton.Enabled = true;

                    SaveRegularYieldDetailButton.Enabled = true;
                }

                RegularServingSizeDropDownList.Attributes["onchange"] = "javascript:updateregularyield()";
                RegularVolumeDropDownList.Attributes["onchange"] = "javascript:updateregularyield()";
                CookChillWeightDropDownList.Attributes["onchange"] = "javascript:updatecookchillyield()";
                VolumeServingSizeDropDownList.Attributes["onchange"] = "javascript:updatecookchillyield()";
                Load += (Page_Load);
                DeliveryCalendarControl.UseAccessibleHeader = false;

                //CancelRegularYieldDetailButton.OnClientClick = String.Format("fnOpenMealDetail('{0}','{1}')", CancelRegularYieldDetailButton.UniqueID, "");

                //CancelCookChillYieldDetailButton.OnClientClick = String.Format("fnOpenMealDetail('{0}','{1}')", CancelCookChillYieldDetailButton.UniqueID, "");

                if (!IsPostBack)
                {
                    SaveMealDetailButton.Attributes.Add("onclick", "value='Saving...Please Wait...';");

                    Populate_MonthList();

                    Populate_YearList();

                    DeliveryCalendarControl.VisibleDate = DateTime.Today;

                    GetCalendarData();
                }

                if (Request.Form["__EVENTTARGET"] == "CalendarLinkButton")
                {
                    CalendarLinkButton_Click(Request.Form["__EVENTARGUMENT"]);
                }
            }
        }
        else
        {
            Response.Redirect("Default.aspx", false);

            Response.End();
        }
    }

    protected void GetCalendarData()
    {
        DateTime firstDate = GetFirstDisplayedDayOfCalendar();

        DateTime lastDate = firstDate.AddDays(40);

        calendarCellTable = new CreateMealCalendarCellTable();

        m_SQL = "SELECT DISTINCT SUM(MealCount) AS MealCount, MealID, RecipeName, RecipeTypeID, SortOrder, DeliveryDate FROM vwMealDelivery WHERE DeliveryDate >= '" + firstDate.ToString("MM/dd/yyyy") + "' AND DeliveryDate < '" + lastDate.ToString("MM/dd/yyyy") + "' AND MealID IS NOT NULL AND MealTypeID = 1 AND RecipeTypeID <> 11 AND DeliveryTypeName = 'Scheduled' GROUP BY MealTypeID, MealID, DeliveryTypeName, RecipeName, RecipeTypeID, SortOrder, DeliveryDate ORDER BY MealID, SortOrder";
        calendarCellTable.CalendarScheduledDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        m_SQL = "SELECT DISTINCT SUM(MealCount) AS MealCount, MealID, RecipeName, RecipeTypeID, SortOrder, DeliveryDate FROM vwMealDelivery WHERE DeliveryDate >= '" + firstDate.ToString("MM/dd/yyyy") + "' AND DeliveryDate < '" + lastDate.ToString("MM/dd/yyyy") + "' AND MealID IS NOT NULL AND MealTypeID = 1  AND RecipeTypeID <> 11 AND DeliveryTypeName = 'Rescheduled' GROUP BY MealTypeID, MealID, DeliveryTypeName, RecipeName, RecipeTypeID, SortOrder, DeliveryDate ORDER BY MealID, SortOrder";
        calendarCellTable.CalendarRescheduledDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        m_SQL = "SELECT SUM(MealCount) AS MealCount, DeliveryDate FROM vwDelivery WHERE DeliveryDate >= '" + firstDate.ToString("MM/dd/yyyy") + "' AND DeliveryDate < '" + lastDate.ToString("MM/dd/yyyy") + "' AND MealTypeID = 1 AND DeliveryTypeName = 'Scheduled' GROUP BY MealTypeID, DeliveryTypeName, DeliveryDate";
        calendarCellTable.ScheduledTotalMealCountDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        m_SQL = "SELECT SUM(MealCount) AS MealCount, DeliveryDate FROM vwDelivery WHERE DeliveryDate >= '" + firstDate.ToString("MM/dd/yyyy") + "' AND DeliveryDate < '" + lastDate.ToString("MM/dd/yyyy") + "' AND MealTypeID = 1 AND DeliveryTypeName = 'Rescheduled' GROUP BY MealTypeID, DeliveryTypeName, DeliveryDate";
        calendarCellTable.RescheduledTotalMealCountDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);
         
        drpCalYear.SelectedIndex = drpCalYear.Items.IndexOf(drpCalYear.Items.FindByText(DeliveryCalendarControl.VisibleDate.Year.ToString()));

        drpCalMonth.SelectedIndex = drpCalMonth.Items.IndexOf(drpCalMonth.Items.FindByText(DeliveryCalendarControl.VisibleDate.ToString("MMMM")));

        PreviousMonthLinkButton.Text = DeliveryCalendarControl.VisibleDate.AddMonths(-1).ToString("MMMM").Substring(0, 3);

        PreviousMonthHiddenField.Value = DeliveryCalendarControl.VisibleDate.AddMonths(-1).ToString("MMMM");

        NextMonthLinkButton.Text = DeliveryCalendarControl.VisibleDate.AddMonths(1).ToString("MMMM").Substring(0, 3);

        NextMonthHiddenField.Value = DeliveryCalendarControl.VisibleDate.AddMonths(1).ToString("MMMM");
    }

    protected DateTime GetFirstDisplayedDayOfCalendar()
    {
        DateTime firstDisplayedDate = new DateTime(DeliveryCalendarControl.VisibleDate.Year, DeliveryCalendarControl.VisibleDate.Month, 1);

        if ((int)firstDisplayedDate.DayOfWeek != 1)
        {
            firstDisplayedDate = firstDisplayedDate.AddDays(-(int)firstDisplayedDate.DayOfWeek + 1);
        }

        return firstDisplayedDate;
    }

    protected void MenuDeliveryCalendarControl_DayRender(object sender, DayRenderEventArgs e)
    {
        e.Cell.Width = Unit.Point(300);
        e.Cell.HorizontalAlign = HorizontalAlign.Center;
        e.Cell.VerticalAlign = VerticalAlign.Top;

        e.Cell.Controls.Add(new LiteralControl("<hr />"));

        if (calendarCellTable != null)
        {
            calendarCellTable.MealDate = e.Day.Date;
            calendarCellTable.MealTypeID = "1";

            e.Cell.Controls.Add(calendarCellTable.CalendarCellTable());
        }
    }

    protected void MenuDeliveryCalendarControl_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        GetCalendarData();
    }

    public void CalendarLinkButton_Click(string arguement)
    {
        if (arguement.Substring(0,1) == "E")
        {
            MealDetailForNewMeal(arguement.Substring(14), "Rescheduled");  //Create new extra meal
        }
        else
        {         
            MealDetailForMealUpdate(arguement);
        }

        GetCalendarData();

        ClientScript.RegisterStartupScript(GetType(), "key", "launchMealDetailModal();", true);
    }

    protected int TotalMealCount(string mealDate, string DeliveryTypeName)
    {
        try
        {
            int totalMealCount = 0;

            m_SQL = "SELECT SUM(MealCount) AS MealCount FROM vwDelivery WHERE CONVERT(varchar(50), DeliveryDate, 101) = '" + mealDate + "' AND DeliveryTypeName = '" + DeliveryTypeName + "' AND MealTypeID = 1";
            DataSet deliveriesDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            if (!String.IsNullOrEmpty(GCFDGlobals.dbGetValue(deliveriesDataSet.Tables[0].Rows[0], "MealCount")))
            {
                totalMealCount = Convert.ToInt32(GCFDGlobals.dbGetValue(deliveriesDataSet.Tables[0].Rows[0], "MealCount"));
            }

            return totalMealCount;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving the total regular meal count - " + ex.Message);

            throw;
        }
    }

    public void MealCountForCurrentMeal()
    {
        int mealCount = 0;

        for (int i = 0; i < lstSelect.Items.Count; i++)
        {
            m_SQL = "SELECT MealCount FROM vwDelivery WHERE DeliveryID = " + lstSelect.Items[i].Value;
            DataSet deliveryDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            foreach (DataRow deliveryData in deliveryDataSet.Tables[0].Rows)
            {
                mealCount = Convert.ToInt32(GCFDGlobals.dbGetValue(deliveryData, "MealCount")) + mealCount;
            }
        }

        CurrentMealCountLabel.Text = mealCount.ToString();

        GetRoundedMealCount roundedMealCount = new GetRoundedMealCount();

        roundedMealCount.MealCount = mealCount.ToString();

        RoundedCurrentMealCountLabel.Text = roundedMealCount.RoundedMealCountForCurrentMeal();
    }
    
    public void MealDetailRecipeNameDropDownBoxes()
    {
        m_SQL = "SELECT RecipeID, RecipeName FROM Recipe WHERE RecipeTypeID IN(1,6) ORDER BY RecipeName";
        DataSet proteinMealComponent = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        m_SQL = "SELECT RecipeID, RecipeName FROM Recipe WHERE RecipeTypeID = 2 ORDER BY RecipeName";
        DataSet vegetableMealComponent = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        m_SQL = "SELECT RecipeID, RecipeName FROM Recipe WHERE RecipeTypeID = 3 ORDER BY RecipeName";
        DataSet fruitMealComponent = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        m_SQL = "SELECT RecipeID, RecipeName FROM Recipe WHERE RecipeTypeID IN(4,5) ORDER BY RecipeName";
        DataSet mealOtherDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        ProteinDropDownList.DataValueField = "RecipeID";
        ProteinDropDownList.DataTextField = "RecipeName";
        ProteinDropDownList.DataSource = proteinMealComponent.Tables[0];
        ProteinDropDownList.DataBind();
        ProteinDropDownList.Items.Insert(0, new ListItem("SELECT PROTEIN", "-1"));
        ProteinDropDownList.Items.Insert(1, new ListItem("None", "1"));
        ViewProteinYieldDetailButton.Enabled = false;

        VegetableDropDownList.DataValueField = "RecipeID";
        VegetableDropDownList.DataTextField = "RecipeName";
        VegetableDropDownList.DataSource = vegetableMealComponent.Tables[0];
        VegetableDropDownList.DataBind();
        VegetableDropDownList.Items.Insert(0, new ListItem("SELECT VEGETABLE", "-1"));
        VegetableDropDownList.Items.Insert(1, new ListItem("None", "1"));
        ViewVegetableYieldDetailButton.Enabled = false;

        FruitDropDownList.DataValueField = "RecipeID";
        FruitDropDownList.DataTextField = "RecipeName";
        FruitDropDownList.DataSource = fruitMealComponent.Tables[0];
        FruitDropDownList.DataBind();
        FruitDropDownList.Items.Insert(0, "SELECT FRUIT");
        FruitDropDownList.Items.Insert(1, new ListItem("None", "1"));
        ViewFruitYieldDetailButton.Enabled = false;

        Other1DropDownList.DataValueField = "RecipeID";
        Other1DropDownList.DataTextField = "RecipeName";
        Other1DropDownList.DataSource = mealOtherDataSet.Tables[0];
        Other1DropDownList.DataBind();
        Other1DropDownList.Items.Insert(0, new ListItem("SELECT OTHER", "-1"));
        ViewOther1YieldDetailButton.Enabled = false;

        Other2DropDownList.DataValueField = "RecipeID";
        Other2DropDownList.DataTextField = "RecipeName";
        Other2DropDownList.DataSource = mealOtherDataSet.Tables[0];
        Other2DropDownList.DataBind();
        Other2DropDownList.Items.Insert(0, new ListItem("SELECT OTHER", "-1"));
        ViewOther2YieldDetailButton.Enabled = false;

        Other3DropDownList.DataValueField = "RecipeID";
        Other3DropDownList.DataTextField = "RecipeName";
        Other3DropDownList.DataSource = mealOtherDataSet.Tables[0];
        Other3DropDownList.DataBind();
        Other3DropDownList.Items.Insert(0, new ListItem("SELECT OTHER", "-1"));
        ViewOther3YieldDetailButton.Enabled = false;
    }

    public void MealDetailSiteNameListBoxes(string mealMode, string deliveryTypeName)
    {
        string servingDay;
        string listLine;
        string cancellationDate;
        string lastSiteName = "";
        DataSet deliveryCancellationDateDataSet = new DataSet();
        ListItem selectListItem = new ListItem();

        lstMain.Items.Clear();
        lstSelect.Items.Clear();

        switch (mealMode)
        {                
            case "New":
                m_SQL = "SELECT DISTINCT DeliveryID, ParentDeliveryID, SiteName, ServingDate FROM vwMealDelivery WHERE CONVERT(varchar(10), DeliveryDate, 101) = '" + MealDateLabel.Text + "' AND DeliveryTypeName = '" + deliveryTypeName + "' AND MealID IS NULL AND MealTypeID = 1 AND DeliveryTypeName <> 'Cancelled' ORDER BY SiteName";
                mealDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);                

                foreach (DataRow mealDataRow in mealDataSet.Tables[0].Rows)
                {
                    if (deliveryTypeName == "Scheduled" || deliveryTypeName == "Rescheduled")
                    {
                        servingDay = Convert.ToDateTime(GCFDGlobals.dbGetValue(mealDataRow, "ServingDate")).DayOfWeek.ToString();

                        listLine = GCFDGlobals.dbGetValue(mealDataRow, "SiteName") + " (" + servingDay + " Serving Day)";

                        selectListItem = new ListItem(listLine, GCFDGlobals.dbGetValue(mealDataRow, "DeliveryID"));

                        if (!String.IsNullOrEmpty(lastSiteName) && lastSiteName == GCFDGlobals.dbGetValue(mealDataRow, "SiteName"))
                        {
                            selectListItem.Attributes.Add("style", "background-color: RED");
                        }

                        lastSiteName = GCFDGlobals.dbGetValue(mealDataRow, "SiteName");
                    }
                    else
                    {

                        //// extra meals cancel error checking extra meals
                        m_SQL = "SELECT DISTINCT DeliveryDate, ServingDate FROM vwDelivery WHERE DeliveryID = " + GCFDGlobals.dbGetValue(mealDataRow, "ParentDeliveryID");
                        deliveryCancellationDateDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                        cancellationDate = GCFDGlobals.dbGetValue(deliveryCancellationDateDataSet.Tables[0].Rows[0], "DeliveryDate");

                        servingDay = Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryCancellationDateDataSet.Tables[0].Rows[0], "ServingDate")).DayOfWeek.ToString();

                       listLine = GCFDGlobals.dbGetValue(mealDataRow, "SiteName") + " (" + servingDay + " Serving Day/Rescheduled From " + cancellationDate + ")";                    

                        selectListItem = new ListItem(listLine, GCFDGlobals.dbGetValue(mealDataRow, "DeliveryID"));

                        if (!String.IsNullOrEmpty(lastSiteName) && lastSiteName == GCFDGlobals.dbGetValue(mealDataRow, "SiteName"))
                        {
                            selectListItem.Attributes.Add("style", "background-color: RED");
                        }

                        lastSiteName = GCFDGlobals.dbGetValue(mealDataRow, "SiteName");
                    }                    

                    lstSelect.Items.Add(selectListItem);

                }

                break;
            case "Update":
                m_SQL = "SELECT DISTINCT DeliveryID, ParentDeliveryID, SiteName, ServingDate FROM vwMealDelivery WHERE DeliveryTypeName <> 'Cancelled' AND MealID = " + MealIDHiddenTextBox.Value + " ORDER BY SiteName";
                mealDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                foreach (DataRow mealDataRow in mealDataSet.Tables[0].Rows)
                {
                    if (deliveryTypeName == "Scheduled" || deliveryTypeName == "Rescheduled")
                    { 
                        servingDay = Convert.ToDateTime(GCFDGlobals.dbGetValue(mealDataRow, "ServingDate")).DayOfWeek.ToString();

                        listLine = GCFDGlobals.dbGetValue(mealDataRow, "SiteName") + " (" + servingDay + " Serving Day)";

                        selectListItem = new ListItem(listLine, GCFDGlobals.dbGetValue(mealDataRow, "DeliveryID"));

                        if(!String.IsNullOrEmpty(lastSiteName) && lastSiteName == GCFDGlobals.dbGetValue(mealDataRow, "SiteName"))  
                        {
                            selectListItem.Attributes.Add("style", "background-color: RED");
                        }                        
                        
                        lastSiteName = GCFDGlobals.dbGetValue(mealDataRow, "SiteName");
                    }
                    else
                    {
                        m_SQL = "SELECT DISTINCT DeliveryDate, ServingDate FROM vwDelivery WHERE DeliveryTypeName <> 'Cancelled' AND DeliveryID = " + GCFDGlobals.dbGetValue(mealDataRow, "ParentDeliveryID");
                        deliveryCancellationDateDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                       cancellationDate = GCFDGlobals.dbGetValue(deliveryCancellationDateDataSet.Tables[0].Rows[0], "DeliveryDate");

                        servingDay = Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryCancellationDateDataSet.Tables[0].Rows[0], "ServingDate")).DayOfWeek.ToString();

                        listLine = GCFDGlobals.dbGetValue(mealDataRow, "SiteName") + " (" + servingDay + " Serving Day/Rescheduled From " + cancellationDate + ")";

                    
                        selectListItem = new ListItem(listLine, GCFDGlobals.dbGetValue(mealDataRow, "DeliveryID"));

                        if (!String.IsNullOrEmpty(lastSiteName) && lastSiteName == GCFDGlobals.dbGetValue(mealDataRow, "SiteName"))
                        {
                            selectListItem.Attributes.Add("style", "background-color: RED");
                        }

                        lastSiteName = GCFDGlobals.dbGetValue(mealDataRow, "SiteName");                    
                    }

                    lstSelect.Items.Add(selectListItem);
                }

                m_SQL = "SELECT DISTINCT DeliveryID, ParentDeliveryID, SiteName, ServingDate FROM vwMealDelivery WHERE CONVERT(varchar(10), DeliveryDate, 101) = '" + MealDateLabel.Text + "' AND DeliveryTypeName <> 'Cancelled' AND DeliveryTypeName = '" + deliveryTypeName + "' AND MealID IS NULL AND MealTypeID = 1 ORDER BY SiteName";
                mealDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);
                
                foreach (DataRow mealDataRow in mealDataSet.Tables[0].Rows)
                {
                    if (deliveryTypeName == "Scheduled" || deliveryTypeName == "Rescheduled")
                    {                    
                        servingDay = Convert.ToDateTime(GCFDGlobals.dbGetValue(mealDataRow, "ServingDate")).DayOfWeek.ToString();

                        listLine = GCFDGlobals.dbGetValue(mealDataRow, "SiteName") + " (" + servingDay + " Serving Day)";

                        selectListItem = new ListItem(listLine, GCFDGlobals.dbGetValue(mealDataRow, "DeliveryID"));

                        if (!String.IsNullOrEmpty(lastSiteName) && lastSiteName == GCFDGlobals.dbGetValue(mealDataRow, "SiteName"))
                        {
                            selectListItem.Attributes.Add("style", "background-color: RED");
                        }

                        lastSiteName = GCFDGlobals.dbGetValue(mealDataRow, "SiteName");
                    }
                    else
                    {
                        m_SQL = "SELECT DISTINCT DeliveryDate, ServingDate FROM vwDelivery WHERE DeliveryTypeName <> 'Cancelled' AND DeliveryID = " + GCFDGlobals.dbGetValue(mealDataRow, "ParentDeliveryID");
                        deliveryCancellationDateDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                        cancellationDate = GCFDGlobals.dbGetValue(deliveryCancellationDateDataSet.Tables[0].Rows[0], "DeliveryDate");

                        servingDay = Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryCancellationDateDataSet.Tables[0].Rows[0], "ServingDate")).DayOfWeek.ToString();

                        listLine = GCFDGlobals.dbGetValue(mealDataRow, "SiteName") + " (" + servingDay + " Serving Day/Rescheduled From " + cancellationDate + ")";

                        selectListItem = new ListItem(listLine, GCFDGlobals.dbGetValue(mealDataRow, "DeliveryID"));

                        if (!String.IsNullOrEmpty(lastSiteName) && lastSiteName == GCFDGlobals.dbGetValue(mealDataRow, "SiteName"))
                        {
                            selectListItem.Attributes.Add("style", "background-color: RED");
                        }

                        lastSiteName = GCFDGlobals.dbGetValue(mealDataRow, "SiteName");
                    }

                    lstMain.Items.Add(selectListItem);
                }
                break;            
        }

        UpdateListBoxHeightWidth();
    }

    public void MealDetailForNewMeal(string mealDate, string deliveryTypeName)
    {
        MealIDHiddenTextBox.Value = "";
        ProteinRecipeDetailIDHiddenField.Value = "";
        VegetableRecipeDetailIDHiddenField.Value = "";
        FruitRecipeDetailIDHiddenField.Value = "";
        Other1RecipeDetailIDHiddenField.Value = "";
        Other2RecipeDetailIDHiddenField.Value = "";
        Other3RecipeDetailIDHiddenField.Value = "";

        if (deliveryTypeName == "Scheduled")
        {
            TotalMealCountLabel.Text = "Total Regular Meal Count For This Date:";
        }
        else
        {
            TotalMealCountLabel.Text = "Total Extra Meal Count For This Date:";
        }

        MealDateLabel.Text = mealDate;
        MealIDHiddenTextBox.Value = "-1";

        TotalMealCountForDateLabel.Text = TotalMealCount(MealDateLabel.Text, deliveryTypeName).ToString();
        
        NotesTextBox.Text = "Store all food except canned fruit in refrigerator until ready to reheat (hot meal items) or serve (fresh fruit, cold salads, etc.) Before serving, reheat hot meal items within 2 hours to an internal temperature of 165ºF for at least 15 seconds, or discard. Serve reheated food within 2 hours. While serving, hot meal items must be kept at 135°F or higher. Discard unused portions.";
                
        MealDetailSiteNameListBoxes("New", deliveryTypeName);

        MealCountForCurrentMeal();

        MealDetailRecipeNameDropDownBoxes();

        DeliveryCalendarControl.SelectedDates.Clear();

        GetCalendarData();
        
        ClientScript.RegisterStartupScript(GetType(), "key", "launchMealDetailModal();", true);
    }

    public void MealDetailForMealUpdate(string mealID)
    {
        MealIDHiddenTextBox.Value = "";
        ProteinRecipeDetailIDHiddenField.Value = "";
        VegetableRecipeDetailIDHiddenField.Value = "";
        FruitRecipeDetailIDHiddenField.Value = "";
        Other1RecipeDetailIDHiddenField.Value = "";
        Other2RecipeDetailIDHiddenField.Value = "";
        Other3RecipeDetailIDHiddenField.Value = "";

        int otherCount = 1;
        string recipeTypeObjectName;
        string deliveryTypeName = "";
        DataSet recipeDataSet = new DataSet();
        string recipeName = "";
      //  DropDownList dropDownList = new DropDownList();

        MealIDHiddenTextBox.Value = mealID;
        
        MealDetailRecipeNameDropDownBoxes();

        m_SQL = "SELECT DISTINCT RecipeID, RecipeName, RecipeTypeObjectName, ServingSize, ServingSizeTypeName, YieldTypeID, DeliveryTypeName, RecipeDetailID, DeliveryDate, MealNotes FROM vwMealDelivery WHERE RecipeTypeID <> 11 AND DeliveryTypeName <> 'Cancelled' AND MealID = " + mealID + " ";
        mealDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

      ErrorLabel.Text = "";

        foreach (DataRow mealDataRow in mealDataSet.Tables[0].Rows)
        {
            recipeTypeObjectName = GCFDGlobals.dbGetValue(mealDataRow, "RecipeTypeObjectName");
            deliveryTypeName = GCFDGlobals.dbGetValue(mealDataRow, "DeliveryTypeName");
            recipeName = GCFDGlobals.dbGetValue(mealDataRow, "RecipeName");

    //        ErrorLabel.Text += "recipeTypeObjectName " + recipeTypeObjectName + "<br>";
    //        ErrorLabel.Text += "deliveryTypeName " + deliveryTypeName + "<br>";
    //        ErrorLabel.Text += "recipe Name " + recipeName + "<br>";

            if (recipeTypeObjectName == "Other")
            {
                recipeTypeObjectName = recipeTypeObjectName + otherCount.ToString();

                otherCount = otherCount + 1;
            }

            this.dropDownList = this.Master.FindControl("MainContent").FindControl(recipeTypeObjectName + "DropDownList") as DropDownList;

            this.dropDownList.SelectedIndex = this.dropDownList.Items.IndexOf(this.dropDownList.Items.FindByText(recipeName));

            this.hiddenField = this.Master.FindControl("MainContent").FindControl(recipeTypeObjectName + "RecipeDetailIDHiddenField") as HiddenField;

            this.hiddenField.Value = GCFDGlobals.dbGetValue(mealDataRow, "RecipeDetailID");

            this.label = this.Master.FindControl("MainContent").FindControl(recipeTypeObjectName + "ServingSizeLabel") as Label;

            this.label.Text = GCFDGlobals.dbGetValue(mealDataRow, "ServingSize");

            this.label = this.Master.FindControl("MainContent").FindControl(recipeTypeObjectName + "ServingSizeTypeLabel") as Label;

            this.label.Text = GCFDGlobals.dbGetValue(mealDataRow, "ServingSizeTypeName");

            MealDateLabel.Text = Convert.ToDateTime(GCFDGlobals.dbGetValue(mealDataRow, "DeliveryDate")).ToString("MM/dd/yyyy");

            TotalMealCountForDateLabel.Text = TotalMealCount(MealDateLabel.Text, deliveryTypeName).ToString();

            NotesTextBox.Text = GCFDGlobals.dbGetValue(mealDataRow, "MealNotes");

            recipeName = this.dropDownList.SelectedItem.Text;

            if (recipeName != "SELECT " + recipeTypeObjectName)
            {
                this.button = this.Master.FindControl("MainContent").FindControl("View" + recipeTypeObjectName + "YieldDetailButton") as Button;

                this.button.Enabled = true;
            }
        }

        MealDetailSiteNameListBoxes("Update", deliveryTypeName);

        MealCountForCurrentMeal();

        DeliveryCalendarControl.SelectedDates.Clear();
    }

    protected void ViewSiteListButton_Click(object sender, EventArgs e)
    {
        GetCalendarData();

        Response.Redirect("Sites.aspx", false);
    }   
            
    public void RecipeTypeDropDownListSelectionChanged(string recipeTypeName)
    {
        DataSet yieldDataSet = new DataSet();
        string textTest = "";

        if(recipeTypeName.Substring(0, 5) == "Other")
        {
            textTest = "OTHER";
        }
        else
        {
            textTest = recipeTypeName;
        }

        this.dropDownList = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "DropDownList") as DropDownList;

        string dropDownValue = this.dropDownList.SelectedItem.Text;

        if (dropDownValue == "SELECT " + textTest || dropDownValue == "None")
        {
            this.label = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "ServingSizeLabel") as Label;

            this.label.Text = "0";

            this.label = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "ServingSizeTypeLabel") as Label;

            this.label.Text = "ounce(s)";

            this.button = this.Master.FindControl("MainContent").FindControl("View" + recipeTypeName + "YieldDetailButton") as Button;

            this.button.Enabled = false;

            this.hiddenField = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "RecipeDetailIDHiddenField") as HiddenField;
            m_SQL = "DELETE FROM RecipeDetail WHERE RecipeDetailID = " + this.hiddenField.Value + " AND IsDefault <> 'true'";
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

            this.hiddenField.Value = "";
        }
        else
        {
            this.hiddenField = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "RecipeDetailIDHiddenField") as HiddenField;

            m_SQL = "DELETE FROM RecipeDetail WHERE RecipeDetailID = " + this.hiddenField.Value + " AND IsDefault <> 'true'";
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

            this.dropDownList = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "DropDownList") as DropDownList;

            if (this.dropDownList.SelectedItem.Value != "-1" && this.dropDownList.SelectedItem.Value != "1")
            {
                m_SQL = "SELECT ServingSize, ServingSizeTypeName, YieldTypeName, RecipeDetailID FROM vwRecipeDetail WHERE RecipeID = " + this.dropDownList.SelectedItem.Value + " AND IsDefault = 'true'";
                yieldDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                this.label = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "ServingSizeLabel") as Label;

                this.label.Text = Convert.ToString(GCFDGlobals.dbGetValue(yieldDataSet.Tables[0].Rows[0], "ServingSize")) + " ";

                if (GCFDGlobals.dbGetValue(yieldDataSet.Tables[0].Rows[0], "YieldTypeName") == "CookChill")
                {
                    this.label = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "ServingSizeTypeLabel") as Label;

                    this.label.Text = "cup(s)";
                }
                else
                {
                    this.label = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "ServingSizeTypeLabel") as Label;

                    this.label.Text = Convert.ToString(GCFDGlobals.dbGetValue(yieldDataSet.Tables[0].Rows[0], "ServingSizeTypeName"));
                }

                this.button = this.Master.FindControl("MainContent").FindControl("View" + recipeTypeName + "YieldDetailButton") as Button;

                this.button.Enabled = true;

                this.hiddenField = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "RecipeDetailIDHiddenField") as HiddenField;

                this.hiddenField.Value = GCFDGlobals.dbGetValue(yieldDataSet.Tables[0].Rows[0], "RecipeDetailID");
            }
            else
            {
                this.label = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "ServingSizeLabel") as Label;

                this.label.Text = "";

                this.label = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "ServingSizeTypeLabel") as Label;

                this.label.Text = "cup(s)";

                this.button = this.Master.FindControl("MainContent").FindControl("View" + recipeTypeName + "YieldDetailButton") as Button;

                this.button.Enabled = false;

                this.hiddenField = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "RecipeDetailIDHiddenField") as HiddenField;

                this.hiddenField.Value = "";
            }
        }
               
        GetCalendarData();

        ClientScript.RegisterStartupScript(GetType(), "key", "launchMealDetailModal();", true);         
    }

    protected void ProteinDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        RecipeTypeDropDownListSelectionChanged("Protein");
    }

    protected void VegetableDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        RecipeTypeDropDownListSelectionChanged("Vegetable");
    }

    protected void FruitDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        RecipeTypeDropDownListSelectionChanged("Fruit");
    }

    protected void Other1DropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        RecipeTypeDropDownListSelectionChanged("Other1");
    }

    protected void Other2DropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {        
        RecipeTypeDropDownListSelectionChanged("Other2");
    }

    protected void Other3DropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        RecipeTypeDropDownListSelectionChanged("Other3");
    }

    protected void ViewProteinYieldDetailButton_Click(object sender, EventArgs e)
    {
        m_SQL = "SELECT DISTINCT YieldTypeID FROM RecipeDetail WHERE RecipeDetailID = " + ProteinRecipeDetailIDHiddenField.Value;
        DataSet recipeDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        if (GCFDGlobals.dbGetValue(recipeDataSet.Tables[0].Rows[0], "YieldTypeID") == "1")
        {
            RegularYieldDetail(ProteinRecipeDetailIDHiddenField.Value, "Protein");
        }
        else
        {
            CookChillYieldDetail(ProteinRecipeDetailIDHiddenField.Value, "Protein");
        }
    }

    protected void ViewVegetableYieldDetailButton_Click(object sender, EventArgs e)
    {
        m_SQL = "SELECT DISTINCT YieldTypeID FROM RecipeDetail WHERE RecipeDetailID = " + VegetableRecipeDetailIDHiddenField.Value;
        DataSet recipeDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        if (GCFDGlobals.dbGetValue(recipeDataSet.Tables[0].Rows[0], "YieldTypeID") == "1")
        {
            RegularYieldDetail(VegetableRecipeDetailIDHiddenField.Value, "Vegetable");
        }
        else
        {
            CookChillYieldDetail(VegetableRecipeDetailIDHiddenField.Value, "Vegetable");
        }
    }

    protected void ViewFruitYieldDetailButton_Click(object sender, EventArgs e)
    {
        m_SQL = "SELECT DISTINCT YieldTypeID FROM RecipeDetail WHERE RecipeDetailID = " + FruitRecipeDetailIDHiddenField.Value;
        DataSet recipeDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        if (GCFDGlobals.dbGetValue(recipeDataSet.Tables[0].Rows[0], "YieldTypeID") == "1")
        {
            RegularYieldDetail(FruitRecipeDetailIDHiddenField.Value, "Fruit");
        }
        else
        {
            CookChillYieldDetail(FruitRecipeDetailIDHiddenField.Value, "Fruit");
        }
    }

    protected void ViewOther1YieldDetailButton_Click(object sender, EventArgs e)
    {
        m_SQL = "SELECT DISTINCT YieldTypeID FROM RecipeDetail WHERE RecipeDetailID = " + Other1RecipeDetailIDHiddenField.Value;
        DataSet recipeDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        if (GCFDGlobals.dbGetValue(recipeDataSet.Tables[0].Rows[0], "YieldTypeID") == "1")
        {
            RegularYieldDetail(Other1RecipeDetailIDHiddenField.Value, "Other1");
        }
        else
        {
            CookChillYieldDetail(Other1RecipeDetailIDHiddenField.Value, "Other1");
        }
    }

    protected void ViewOther2YieldDetailButton_Click(object sender, EventArgs e)
    {
        m_SQL = "SELECT DISTINCT YieldTypeID FROM RecipeDetail WHERE RecipeDetailID = " + Other2RecipeDetailIDHiddenField.Value;
        DataSet recipeDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        if (GCFDGlobals.dbGetValue(recipeDataSet.Tables[0].Rows[0], "YieldTypeID") == "1")
        {
            RegularYieldDetail(Other2RecipeDetailIDHiddenField.Value, "Other2");
        }
        else
        {
            CookChillYieldDetail(Other2RecipeDetailIDHiddenField.Value, "Other2");
        }
    }

    protected void ViewOther3YieldDetailButton_Click(object sender, EventArgs e)
    {
        m_SQL = "SELECT DISTINCT YieldTypeID FROM RecipeDetail WHERE RecipeDetailID = " + Other3RecipeDetailIDHiddenField.Value;
        DataSet recipeDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        if (GCFDGlobals.dbGetValue(recipeDataSet.Tables[0].Rows[0], "YieldTypeID") == "1")
        {
            RegularYieldDetail(Other3RecipeDetailIDHiddenField.Value, "Other3");
        }
        else
        {
            CookChillYieldDetail(Other3RecipeDetailIDHiddenField.Value, "Other3");
        }
    }

    public void RegularYieldDetail(string recipeDetailID, string recipeType)
    {
        RecipeDetail  mealRecipeYield = new RecipeDetail();

        YieldDetailRecipeDetailIDHiddenField.Value = recipeDetailID;

        YieldDetailRecipeTypeNameHiddenField.Value = recipeType;

        mealRecipeYield.RecipeDetailID = recipeDetailID;

        mealRecipeYield.GetMealRecipeDetail();
        
        if (mealRecipeYield.ServingSizeTypeName == "ounce(s)")
        {
            VolumeWeightLabel.Text = "Volume Equivalent:";

            VolumeWeightTypeLabel.Text = "cup(s)";
        }
        else
        {
            VolumeWeightLabel.Text = "Weight Equivalent:";

            VolumeWeightTypeLabel.Text = "ounce(s)";
        }

        RecipeNotesTextBox.Text = mealRecipeYield.Notes;

        RecipeNumberofServings.Text = mealRecipeYield.NumberOfServings;

        RegularServingSizeDropDownList.SelectedIndex =
            RegularServingSizeDropDownList.Items.IndexOf(
                RegularServingSizeDropDownList.Items.FindByValue(mealRecipeYield.ServingSize));

        ServingSizeTypeDropdownList.SelectedIndex =
            ServingSizeTypeDropdownList.Items.IndexOf(
                ServingSizeTypeDropdownList.Items.FindByText(mealRecipeYield.ServingSizeTypeName));

        RegularVolumeDropDownList.SelectedIndex =
            RegularVolumeDropDownList.Items.IndexOf(
                RegularVolumeDropDownList.Items.FindByValue(mealRecipeYield.VolumeWeight));

        ConversionFactorTextBox.Text = mealRecipeYield.Conversion;

        CurrentMealRoundedMealCountTextBox.Text = RoundedCurrentMealCountLabel.Text;

        if (mealRecipeYield.IsDefault == "False")
        {          
            mealRecipeYield.GetDefaultRecipeDetail();
        }

        RecipeOriginalYieldHiddenField.Value = mealRecipeYield.YieldInPounds;
        OriginalVolumeHiddenField.Value = mealRecipeYield.VolumeWeight;
        OriginalServingSizeHiddenField.Value = mealRecipeYield.ServingSize;

        GetCalendarData();

        this.updatePanel = this.Master.FindControl("MainContent").FindControl("UpdatePanel2") as UpdatePanel;

        this.updatePanel.Update();

        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "DisplayPopup", "$find('RegularYieldPopupBehavior').show();", true);
    }
    
    public void CookChillYieldDetail(string recipeDetailID, string recipeType)
    {
        RecipeDetail mealRecipeYield = new RecipeDetail();

        YieldDetailRecipeDetailIDHiddenField.Value = recipeDetailID;

        YieldDetailRecipeTypeNameHiddenField.Value = recipeType;

        mealRecipeYield.RecipeDetailID = recipeDetailID;

        mealRecipeYield.GetMealRecipeDetail();

        PackageTypeTextBox.Text = mealRecipeYield.PackageTypeName;

        PackageTypeLabel.Text = mealRecipeYield.PackageTypeName;

        PackageTypeIDHiddenField.Value = mealRecipeYield.PackageTypeID;

        ServingSizeTypeIDHiddenField.Value = mealRecipeYield.ServingSizeTypeID;

        NumberofPackageTypeTextBox.Text = mealRecipeYield.PackagesPerBatch;

        VolumeServingSizeDropDownList.SelectedIndex =
            VolumeServingSizeDropDownList.Items.IndexOf(
                VolumeServingSizeDropDownList.Items.FindByValue(mealRecipeYield.ServingSize));

        CookChillWeightDropDownList.SelectedIndex =
            CookChillWeightDropDownList.Items.IndexOf(
                CookChillWeightDropDownList.Items.FindByValue(mealRecipeYield.VolumeWeight));

        ServingsPerPackageTextBox.Text = mealRecipeYield.ServingsPerPackage;

        ServingsPerBatchTextBox.Text = mealRecipeYield.ServingsPerBatch;

        BatchYieldInPoundsTextBox.Text = mealRecipeYield.YieldInPounds;

        CookchillRoundMealCountTextBox.Text = RoundedCurrentMealCountLabel.Text;

        CookChillConversionFactorTextBox.Text = mealRecipeYield.Conversion;

        if (mealRecipeYield.IsDefault == "False")
        {
            mealRecipeYield.GetDefaultRecipeDetail();
        }

        RecipeOriginalCCYieldHiddenField.Value = mealRecipeYield.YieldInPounds;
        OriginalCCVolumeHiddenField.Value = mealRecipeYield.VolumeWeight;
        OriginalCCServingSizeHiddenField.Value = mealRecipeYield.ServingSize;

        GetCalendarData();

        this.updatePanel = this.Master.FindControl("MainContent").FindControl("UpdatePanel3") as UpdatePanel;

        this.updatePanel.Update();

        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "DisplayPopup", "$find('CookChillYieldPopupBehavior').show();", true);
    }

    protected void ServingSizeTypeDropdownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ServingSizeTypeDropdownList.SelectedItem.Text == "ounce(s)")
        {
            VolumeWeightLabel.Text = "Serving Size (Volume):";

            VolumeWeightTypeLabel.Text = "cup(s)";
        }
        else
        {
            VolumeWeightLabel.Text = "Serving Size (Weight):";

            VolumeWeightTypeLabel.Text = "ounce(s)";
        }

        GetCalendarData();

        ClientScript.RegisterStartupScript(GetType(), "key", "launchRegularYieldPopupModal();", true);
    }

    protected void SaveRegularYieldDetailButton_Click(object sender, EventArgs e)
    {
        string conversion = "";
        string recipeTypeName = YieldDetailRecipeTypeNameHiddenField.Value;

        if (ConversionFactorTextBox.Text == "N/A")
        {
            conversion = "0.00";
        }
        else
        {
            conversion = ConversionFactorTextBox.Text;
        }

        this.dropDownList = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "DropDownList") as DropDownList;

        m_SQL = "DECLARE @NewRecipeDetailID int EXEC spUpdateMealRegularRecipeYield " + this.dropDownList.SelectedItem.Value + ", " + YieldDetailRecipeDetailIDHiddenField.Value + ", " + RegularServingSizeDropDownList.SelectedItem.Value + ", " + ServingSizeTypeDropdownList.SelectedItem.Value + ", " + RegularVolumeDropDownList.SelectedItem.Value + ", " + conversion + ", " + RecipeNumberofServings.Text + ", '" + NotesTextBox.Text + "', @NewRecipeDetailID = @NewRecipeDetailID OUTPUT SELECT @NewRecipeDetailID AS 'NewRecipeDetailID'";
        DataSet regularYieldDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        this.hiddenField = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "RecipeDetailIDHiddenField") as HiddenField;

        this.hiddenField.Value = GCFDGlobals.dbGetValue(regularYieldDataSet.Tables[0].Rows[0], "NewRecipeDetailID");

        this.label = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "ServingSizeLabel") as Label;

        this.label.Text = RegularServingSizeDropDownList.SelectedItem.Value;

        this.label = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "ServingSizeTypeLabel") as Label;

        this.label.Text = ServingSizeTypeDropdownList.SelectedItem.Text;

        GetCalendarData();

        ClientScript.RegisterStartupScript(GetType(), "key", "launchMealDetailModal();", true);
    }

    protected void SaveCookChillYieldDetail_Click(object sender, EventArgs e)
    {
        string conversion = "";
        string recipeTypeName = YieldDetailRecipeTypeNameHiddenField.Value;

        if (CookChillConversionFactorTextBox.Text == "N/A")
        {
            conversion = "0.00";
        }
        else
        {
            conversion = CookChillConversionFactorTextBox.Text;
        }

        this.dropDownList = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "DropDownList") as DropDownList;

        m_SQL = "DECLARE @NewRecipeDetailID int EXEC spUpdateMealCookChillRecipeYield " + ServingSizeTypeIDHiddenField.Value + ", " + this.dropDownList.SelectedItem.Value + ", " + YieldDetailRecipeDetailIDHiddenField.Value + ", " + PackageTypeIDHiddenField.Value + ", " + VolumeServingSizeDropDownList.SelectedItem.Value + ", " + CookChillWeightDropDownList.SelectedItem.Value + ", " + conversion + ", " + NumberofPackageTypeTextBox.Text + ", " + ServingsPerPackageTextBox.Text + ", " + ServingsPerBatchTextBox.Text + ", " + BatchYieldInPoundsTextBox.Text + ", '" + NotesTextBox.Text + "', @NewRecipeDetailID = @NewRecipeDetailID OUTPUT SELECT @NewRecipeDetailID AS 'NewRecipeDetailID'";
        DataSet regularYieldDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        this.hiddenField = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "RecipeDetailIDHiddenField") as HiddenField;

        this.hiddenField.Value = GCFDGlobals.dbGetValue(regularYieldDataSet.Tables[0].Rows[0], "NewRecipeDetailID");

        this.label = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "ServingSizeLabel") as Label;

        this.label.Text = VolumeServingSizeDropDownList.SelectedItem.Value;

        this.label = this.Master.FindControl("MainContent").FindControl(recipeTypeName + "ServingSizeTypeLabel") as Label;

        this.label.Text = CookChillServingSizeTypeLabel.Text;

        ClientScript.RegisterStartupScript(GetType(), "key", "launchMealDetailModal();", true);
    }
    
    protected void Button2_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < lstSelect.Items.Count; i++)
        {
            if (lstSelect.Items[i].Selected)
            {
                string s = lstSelect.SelectedItem.Value;
                string t = lstSelect.SelectedItem.Text;

                lstMain.Items.Add(new ListItem(t, s));

                lstSelect.Items.Remove(new ListItem(t, s));

                i--;
            }
        }

        UpdateListBoxHeightWidth();

        MealCountForCurrentMeal();

        //ClientScript.RegisterStartupScript(GetType(), "key", "launchMealDetailModal();", true);

        //GetCalendarData();
    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < lstMain.Items.Count; i++)
        {
            if (lstMain.Items[i].Selected)
            {
                string s = lstMain.SelectedItem.Value;
                string t = lstMain.SelectedItem.Text;

                lstSelect.Items.Add(new ListItem(t, s));

                lstMain.Items.Remove(new ListItem(t, s));

                i--;
            }
        }

        UpdateListBoxHeightWidth();

        MealCountForCurrentMeal();

        //ClientScript.RegisterStartupScript(GetType(), "key", "launchMealDetailModal();", true);

        //GetCalendarData();
    }

    protected void UpdateListBoxHeightWidth()
    {
        int nItem;
        int tItem;

        if (lstSelect.Items.Count < 5)
        {
            if (lstMain.Items.Count < 5)
            {
                nItem = 100;
            }
            else
            {
                nItem = Convert.ToInt32(lstMain.Items.Count * 17);
            }
        }
        else
        {
            nItem = Convert.ToInt32(lstSelect.Items.Count * 17);
        }

        lstSelect.Height = nItem; //Set height depends on the font size.
        lstSelect.Width = 400; //This will ensure the list item won't be shrinked!

        if (lstMain.Items.Count < 5)
        {
            if (lstSelect.Items.Count < 5)
            {
                tItem = 100;
            }
            else
            {
                tItem = Convert.ToInt32(lstSelect.Items.Count * 17);
            }
        }
        else
        {
            tItem = Convert.ToInt32(lstMain.Items.Count * 17);
        }

        lstMain.Height = tItem; //Set height depends on the font size.
        lstMain.Width = 400; //This will ensure the list item won't be shrinked!
    }

    protected void SaveMealDetailButton_Click(object sender, EventArgs e)
    {
        string mealID;

        m_SQL = "DECLARE @MealID int EXEC spUpdateCreateMeal " + MealIDHiddenTextBox.Value + ", '" + NotesTextBox.Text + "', '" + ProteinRecipeDetailIDHiddenField.Value + "', '" + VegetableRecipeDetailIDHiddenField.Value + "', '" + FruitRecipeDetailIDHiddenField.Value + "', '" + Other1RecipeDetailIDHiddenField.Value + "', '" + Other2RecipeDetailIDHiddenField.Value + "', '" + Other3RecipeDetailIDHiddenField.Value + "', @MealID OUTPUT SELECT @MealID as 'MealID'";
        DataSet mealCreationDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        mealID = GCFDGlobals.dbGetValue(mealCreationDataSet.Tables[0].Rows[0], "MealID");

        for (int i = 0; i < lstSelect.Items.Count; i++)
        {
            m_SQL = "INSERT INTO MealDelivery (MealID, DeliveryID) VALUES(" + mealID + ", " + lstSelect.Items[i].Value + ")";
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
        }

        MealIDHiddenTextBox.Value = "";
        ProteinRecipeDetailIDHiddenField.Value = "";
        VegetableRecipeDetailIDHiddenField.Value = "";
        FruitRecipeDetailIDHiddenField.Value = "";
        Other1RecipeDetailIDHiddenField.Value = "";
        Other2RecipeDetailIDHiddenField.Value = "";
        Other3RecipeDetailIDHiddenField.Value = "";

        GetCalendarData();
    }

    protected void Populate_MonthList()
    {
        //Add each month to the list
        drpCalMonth.Items.Add(new ListItem("January", "1"));
        drpCalMonth.Items.Add(new ListItem("February", "2"));
        drpCalMonth.Items.Add(new ListItem("March", "3"));
        drpCalMonth.Items.Add(new ListItem("April", "4"));
        drpCalMonth.Items.Add(new ListItem("May", "5"));
        drpCalMonth.Items.Add(new ListItem("June", "6"));
        drpCalMonth.Items.Add(new ListItem("July", "7"));
        drpCalMonth.Items.Add(new ListItem("August", "8"));
        drpCalMonth.Items.Add(new ListItem("September", "9"));
        drpCalMonth.Items.Add(new ListItem("October", "10"));
        drpCalMonth.Items.Add(new ListItem("November", "11"));
        drpCalMonth.Items.Add(new ListItem("December", "12"));

        //Make the current month selected item in the list
        drpCalMonth.SelectedIndex =
            drpCalMonth.Items.IndexOf(
                drpCalMonth.Items.FindByText(DateTime.Now.ToString("MMMM")));
    }

    protected void Populate_YearList()
    {
        //Year list can be changed by changing the lower and upper 
        //limits of the For statement    
        for (int intYear = DateTime.Now.Year - 20; intYear <= DateTime.Now.Year + 20; intYear++)
        {
            drpCalYear.Items.Add(new ListItem(intYear.ToString(), intYear.ToString()));
        }

        //Make the current year selected item in the list
        drpCalYear.SelectedIndex =
            drpCalYear.Items.IndexOf(
                drpCalYear.Items.FindByText(DateTime.Now.Year.ToString()));
    }

    protected void drpCalMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        Set_Calendar();

        GetCalendarData();
    }

    protected void Set_Calendar()
    {
        DateTime visibleDate = new DateTime(Convert.ToInt32(drpCalYear.SelectedItem.Text), Convert.ToInt32(drpCalMonth.SelectedItem.Value), 1);

        DeliveryCalendarControl.VisibleDate = visibleDate;
        DeliveryCalendarControl.TodaysDate = visibleDate;
    }

    protected void drpCalYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Set_Calendar();

        GetCalendarData();
    }

    protected void NextMonthLinkButton_Click(object sender, EventArgs e)
    {
        int calendarYear = Convert.ToInt32(drpCalYear.SelectedItem.Text);
        int calendarMonth = Convert.ToInt32(drpCalMonth.SelectedItem.Value);

        if (NextMonthHiddenField.Value == "January")
        {
            calendarYear = calendarYear + 1;

            calendarMonth = 1;
        }
        else
        {
            calendarMonth = calendarMonth + 1;
        }

        DateTime visibleDate = new DateTime(calendarYear, calendarMonth, 1);

        DeliveryCalendarControl.VisibleDate = visibleDate;
        DeliveryCalendarControl.TodaysDate = visibleDate;

        GetCalendarData();
    }

    protected void PreviousMonthLinkButton_Click(object sender, EventArgs e)
    {
        int calendarYear = Convert.ToInt32(drpCalYear.SelectedItem.Text);
        int calendarMonth = Convert.ToInt32(drpCalMonth.SelectedItem.Value);

        if (PreviousMonthHiddenField.Value == "December")
        {
            calendarYear = calendarYear - 1;

            calendarMonth = 12;
        }
        else
        {
            calendarMonth = calendarMonth - 1;
        }

        DateTime visibleDate = new DateTime(calendarYear, calendarMonth, 1);

        DeliveryCalendarControl.VisibleDate = visibleDate;
        DeliveryCalendarControl.TodaysDate = visibleDate;

        GetCalendarData();
    }
}
