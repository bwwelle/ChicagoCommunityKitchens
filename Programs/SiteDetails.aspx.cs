using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using GCFDGlobalsNamespace;
using System.Data.SqlClient;
using System.Globalization;
using AjaxControlToolkit;
using System.Diagnostics;

public partial class SiteDetails : System.Web.UI.Page
{
    public string m_SQL;
    public DataSet siteDataSet;
    public string mealCount = "";
    public DateTime startDeliveryDate;
    public int deliveryCount;
    public DataSet deliveryRecurrenceDataSet = new DataSet();
    public string deliveryRecurrenceID = "";
    public DateTime endDeliveryDate;
    public DateTime deliveryDate;
    public string deliveryDay = "";
    public string servingDay = "";
    public string recurrenceID = "";
    public string deliveryDetailNumber = "";
    public string recurrenceDetailID = "";
    public DataSet m_DeliveriesDataSet;
    int nItem;
    int tItem;
    public string editStartDate = "";
    public DataSet calendarDataSet;
    public DataSet mealCountTotalDataSet;
    protected DropDownList dropDownList = null;
    protected HiddenField hiddenField = null;
    protected Label label = null;
    protected Button button = null;
    protected TextBox textBox = null;
    public DateTime permittedStartDate;
    public string prohibitPastModifications;
    public string hasWeekendActivity;
    public int futureActivityInterval;
    public string servingDayInternal;
    
