using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using GCFDGlobalsNamespace;

public partial class DeliveryCalendar : System.Web.UI.Page
{
    public string m_SQL;
    protected DataSet m_DeliveriesDataSet;
    public string exceptionType = "";
    public DataSet deliveryExceptionDataSet;
	public DataSet deliveriesDataSet;
	public string m_MealCount = "0";
    public DataSet calendarDataSet;
    public DataSet mealCountTotalDataSet;
    public DateTime permittedStartDate;
    public string prohibitPastModifications;
    public string hasWeekendActivity;
    public int futureActivityInterval;
    public string servingDayInternal;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated && (User.IsInRole("Programs") || User.IsInRole("Kitchen-Admin")))
        {
            if (Session["SessionID"] == null)
            {
                System.Web.Security.FormsAuthentication.SignOut();

                Response.Redirect("~/Account/Login.aspx", false);

                Response.End();
            }
            else
            {
                if (User.IsInRole("Kitchen-Admin") && !User.IsInRole("Administrator"))
                {
                    CreateRangeCancellationButton.Enabled = false;
                }

                Load += (Page_Load);

                if (!IsPostBack)
                {
                    Populate_MonthList();

                    Populate_YearList();
                 
                    DeliveryCalendarControl.VisibleDate = DateTime.Today;

                    GetCalendarData();
                }

                if (Request.Form["__EVENTTARGET"] == "CalendarLinkButton")
                {
                    CalendarLinkButton_Click(Request.Form["__EVENTARGUMENT"]);
                }

                if (Session["DeliveryEventMode"].ToString() == "ViewEvent")
                {
                    ClientScript.RegisterStartupScript(GetType(), "key", "launchModal();", true);
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

        m_SQL = "SELECT DISTINCT DeliveryTypeID, DeliveryDate, DeliveryTypeName, MealTypeID, MealTypeName FROM vwDelivery WHERE DeliveryDate >= '" + firstDate.ToString("MM/dd/yyyy") + "' AND DeliveryDate < '" + lastDate.ToString("MM/dd/yyyy") + "'";
        calendarDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        m_SQL = "SELECT DISTINCT SUM(MealCount) as MealCount, MealTypeID, MealTypeName, DeliveryDate FROM vwDelivery WHERE DeliveryDate >= '" + firstDate.ToString("MM/dd/yyyy") + "' AND DeliveryDate < '" + lastDate.ToString("MM/dd/yyyy") + "' AND DeliveryTypeName <> 'Cancelled' GROUP BY MealTypeID, MealTypeName, DeliveryDate";
        mealCountTotalDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

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

    protected void SiteDeliveryCalendar_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        GetCalendarData();
    }

    public void MenuDeliveryCalendarControl_DayRender(object sender, DayRenderEventArgs e)
    {
        Boolean showLine = true;

        if (calendarDataSet != null)
        {
            foreach (DataRow dr in calendarDataSet.Tables[0].Rows)
            {
                if (Convert.ToDateTime(dr["DeliveryDate"]).ToString("MM/dd/yyyy") == e.Day.Date.ToString("MM/dd/yyyy"))
                {
                    LinkButton calendarLinkButton = new LinkButton();

                    string deliveryType = dr["DeliveryTypeName"].ToString().Trim();

                    if (deliveryType == "Rescheduled")
                    {
                        deliveryType = "Extra";
                    }

                    e.Cell.Controls.Add(new LiteralControl("<br />"));
                    calendarLinkButton.Text = deliveryType + " " + dr["MealTypeName"].ToString().Trim();
                    calendarLinkButton.Font.Name = "Arial";
                    calendarLinkButton.Font.Bold = false;
                    calendarLinkButton.Font.Size = FontUnit.Parse("8");

                    if (e.Day.Date.Month != DeliveryCalendarControl.VisibleDate.Month)
                    {
                        calendarLinkButton.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004D45");
                    }
                    else
                    {
                        calendarLinkButton.ForeColor = System.Drawing.Color.White;
                    }

                    calendarLinkButton.ID = "ExceptionLinkButton" + GCFDGlobals.dbGetValue(dr, "DeliveryTypeID");
                    calendarLinkButton.Attributes.Add("href",
                                                        "javascript:__doPostBack('CalendarLinkButton','" +
                                                        e.Day.Date.ToString("MM/dd/yyyy") + "," +
                                                        dr["DeliveryTypeName"].ToString().Trim() + "," + dr["MealTypeID"].ToString().Trim() + "')");

                    e.Cell.Controls.Add(calendarLinkButton);
                }
            }

            DateTime firstDate = e.Day.Date;

            DataRow[] drFiltered = mealCountTotalDataSet.Tables[0].Select("DeliveryDate = #" + e.Day.Date.ToString("MM/dd/yyyy") + "#");

            for (int i = 0; i < drFiltered.Length; i++)
            {
                string mealCount = "0";

                if (!String.IsNullOrEmpty(GCFDGlobals.dbGetValue(drFiltered[i], "MealCount")))
                {
                    mealCount = GCFDGlobals.dbGetValue(drFiltered[i], "MealCount");
                }

                if (showLine)
                {
                    e.Cell.Controls.Add(new LiteralControl("<hr />"));

                    showLine = false;
                }

                Label mealCountLabel = new Label();
                mealCountLabel.Text = GCFDGlobals.dbGetValue(drFiltered[i], "MealTypeName") + " Count = " + mealCount;
                mealCountLabel.Font.Name = "Arial";
                mealCountLabel.Font.Bold = false;
                mealCountLabel.Font.Size = FontUnit.Parse("8");
                mealCountLabel.ID = "MealCountButton" + GCFDGlobals.dbGetValue(drFiltered[i], "MealTypeID");
                e.Cell.Controls.Add(mealCountLabel);
                e.Cell.Controls.Add(new LiteralControl("<br />"));
            }
        }
    }

    protected void MenuDeliveryCalendarControl_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        GetCalendarData();
    }

    public void FillSiteDataGridView(DataSet siteDataSet, DateTime dateChoosen, string clickType)
    {
        if (clickType == "DayClick")
        {
            try
            {
                if (siteDataSet.Tables[0].Rows.Count == 0)
                {
                    AddDummySiteData();

                    int columnCount = DayDetailsGridView.Rows[0].Cells.Count;

                    DayDetailsGridView.Rows[0].Cells.Clear();

                    DayDetailsGridView.Rows[0].Cells.Add(new TableCell());

                    DayDetailsGridView.Rows[0].Cells[0].ColumnSpan = columnCount;

                    DayDetailsGridView.Rows[0].Cells[0].Text = "No sites receive a delivery this day";
                }
                else
                {
                    m_SQL = "SELECT DISTINCT SiteID, SiteName, MealCount, DeliveryTypeName, ServingDate, MealTypeName, GroupCancellationID FROM vwDelivery WHERE CONVERT(varchar(50),deliverydate,101) =  '" + dateChoosen.ToString("MM/dd/yyyy") + "' ORDER BY SiteName";
                    deliveriesDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                    DataSet deliverySite = new DataSet();
                    DataTable deliverySiteTable = deliverySite.Tables.Add();

                    deliverySiteTable.Columns.Add("SiteID", typeof (int));
                    deliverySiteTable.Columns.Add("Site Name", typeof (string));
                    deliverySiteTable.Columns.Add("Delivery Type", typeof(string));
                    deliverySiteTable.Columns.Add("Group Cancellation?", typeof(string));
                    deliverySiteTable.Columns.Add("Meal Type", typeof (string));
                    deliverySiteTable.Columns.Add("Meal Count", typeof (int));
                    deliverySiteTable.Columns.Add("Serving Day", typeof (string));

                    string servingDay;
                    string idField;

                    foreach (DataRow deliverySiteData in deliveriesDataSet.Tables[0].Rows)
                    {
                        idField = GCFDGlobals.dbGetValue(deliverySiteData, "SiteID");
                        m_MealCount = GCFDGlobals.dbGetValue(deliverySiteData, "MealCount");
                        servingDay = Convert.ToDateTime(GCFDGlobals.dbGetValue(deliverySiteData, "ServingDate")).DayOfWeek.ToString();
                        string deliveryType = GCFDGlobals.dbGetValue(deliverySiteData, "DeliveryTypeName");
                        string mealType = GCFDGlobals.dbGetValue(deliverySiteData, "MealTypeName");
                        string groupCancellation = GCFDGlobals.dbGetValue(deliverySiteData, "GroupCancellationID");

                        switch (deliveryType)
                        {
                            case "Cancelled":
                                m_MealCount = "0";

                                break;

                            case "Rescheduled":
                                deliveryType = "Extra";

                                break;
                        }

                        if (deliveryType == "Extra" || deliveryType == "Cancelled")
                        {
                            if (!String.IsNullOrEmpty(groupCancellation))
                            {
                                groupCancellation = "Yes";
                            }
                            else
                            {
                                groupCancellation = "No";
                            }

                        }
                        else
                        {
                            groupCancellation = "N/A";
                        }

                        deliverySiteTable.Rows.Add(idField, GCFDGlobals.dbGetValue(deliverySiteData, "SiteName"), deliveryType, groupCancellation, mealType,
                                                       m_MealCount, servingDay);
                    }

                    DayDetailsGridView.DataMember = deliverySite.Tables[0].TableName;

                    DayDetailsGridView.DataSource = deliverySite;

                    DayDetailsGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving site information - " + ex.Message);
            }
        }
        else
        {
            try
            {
                if (siteDataSet.Tables[0].Rows.Count == 0)
                {
                    AddDummySiteData();

                    int columnCount = DayDetailsGridView.Rows[0].Cells.Count;

                    DayDetailsGridView.Rows[0].Cells.Clear();

                    DayDetailsGridView.Rows[0].Cells.Add(new TableCell());

                    DayDetailsGridView.Rows[0].Cells[0].ColumnSpan = columnCount;

                    DayDetailsGridView.Rows[0].Cells[0].Text = "No sites receive a delivery this day";
                }
                else
                {
                    DataSet deliverySite = new DataSet();
                    DataTable deliverySiteTable = deliverySite.Tables.Add();

                    //commented out due to not using group cancellation functionality
                    deliverySiteTable.Columns.Add("DeliveryID", typeof(int));
                    deliverySiteTable.Columns.Add("Site Name", typeof(string));
                    deliverySiteTable.Columns.Add("Delivery Type", typeof(string));
                    deliverySiteTable.Columns.Add("Group Cancellation?", typeof(string));
                    deliverySiteTable.Columns.Add("Meal Type", typeof(string));
                    deliverySiteTable.Columns.Add("Meal Count", typeof(string));  
                    deliverySiteTable.Columns.Add("Serving Day", typeof (string));

                    foreach (DataRow deliverySiteData in siteDataSet.Tables[0].Rows)
                    {
                        string deliveryType = GCFDGlobals.dbGetValue(deliverySiteData, "DeliveryTypeName");
                        m_MealCount = GCFDGlobals.dbGetValue(deliverySiteData, "MealCount");
                        string servingDay = Convert.ToDateTime(GCFDGlobals.dbGetValue(deliverySiteData, "ServingDate")).DayOfWeek.ToString();
                        string groupCancellation = GCFDGlobals.dbGetValue(deliverySiteData, "GroupCancellationID");

                        switch (deliveryType)
                        {                       
                            case "Cancelled":
                                m_MealCount = "0";

                                break;
                            case "Rescheduled":
                                deliveryType = "Extra";

                                break;
                        }

                        if(deliveryType == "Extra" || deliveryType == "Cancelled")
                        {
                            if(!String.IsNullOrEmpty(groupCancellation))
                            {
                                groupCancellation = "Yes";
                            }
                            else
                            {
                                groupCancellation = "No";
                            }

                        }
                        else
                        {
                            groupCancellation = "N/A";
                        }

                        //commented out due to not using group cancellation functionality
                        deliverySiteTable.Rows.Add(GCFDGlobals.dbGetValue(deliverySiteData, "DeliveryID"), GCFDGlobals.dbGetValue(deliverySiteData, "SiteName"), deliveryType, groupCancellation,
                                                   GCFDGlobals.dbGetValue(deliverySiteData, "MealTypeName"), m_MealCount, Convert.ToDateTime(GCFDGlobals.dbGetValue(deliverySiteData, "ServingDate")).DayOfWeek.ToString());
                        //deliverySiteTable.Rows.Add(GCFDGlobals.dbGetValue(deliverySiteData, "SiteName"), deliveryType, GCFDGlobals.dbGetValue(deliverySiteData, "MealTypeName"),
                        //                           m_MealCount, servingDay);
                    }
                    
                    DayDetailsGridView.DataMember = deliverySite.Tables[0].TableName;

                    DayDetailsGridView.DataSource = deliverySite;

                    DayDetailsGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving site information - " + ex.Message);
            }
        }
    }

    public void AddDummySiteData()
    {
        DataTable siteDataTable = new DataTable("DummyTable");

        siteDataTable.Columns.Add("Site Name");

        DataRow newRow = siteDataTable.NewRow();

        siteDataTable.Rows.Add(newRow);

        DayDetailsGridView.DataSource = siteDataTable;

        DayDetailsGridView.DataBind();
    }

    protected void ViewSiteListButton_Click(object sender, EventArgs e)
    {
        GetCalendarData();

        Response.Redirect("Sites.aspx", false);
    }

    public void CalendarLinkButton_Click(string arguement)
    {
        string[] linkValue = arguement.Split(',');
        string selectedDeliveryDate = linkValue[0];
        string selectedDeliveryType = linkValue[1];
        string mealTypeID = linkValue[2];

        Session["DeliveryCalendarMode"] = "ExceptionView";

        m_SQL = "SELECT * FROM vwDelivery WHERE DeliveryDate = '" + selectedDeliveryDate + "' AND DeliveryTypeName = '" + selectedDeliveryType + "' AND MealTypeID = " + mealTypeID + " ORDER BY SiteName";
        m_DeliveriesDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        FillSiteDataGridView(m_DeliveriesDataSet, Convert.ToDateTime(selectedDeliveryDate), "ExceptionClick");

        ScheduledDeliveryWeekdayLabel.Text = selectedDeliveryDate;

        DayOfScheduledTaskLabel.Text = "Date of " + selectedDeliveryType + " " + GCFDGlobals.dbGetValue(m_DeliveriesDataSet.Tables[0].Rows[0], "MealTypeName") + " Delivery:";

        GetCalendarData();

        ClientScript.RegisterStartupScript(GetType(), "key", "launchDeliveryDayDetailModal();", true);
    }

    protected void CreateRangeCancellationButton_Click(object sender, EventArgs e)
    {
        m_SQL = "SELECT * FROM MealTypeDict";
        DataSet m_MealDeliveryType = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        MealDeliveryTypeDropDownList.DataValueField = "MealTypeID";
        MealDeliveryTypeDropDownList.DataTextField = "MealTypeName";
        MealDeliveryTypeDropDownList.DataSource = m_MealDeliveryType.Tables[0];
        MealDeliveryTypeDropDownList.DataBind();
        MealDeliveryTypeDropDownList.Items.Insert(0, new ListItem("SELECT MEAL TYPE", "-1"));

        GetCalendarData();

        ClientScript.RegisterStartupScript(GetType(), "key", "launchContinueModal();", true);
    }

    protected void SaveGroupCancellations_Click(object sender, EventArgs e)
    {
        string mealTypeID;
        string groupExceptionID;
        string partnerDeliveryDate;

        mealTypeID = MealDeliveryTypeHiddenTextBox.Value;

        if (RescheduleDateTextBox.Text == "Delivery Not Rescheduled" || String.IsNullOrEmpty(RescheduleDateTextBox.Text))
        {
            partnerDeliveryDate = new DateTime(1900, 01, 01).ToString();
        }
        else
        {
            partnerDeliveryDate = RescheduleDateTextBox.Text;
        }

        if (String.IsNullOrEmpty(DeliveryEventIDHiddenTextBox.Value))
        {
            m_SQL =
                "INSERT INTO GroupCancellation(CancellationStartDate, CancellationEndDate, RescheduledDate, MealTypeID, Notes) VALUES('" +
                CancellationRangeStartDateLabel.Text + "', '" + CancellationRangeEndDateLabel.Text + "', '" +
                partnerDeliveryDate + "', " + mealTypeID + ", '" + CancellationScheduleTypeLabel.Text + "', '" + NotesTextBox.Text + "')";
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

            m_SQL = "SELECT IDENT_CURRENT('GroupCancellation') AS 'GroupCancellationID'";
            DataSet groupExceptionIDDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            groupExceptionID = GCFDGlobals.dbGetValue(groupExceptionIDDataSet.Tables[0].Rows[0], "GroupCancellationID");
        }
        else
        {
            groupExceptionID = DeliveryEventIDHiddenTextBox.Value;
        }

        for (int i = 0; i < AddedDeliverySiteList.Items.Count; i++)
        {
            m_SQL = "SELECT * FROM vwDelivery WHERE DeliveryID = " + AddedDeliverySiteList.Items[i].Value + " AND DeliveryTypeName NOT IN('Cancelled', 'Rescheduled')";
            m_DeliveriesDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            if (m_DeliveriesDataSet.Tables[0].Rows.Count > 0)
            {
                string siteID = GCFDGlobals.dbGetValue(m_DeliveriesDataSet.Tables[0].Rows[0], "SiteID");
                string deliveryDate = GCFDGlobals.dbGetValue(m_DeliveriesDataSet.Tables[0].Rows[0], "DeliveryDate");
                string servingDate = GCFDGlobals.dbGetValue(m_DeliveriesDataSet.Tables[0].Rows[0], "ServingDate");
                string mealCount = GCFDGlobals.dbGetValue(m_DeliveriesDataSet.Tables[0].Rows[0], "MealCount");
                string deliveryRecurrenceDetailID = GCFDGlobals.dbGetValue(m_DeliveriesDataSet.Tables[0].Rows[0], "DeliveryRecurrenceDetailID");

                m_SQL = "UPDATE Delivery SET DeliveryTypeID = 3, GroupCancellationID = " + groupExceptionID + " WHERE DeliveryID = " + AddedDeliverySiteList.Items[i].Value;
                GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
                
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
                    m_SQL = "UPDATE Delivery SET DeliveryTypeID = 1, ParentDeliveryID = null,  GroupCancellationID = null WHERE DeliveryID = " + GCFDGlobals.dbGetValue(removedExceptionDataSet.Tables[0].Rows[0], "DeliveryID");
                    GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                    m_SQL = "DELETE FROM Delivery WHERE DeliveryID = " + GCFDGlobals.dbGetValue(removedExceptionDataSet.Tables[0].Rows[0], "ParentDeliveryID");
                    GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
                }
            }

            if ((Convert.ToDateTime(GroupCancellationRescheduledDateHiddenTextBox.Value).ToString("MM/dd/yyyy") != partnerDeliveryDate) && (!String.IsNullOrEmpty(DeliveryEventIDHiddenTextBox.Value)))
            {
                m_SQL = "UPDATE Delivery SET DeliveryDate = '" + partnerDeliveryDate + "' WHERE GroupCancellationID = " + groupExceptionID + " AND DeliveryTypeID = 2 AND CONVERT(varchar(50), DeliveryDate, 101) = '" + Convert.ToDateTime(GroupCancellationRescheduledDateHiddenTextBox.Value).ToString("MM/dd/yyyy") + "'";
                GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                m_SQL = "UPDATE GroupCancellation SET RescheduledDate = '" + partnerDeliveryDate + "' WHERE GroupCancellationID = " + groupExceptionID;
                GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
            }
        }

        GetCalendarData();
    }

    protected void AddGroupExceptions()
    {
        string mealType = MealDeliveryTypeDropDownList.SelectedItem.Value;
        string servingDate;
        string listLine;
        string deliveryDate;
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

        MealDeliveryTypeHiddenTextBox.Value = MealDeliveryTypeDropDownList.SelectedItem.Value;

        m_SQL = "SELECT * FROM vwDelivery WHERE DeliveryDate BETWEEN '" + CancellationRangeStartDateTextBox.Text + "' AND '" + CancellationRangeEndDateTextBox.Text + "' AND DeliveryTypeName NOT IN('Cancelled', 'Rescheduled') AND MealTypeID = " + mealType + " AND ScheduleTypeID IN(" + scheduleType + ")";
        m_DeliveriesDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        RemovedDeliverySiteList.Items.Clear();
        AddedDeliverySiteList.Items.Clear();

        foreach (DataRow deliveryDataRow in m_DeliveriesDataSet.Tables[0].Rows)
        {
            servingDate = Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryDataRow, "ServingDate")).ToString("MM/dd/yyyy");
            deliveryDate = Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryDataRow, "DeliveryDate")).ToString("MM/dd/yyyy");
            scheduleTypeName = GCFDGlobals.dbGetValue(deliveryDataRow, "ScheduleTypeName");

            listLine = GCFDGlobals.dbGetValue(deliveryDataRow, "SiteName") + " (" + deliveryDate + " Delivery Date|" + servingDate + " Serving Date|" + scheduleTypeName + ")";

            AddedDeliverySiteList.Items.Add(new ListItem(listLine, GCFDGlobals.dbGetValue(deliveryDataRow, "DeliveryID")));
        }
    }

    protected void UpdateGroupExceptions(string groupExceptionID)
    {
        string mealType = "";
        string mealTypeName = "";
        string servingDate;
        string rangeStartDate = "";
        string rangeEndDate = "";
        string listLine;
        string deliveryDate;
        string rescheduledDate = "";
        string scheduleTypeText = "";
        string scheduleType = "";

        m_SQL = "SELECT DISTINCT * FROM vwDelivery WHERE GroupCancellationID = " + groupExceptionID + " AND DeliveryTypeName = 'Cancelled'";
        DataSet m_GroupExceptionDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        mealType = GCFDGlobals.dbGetValue(m_GroupExceptionDataSet.Tables[0].Rows[0], "MealTypeID");
        rangeStartDate = Convert.ToDateTime(GCFDGlobals.dbGetValue(m_GroupExceptionDataSet.Tables[0].Rows[0], "CancellationStartDate")).ToString("MM/dd/yyyy");
        rangeEndDate = Convert.ToDateTime(GCFDGlobals.dbGetValue(m_GroupExceptionDataSet.Tables[0].Rows[0], "CancellationEndDate")).ToString("MM/dd/yyyy");
        rescheduledDate = Convert.ToDateTime(GCFDGlobals.dbGetValue(m_GroupExceptionDataSet.Tables[0].Rows[0], "RescheduledDate")).ToString("MM/dd/yyyy");
        mealTypeName = GCFDGlobals.dbGetValue(m_GroupExceptionDataSet.Tables[0].Rows[0], "MealTypeName");
        scheduleTypeText = GCFDGlobals.dbGetValue(m_GroupExceptionDataSet.Tables[0].Rows[0], "ScheduleTypeText");

        GroupCancellationRescheduledDateHiddenTextBox.Value = rescheduledDate;

        if (rescheduledDate == "01/01/1900")
        {
            RescheduleDateTextBox.Text = "Delivery Not Rescheduled";
        }
        else
        {
            RescheduleDateTextBox.Text = rescheduledDate;
        }

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

        m_SQL = "SELECT * FROM vwDelivery WHERE DeliveryDate BETWEEN '" + rangeStartDate + "' AND '" + rangeEndDate + "' AND MealTypeName = '" + mealTypeName + "' AND DeliveryTypeName = 'Scheduled'";
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
        CancellationScheduleTypeLabel.Text = scheduleTypeText;

        GetCalendarData();

        ClientScript.RegisterStartupScript(GetType(), "key", "launchGroupCancellationModal();", true);
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

        if (SFSPCheckbox.Checked)
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
            CancellationRangeStartDateLabel.Text = CancellationRangeStartDateTextBox.Text;
            CancellationRangeEndDateLabel.Text = CancellationRangeEndDateTextBox.Text;
            CancellationMealTypeLabel.Text = MealDeliveryTypeDropDownList.SelectedItem.Text;
            CancellationScheduleTypeLabel.Text = scheduleType;

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

    public void DayDetailsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";

            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(DayDetailsGridView,
                                                                                  "Select$" + e.Row.RowIndex);
        }

        e.Row.Cells[0].Visible = false;
    }

    public void DayDetailsGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow row = DayDetailsGridView.Rows[e.NewSelectedIndex];
    }

    public void DayDetailsGridView_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Session["DeliveryCalendarMode"] = "";
        Session["DeliveryEventMode"] = "";

        m_SQL = "SELECT * FROM vwDelivery WHERE DeliveryID = " + DayDetailsGridView.SelectedRow.Cells[0].Text;
        DataSet deliveryException = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        foreach (DataRow deliveryExceptionData in deliveryException.Tables[0].Rows)
        {
            if (!String.IsNullOrEmpty(GCFDGlobals.dbGetValue(deliveryExceptionData, "GroupCancellationID")))
            {
                DeliveryEventIDHiddenTextBox.Value = GCFDGlobals.dbGetValue(deliveryExceptionData, "GroupCancellationID");

                UpdateGroupExceptions(GCFDGlobals.dbGetValue(deliveryExceptionData, "GroupCancellationID"));

                GetCalendarData();
            }
            else
            {
                Session["SiteID"] = GCFDGlobals.dbGetValue(deliveryExceptionData, "SiteID");

                Session["SiteMode"] = "Update";

                Response.Redirect("SiteDetails.aspx", false);

                GetCalendarData();
            }
        }
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

            if (AddedDeliverySiteList.Items.Count > 0)
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

            UpdateListBoxHeightWidth();
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

        GetCalendarData();

        ClientScript.RegisterStartupScript(GetType(), "key", "launchGroupCancellationModal();", true);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string mealType = MealDeliveryTypeHiddenTextBox.Value;

        for (int i = 0; i < RemovedDeliverySiteList.Items.Count; i++)
        {
            if (RemovedDeliverySiteList.Items[i].Selected)
            {
                string s = RemovedDeliverySiteList.SelectedItem.Value;
                string t = RemovedDeliverySiteList.SelectedItem.Text;

                AddedDeliverySiteList.Items.Add(new ListItem(t, s));

                RemovedDeliverySiteList.Items.Remove(new ListItem(t, s));

                i--;
            }

            m_SQL = "SELECT DeliveryDate FROM vwDelivery WHERE DeliveryID = " + RemovedDeliverySiteList.Items[i].Value;
            m_DeliveriesDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            RetrieveMealTypeValues(mealType);

            if (prohibitPastModifications == "1" && Convert.ToInt32(Convert.ToDateTime(GCFDGlobals.dbGetValue(m_DeliveriesDataSet.Tables[0].Rows[0], "DeliveryDate")).ToString("yyyyMMdd")) < Convert.ToInt32(System.DateTime.Now.ToString("yyyyMMdd")))
            {
                ListItem li = RemovedDeliverySiteList.Items.FindByValue(RemovedDeliverySiteList.Items[i].Value);

                li.Attributes.Add("style", "color:gray;");
                li.Attributes.Add("disabled", "true");
            }

            UpdateListBoxHeightWidth();
        }

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
}