    DeliveryRecurrenceDetail deliveryRecurrenceDetail = new DeliveryRecurrenceDetail();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated && (User.IsInRole("Programs") || User.IsInRole("Compliance")))
        {
            if (Session["SessionID"] == null)
            {
                System.Web.Security.FormsAuthentication.SignOut();
                           
                Response.Redirect("~/Account/Login.aspx", false);

                Response.End();
            }
            else
            {
                EditRecurRadioButton.Attributes["onclick"] = "javascript:recurrenceonclick()";
                ExceptionRadioButton.Attributes["onclick"] = "javascript:exceptiononclick()";

                SFSPRadioButton.Attributes["onclick"] = "javascript:SFSPclick()";
                CACFPRadioButton.Attributes["onclick"] = "javascript:CACFPclick()";

                Load += (Page_Load);

                if (!IsPostBack)
                {
                    SiteModeHiddenField.Value = Request.QueryString.Get("SiteMode");
                    SiteIDHiddenField.Value = Request.QueryString.Get("SiteID");

                    if (SiteModeHiddenField.Value == "New")
                    {
                        DeleteSiteButton.Enabled = false;

                        NextSiteButton.Enabled = false;
                        PreviousSiteButton.Enabled = false;
                    }
                    else
                    {
                        DeleteSiteButton.Enabled = true;
                        
                        NextSiteButton.Enabled = true;

                        PreviousSiteButton.Enabled = true;

                        FillCommentDetailInGrid();
                    }

                    Populate_MonthList();
    
                    Populate_YearList();

                    SiteDeliveryCalendar.VisibleDate = Convert.ToDateTime(Request.QueryString.Get("CalendarDate"));

                    GetCalendarData();
                    
                    m_SQL = "SELECT * FROM Site WHERE SiteID = " + SiteIDHiddenField.Value;
                    siteDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                    foreach (DataRow siteData in siteDataSet.Tables[0].Rows)
                    {
                        SiteNameTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SiteName");

                        OriginalSiteNameHiddenField.Value = GCFDGlobals.dbGetValue(siteData, "SiteName");

                        DeleteSiteButton.Attributes.Add("onclick", "return confirm('Are you sure you want to PERMANENTLY DELETE all data associated with the " + GCFDGlobals.dbGetValue(siteData, "SiteName") + " site?')");
                        
                        SitePhoneTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SitePhone");
                        SiteAddressTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SiteAddress");
                        SiteCityTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SiteCity");
                        ZipCodeTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SiteZip");
                        DeliveryContactNameTextBox.Text = GCFDGlobals.dbGetValue(siteData, "DeliveryContactName");
                        DeliveryContactPhoneTextBox.Text = GCFDGlobals.dbGetValue(siteData, "DeliveryContactPhone");
                        RouteDropDownList.SelectedIndex = RouteDropDownList.Items.IndexOf(RouteDropDownList.Items.FindByText(GCFDGlobals.dbGetValue(siteData, "SiteRoute").Trim()));
                        SiteTypeDropDownListBox.SelectedIndex = SiteTypeDropDownListBox.Items.IndexOf(SiteTypeDropDownListBox.Items.FindByText(GCFDGlobals.dbGetValue(siteData, "SiteType").Trim()));
                        CACFPPrimaryContactNameTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPPrimaryContactName");
                        CACFPPrimaryContactPhoneTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPPrimaryContactPhone");
                        CACFPPrimaryContactEmailTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPPrimaryContactEmail");
                        CACFPAltContactNameTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPAltContactName");
                        CACFPAltContactPhoneTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPAltContactPhone");
                        CACFPAltContactEmailTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPAltContactEmail");
                        CACFPFaxTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPFax");
                        CACFPEmergencyContactNameTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPEmergencyContactName");
                        CACFPEmergencyContactPhoneTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPEmergencyContactPhone");
                        CACFPEmergencyContactNotesTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPEmergencyContactNotes");
                        SFSPPrimaryContactNameTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPPrimaryContactName");
                        SFSPPrimaryContactPhoneTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPPrimaryContactPhone");
                        SFSPPrimaryContactEmailTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPPrimaryContactEmail");
                        SFSPAltContactNameTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPAltContactName");
                        SFSPAltContactPhoneTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPAltContactPhone");
                        SFSPAltContactEmailTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPAltContactEmail");
                        SFSPFaxTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPFax");
                        SFSPEmergencyContactNameTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPEmergencyContactName");
                        SFSPEmergencyContactPhoneTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPEmergencyContactPhone");
                        SFSPEmergencyContactNotesTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPEmergencyContactNotes");

                        AgencyIDTextBox.Text = GCFDGlobals.dbGetValue(siteData, "AgencyID");
                        ProgramIDTextBox.Text = GCFDGlobals.dbGetValue(siteData, "ProgramID");
                        ISBETextBox.Text = GCFDGlobals.dbGetValue(siteData, "ISBECode");
                        FEINTextBox.Text = GCFDGlobals.dbGetValue(siteData, "FEINNo");
                        LAHCodeTextBox.Text = GCFDGlobals.dbGetValue(siteData, "LAHSiteCode");
                        NearestSchoolTextBox.Text = GCFDGlobals.dbGetValue(siteData, "NearestSchool");
                        UnmetNeedTextBox.Text = GCFDGlobals.dbGetValue(siteData, "UnmetNeed");
                        CommunityAreaTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CommunityArea");
                        NotesTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SiteNotes");
                        CACFPPreOpVisitTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPPreOpVisit");
                        CACFPFirstWeekVisitTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPFirstWeekVisit");
                        CACFPFirstMonthVisitTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPFirstMonthVisit");
                        Visit1TextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPVisit1");
                        Visit2TextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPVisit2");
                        CACFPAdditionalVisitsTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPAdditionalVisits");
                        CACFPStartDateTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPStartDate");
                        CACFPEndDateTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPEndDate");
                        CACFPServingDaysTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPServingDays");
                        CACFPServingTimeTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPServingTime");
                        CACFPDatesofNoServiceTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPDatesofNoService");
                        CACFPTrainingDateTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPTrainingDate");
                        CACFPTraineesTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPTrainees");
                        CACFPNotesTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPNotes");
                        CACFPMonitorTextBox.Text = GCFDGlobals.dbGetValue(siteData, "CACFPMonitor");

                        SFSPPreOpVisitTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPPreOpVisit");
                        SFSPFirstWeekVisitTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPFirstWeekVisit");
                        SFSPFirstMonthVisitTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPFirstMonthVisit");
                        SFSPAdditionalVisitsTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPAdditionalVisits");
                        SFSPStartDateTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPStartDate");
                        SFSPEndDateTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPEndDate");
                        SFSPLunchServingDaysTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPLunchServingDays");
                        SFSPLunchServingTimeTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPLunchServingTime");
                        SFSPBreakfastServingDaysTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPBreakfastServingDays");
                        SFSPBreakfastServingTimeTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPBreakfastServingTime");
                        SFSPDatesOfNoServiceTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPDatesofNoService");
                        SFSPTrainingDateTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPTrainingDate");
                        SFSPTraineesTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPTrainees");
                        SFSPNotesTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPNotes");
                        SFSPMonitorTextBox.Text = GCFDGlobals.dbGetValue(siteData, "SFSPMonitor");
                        ActiveCheckBox.Checked = Convert.ToBoolean(GCFDGlobals.dbGetValue(siteData, "Active"));
                    }

                    switch (Request.QueryString.Get("RecurrenceMode"))
                    {
                        case "Update":
                            m_SQL = "SELECT DISTINCT DeliveryRecurrenceLastModified, DeliveryRecurrenceID, MealTypeName, MealTypeID, StartDate, EndDate, CONVERT(varchar, DeliveryRecurrenceLastModified, 121) AS DeliveryRecurrenceLastModified FROM vwDelivery WHERE DeliveryRecurrenceID = " + Request.QueryString.Get("DeliveryRecurrenceID");
                            DataSet deliveryDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                            DeliveryRecurrenceIDHiddenField.Value = Request.QueryString.Get("DeliveryRecurrenceID");

                            RecurrenceLastModifiedHiddenField.Value = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "DeliveryRecurrenceLastModified");

                            RecurrenceMealTypeLabel.Text = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "MealTypeName");

                            MealTypeIDHiddenField.Value = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "MealTypeID");

                            RecurrenceStartDateTextBox.Text = Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "StartDate")).ToString("MM/dd/yyyy");

                            RecurrenceEndDateTextBox.Text = Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "EndDate")).ToString("MM/dd/yyyy");

                            FillDeliveryRecurrenceDetailInGrid(Request.QueryString.Get("DeliveryRecurrenceID"));

                            MealTypeDropDownList.Visible = false;

                            RecurrenceMealTypeLabel.Visible = true;

                            string mealTypeID = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "MealTypeID");
                    
                            int endDate = Convert.ToInt32(Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "EndDate")).ToString("yyyyMMdd"));
                            int startDate = Convert.ToInt32(Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "StartDate")).ToString("yyyyMMdd"));

                            RetrieveMealTypeValues(mealTypeID);

                            int formattedpermittedStartDate = Convert.ToInt32(permittedStartDate.ToString("yyyyMMdd"));;
                            
                            if (prohibitPastModifications == "1")
                            {
                                if (startDate < formattedpermittedStartDate)
                                    RecurrenceStartDateTextBox.Enabled = false;
                                else
                                    RecurrenceStartDateTextBox.Enabled = true;

                                if (endDate < formattedpermittedStartDate)
                                {
                                    RecurrenceEndDateTextBox.Enabled = false;

                                    DeleteRecurrenceButton.Enabled = false;

                                    SaveDeliveryRecurrenceButton.Enabled = false;

                                    foreach (GridViewRow row in DeliveryRecurrenceDetailGridView.Rows)
                                    {
                                        Panel popupPanel = (Panel)row.FindControl("PopupMenu");

                                        popupPanel.Visible = false;
                                    }
                                }
                                else
                                {
                                    RecurrenceEndDateTextBox.Enabled = true;
                            
                                    DeleteRecurrenceButton.Enabled = true;
                                }
                            }

                            FillDeliveryRecurrenceDetailInGrid(Request.QueryString.Get("DeliveryRecurrenceID"));

                            ClientScript.RegisterStartupScript(GetType(), "key", "launchDeliveryRecurrencePopupModal();", true);

                            break;

                        case "DeliveryRecurrenceConcurrencyError":
                            MessageBox.Show("Another user has edited the delivery recurrence since you started to edit it.");
                            
                            break;

                        case "DeliveryRecurrenceDeleteError":
                            MessageBox.Show("Another user has deleted the delivery recurrence since you started to edit it.");
                            
                            break;

                        case "DeletedLastDetail":
                            m_SQL = "SELECT DISTINCT DeliveryRecurrenceID, MealTypeID, StartDate, EndDate  FROM deliveryrecurrence WHERE DeliveryRecurrenceID = " + Request.QueryString.Get("DeliveryRecurrenceID");
                            DataSet deliveryRecurrenceDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                            DeleteRecurrenceButton.Enabled = false;

                            RecurrenceStartDateTextBox.Text = Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryRecurrenceDataSet.Tables[0].Rows[0], "StartDate")).ToString("MM/dd/yyyy");

                            RecurrenceEndDateTextBox.Text = Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryRecurrenceDataSet.Tables[0].Rows[0], "EndDate")).ToString("MM/dd/yyyy");
                            
                            MealTypeDropDownList.Visible = true;

                            RecurrenceStartDateTextBox.Enabled = true;

                            RecurrenceEndDateTextBox.Enabled = true;

                            RecurrenceModeHiddenField.Value = "New";

                            DeliveryRecurrenceDetailPanel.Visible = false;

                            SaveDeliveryRecurrenceButton.Text = "Add Recurrence Details";

                            m_SQL = "SELECT * FROM MealTypeDict";
                            DataSet m_MealType = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                            MealTypeDropDownList.DataValueField = "MealTypeID";
                            MealTypeDropDownList.DataTextField = "MealTypeName";
                            MealTypeDropDownList.DataSource = m_MealType.Tables[0];
                            MealTypeDropDownList.DataBind();
                            MealTypeDropDownList.Items.Insert(0, new ListItem("SELECT MEAL TYPE", "default"));

                            MealTypeDropDownList.SelectedIndex = MealTypeDropDownList.Items.IndexOf(MealTypeDropDownList.Items.FindByValue(GCFDGlobals.dbGetValue(deliveryRecurrenceDataSet.Tables[0].Rows[0], "MealTypeID")));

                            RecurrenceMealTypeLabel.Visible = false; 
                           
                            m_SQL = "EXECUTE spDeleteDeliveryRecurrence " + DeliveryRecurrenceIDHiddenField.Value;
                            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                            ClientScript.RegisterStartupScript(GetType(), "key", "launchDeliveryRecurrencePopupModal();", true);

                            break;
                    }
                }

                if (Request.Form["__EVENTTARGET"] == "CalendarExceptionLinkButton")
                {
                    CalendarExceptionLinkButton_Click(Request.Form["__EVENTARGUMENT"]);
                }

                if (Request.Form["__EVENTTARGET"] == "DeliveryQuestionButton")
                {
                    ShowDeliveryQuestionPopup(Request.Form["__EVENTARGUMENT"]);
                }                
            }
        }
        else
        {
            Response.Redirect("Default.aspx", false);

            Response.End();
        }
    }

    public void FillDeliveryRecurrenceDetailInGrid(string DeliveryRecurrenceID)
    {
        DataTable dtDeliveryRecurrenceDetail = deliveryRecurrenceDetail.Fetch(DeliveryRecurrenceID);

        DeliveryRecurrenceDetailGridView.DataSource = null;

        DeliveryRecurrenceDetailGridView.DataSource = dtDeliveryRecurrenceDetail;

        DeliveryRecurrenceDetailGridView.DataBind();
    }
    
    protected void GetCalendarData()
    {
        DateTime firstDate = GetFirstDisplayedDayOfCalendar();

        DateTime lastDate = firstDate.AddDays(40);

        m_SQL = "SELECT * FROM vwDelivery WHERE DeliveryDate >= '" + firstDate.ToString("MM/dd/yyyy") + "' AND DeliveryDate < '" + lastDate.ToString("MM/dd/yyyy") + "' AND SiteID = " + SiteIDHiddenField.Value + " ORDER BY MealTypeID";
        calendarDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        m_SQL = "SELECT SUM(MealCount) AS MealCountTotal, MealTypeName, DeliveryDate FROM vwDelivery WHERE DeliveryDate >= '" + firstDate.ToString("MM/dd/yyyy") + "' AND DeliveryDate < '" + lastDate.ToString("MM/dd/yyyy") + "' AND SiteID = " + SiteIDHiddenField.Value + " AND DeliveryTypeName <> 'Cancelled' GROUP BY MealTypeName, DeliveryDate";
        mealCountTotalDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        drpCalYear.SelectedIndex = drpCalYear.Items.IndexOf(drpCalYear.Items.FindByText(SiteDeliveryCalendar.VisibleDate.Year.ToString()));

        drpCalMonth.SelectedIndex = drpCalMonth.Items.IndexOf(drpCalMonth.Items.FindByText(SiteDeliveryCalendar.VisibleDate.ToString("MMMM")));

        PreviousMonthLinkButton.Text = SiteDeliveryCalendar.VisibleDate.AddMonths(-1).ToString("MMMM").Substring(0,3);

        PreviousMonthHiddenField.Value = SiteDeliveryCalendar.VisibleDate.AddMonths(-1).ToString("MMMM");

        NextMonthLinkButton.Text = SiteDeliveryCalendar.VisibleDate.AddMonths(1).ToString("MMMM").Substring(0,3);

        NextMonthHiddenField.Value = SiteDeliveryCalendar.VisibleDate.AddMonths(1).ToString("MMMM");
    }

    protected DateTime GetFirstDisplayedDayOfCalendar()
    {
        DateTime firstDisplayedDate = new DateTime(SiteDeliveryCalendar.VisibleDate.Year, SiteDeliveryCalendar.VisibleDate.Month, 1);

        if ((int)firstDisplayedDate.DayOfWeek != 1)
        {
            firstDisplayedDate = firstDisplayedDate.AddDays(-(int)firstDisplayedDate.DayOfWeek + 1);
        }

        return firstDisplayedDate;
    }

    protected void SiteDeliveryCalendar_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        GetCalendarData();
    }

    protected void ShowDeliveryQuestionPopup(string deliveryID)
    {
        DeliveryIDHiddenField.Value = deliveryID;

        EditRecurRadioButton.Checked = true;

        ExceptionRadioButton.Checked = false;

        GetCalendarData();

        ClientScript.RegisterStartupScript(GetType(), "key", "launchDeliveryQuestionPopupModal();", true);
    }

    public void SiteDeliveryCalendar_DayRender(object sender, DayRenderEventArgs e)
    {
        Boolean displayTotal = false;

        e.Day.IsSelectable = false;

        if (calendarDataSet != null)
        {
            foreach (DataRow dr in calendarDataSet.Tables[0].Rows)
            {
                if (Convert.ToDateTime(GCFDGlobals.dbGetValue(dr, "DeliveryDate")).ToString("MM/dd/yyyy") == e.Day.Date.ToString("MM/dd/yyyy"))
                {
                    displayTotal = true;
                    string linkText = "";
                    string linkAttribute = "";
                    string deliveryID = dr["DeliveryID"].ToString();
                    string scheduleTypeText = dr["ScheduleTypeName"].ToString() + " ";

                    if (scheduleTypeText == "N/A ")
                    {
                        scheduleTypeText = "";
                    }

                    switch (dr["DeliveryTypeName"].ToString())
                    {
                        case "Scheduled":
                            {
                                linkText = dr["MealTypeName"] + " Delivery";
                                
                                linkAttribute = "DeliveryQuestionButton";

                                break;
                            }
                        case "Cancelled":
                            {
                                linkText = "Cancelled " + dr["MealTypeName"] + " Delivery";

                                linkAttribute = "CalendarExceptionLinkButton";

                                break;
                            }
                        case "Rescheduled":
                            {
                                linkText = "Extra " + dr["MealTypeName"] + " Delivery";

                                linkAttribute = "CalendarExceptionLinkButton";

                                break;
                            }
                    }

                    linkText = scheduleTypeText + linkText;

                    LinkButton calendarLinkButton = new LinkButton();

                    e.Cell.Controls.Add(new LiteralControl("<br />"));
                    calendarLinkButton.Text = linkText;
                    calendarLinkButton.Font.Name = "Arial";
                    calendarLinkButton.Font.Bold = false;
                    calendarLinkButton.ForeColor = System.Drawing.Color.DarkTurquoise;
                    calendarLinkButton.Font.Size = FontUnit.Parse("8");
                    calendarLinkButton.ID = deliveryID + "LinkButton";
                    calendarLinkButton.Attributes.Add("href",
                                                        "javascript:__doPostBack('" + linkAttribute + "','" +
                                                        deliveryID + "')");

                    e.Cell.Controls.Add(calendarLinkButton);

                    Label forLabel = new Label();
                    forLabel.Text = "For";
                    forLabel.Font.Name = "Arial";
                    forLabel.Font.Bold = false;
                    forLabel.Font.Size = FontUnit.Parse("8");
                    forLabel.ID = "For" + deliveryID + "DeliveryLinkButton";

                    Label mealDayLabel = new Label();
                    mealDayLabel.Text = dr["ServingDay"] + " Serving Day";
                    mealDayLabel.Font.Name = "Arial";
                    mealDayLabel.Font.Bold = false;
                    mealDayLabel.Font.Size = FontUnit.Parse("8");
                    mealDayLabel.ID = deliveryID + "DeliveryDayLinkButton";

                    e.Cell.Controls.Add(new LiteralControl("<br />"));
                    e.Cell.Controls.Add(forLabel);
                    e.Cell.Controls.Add(new LiteralControl("<br />"));
                    e.Cell.Controls.Add(mealDayLabel);
                }
            }

            if (displayTotal)
            {
                DataRow[] drFiltered = mealCountTotalDataSet.Tables[0].Select("DeliveryDate = #" + e.Day.Date.ToString("MM/dd/yyyy") + "#");

                Boolean printLine = true;
                int mealCount = 1;

                for (int i = 0; i < drFiltered.Length; i++)
                {
                    if (printLine)
                    {
                        e.Cell.Controls.Add(new LiteralControl("<hr />"));

                        printLine = false;
                    }

                    Label mealCountLabel = new Label();
                    mealCountLabel.Text = GCFDGlobals.dbGetValue(drFiltered[i], "MealTypeName") + " Count = " + GCFDGlobals.dbGetValue(drFiltered[i], "MealCountTotal");
                    mealCountLabel.Font.Name = "Arial";
                    mealCountLabel.Font.Bold = false;
                    mealCountLabel.Font.Size = FontUnit.Parse("8");
                    mealCountLabel.ID = Convert.ToString(mealCount) + "MealCountButton";
                    e.Cell.Controls.Add(mealCountLabel);
                    e.Cell.Controls.Add(new LiteralControl("<br />"));

                    mealCount = mealCount + 1;
                }

                e.Cell.Controls.Add(new LiteralControl("<br />"));
            }
        }
    }

    /// <summary>
    /// Executes when a calendar exception link button is clicked
    /// </summary>
    /// <param name="arguement"></param>
    public void CalendarExceptionLinkButton_Click(string arguement)
    {
        m_SQL = "SELECT * FROM vwDelivery WHERE DeliveryID = " + arguement;
        DataSet deliveryDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        ParentDeliveryIDHiddenField.Value = "";

        DeleteExceptionButton.Enabled = true;
        AddExceptionButton.Enabled = true;

        string scheduleType = "";

        foreach (DataRow deliveryDataRow in deliveryDataSet.Tables[0].Rows)
        {
            DeliveryIDHiddenField.Value = GCFDGlobals.dbGetValue(deliveryDataRow, "DeliveryID");

            if (GCFDGlobals.dbGetValue(deliveryDataRow, "ScheduleTypeID") == "1")
            {
                scheduleType = " SFSP";
            }
            else if (GCFDGlobals.dbGetValue(deliveryDataRow, "ScheduleTypeID") == "2")
            {
                scheduleType = " CACFP";
            }

            if (!String.IsNullOrEmpty(GCFDGlobals.dbGetValue(deliveryDataRow, "GroupCancellationID")) && GCFDGlobals.dbGetValue(deliveryDataRow, "GroupCancellationID") != "0")
            {
                DeliveryEventIDHiddenTextBox.Value = GCFDGlobals.dbGetValue(deliveryDataRow, "GroupCancellationID");

                UpdateGroupExceptions(GCFDGlobals.dbGetValue(deliveryDataRow, "GroupCancellationID"));
            }
            else
            {
                if (GCFDGlobals.dbGetValue(deliveryDataRow, "DeliveryTypeName") == "Cancelled")
                {
                    DeliveryExceptionTypeLabel.Text = "Cancelled " + GCFDGlobals.dbGetValue(deliveryDataRow, "MealTypeName") + scheduleType + " Delivery";
                }
                else
                {
                    DeliveryExceptionTypeLabel.Text = "Extra " + GCFDGlobals.dbGetValue(deliveryDataRow, "MealTypeName") + scheduleType + " Delivery";
                }
                
                if (String.IsNullOrEmpty(GCFDGlobals.dbGetValue(deliveryDataRow, "ParentDeliveryID")) == false)
                {
                    ParentDeliveryIDHiddenField.Value = GCFDGlobals.dbGetValue(deliveryDataRow, "ParentDeliveryID");
                }

                DeliveryDateLabel.Text = Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryDataRow, "DeliveryDate")).ToShortDateString();

                DeliveryExceptionNotes.Text = GCFDGlobals.dbGetValue(deliveryDataRow, "Notes");           

                if (!String.IsNullOrEmpty(ParentDeliveryIDHiddenField.Value) && ParentDeliveryIDHiddenField.Value != "0")
                {
                    m_SQL = "SELECT * FROM Delivery WHERE DeliveryID = " + ParentDeliveryIDHiddenField.Value;
                    DataSet deliveryParentDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                    if (deliveryParentDataSet.Tables[0].Rows.Count == 0)
                    {
                        DeliveryCancellationTextBox.Text = "Delivery Not Rescheduled";
                    }
                    else
                    {
                        DeliveryCancellationTextBox.Text = Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryParentDataSet.Tables[0].Rows[0], "DeliveryDate")).ToShortDateString();
                    }
                }

                if (DeliveryCancellationTextBox.Text == "1/1/1900" || String.IsNullOrEmpty(DeliveryCancellationTextBox.Text))
                {
                    DeliveryCancellationTextBox.Text = "Delivery Not Rescheduled";
                }

                if (DeliveryExceptionTypeLabel.Text.Substring(0, 9) == "Cancelled")
                {
                    DeliveryEventDetailsLabelLabel.Text = "Delivery Rescheduled Date:";

                    DeliveryCancellationTextBox.Enabled = true;
                }
                else if (DeliveryExceptionTypeLabel.Text.Substring(0, 5) == "Extra")
                {
                    DeliveryEventDetailsLabelLabel.Text = "Due To Cancellation On:";

                    DeliveryCancellationTextBox.Enabled = false;

                    AddExceptionButton.Enabled = false;
                }

                string mealTypeID = GCFDGlobals.dbGetValue(deliveryDataRow, "MealTypeID");

                RetrieveMealTypeValues(mealTypeID);

                int formattedpermittedStartDate = Convert.ToInt32(permittedStartDate.ToString("yyyyMMdd"));;
                            
                if (prohibitPastModifications == "1")
                {
                    if (DeliveryExceptionTypeLabel.Text.Substring(0, 9) == "Cancelled" && Convert.ToInt32(Convert.ToDateTime(DeliveryDateLabel.Text).ToString("yyyyMMdd")) < formattedpermittedStartDate)
                    {
                        DeleteExceptionButton.Enabled = false;
                        AddExceptionButton.Enabled = false;
                    }
                    else if (DeliveryExceptionTypeLabel.Text.Substring(0, 5) == "Extra" && Convert.ToInt32(Convert.ToDateTime(DeliveryCancellationTextBox.Text).ToString("yyyyMMdd")) < formattedpermittedStartDate)
                    {
                        DeleteExceptionButton.Enabled = false;
                        AddExceptionButton.Enabled = false;
                    }
                }

                ClientScript.RegisterStartupScript(GetType(), "key", "launchModal();", true);
            }
        }

        GetCalendarData();
    }

    protected void UpdateGroupExceptions(string groupExceptionID)
    {
        string mealType = "";
        string mealTypeName = "";
        string servingDate;
        string rangeStartDate = "";
        string rangeEndDate = "";
        string listLine;
        string deliveryDate = "";
        string rescheduledDate = "";
        string scheduleTypeText = "";
        string scheduleType = "";

        m_SQL = "SELECT DISTINCT * FROM vwDelivery WHERE GroupCancellationID = " + groupExceptionID + " AND DeliveryTypeName = 'Cancelled' AND SiteID = " + SiteIDHiddenField.Value;
        DataSet m_GroupExceptionDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        mealType = GCFDGlobals.dbGetValue(m_GroupExceptionDataSet.Tables[0].Rows[0], "MealTypeID");
        rangeStartDate = Convert.ToDateTime(GCFDGlobals.dbGetValue(m_GroupExceptionDataSet.Tables[0].Rows[0], "CancellationStartDate")).ToString("MM/dd/yyyy");
        rangeEndDate = Convert.ToDateTime(GCFDGlobals.dbGetValue(m_GroupExceptionDataSet.Tables[0].Rows[0], "CancellationEndDate")).ToString("MM/dd/yyyy");
        rescheduledDate = Convert.ToDateTime(GCFDGlobals.dbGetValue(m_GroupExceptionDataSet.Tables[0].Rows[0], "RescheduledDate")).ToString("MM/dd/yyyy");
        mealTypeName = GCFDGlobals.dbGetValue(m_GroupExceptionDataSet.Tables[0].Rows[0], "MealTypeName");
        scheduleTypeText = GCFDGlobals.dbGetValue(m_GroupExceptionDataSet.Tables[0].Rows[0], "ScheduleTypeText");

        if (rescheduledDate == "01/01/1900")
        {
            RescheduleDateTextBox.Text = "Delivery Not Rescheduled";
        }
        else
        {
            RescheduleDateTextBox.Text = rescheduledDate;
        }

        RescheduleDateTextBox.Enabled = false;

        MealDeliveryTypeHiddenTextBox.Value = mealType;

        RemovedDeliverySiteList.Items.Clear();
        AddedDeliverySiteList.Items.Clear();

        RetrieveMealTypeValues(mealType);

        foreach (DataRow groupExceptionData in m_GroupExceptionDataSet.Tables[0].Rows)
        {
            servingDate = Convert.ToDateTime(GCFDGlobals.dbGetValue(groupExceptionData, "ServingDate")).ToString("MM/dd/yyyy");
            deliveryDate = Convert.ToDateTime(GCFDGlobals.dbGetValue(groupExceptionData, "DeliveryDate")).ToString("MM/dd/yyyy");
            scheduleType = GCFDGlobals.dbGetValue(groupExceptionData, "ScheduleTypeName");

            listLine = GCFDGlobals.dbGetValue(groupExceptionData, "SiteName") + " (" + deliveryDate + " Delivery Date|" + servingDate + " Serving Date|" + scheduleType + ")";

            AddedDeliverySiteList.Items.Add(new ListItem(listLine, GCFDGlobals.dbGetValue(groupExceptionData, "DeliveryID")));
            
            if (prohibitPastModifications == "1" && Convert.ToInt32(Convert.ToDateTime(deliveryDate).ToString("yyyyMMdd")) < Convert.ToInt32(System.DateTime.Now.ToString("yyyyMMdd")))
            {
                ListItem i = AddedDeliverySiteList.Items.FindByValue(GCFDGlobals.dbGetValue(groupExceptionData, "DeliveryID"));

                i.Attributes.Add("style", "color:gray;");
                i.Attributes.Add("disabled", "true");
            }
        }
        
        m_SQL = "SELECT * FROM vwDelivery WHERE DeliveryDate BETWEEN '" + rangeStartDate + "' AND '" + rangeEndDate + "' AND MealTypeName = '" + mealTypeName + "' AND DeliveryTypeName = 'Scheduled' AND SiteID = " + SiteIDHiddenField.Value;
        m_DeliveriesDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        foreach (DataRow deliveryDataRow in m_DeliveriesDataSet.Tables[0].Rows)
        {
            servingDate = Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryDataRow, "ServingDate")).ToString("MM/dd/yyyy");
            deliveryDate = Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryDataRow, "DeliveryDate")).ToString("MM/dd/yyyy");
            scheduleType = GCFDGlobals.dbGetValue(deliveryDataRow, "ScheduleTypeName");

            listLine = GCFDGlobals.dbGetValue(deliveryDataRow, "SiteName") + " (" + deliveryDate + " Delivery Date|" + servingDate + " Serving Date|" + scheduleType + ")";

            RemovedDeliverySiteList.Items.Add(new ListItem(listLine, GCFDGlobals.dbGetValue(deliveryDataRow, "DeliveryID")));

            if (prohibitPastModifications == "1" && Convert.ToInt32(Convert.ToDateTime(deliveryDate).ToString("yyyyMMdd")) < Convert.ToInt32(System.DateTime.Now.ToString("yyyyMMdd")))
            {
                ListItem i = RemovedDeliverySiteList.Items.FindByValue(GCFDGlobals.dbGetValue(deliveryDataRow, "DeliveryID"));

                i.Attributes.Add("style", "color:gray;");
                i.Attributes.Add("disabled", "true");
            }
        }

        AddedDeliverySiteList.Rows = AddedDeliverySiteList.Items.Count;
        RemovedDeliverySiteList.Rows = AddedDeliverySiteList.Items.Count;

        RemovedDeliverySiteList.Width = AddedDeliverySiteList.Width;

        CancellationRangeStartDateLabel.Text = rangeStartDate;
        CancellationRangeEndDateLabel.Text = rangeEndDate;
        CancellationMealTypeLabel.Text = mealTypeName;
        CancellationRangeScheduleTypeLabel.Text = scheduleTypeText;

        UpdateListBoxHeightWidth();

        GetCalendarData();

        ClientScript.RegisterStartupScript(GetType(), "key", "launchGroupCancellationModal();", true);
    }

    protected void SaveSiteChangesButton_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if ((SiteModeHiddenField.Value == "New" && CreateItem() == true) || (SiteModeHiddenField.Value == "Update" && SiteNameTextBox.Text != OriginalSiteNameHiddenField.Value && CreateItem() == true) || (SiteModeHiddenField.Value == "Update" && SiteNameTextBox.Text == OriginalSiteNameHiddenField.Value))
            {
                SqlConnection m_SqlConnection = (SqlConnection)GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbConnection;

                SqlCommand cmd = m_SqlConnection.CreateCommand();

                cmd.Parameters.Add(new SqlParameter("@SiteID", SiteIDHiddenField.Value));
                cmd.Parameters.Add(new SqlParameter("@SiteName", SiteNameTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SitePhone", SitePhoneTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SiteAddress", SiteAddressTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SiteCity", SiteCityTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SiteZip", ZipCodeTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@DeliveryContactName", DeliveryContactNameTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@DeliveryContactPhone", DeliveryContactPhoneTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SiteRoute", RouteDropDownList.SelectedItem.Text));
                cmd.Parameters.Add(new SqlParameter("@SiteType", SiteTypeDropDownListBox.SelectedItem.Text));

                cmd.Parameters.Add(new SqlParameter("@CACFPPrimaryContactName", CACFPPrimaryContactNameTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPPrimaryContactPhone", CACFPPrimaryContactPhoneTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPPrimaryContactEmail", CACFPPrimaryContactEmailTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPAltContactName", CACFPAltContactNameTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPAltContactPhone", CACFPAltContactPhoneTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPAltContactEmail", CACFPAltContactEmailTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPFax", CACFPFaxTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPEmergencyContactName", CACFPEmergencyContactNameTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPEmergencyContactPhone", CACFPEmergencyContactPhoneTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPEmergencyContactNotes", CACFPEmergencyContactNotesTextBox.Text));

                cmd.Parameters.Add(new SqlParameter("@SFSPPrimaryContactName", SFSPPrimaryContactNameTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPPrimaryContactPhone", SFSPPrimaryContactPhoneTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPPrimaryContactEmail", SFSPPrimaryContactEmailTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPAltContactName", SFSPAltContactNameTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPAltContactPhone", SFSPAltContactPhoneTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPAltContactEmail", SFSPAltContactEmailTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPFax", SFSPFaxTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPEmergencyContactName", SFSPEmergencyContactNameTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPEmergencyContactPhone", SFSPEmergencyContactPhoneTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPEmergencyContactNotes", SFSPEmergencyContactNotesTextBox.Text));

                cmd.Parameters.Add(new SqlParameter("@AgencyID", AgencyIDTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@ProgramID", ProgramIDTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@ISBECode", ISBETextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@FEINNo", FEINTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@LAHSiteCode", LAHCodeTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@NearestSchool", NearestSchoolTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@UnmetNeed", UnmetNeedTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CommunityArea", CommunityAreaTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SiteNotes", NotesTextBox.Text));

                cmd.Parameters.Add(new SqlParameter("@CACFPPreOpVisit", CACFPPreOpVisitTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPFirstWeekVisit", CACFPFirstWeekVisitTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPFirstMonthVisit", CACFPFirstMonthVisitTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPVisit1", Visit1TextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPVisit2", Visit2TextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPAdditionalVisits", CACFPAdditionalVisitsTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPStartDate", CACFPStartDateTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPEndDate", CACFPEndDateTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPServingDays", CACFPServingDaysTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPServingTime", CACFPServingTimeTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPDatesofNoService", CACFPDatesofNoServiceTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPTrainingDate", CACFPTrainingDateTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPTrainees", CACFPTraineesTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPNotes", CACFPNotesTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@CACFPMonitor", CACFPMonitorTextBox.Text));

                cmd.Parameters.Add(new SqlParameter("@SFSPPreOpVisit", SFSPPreOpVisitTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPFirstWeekVisit", SFSPFirstWeekVisitTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPFirstMonthVisit", SFSPFirstMonthVisitTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPAdditionalVisits", SFSPAdditionalVisitsTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPStartDate", SFSPStartDateTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPEndDate", SFSPEndDateTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPLunchServingDays", SFSPLunchServingDaysTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPLunchServingTime", SFSPLunchServingTimeTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPBreakfastServingDays", SFSPBreakfastServingDaysTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPBreakfastServingTime", SFSPBreakfastServingTimeTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPDatesofNoService", SFSPDatesOfNoServiceTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPTrainingDate", SFSPTrainingDateTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPTrainees", SFSPTraineesTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPNotes", SFSPNotesTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@SFSPMonitor", SFSPMonitorTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@Active", ActiveCheckBox.Checked.ToString()));

                cmd.CommandText = "spUpdateSite";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                m_SQL = "INSERT INTO UserAction (UserName, Action) VALUES('" + User.Identity.Name + "', 'Updated Site Information For " + SiteNameTextBox.Text + "')";
                GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                GetCalendarData();

                SiteModeHiddenField.Value = "Update";

                PreviousSiteButton.Enabled = true;
                NextSiteButton.Enabled = true;
                DeleteSiteButton.Enabled = true;
                OriginalSiteNameHiddenField.Value = SiteNameTextBox.Text;

                DeleteSiteButton.Attributes.Add("onclick", "return confirm('Are you sure you want to PERMANENTLY DELETE all data associated with the " + SiteNameTextBox.Text + " site?')");

                MessageBox.Show("The " + SiteNameTextBox.Text + " Site Has Been Saved");
            }
            else
            {
                MessageBox.Show("There already is a site within the system with the same name as the site you're attempting to currently save. Change the current site's name before attempting to save.");
            }
        }
    }

    protected Boolean CreateItem()
    {
        Boolean createItem = true;

        m_SQL = "SELECT * FROM Site WHERE SiteName = '" + SiteNameTextBox.Text + "'";
        DataSet siteNameTestDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        if (siteNameTestDataSet.Tables.Count > 0)
        {
            if (siteNameTestDataSet.Tables[0].Rows.Count > 0)
            {
                createItem = false;
            }
            else
            {
                createItem = true;
            }
        }
        else
        {
            createItem = true;
        }

        return createItem;
    }

    protected void CancelSiteChangesButton_Click(object sender, EventArgs e)
    {
        m_SQL = "DELETE FROM Site WHERE SiteID = 'Enter Site Name Here'";
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

        GetCalendarData();

        Response.Redirect("Sites.aspx", false);
    }

    protected void DeleteSiteButton_Click(object sender, EventArgs e)
    {
        m_SQL = "DELETE Site WHERE SiteID = " + SiteIDHiddenField.Value;
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

        m_SQL = "INSERT INTO UserAction (UserName, Action) VALUES('" + User.Identity.Name + "', 'Deleted Site " + SiteNameTextBox.Text + "')";
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

        GetCalendarData();

        Response.Redirect("Sites.aspx", false);
    }

    /// <summary>
    /// Executes when the save/add exception button is clicked on the exceptions panel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddExceptionButton_Click(object sender, EventArgs e)
    {        
        string parentDeliveryID = "";
        string exceptionDate = "";

        m_SQL = "SELECT * FROM vwDelivery WHERE DeliveryID = " + DeliveryIDHiddenField.Value;
        DataSet deliveryDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        string deliveryRecurrenceDetailID = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "DeliveryRecurrenceDetailID");
        string servingDate = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "ServingDate");
        string mealTypeID = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "MealTypeID");

        RetrieveMealTypeValues(mealTypeID);

        if (prohibitPastModifications == "1" && Convert.ToInt32(Convert.ToDateTime(DeliveryDateLabel.Text).ToString("yyyyMMdd")) < Convert.ToInt32(System.DateTime.Now.ToString("yyyyMMdd")))
        {
            MessageBox.Show("A delivery earlier than " + System.DateTime.Now.AddDays(Convert.ToInt16(servingDayInternal)).ToString("MM/dd/yyyy") +  " cannot be cancelled.");
        }
        else
        {
            if (!String.IsNullOrEmpty(ParentDeliveryIDHiddenField.Value) && DeliveryExceptionTypeLabel.Text.Substring(0, 9) == "Cancelled")
            {
                m_SQL = "DELETE Delivery WHERE DeliveryID = " + ParentDeliveryIDHiddenField.Value;
                GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
            }

            if (String.IsNullOrEmpty(DeliveryCancellationTextBox.Text))
            {
                exceptionDate = "1/1/1900";
            }
            else
            {
                exceptionDate = DeliveryCancellationTextBox.Text;
            }

            m_SQL = "INSERT INTO Delivery (DeliveryRecurrenceDetailID, DeliveryTypeID, ParentDeliveryID, DeliveryDate, ServingDate) VALUES(" + deliveryRecurrenceDetailID + ", 2, " + DeliveryIDHiddenField.Value + ", '" + exceptionDate + "', '" + servingDate + "')";
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

            m_SQL = "SELECT IDENT_CURRENT('Delivery') AS 'DeliveryID'";
            deliveryDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            parentDeliveryID = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "DeliveryID");

            m_SQL = "UPDATE Delivery SET DeliveryTypeID = 3, ParentDeliveryID = " + parentDeliveryID + " WHERE DeliveryID = " + DeliveryIDHiddenField.Value;
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

            m_SQL = "DELETE FROM MealDelivery WHERE DeliveryID = " + DeliveryIDHiddenField.Value;
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

            m_SQL = "INSERT INTO UserAction (UserName, Action) VALUES('" + User.Identity.Name + "', '" + DeliveryExceptionTypeLabel.Text + " On " + DeliveryDateLabel.Text + " For " + SiteNameTextBox.Text + "')";
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
        }

        ParentDeliveryIDHiddenField.Value = "";
        DeliveryIDHiddenField.Value = "";

        GetCalendarData();
    }

    /// <summary>
    /// Executes when an the delete exception button is clicked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DeleteExceptionButton_Click(object sender, EventArgs e)
    {
        m_SQL = "SELECT * FROM vwDelivery WHERE DeliveryID = " + DeliveryIDHiddenField.Value;
        DataSet deliveryDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        string mealTypeID = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "MealTypeID");

         RetrieveMealTypeValues(mealTypeID);

        if (prohibitPastModifications == "1" && Convert.ToInt32(Convert.ToDateTime(DeliveryDateLabel.Text).ToString("yyyyMMdd")) < Convert.ToInt32(System.DateTime.Now.ToString("yyyyMMdd")))
        {
            MessageBox.Show("A delivery earlier than " + System.DateTime.Now.AddDays(Convert.ToInt16(servingDayInternal)).ToString("MM/dd/yyyy") + " cannot be cancelled.");
        }
        else
        {
            if (!String.IsNullOrEmpty(ParentDeliveryIDHiddenField.Value) && DeliveryExceptionTypeLabel.Text.Substring(0, 9) == "Cancelled")
            {
                m_SQL = "DELETE FROM Delivery WHERE DeliveryID = " + ParentDeliveryIDHiddenField.Value;
                GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                m_SQL = "UPDATE Delivery SET DeliveryTypeID = 1 WHERE DeliveryID = " + DeliveryIDHiddenField.Value;
                GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
            }
            else if (DeliveryExceptionTypeLabel.Text.Substring(0, 9) == "Cancelled")
            {
                m_SQL = "UPDATE Delivery SET DeliveryTypeID = 1 WHERE DeliveryID = " + DeliveryIDHiddenField.Value;
                GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
            }
            else
            {
                m_SQL = "DELETE FROM Delivery WHERE DeliveryID = " + DeliveryIDHiddenField.Value;
                GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                m_SQL = "UPDATE Delivery SET DeliveryTypeID = 1 WHERE DeliveryID = " + ParentDeliveryIDHiddenField.Value;
                GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
            }

            m_SQL = "INSERT INTO UserAction (UserName, Action) VALUES('" + User.Identity.Name + "', 'Deleted Cancelled Delivery On " + DeliveryDateLabel.Text + " For " + SiteNameTextBox.Text + "')";
          GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
        }

        ParentDeliveryIDHiddenField.Value = "";
        DeliveryIDHiddenField.Value = "";

        GetCalendarData();
    }

    protected void MainDeliveryCalendar_Click(object sender, EventArgs e)
    {
        Response.Redirect("DeliveryCalendar.aspx", false);
    }

    /// <summary>
    /// Displays recurrence panel for new delivery recurrence creation
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CreateNewRecurrenceButton_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        
        if (SiteModeHiddenField.Value == "New")
        {
            MessageBox.Show("Please Save Site Information Before Scheduling Meals");
        }
        else
        {
            ScheduleTypeLabel.Visible = false;
            CACFPRadioButton.Visible = true;
            SFSPRadioButton.Visible = true;

            RecurrenceStartDateTextBox.Enabled= true;

            RecurrenceEndDateTextBox.Enabled = true;

            RecurrenceModeHiddenField.Value = "New";

            DeliveryRecurrenceDetailPanel.Visible = false;

            SaveDeliveryRecurrenceButton.Text = "Add Recurrence Details";

            MealTypeDropDownList.Visible = true;

            RecurrenceMealTypeLabel.Visible = false;
            
            RecurrenceStartDateTextBox.Text = DateTime.Now.ToString("MM/dd/yyyy");

            DeleteRecurrenceButton.Enabled = false;

            RecurrenceEndDateTextBox.Text = "";

            MealTypeDropDownList.DataSource = null;
            MealTypeDropDownList.DataBind();            

            m_SQL = "SELECT * FROM MealTypeDict";
            DataSet m_MealType = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            MealTypeDropDownList.DataValueField = "MealTypeID";
            MealTypeDropDownList.DataTextField = "MealTypeName";
            MealTypeDropDownList.DataSource = m_MealType.Tables[0];
            MealTypeDropDownList.DataBind();
            MealTypeDropDownList.Items.Insert(0, new ListItem("SELECT MEAL TYPE", "default"));

            if (System.DateTime.Now.AddDays(2).DayOfWeek.ToString() == "Saturday")
            {
                RecurrenceStartDateTextBox.Text = System.DateTime.Now.AddDays(4).ToString("MM/dd/yyyy");
            }
            else if (System.DateTime.Now.AddDays(2).DayOfWeek.ToString() == "Sunday")
            {
                RecurrenceStartDateTextBox.Text = System.DateTime.Now.AddDays(3).ToString("MM/dd/yyyy");
            }
            else
            {
                RecurrenceStartDateTextBox.Text = System.DateTime.Now.AddDays(2).ToString("MM/dd/yyyy");
            }

            GetCalendarData();

            ClientScript.RegisterStartupScript(GetType(), "key", "launchDeliveryRecurrencePopupModal();", true);
        }
    }

    /// <summary>
    /// Executes when the save/edit button is clicked on the recurrence panel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void SaveDeliveryRecurrenceButton_Click(object sender, EventArgs e)
    {
        int startDate = Convert.ToInt32(Convert.ToDateTime(RecurrenceStartDateTextBox.Text).ToString("yyyyMMdd"));

        int endDate = Convert.ToInt32(Convert.ToDateTime(RecurrenceEndDateTextBox.Text).ToString("yyyyMMdd"));
        int scheduleType = 0;

        if (CACFPRadioButton.Checked)
        {
            scheduleType = 2;

            ScheduleTypeLabel.Text = "CACFP";
        }
        else if (SFSPRadioButton.Checked)
        {
            scheduleType = 1;

            ScheduleTypeLabel.Text = "SFSP";
        }

        if (scheduleType != 0)
        {
            if (startDate <= endDate)
            {
                if (RecurrenceModeHiddenField.Value == "New")
                {
                    RetrieveMealTypeValues(MealTypeDropDownList.SelectedItem.Value);

                    if ((prohibitPastModifications == "1") && (Convert.ToInt32(Convert.ToDateTime(RecurrenceStartDateTextBox.Text).ToString("yyyyMMdd")) < Convert.ToInt32(permittedStartDate.ToString("yyyyMMdd")) || (Convert.ToInt32(Convert.ToDateTime(RecurrenceEndDateTextBox.Text).ToString("yyyyMMdd")) < Convert.ToInt32(permittedStartDate.ToString("yyyyMMdd")))))
                    {
                        RecurrenceStartDateTextBox.Text = permittedStartDate.ToString("MM/dd/yyyy");

                        RecurrenceEndDateTextBox.Text = "";

                        if (hasWeekendActivity == "0")
                        {
                            MessageBox.Show("The start/end date of the recurrence must be a date " + Convert.ToString(futureActivityInterval) + " business days from the current date for this meal type.");
                        }
                        else
                        {
                            MessageBox.Show("The start/end date of the recurrence must be a date " + Convert.ToString(futureActivityInterval) + " days from the current date for this meal type.");
                        }
                    }
                    else
                    {
                        CACFPRadioButton.Visible = false;
                        SFSPRadioButton.Visible = false;
                        ScheduleTypeLabel.Visible = true;

                        string recurrenceLastModified = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                        m_SQL = "DECLARE @NewDeliveryRecurrenceID int EXECUTE spDeliveryRecurrence '" + User.Identity.Name + "', 0, " + MealTypeDropDownList.SelectedItem.Value + ", " + scheduleType + ", " + SiteIDHiddenField.Value + ", '" + RecurrenceStartDateTextBox.Text + "', '" + RecurrenceEndDateTextBox.Text + "', '" + recurrenceLastModified + "', @NewDeliveryRecurrenceID = @NewDeliveryRecurrenceID OUTPUT SELECT @NewDeliveryRecurrenceID as 'DeliveryRecurrenceID'";
                        DataSet m_DeliveryRecurrenceDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                        DeliveryRecurrenceIDHiddenField.Value = GCFDGlobals.dbGetValue(m_DeliveryRecurrenceDataSet.Tables[0].Rows[0], "DeliveryRecurrenceID");

                        m_SQL = "SELECT LastModified FROM DeliveryRecurrence WHERE DeliveryRecurrenceID = " + DeliveryRecurrenceIDHiddenField.Value;
                        m_DeliveryRecurrenceDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                        RecurrenceLastModifiedHiddenField.Value = GCFDGlobals.dbGetValue(m_DeliveryRecurrenceDataSet.Tables[0].Rows[0], "LastModified");

                        DeliveryRecurrenceDetailPanel.Visible = true;

                        RecurrenceModeHiddenField.Value = "Update";

                        MealTypeDropDownList.Visible = false;

                        RecurrenceMealTypeLabel.Visible = true;

                        RecurrenceMealTypeLabel.Text = MealTypeDropDownList.SelectedItem.Text;

                        DeliveryRecurrenceDetailGridView.EditIndex = 0;

                        RecurrenceDetailModeHiddenField.Value = "New";

                        FillDeliveryRecurrenceDetailInGrid(DeliveryRecurrenceIDHiddenField.Value);

                        SaveDeliveryRecurrenceButton.Text = "Save Start/End Date Changes";

                        DataTable dt = ((DataTable)DeliveryRecurrenceDetailGridView.DataSource);

                        DataRow dataRow = dt.NewRow();

                        dataRow["DeliveryRecurrenceDetailID"] = "0";
                        dataRow["MealCount"] = "0";
                        dataRow["DeliveryDay"] = "Monday";

                        string mealType = RecurrenceMealTypeLabel.Text;

                        switch (servingDayInternal)
                        {
                            case "0":
                                dataRow["ServingDay"] = "Monday";

                                break;
                            case "1":
                                dataRow["ServingDay"] = "Tuesday";

                                break;
                            case "2":
                                dataRow["ServingDay"] = "Wednesday";

                                break;
                            case "3":
                                dataRow["ServingDay"] = "Thursday";

                                break;
                            case "4":
                                dataRow["ServingDay"] = "Friday";

                                break;
                            case "5":
                                dataRow["ServingDay"] = "Saturday";

                                break;
                            case "6":
                                dataRow["ServingDay"] = "Sunday";

                                break;
                        }

                        dataRow["LastModified"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                        dt.Rows.Add(dataRow);

                        DeliveryRecurrenceDetailGridView.DataBind();

                        int index = DeliveryRecurrenceDetailGridView.Rows.Count - 1;

                        GridViewRow row = DeliveryRecurrenceDetailGridView.Rows[index];

                        DropDownList deliveryDay = (DropDownList)row.FindControl("DeliveryDayDropDownList");

                        deliveryDay.Attributes["onchange"] = "setDefaultServingDay('" + deliveryDay.ClientID + "');";
                    }
                }
                else
                {
                    CACFPRadioButton.Visible = false;
                    SFSPRadioButton.Visible = false;
                    ScheduleTypeLabel.Visible = true;

                    m_SQL = "DECLARE @NewDeliveryRecurrenceID int EXECUTE spDeliveryRecurrence '" + User.Identity.Name + "', " + DeliveryRecurrenceIDHiddenField.Value + ", " + MealTypeIDHiddenField.Value + ", " + scheduleType + ", " + SiteIDHiddenField.Value + ", '" + RecurrenceStartDateTextBox.Text + "', '" + RecurrenceEndDateTextBox.Text + "', '" + RecurrenceLastModifiedHiddenField.Value + "', @NewDeliveryRecurrenceID = @NewDeliveryRecurrenceID OUTPUT SELECT @NewDeliveryRecurrenceID as 'DeliveryRecurrenceID'";
                    DataSet m_DeliveryRecurrenceDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                    DeliveryRecurrenceIDHiddenField.Value = GCFDGlobals.dbGetValue(m_DeliveryRecurrenceDataSet.Tables[0].Rows[0], "DeliveryRecurrenceID");

                    MessageBox.Show("The start/end date change of the recurrence has been saved.");
                }
            }
            else
            {
                MessageBox.Show("The start date of the recurrence must be a date before or equal to the end date of the recurrence");
            }
        }
        else
        {
            MessageBox.Show("A Schedule Type(CACFP/SFSP) Needs To Be Choosen Before Creating The Delivery Recurrence.");
        }

        ClientScript.RegisterStartupScript(GetType(), "key", "launchDeliveryRecurrencePopupModal();", true);

        GetCalendarData();
    }

    protected void DeleteRecurrenceButton_Click(object sender, EventArgs e)
    {
        m_SQL = "EXECUTE spDeleteDeliveryRecurrence " + DeliveryRecurrenceIDHiddenField.Value;
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

        RecurrencePopupExtender.Hide();

        DateTime visibleDate = new DateTime(Convert.ToInt32(drpCalYear.SelectedItem.Text), Convert.ToInt32(drpCalMonth.SelectedItem.Value), 1);

        Response.Redirect("SiteDetails.aspx?SiteID=" + SiteIDHiddenField.Value + "&SiteMode=Update&CalendarDate=" + visibleDate.ToString(), false);
    }
    
    protected void Populate_MonthList()
    {
        //Add each month to the list
        drpCalMonth.Items.Add(new ListItem("January","1"));
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
        for(int intYear = DateTime.Now.Year - 20; intYear <= DateTime.Now.Year + 20; intYear++)
        {           
            drpCalYear.Items.Add(new ListItem(intYear.ToString(),intYear.ToString()));
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

        SiteDeliveryCalendar.VisibleDate = visibleDate;
        SiteDeliveryCalendar.TodaysDate = visibleDate;
    }

    protected void drpCalYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Set_Calendar();

        GetCalendarData();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string mealType = MealDeliveryTypeHiddenTextBox.Value;

        for (int i = 0; i < AddedDeliverySiteList.Items.Count; i++)
        {
            if (AddedDeliverySiteList.Items[i].Selected)
            {
                string s = AddedDeliverySiteList.SelectedItem.Value;
                string t = AddedDeliverySiteList.SelectedItem.Text;

                RemovedDeliverySiteList.Items.Add(new ListItem(t, s));

                AddedDeliverySiteList.Items.Remove(new ListItem(t, s));

                i--;
            }
        }
        
        if (AddedDeliverySiteList.Items.Count > 0)
        {
            for (int i = 0; i < AddedDeliverySiteList.Items.Count; i++)
            {
                m_SQL = "SELECT DeliveryDate FROM vwDelivery WHERE DeliveryID = " + AddedDeliverySiteList.Items[i].Value;
                m_DeliveriesDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                RetrieveMealTypeValues(mealType);

                if (prohibitPastModifications == "1" && Convert.ToInt32(Convert.ToDateTime(GCFDGlobals.dbGetValue(m_DeliveriesDataSet.Tables[0].Rows[0], "DeliveryDate")).ToString("yyyyMMdd")) < Convert.ToInt32(System.DateTime.Now.ToString("yyyyMMdd")))
                {
                    ListItem li = AddedDeliverySiteList.Items.FindByValue(AddedDeliverySiteList.Items[i].Value);

                    li.Attributes.Add("style", "color:gray;");
                    li.Attributes.Add("disabled", "true");
                }
            }
        }

        UpdateListBoxHeightWidth();

        if (RemovedDeliverySiteList.Items.Count > 0)
        {
            for (int i = 0; i < RemovedDeliverySiteList.Items.Count; i++)
            {
                m_SQL = "SELECT DeliveryDate FROM vwDelivery WHERE DeliveryID = " + RemovedDeliverySiteList.Items[i].Value;
                m_DeliveriesDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                RetrieveMealTypeValues(mealType);

                if (prohibitPastModifications == "1" && Convert.ToInt32(Convert.ToDateTime(GCFDGlobals.dbGetValue(m_DeliveriesDataSet.Tables[0].Rows[0], "DeliveryDate")).ToString("yyyyMMdd")) < Convert.ToInt32(System.DateTime.Now.ToString("yyyyMMdd")))
                {
                    ListItem li = RemovedDeliverySiteList.Items.FindByValue(RemovedDeliverySiteList.Items[i].Value);

                    li.Attributes.Add("style", "color:gray;");
                    li.Attributes.Add("disabled", "true");
                }
            }
        }

        GetCalendarData();

        ClientScript.RegisterStartupScript(GetType(), "key", "launchGroupCancellationModal();", true);
    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        string mealType = MealDeliveryTypeHiddenTextBox.Value;

        for (int i = 0; i < RemovedDeliverySiteList.Items.Count; i++)
        {

            if (RemovedDeliverySiteList.Items.Count == 1)
            {
                i = 0;
            }

            if (RemovedDeliverySiteList.Items[i].Selected)
            {
                string s = RemovedDeliverySiteList.SelectedItem.Value;
                string t = RemovedDeliverySiteList.SelectedItem.Text;

                AddedDeliverySiteList.Items.Add(new ListItem(t, s));

                RemovedDeliverySiteList.Items.Remove(new ListItem(t, s));
            }
        }

        if (RemovedDeliverySiteList.Items.Count > 0)
        {
            for (int i = 0; i < RemovedDeliverySiteList.Items.Count; i++)
            {
                m_SQL = "SELECT DeliveryDate FROM vwDelivery WHERE DeliveryID = " + RemovedDeliverySiteList.Items[i].Value;
                m_DeliveriesDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                RetrieveMealTypeValues(mealType);

                if (prohibitPastModifications == "1" && Convert.ToInt32(Convert.ToDateTime(GCFDGlobals.dbGetValue(m_DeliveriesDataSet.Tables[0].Rows[0], "DeliveryDate")).ToString("yyyyMMdd")) < Convert.ToInt32(System.DateTime.Now.ToString("yyyyMMdd")))
                {
                    ListItem li = RemovedDeliverySiteList.Items.FindByValue(RemovedDeliverySiteList.Items[i].Value);

                    li.Attributes.Add("style", "color:gray;");
                    li.Attributes.Add("disabled", "true");
                }
            }
        }

        UpdateListBoxHeightWidth();

        if (AddedDeliverySiteList.Items.Count > 0)
        {
            for (int i = 0; i < AddedDeliverySiteList.Items.Count; i++)
            {
                m_SQL = "SELECT DeliveryDate FROM vwDelivery WHERE DeliveryID = " + AddedDeliverySiteList.Items[i].Value;
                m_DeliveriesDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                RetrieveMealTypeValues(mealType);

                if (prohibitPastModifications == "1" && Convert.ToInt32(Convert.ToDateTime(GCFDGlobals.dbGetValue(m_DeliveriesDataSet.Tables[0].Rows[0], "DeliveryDate")).ToString("yyyyMMdd")) < Convert.ToInt32(System.DateTime.Now.ToString("yyyyMMdd")))
                {
                    ListItem li = AddedDeliverySiteList.Items.FindByValue(AddedDeliverySiteList.Items[i].Value);

                    li.Attributes.Add("style", "color:gray;");
                    li.Attributes.Add("disabled", "true");
                }
            }
        }

        GetCalendarData();

        ClientScript.RegisterStartupScript(GetType(), "key", "launchGroupCancellationModal();", true);
    }

    protected void UpdateListBoxHeightWidth()
    {
        int nItem;
        int tItem;

        if (AddedDeliverySiteList.Items.Count < 5)
        {
            nItem = 109;
        }
        else
        {
            nItem = Convert.ToInt32(AddedDeliverySiteList.Items.Count * 22);
        }

        if (AddedDeliverySiteList.Items.Count == 0)
        {
            AddedDeliverySiteList.Width = 400; //This will ensure the list item won't be shrinked!
        }

        AddedDeliverySiteList.Height = nItem; //Set height depends on the font size.
        

        if (RemovedDeliverySiteList.Items.Count < 5)
        {
            tItem = 109;
        }
        else
        {
            tItem = Convert.ToInt32(RemovedDeliverySiteList.Items.Count * 22);
        }

        if (AddedDeliverySiteList.Items.Count == 0)
        {
            RemovedDeliverySiteList.Width = 400; //This will ensure the list item won't be shrinked!
        }

        RemovedDeliverySiteList.Height = tItem; //Set height depends on the font size.        
    }
   
    protected void CreateRangeCancellationButton_Click(object sender, EventArgs e)
    {
        if (SiteModeHiddenField.Value == "New")
        {
            MessageBox.Show("Please Save Site Information Before Canceling Meals.");
        }
        else
        {
            m_SQL = "SELECT * FROM MealTypeDict";
            DataSet m_MealDeliveryType = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            DeliveryEventIDHiddenTextBox.Value = "";
            MealDeliveryTypeHiddenTextBox.Value = "";
            MealDeliveryTypeDropDownList.DataValueField = "MealTypeID";
            MealDeliveryTypeDropDownList.DataTextField = "MealTypeName";
            MealDeliveryTypeDropDownList.DataSource = m_MealDeliveryType.Tables[0];
            MealDeliveryTypeDropDownList.DataBind();
            MealDeliveryTypeDropDownList.Items.Insert(0, new ListItem("SELECT MEAL TYPE", "-1"));

            GetCalendarData();

            ClientScript.RegisterStartupScript(GetType(), "key", "launchContinueModal();", true);
        }
    }

    protected void AddGroupExceptions()
    {
        string mealType = MealDeliveryTypeDropDownList.SelectedItem.Value;
        string scheduleType = "";

        if (CACFPCheckbox.Checked)
        {
            scheduleType = "2";
        }

        if (SFSPCheckbox.Checked)
        {
            if (String.IsNullOrEmpty(scheduleType) == true)
            {
                scheduleType = "1";
            }
            else
            {
                scheduleType = scheduleType + ",1";
            }
        }

        if (NACheckbox.Checked)
        {
            if (String.IsNullOrEmpty(scheduleType) == true)
            {
                scheduleType = "3";
            }
            else
            {
                scheduleType = scheduleType + ",3";
            }
        }

        string scheduleTypeName;
        string servingDate;
        string listLine;
        string deliveryDate = "";

        MealDeliveryTypeHiddenTextBox.Value = MealDeliveryTypeDropDownList.SelectedItem.Value;

        m_SQL = "SELECT * FROM vwDelivery WHERE DeliveryDate BETWEEN '" + CancellationRangeStartDateTextBox.Text + "' AND '" + CancellationRangeEndDateTextBox.Text + "' AND DeliveryTypeName <> 'Cancelled' AND MealTypeID = " + mealType + " AND ScheduleTypeID IN(" + scheduleType + ") AND SiteID = " + SiteIDHiddenField.Value;
        m_DeliveriesDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        RemovedDeliverySiteList.Items.Clear();
        AddedDeliverySiteList.Items.Clear();

        foreach (DataRow deliveryDataRow in m_DeliveriesDataSet.Tables[0].Rows)
        {
            servingDate = Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryDataRow, "ServingDate")).ToString("MM/dd/yyyy");
            deliveryDate = Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryDataRow, "DeliveryDate")).ToString("MM/dd/yyyy");
            scheduleTypeName = GCFDGlobals.dbGetValue(deliveryDataRow, "ScheduleTypeName");

            listLine = GCFDGlobals.dbGetValue(deliveryDataRow, "SiteName") + " (" + deliveryDate + " Delivery Date|" + servingDate + " Serving Date|" + scheduleTypeName +")";

            AddedDeliverySiteList.Items.Add(new ListItem(listLine, GCFDGlobals.dbGetValue(deliveryDataRow, "DeliveryID")));
        }    
    }

    protected void ContinueButton_Click(object sender, EventArgs e)
    {
        RescheduleDateTextBox.Text = "";

        AddGroupExceptions();

        string scheduleType = "";

        if (CACFPCheckbox.Checked)
        {
            scheduleType = "CACP";
        }

        if (SFSPCheckbox.Checked)
        {
            if (String.IsNullOrEmpty(scheduleType) == true)
            {
                scheduleType = "SFSP";
            }
            else
            {
                scheduleType = scheduleType + " and SFSP";
            }
        }

        if (NACheckbox.Checked)
        {
            if (String.IsNullOrEmpty(scheduleType) == true)
            {
                scheduleType = "N/A";
            }
            else
            {
                scheduleType = scheduleType + " and N/A";
            }
        }

        if (AddedDeliverySiteList.Items.Count != 0)
        {
            RescheduleDateTextBox.Enabled = true;

            CancellationRangeStartDateLabel.Text = CancellationRangeStartDateTextBox.Text;
            CancellationRangeEndDateLabel.Text = CancellationRangeEndDateTextBox.Text;
            CancellationMealTypeLabel.Text = MealDeliveryTypeDropDownList.SelectedItem.Text;
            CancellationRangeScheduleTypeLabel.Text = scheduleType;

            ClientScript.RegisterStartupScript(GetType(), "key", "launchGroupCancellationModal();", true);
        }
        else
        {
            CancellationRangeStartDateLabel.Text = "";
            CancellationRangeEndDateLabel.Text = "";

            MessageBox.Show("There are no deliveries for the dates you requested.  Please enter a new date range.");

            ClientScript.RegisterStartupScript(GetType(), "key", "launchContinueModal();", true);
        }

        GetCalendarData();
    }

    protected void SaveGroupCancellations_Click(object sender, EventArgs e)
    {
        string mealTypeID;
        string groupExceptionID;
        string partnerDeliveryDate;

        mealTypeID = MealDeliveryTypeHiddenTextBox.Value;

        if (String.IsNullOrEmpty(DeliveryEventIDHiddenTextBox.Value))
        {
            m_SQL =
                "INSERT INTO GroupCancellation(CancellationStartDate, CancellationEndDate, RescheduledDate, MealTypeID, ScheduleTypeText, Notes) VALUES('" +
                CancellationRangeStartDateLabel.Text + "', '" + CancellationRangeEndDateLabel.Text + "', '" +
                RescheduleDateTextBox.Text + "', " + mealTypeID + ", '" + CancellationRangeScheduleTypeLabel.Text + "', '" + NotesTextBox.Text + "')";
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

            m_SQL = "SELECT IDENT_CURRENT('GroupCancellation') AS 'GroupCancellationID'";
            DataSet groupExceptionIDDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            groupExceptionID = GCFDGlobals.dbGetValue(groupExceptionIDDataSet.Tables[0].Rows[0], "GroupCancellationID");
        }
        else
        {
            groupExceptionID = DeliveryEventIDHiddenTextBox.Value;
        }

        int cancelledDeliveryCounter = 0;

        for (int i = 0; i < AddedDeliverySiteList.Items.Count; i++)
        {
            m_SQL = "SELECT * FROM vwDelivery WHERE DeliveryID = " + AddedDeliverySiteList.Items[i].Value + " AND DeliveryTypeName NOT IN('Cancelled', 'Rescheduled')";
            m_DeliveriesDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            if (m_DeliveriesDataSet.Tables[0].Rows.Count > 0)
            {
                cancelledDeliveryCounter = cancelledDeliveryCounter + 1;

                string siteID = GCFDGlobals.dbGetValue(m_DeliveriesDataSet.Tables[0].Rows[0], "SiteID");
                string deliveryDate = GCFDGlobals.dbGetValue(m_DeliveriesDataSet.Tables[0].Rows[0], "DeliveryDate");
                string servingDate = GCFDGlobals.dbGetValue(m_DeliveriesDataSet.Tables[0].Rows[0], "ServingDate");
                string mealCount = GCFDGlobals.dbGetValue(m_DeliveriesDataSet.Tables[0].Rows[0], "MealCount");
                string deliveryRecurrenceDetailID = GCFDGlobals.dbGetValue(m_DeliveriesDataSet.Tables[0].Rows[0], "DeliveryRecurrenceDetailID");

                m_SQL = "UPDATE Delivery SET DeliveryTypeID = 3, GroupCancellationID = " + groupExceptionID + " WHERE DeliveryID = " + AddedDeliverySiteList.Items[i].Value;
                GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                if (AddedDeliverySiteList.Items.Count != cancelledDeliveryCounter)
                {
                    partnerDeliveryDate = new DateTime(1900, 01, 01).ToString();
                }
                else
                {
                    if(RescheduleDateTextBox.Text == "Delivery Not Rescheduled" || String.IsNullOrEmpty(RescheduleDateTextBox.Text))
                    {
                        partnerDeliveryDate = new DateTime(1900, 01, 01).ToString();
                    }
                    else
                    {
                        partnerDeliveryDate = RescheduleDateTextBox.Text;
                    }
                }

                m_SQL = "INSERT INTO Delivery(DeliveryDate, ServingDate, DeliveryRecurrenceDetailID, DeliveryTypeID, ParentDeliveryID, GroupCancellationID) VALUES('" + partnerDeliveryDate + "', '" + servingDate + "', " + deliveryRecurrenceDetailID + ", 2, " + AddedDeliverySiteList.Items[i].Value + ", " + groupExceptionID + ")";
                GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                m_SQL = "SELECT IDENT_CURRENT('Delivery') AS 'DeliveryID'";
                DataSet deliveryDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                m_SQL = "UPDATE Delivery SET ParentDeliveryID = " + GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "DeliveryID") + " WHERE DeliveryID = " + AddedDeliverySiteList.Items[i].Value;
                GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
            }
        }

        if (!String.IsNullOrEmpty(DeliveryEventIDHiddenTextBox.Value))
        {
            for (int i = 0; i < RemovedDeliverySiteList.Items.Count; i++)
            {
                m_SQL = "SELECT * FROM vwDelivery WHERE DeliveryID = " + RemovedDeliverySiteList.Items[i].Value + " AND GroupCancellationID = " + DeliveryEventIDHiddenTextBox.Value;
                DataSet removedExceptionDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                if (removedExceptionDataSet.Tables[0].Rows.Count > 0)
                {
                    m_SQL = "UPDATE Delivery SET DeliveryTypeID = 1, ParentDeliveryID = null, GroupCancellationID = null WHERE DeliveryID = " + GCFDGlobals.dbGetValue(removedExceptionDataSet.Tables[0].Rows[0], "DeliveryID");
                    GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                    m_SQL = "DELETE FROM Delivery WHERE DeliveryID = " + GCFDGlobals.dbGetValue(removedExceptionDataSet.Tables[0].Rows[0], "ParentDeliveryID");
                    GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
                }
            }
        }       

        GetCalendarData();
    }

    protected void CancellationDateTextBox_TextChanged(object sender, EventArgs e)
    {
        CancellationMealTypeDropDownList.Items.Clear();

        CancellationRescheduleDateTextBox.Text = "";

        m_SQL = "SELECT DISTINCT MealTypeName, MealTypeID FROM vwDelivery WHERE DeliveryDate = '" + CancellationDateTextBox.Text + "' AND DeliveryTypeID = 1 AND SiteID = " + SiteIDHiddenField.Value.ToString();
        DataSet mealTypeDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        if (mealTypeDataSet.Tables[0].Rows.Count == 0)
        {
            CancellationMealTypeDropDownList.Items.Insert(0, new ListItem("No Deliveries Scheduled For Date Chosen", "-1"));
        }
        else
        {
            CancellationMealTypeDropDownList.DataValueField = "MealTypeID";
            CancellationMealTypeDropDownList.DataTextField = "MealTypeName";
            CancellationMealTypeDropDownList.DataSource = mealTypeDataSet.Tables[0];
            CancellationMealTypeDropDownList.DataBind();
        }

        GetCalendarData();

        ClientScript.RegisterStartupScript(GetType(), "key", "launchCancelRescheduleModal();", true);
    }

    protected void CancelRescheduleDeliveriesButton_Click(object sender, EventArgs e)
    {
        if (SiteModeHiddenField.Value == "New")
        {
            MessageBox.Show("Please Save Site Information Before Canceling Meals.");
        }
        else
        {
            CancellationRescheduleDateTextBox.Text = "";

            CancellationDateTextBox.Text = "";

            CancellationRescheduleNotesTextBox.Text = "";

            CancellationMealTypeDropDownList.Items.Clear();

            GetCalendarData();

            ClientScript.RegisterStartupScript(GetType(), "key", "launchCancelRescheduleModal();", true);
        }
    }

    protected void SaveCancellationRescheduleButton_Click(object sender, EventArgs e)
    {
        string parentDeliveryID = "";
        string exceptionDate = "";
        string scheduleType = IndividualCancellationScheduleRadioButtonList.SelectedItem.Text;
       
        m_SQL = "SELECT * FROM vwDelivery WHERE DeliveryDate = '" + CancellationDateTextBox.Text + "' AND MealTypeID = " + CancellationMealTypeDropDownList.SelectedItem.Value + " AND ScheduleTypeName = '" + scheduleType + "' AND SiteID = " + SiteIDHiddenField.Value;
        DataSet deliveryDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        if (deliveryDataSet.Tables[0].Rows.Count == 0)
        {
            MessageBox.Show("There is no deliveries to be cancelled for the date chosen.");
        }
        else
        {
            string deliveryRecurrenceDetailID = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "DeliveryRecurrenceDetailID");
            string servingDate = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "ServingDate");
            string mealTypeID = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "MealTypeID");
            string deliveryID = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "DeliveryID");

            RetrieveMealTypeValues(mealTypeID);

            if (prohibitPastModifications == "1" && Convert.ToInt32(Convert.ToDateTime(CancellationDateTextBox.Text).ToString("yyyyMMdd")) < Convert.ToInt32(System.DateTime.Now.ToString("yyyyMMdd")))
            {
                MessageBox.Show("A delivery earlier than " + System.DateTime.Now.AddDays(Convert.ToInt16(servingDayInternal)).ToString("MM/dd/yyyy") + " cannot be cancelled.");
            }
            else
            {
                if (String.IsNullOrEmpty(CancellationRescheduleDateTextBox.Text))
                {
                    exceptionDate = "1/1/1900";
                }
                else
                {
                    exceptionDate = CancellationRescheduleDateTextBox.Text;
                }

                m_SQL = "INSERT INTO Delivery (DeliveryRecurrenceDetailID, DeliveryTypeID, ParentDeliveryID, DeliveryDate, ServingDate) VALUES(" + deliveryRecurrenceDetailID + ", 2, " + deliveryID + ", '" + exceptionDate + "', '" + servingDate + "')";
                GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                m_SQL = "SELECT IDENT_CURRENT('Delivery') AS 'DeliveryID'";
                deliveryDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                parentDeliveryID = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "DeliveryID");

                m_SQL = "UPDATE Delivery SET DeliveryTypeID = 3, ParentDeliveryID = " + parentDeliveryID + " WHERE DeliveryID = " + deliveryID;
                GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                m_SQL = "DELETE FROM MealDelivery WHERE DeliveryID = " + deliveryID;
                GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                m_SQL = "INSERT INTO UserAction (UserName, Action) VALUES('" + User.Identity.Name + "', 'Cancelled Delivery On " + CancellationDateTextBox.Text + " For " + SiteNameTextBox.Text + "')";
                GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
            }
        }
        
        CancellationDateTextBox.Text = "";
        
        CancellationRescheduleDateTextBox.Text = "";

        CancellationRescheduleNotesTextBox.Text = "";

        CancellationMealTypeDropDownList.Items.Clear();

        CancellationDateTextBox.Focus();
        
        GetCalendarData();

        ClientScript.RegisterStartupScript(GetType(), "key", "launchCancelRescheduleModal();", true);
    }

    protected void DeliveryRecurrenceDetailGridView_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DeliveryRecurrenceDetailGridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        System.Threading.Thread.Sleep(1000);

        DeliveryRecurrenceDetailGridView.EditIndex = e.NewEditIndex;   

        FillDeliveryRecurrenceDetailInGrid(DeliveryRecurrenceIDHiddenField.Value);

        GridViewRow row = DeliveryRecurrenceDetailGridView.Rows[e.NewEditIndex];

        DropDownList deliveryDay = (DropDownList)row.FindControl("DeliveryDayDropDownList");

        deliveryDay.Attributes["onchange"] = "setDefaultServingDay('" + deliveryDay.ClientID + "');";

        ClientScript.RegisterStartupScript(GetType(), "key", "launchDeliveryRecurrencePopupModal();", true);

        GetCalendarData();
    }

    protected void DeliveryRecurrenceDetailGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        System.Threading.Thread.Sleep(1000);

        int index = DeliveryRecurrenceDetailGridView.EditIndex;
        string newDeliveryRecurrenceID = "";

        GridViewRow row = DeliveryRecurrenceDetailGridView.Rows[index];

        Label deliveryRecurrenceDetailID = (Label)row.FindControl("DeliveryRecurrenceDetailIDLabel");

        Label deliveryRecurrenceDetailLastModified = (Label)row.FindControl("DeliveryRecurrenceDetailLastModifiedLabel");  

        TextBox mealCount = (TextBox)row.FindControl("MealCountTextBox");

        DropDownList deliveryDay = (DropDownList)row.FindControl("DeliveryDayDropDownList");

        DropDownList servingDay = (DropDownList)row.FindControl("ServingDayDropDownList");

        if (deliveryRecurrenceDetailID.Text == "0")
        {
            m_SQL = "INSERT INTO DeliveryRecurrenceDetail (DeliveryRecurrenceID, MealCount, ServingDay, DeliveryDay, LastModified) VALUES(" + DeliveryRecurrenceIDHiddenField.Value + ", " + mealCount.Text + ", '', '', '" + deliveryRecurrenceDetailLastModified.Text + "')";
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

            m_SQL = "SELECT SCOPE_IDENTITY() AS DeliveryRecurrenceDetailID";
            DataSet newDeliveryRecurrenceDetailIDDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            newDeliveryRecurrenceID = deliveryRecurrenceDetail.Update(User.Identity.Name, GCFDGlobals.dbGetValue(newDeliveryRecurrenceDetailIDDataSet.Tables[0].Rows[0], "DeliveryRecurrenceDetailID"), mealCount.Text, deliveryDay.SelectedItem.Text, servingDay.SelectedItem.Text, deliveryRecurrenceDetailLastModified.Text);
        }
        else
        {
            newDeliveryRecurrenceID = deliveryRecurrenceDetail.Update(User.Identity.Name, deliveryRecurrenceDetailID.Text, mealCount.Text, deliveryDay.SelectedItem.Text, servingDay.SelectedItem.Text, deliveryRecurrenceDetailLastModified.Text);
        }

        if (newDeliveryRecurrenceID == "2")
        {
            MessageBox.Show("Another user has edited the delivery recurrence since you started to edit it.");
        }
        else if (newDeliveryRecurrenceID == "1")
        {
            MessageBox.Show("Another user has deleted the delivery recurrence since you started to edit it.");
        }
        else
        {
            DeliveryRecurrenceDetailGridView.EditIndex = -1;

            FillDeliveryRecurrenceDetailInGrid(newDeliveryRecurrenceID);

            m_SQL = "SELECT DISTINCT DeliveryRecurrenceLastModified, DeliveryRecurrenceID, MealTypeName, MealTypeID, StartDate, EndDate, CONVERT(varchar, DeliveryRecurrenceLastModified, 121) AS DeliveryRecurrenceLastModified FROM vwDelivery WHERE DeliveryRecurrenceID = " + newDeliveryRecurrenceID;
            DataSet deliveryDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            DeliveryRecurrenceIDHiddenField.Value = newDeliveryRecurrenceID;

            RecurrenceLastModifiedHiddenField.Value = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "DeliveryRecurrenceLastModified");
            
            string mealTypeID = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "MealTypeID");

            int endDate = Convert.ToInt32(Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "EndDate")).ToString("yyyyMMdd"));
            int startDate = Convert.ToInt32(Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "StartDate")).ToString("yyyyMMdd"));

            RetrieveMealTypeValues(mealTypeID);

            int formattedpermittedStartDate = Convert.ToInt32(permittedStartDate.ToString("yyyyMMdd")); ;

            if (prohibitPastModifications == "1")
            {
                if (startDate < formattedpermittedStartDate)
                    RecurrenceStartDateTextBox.Enabled = false;
                else
                    RecurrenceStartDateTextBox.Enabled = true;

                if (endDate < formattedpermittedStartDate)
                {
                    RecurrenceEndDateTextBox.Enabled = false;

                    DeleteRecurrenceButton.Enabled = false;

                    SaveDeliveryRecurrenceButton.Enabled = false;

                    foreach (GridViewRow row1 in DeliveryRecurrenceDetailGridView.Rows)
                    {
                        Panel popupPanel = (Panel)row1.FindControl("PopupMenu");

                        popupPanel.Visible = false;
                    }
                }
                else
                {
                    RecurrenceEndDateTextBox.Enabled = true;

                    DeleteRecurrenceButton.Enabled = true;
                }
            }

            GetCalendarData();

            int calendarYear = Convert.ToInt32(drpCalYear.SelectedItem.Text);
            int calendarMonth = Convert.ToInt32(drpCalMonth.SelectedItem.Value);

            DateTime visibleDate = new DateTime(calendarYear, calendarMonth, 1);

            SiteDeliveryCalendar.VisibleDate = visibleDate;
            SiteDeliveryCalendar.TodaysDate = visibleDate;

            FillDeliveryRecurrenceDetailInGrid(newDeliveryRecurrenceID);

            ClientScript.RegisterStartupScript(GetType(), "key", "launchDeliveryRecurrencePopupModal();", true);
        }
    }

    protected void DeliveryRecurrenceDetailGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    { 
        int index = e.RowIndex;
        string mealCount = "0";

        GridViewRow row = DeliveryRecurrenceDetailGridView.Rows[index];

        Label deliveryRecurrenceDetailID = (Label)row.FindControl("DeliveryRecurrenceDetailIDLabel1");

        Label deliveryRecurrenceDetailLastModified = (Label)row.FindControl("DeliveryRecurrenceDetailLastModifiedLabel1");

        Label deliveryDay = (Label)row.FindControl("Label2");
        Label servingDay = (Label)row.FindControl("Label3");

        string newDeliveryRecurrenceID = deliveryRecurrenceDetail.Delete(User.Identity.Name, deliveryRecurrenceDetailID.Text, mealCount, deliveryDay.Text, servingDay.Text, deliveryRecurrenceDetailLastModified.Text);

        if (newDeliveryRecurrenceID == "2")
        {
            MessageBox.Show("Another user has edited the delivery recurrence since you started to edit it.");
        }
        else if (newDeliveryRecurrenceID == "1")
        {
            MessageBox.Show("Another user has deleted the delivery recurrence since you started to edit it.");
        }

        FillDeliveryRecurrenceDetailInGrid(DeliveryRecurrenceIDHiddenField.Value);

        DateTime visibleDate = new DateTime(Convert.ToInt32(drpCalYear.SelectedItem.Text), Convert.ToInt32(drpCalMonth.SelectedItem.Value), 1);

        if (index == 0)
        {
            Response.Redirect("SiteDetails.aspx?SiteID=" + SiteIDHiddenField.Value + "&RecurrenceMode=DeletedLastDetail&DeliveryRecurrenceID=" + DeliveryRecurrenceIDHiddenField.Value + "&SiteMode=Update&CalendarDate=" + visibleDate.ToString());
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "key", "launchDeliveryRecurrencePopupModal();", true);

            GetCalendarData();
        }
    }

    protected void DeliveryRecurrenceDetailGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            DeliveryRecurrenceDetailGridView.EditIndex = DeliveryRecurrenceDetailGridView.Rows.Count;

            RecurrenceDetailModeHiddenField.Value = "New";

            FillDeliveryRecurrenceDetailInGrid(DeliveryRecurrenceIDHiddenField.Value);
                       
            DataTable dt = ((DataTable)DeliveryRecurrenceDetailGridView.DataSource);

            DataRow dataRow = dt.NewRow(); 

            dataRow["DeliveryRecurrenceDetailID"] = "0";
            dataRow["MealCount"] = "0";
            dataRow["DeliveryDay"] = "Monday";            

            m_SQL = "SELECT DISTINCT MealTypeID FROM MealTypeDict WHERE MealTypeName = '" + RecurrenceMealTypeLabel.Text + "'";
            DataSet mealTypeDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);
            
            string mealType = GCFDGlobals.dbGetValue(mealTypeDataSet.Tables[0].Rows[0], "MealTypeID");

            RetrieveMealTypeValues(mealType);

            switch (servingDayInternal)
            {
                case "0":
                    dataRow["ServingDay"] = "Monday";

                    break;
                case "1":
                    dataRow["ServingDay"] = "Tuesday";

                    break;
                case "2":
                    dataRow["ServingDay"] = "Wednesday";

                    break;
                case "3":
                    dataRow["ServingDay"] = "Thursday";

                    break;
                case "4":
                    dataRow["ServingDay"] = "Friday";

                    break;
                case "5":
                    dataRow["ServingDay"] = "Saturday";

                    break;
                case "6":
                    dataRow["ServingDay"] = "Sunday";

                    break;
            }

            dataRow["LastModified"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            dt.Rows.Add(dataRow);

            DeliveryRecurrenceDetailGridView.DataBind();

            int index = DeliveryRecurrenceDetailGridView.Rows.Count - 1;

            GridViewRow row = DeliveryRecurrenceDetailGridView.Rows[index];

            DropDownList deliveryDay = (DropDownList)row.FindControl("DeliveryDayDropDownList");

            deliveryDay.Attributes["onchange"] = "setDefaultServingDay('" + deliveryDay.ClientID + "');";

            ClientScript.RegisterStartupScript(GetType(), "key", "launchDeliveryRecurrencePopupModal();", true);

            GetCalendarData();
        }
    }

    protected void DeliveryRecurrenceDetailGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        System.Threading.Thread.Sleep(1000);

        if (RecurrenceDetailModeHiddenField.Value == "New")
        {
            int index = DeliveryRecurrenceDetailGridView.EditIndex;

            GridViewRow row = DeliveryRecurrenceDetailGridView.Rows[index];

            Label deliveryRecurrenceDetailID = (Label)row.FindControl("DeliveryRecurrenceDetailIDLabel");

            m_SQL = "DELETE FROM DeliveryRecurrenceDetail WHERE DeliveryRecurrenceDetailID = " + deliveryRecurrenceDetailID.Text;
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

            RecurrenceDetailModeHiddenField.Value = "None";
        }

        DeliveryRecurrenceDetailGridView.EditIndex = -1;

        FillDeliveryRecurrenceDetailInGrid(DeliveryRecurrenceIDHiddenField.Value);

        ClientScript.RegisterStartupScript(GetType(), "key", "launchDeliveryRecurrencePopupModal();", true);

        GetCalendarData();
    }

    protected void CommentsGridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        System.Threading.Thread.Sleep(1000);

        CommentsGridView.EditIndex = e.NewEditIndex;

        FillCommentDetailInGrid();

        GridViewRow row = CommentsGridView.Rows[e.NewEditIndex];

        GetCalendarData();
    }

    protected void CommentsGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        System.Threading.Thread.Sleep(1000);

        int index = CommentsGridView.EditIndex;

        GridViewRow row = CommentsGridView.Rows[index];

        Label commentID = (Label)row.FindControl("CommentIDLabel");

        Label commentDate = (Label)row.FindControl("CommentDateLabel");

        TextBox comment = (TextBox)row.FindControl("CommentTextBox");

        Label userName = (Label)row.FindControl("CommentUserNameLabel");

        if (commentID.Text == "0")
        {
            m_SQL = "INSERT INTO SiteComment (SiteID, CommentDate, Comment, UserName) VALUES(" +  SiteIDHiddenField.Value + ", '" + commentDate.Text + "', '" + comment.Text + "', '" + userName.Text + "')";
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

            m_SQL = "SELECT SCOPE_IDENTITY() AS CommentID";
            DataSet newCommentIDDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);
        }
        else
        {
            m_SQL = "UPDATE SiteComment SET CommentDate = '" + System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', Comment = '" + comment.Text + "', UserName = '" + User.Identity.Name + "' WHERE CommentID = " + commentID.Text;
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
        }

        GetCalendarData();

        int calendarYear = Convert.ToInt32(drpCalYear.SelectedItem.Text);
        int calendarMonth = Convert.ToInt32(drpCalMonth.SelectedItem.Value);

        DateTime visibleDate = new DateTime(calendarYear, calendarMonth, 1);

        SiteDeliveryCalendar.VisibleDate = visibleDate;
        SiteDeliveryCalendar.TodaysDate = visibleDate;

        CommentsGridView.EditIndex = -1;
        
        FillCommentDetailInGrid();
    }

    protected void CommentsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        GridViewRow row = CommentsGridView.Rows[index];

        Label commentID = (Label)row.FindControl("CommentIDLabel1");
        
        m_SQL = "DELETE FROM SiteComment WHERE CommentID = " + commentID.Text;
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

        GetCalendarData();

        int calendarYear = Convert.ToInt32(drpCalYear.SelectedItem.Text);
        int calendarMonth = Convert.ToInt32(drpCalMonth.SelectedItem.Value);

        DateTime visibleDate = new DateTime(calendarYear, calendarMonth, 1);

        SiteDeliveryCalendar.VisibleDate = visibleDate;
        SiteDeliveryCalendar.TodaysDate = visibleDate;

         FillCommentDetailInGrid();

         CommentsGridView.EditIndex = -1;       
    }

    protected void CommentsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            CommentsGridView.EditIndex = CommentsGridView.Rows.Count;

            FillCommentDetailInGrid();

            DataTable dt = ((DataTable)CommentsGridView.DataSource);

            dt.Rows.Add(new object[] { "0", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), "Add Comment Here", User.Identity.Name });

            CommentsGridView.DataBind();

            int index = CommentsGridView.Rows.Count - 1;

            GridViewRow row = CommentsGridView.Rows[index];

            GetCalendarData();
        }
    }

    protected void CommentsGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void CommentsGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        System.Threading.Thread.Sleep(1000);

        CommentsGridView.EditIndex = -1;

        FillCommentDetailInGrid();

        GetCalendarData();
    }

    protected void CloseRecurrenceButton_Click(object sender, EventArgs e)
    {
        RecurrencePopupExtender.Hide();

        DateTime visibleDate = new DateTime(Convert.ToInt32(drpCalYear.SelectedItem.Text), Convert.ToInt32(drpCalMonth.SelectedItem.Value), 1);

        Response.Redirect("SiteDetails.aspx?SiteID=" + SiteIDHiddenField.Value + "&SiteMode=Update&CalendarDate=" + visibleDate.ToString(), false);
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

        SiteDeliveryCalendar.VisibleDate = visibleDate;
        SiteDeliveryCalendar.TodaysDate = visibleDate;

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

        SiteDeliveryCalendar.VisibleDate = visibleDate;
        SiteDeliveryCalendar.TodaysDate = visibleDate;

        GetCalendarData();
    }
    
    protected void NextSiteButton_Click(object sender, EventArgs e)
    {
        m_SQL = "SELECT SiteID, SiteName FROM Site ORDER BY SiteName";
        DataSet SiteDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        Boolean getNext = false;

        DateTime visibleDate = new DateTime(Convert.ToInt32(drpCalYear.SelectedItem.Text), Convert.ToInt32(drpCalMonth.SelectedItem.Value), 1);

        foreach (DataRow siteDataRow in SiteDataSet.Tables[0].Rows)
        {
            if (getNext)
            {
                Response.Redirect("SiteDetails.aspx?SiteID=" + GCFDGlobals.dbGetValue(siteDataRow, "SiteID") + "&SiteMode=Update&CalendarDate=" + visibleDate.ToString(), false);

                break;
            }
            else if (GCFDGlobals.dbGetValue(siteDataRow, "SiteName") == SiteNameTextBox.Text)
            {
                getNext = true;
            }
        }

        if (getNext == false)
        {
            m_SQL = "SELECT TOP 1 SiteID FROM Site ORDER BY SiteName";
            SiteDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            Response.Redirect("SiteDetails.aspx?SiteID=" + GCFDGlobals.dbGetValue(SiteDataSet.Tables[0].Rows[0], "SiteID") + "&SiteMode=Update&CalendarDate=" + visibleDate.ToString(), false);
        }
    }
    
    protected void PreviousSiteButton_Click(object sender, EventArgs e)
    {
        m_SQL = "SELECT SiteID, SiteName FROM Site ORDER BY SiteName DESC";
        DataSet SiteDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        Boolean getNext = false;

        DateTime visibleDate = new DateTime(Convert.ToInt32(drpCalYear.SelectedItem.Text), Convert.ToInt32(drpCalMonth.SelectedItem.Value), 1);

        foreach (DataRow siteDataRow in SiteDataSet.Tables[0].Rows)
        {
            if (getNext)
            {
                Response.Redirect("SiteDetails.aspx?SiteID=" + GCFDGlobals.dbGetValue(siteDataRow, "SiteID") + "&SiteMode=Update&CalendarDate=" + visibleDate.ToString(), false);

                break;
            }
            else if (GCFDGlobals.dbGetValue(siteDataRow, "SiteName") == SiteNameTextBox.Text)
            {
                getNext = true;
            }
        }

        if (getNext == false)
        {
            m_SQL = "SELECT TOP 1 SiteID FROM Site ORDER BY SiteName DESC";
            SiteDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            Response.Redirect("SiteDetails.aspx?SiteID=" + GCFDGlobals.dbGetValue(SiteDataSet.Tables[0].Rows[0], "SiteID") + "&SiteMode=Update&CalendarDate=" + visibleDate.ToString(), false);
        }
    }

    protected void RetrieveMealTypeValues(string mealTypeID)
    {
        m_SQL = "SELECT * FROM MealTypeDict WHERE MealTypeID = " + mealTypeID;
        DataSet m_MealTypeDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        servingDayInternal = GCFDGlobals.dbGetValue(m_MealTypeDataSet.Tables[0].Rows[0], "ServingDayInterval");
        hasWeekendActivity = GCFDGlobals.dbGetValue(m_MealTypeDataSet.Tables[0].Rows[0], "HasWeekendActivity");
        prohibitPastModifications = GCFDGlobals.dbGetValue(m_MealTypeDataSet.Tables[0].Rows[0], "ProhibitPastModifications");
        futureActivityInterval = Convert.ToInt16(GCFDGlobals.dbGetValue(m_MealTypeDataSet.Tables[0].Rows[0], "FutureActivityInterval"));

        if (hasWeekendActivity == "0")
        {
            if (DateTime.Now.DayOfWeek.ToString() == "Friday")
            {
                permittedStartDate = DateTime.Now.AddDays(2 + futureActivityInterval);
            }
            else
            {
                permittedStartDate = DateTime.Now.AddDays(futureActivityInterval);
            }
        }
        else
        {
            permittedStartDate = DateTime.Now.AddDays(futureActivityInterval);
        }


    }

    public void FillCommentDetailInGrid()
    {
        m_SQL = "SELECT CommentID, CommentDate, LEFT(Comment, 50) + '...' AS Comment, UserName FROM SiteComment WHERE SiteID = " + SiteIDHiddenField.Value;
        DataSet m_CommentInformationDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        if (m_CommentInformationDataSet.Tables[0].Rows.Count == 0)
        {
            CommentsAddDiv.Style["display"] = "inline";
            CommentsAddDiv.Style["visibility"] = "visible";

            CommentsDiv.Style["display"] = "none";
            CommentsDiv.Style["visibility"] = "hidden";
        }
        else
        {
            CommentsAddDiv.Style["display"] = "none";
            CommentsAddDiv.Style["visibility"] = "hidden";

            CommentsDiv.Style["display"] = "inline";
            CommentsDiv.Style["visibility"] = "visible";

            DataTable dtCommentDetail = m_CommentInformationDataSet.Tables[0];

            CommentsGridView.DataSource = null;

            CommentsGridView.DataSource = dtCommentDetail;

            CommentsGridView.DataBind();
        }
    }

    protected void CreateNewCommentButton_Click(object sender, EventArgs e)
    {
        CommentsGridView.EditIndex = 0;

        CommentsAddDiv.Style["display"] = "none";
        CommentsAddDiv.Style["visibility"] = "hidden";

        CommentsDiv.Style["display"] = "inline";
        CommentsDiv.Style["visibility"] = "visible";

        DataSet ds = new DataSet();

        DataTable dt = new DataTable();

        dt.Columns.Add("CommentID");
        dt.Columns.Add("CommentDate");
        dt.Columns.Add("Comment");
        dt.Columns.Add("UserName");

        dt.Rows.Add(new object[] { "0", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), "Add Comment Here", User.Identity.Name });

        ds.Tables.Add(dt);

        CommentsGridView.DataSourceID = null;
        CommentsGridView.DataSource = ds.Tables[0].DefaultView;
        CommentsGridView.DataBind();
    }
}

