using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using GCFDGlobalsNamespace;

public partial class SiteDeliveryCount : System.Web.UI.Page
{
    public string m_SQL;
    protected DropDownList dropDownList = null;
    protected HiddenField hiddenField = null;
    protected Label label = null;
    protected Button button = null;
    protected TextBox unusedTextBox = null;
    protected TextBox usedTextBox = null;

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
                if (!User.IsInRole("Programs"))
                {
                    SaveDailyCountButton.Enabled = false;

                    CancelDailyCountsButton.Enabled = false;
                }

                AttendanceTextBox1.Attributes["onkeyup"] = "javascript:updatetotalused('1')";
                FirstMealTextBox1.Attributes["onkeyup"] = "javascript:updatetotalused('1')";
                SecondsTextBox1.Attributes["onkeyup"] = "javascript:updatetotalused('1')";
                ProgAdultsTextBox1.Attributes["onkeyup"] = "javascript:updatetotalused('1')";
                DisallowedTextBox1.Attributes["onkeyup"] = "javascript:updatetotalused('1')";
                TotalUsedTextBox1.Attributes.Add("readonly", "readonly");
                TotalUnusedTextBox1.Attributes.Add("readonly", "readonly");

                AttendanceTextBox2.Attributes["onkeyup"] = "javascript:updatetotalused('2')";
                FirstMealTextBox2.Attributes["onkeyup"] = "javascript:updatetotalused('2')";
                SecondsTextBox2.Attributes["onkeyup"] = "javascript:updatetotalused('2')";
                ProgAdultsTextBox2.Attributes["onkeyup"] = "javascript:updatetotalused('2')";
                DisallowedTextBox2.Attributes["onkeyup"] = "javascript:updatetotalused('2')";
                TotalUsedTextBox2.Attributes.Add("readonly", "readonly");
                TotalUnusedTextBox2.Attributes.Add("readonly", "readonly");

                AttendanceTextBox3.Attributes["onkeyup"] = "javascript:updatetotalused('3')";
                FirstMealTextBox3.Attributes["onkeyup"] = "javascript:updatetotalused('3')";
                SecondsTextBox3.Attributes["onkeyup"] = "javascript:updatetotalused('3')";
                ProgAdultsTextBox3.Attributes["onkeyup"] = "javascript:updatetotalused('3')";
                DisallowedTextBox3.Attributes["onkeyup"] = "javascript:updatetotalused('3')";
                TotalUsedTextBox3.Attributes.Add("readonly", "readonly");
                TotalUnusedTextBox3.Attributes.Add("readonly", "readonly");

                AttendanceTextBox4.Attributes["onkeyup"] = "javascript:updatetotalused('4')";
                FirstMealTextBox4.Attributes["onkeyup"] = "javascript:updatetotalused('4')";
                SecondsTextBox4.Attributes["onkeyup"] = "javascript:updatetotalused('4')";
                ProgAdultsTextBox4.Attributes["onkeyup"] = "javascript:updatetotalused('4')";
                DisallowedTextBox4.Attributes["onkeyup"] = "javascript:updatetotalused('4')";
                TotalUsedTextBox4.Attributes.Add("readonly", "readonly");
                TotalUnusedTextBox4.Attributes.Add("readonly", "readonly");

                AttendanceTextBox5.Attributes["onkeyup"] = "javascript:updatetotalused('5')";
                FirstMealTextBox5.Attributes["onkeyup"] = "javascript:updatetotalused('5')";
                SecondsTextBox5.Attributes["onkeyup"] = "javascript:updatetotalused('5')";
                ProgAdultsTextBox5.Attributes["onkeyup"] = "javascript:updatetotalused('5')";
                DisallowedTextBox5.Attributes["onkeyup"] = "javascript:updatetotalused('5')";
                TotalUsedTextBox5.Attributes.Add("readonly", "readonly");
                TotalUnusedTextBox5.Attributes.Add("readonly", "readonly");

                AttendanceTextBox6.Attributes["onkeyup"] = "javascript:updatetotalused('6')";
                FirstMealTextBox6.Attributes["onkeyup"] = "javascript:updatetotalused('6')";
                SecondsTextBox6.Attributes["onkeyup"] = "javascript:updatetotalused('6')";
                ProgAdultsTextBox6.Attributes["onkeyup"] = "javascript:updatetotalused('6')";
                DisallowedTextBox6.Attributes["onkeyup"] = "javascript:updatetotalused('6')";
                TotalUsedTextBox6.Attributes.Add("readonly", "readonly");
                TotalUnusedTextBox6.Attributes.Add("readonly", "readonly");

                AttendanceTextBox7.Attributes["onkeyup"] = "javascript:updatetotalused('7')";
                FirstMealTextBox7.Attributes["onkeyup"] = "javascript:updatetotalused('7')";
                SecondsTextBox7.Attributes["onkeyup"] = "javascript:updatetotalused('7')";
                ProgAdultsTextBox7.Attributes["onkeyup"] = "javascript:updatetotalused('7')";
                DisallowedTextBox7.Attributes["onkeyup"] = "javascript:updatetotalused('7')";
                TotalUsedTextBox7.Attributes.Add("readonly", "readonly");
                TotalUnusedTextBox7.Attributes.Add("readonly", "readonly");

                if (!IsPostBack)
                {
                    foreach (Control ctrl in test.Controls)
                    {
                        if (ctrl.GetType() == typeof(TextBox))
                        {
                            ((TextBox)ctrl).Enabled = false;

                            ((TextBox)ctrl).Text = "";

                            if (ctrl.ID.Substring(0, 5) != "Total")
                            {
                                ((TextBox)ctrl).BackColor = System.Drawing.Color.Gray;
                            }
                        }
                        else if (ctrl.GetType() == typeof(HiddenField))
                        {
                            ((HiddenField)ctrl).Value = "";
                        }
                    }

                    m_SQL = "SELECT DISTINCT MealTypeID, MealTypeName FROM MealTypeDict";
                    DataSet mealDeliveryTypeDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                    DeliveryTypeDropDownList.DataValueField = "MealTypeID";
                    DeliveryTypeDropDownList.DataTextField = "MealTypeName";
                    DeliveryTypeDropDownList.DataSource = mealDeliveryTypeDataSet.Tables[0];
                    DeliveryTypeDropDownList.DataBind();
                    DeliveryTypeDropDownList.Items.Insert(0, new ListItem("SELECT MEAL TYPE", "-1"));
                }
            }
        }
        else
        {
            Response.Redirect("Default.aspx", false);

            Response.End();
        }
    }

    protected void DeliveryTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["DeliverySiteListItems"] = null;

        SiteNameLabel.Text = "";

        if (DeliveryTypeDropDownList.SelectedItem.Text != "SELECT DELIVERY TYPE")
        {
            MealTypeLabel.Text = DeliveryTypeDropDownList.SelectedItem.Text + " Delivery Counts";
        }

        if (!String.IsNullOrEmpty(DeliveryWeekTextBox.Text))
        {
            DeliverySiteListView.DataSource = DeliverySiteListItems;

            DeliverySiteListView.DataBind();
        }

        int dayCounter = 1;

        foreach (Control ctrl in test.Controls)
        {
            if (ctrl.GetType() == typeof(TextBox))
            {
                ((TextBox)ctrl).Enabled = false;

                ((TextBox)ctrl).Text = "";

                if (ctrl.ID.Substring(0, 5) != "Total")
                {
                    ((TextBox)ctrl).BackColor = System.Drawing.Color.Gray;
                }

                if (ctrl.ID == "DisallowedTextBox" + dayCounter)
                {
                    dayCounter = dayCounter + 1;
                }
            }
            else if (ctrl.GetType() == typeof(HiddenField))
            {
                ((HiddenField)ctrl).Value = "";
            }
            else if (ctrl.GetType() == typeof(DropDownList))
            {
                ((DropDownList)ctrl).SelectedIndex = ((DropDownList)ctrl).Items.IndexOf(((DropDownList)ctrl).Items.FindByText("No"));

                ((DropDownList)ctrl).Enabled = false;
            }
        }
    }
    
    protected void SaveDailyCountButton_Click(object sender, EventArgs e)
    {
        int dayCounter = 1;
        string servingDate = "null";
        string attendance = "null";
        string firstMeals = "null";
        string seconds = "null";
        string progAdults = "null";
        string disallowed = "null";
        string dailyCountID = "null";
        string nutritionEducation = "null";

        foreach (Control ctrl in test.Controls)
        {
            //if (ctrl.GetType() == typeof(Label))
            //{
                if (ctrl.ID == "MealsSentLabel" + dayCounter && ((Label)ctrl).Text != "N/A")
                {
                    foreach (Control textbox in test.Controls)
                    {
                        if (textbox.ID == "DailyCountHiddenField" + dayCounter)
                        {
                            dailyCountID = ((HiddenField)textbox).Value;
                        }
                        else if (textbox.ID == "MealDateLabel" + dayCounter)
                        {
                            if (((Label)textbox).Text != "")
                            {
                                servingDate = ((Label)textbox).Text;
                            }
                            else
                            {
                                servingDate = "null";
                            }
                        }
                        else if (textbox.ID == "AttendanceTextBox" + dayCounter)
                        {
                            if (((TextBox)textbox).Text != "")
                            {
                                attendance = ((TextBox)textbox).Text;
                            }
                            else
                            {
                                attendance = "null";
                            }
                        }
                        else if (textbox.ID == "FirstMealTextBox" + dayCounter)
                        {
                            if (((TextBox)textbox).Text != "")
                            {
                                firstMeals = ((TextBox)textbox).Text;
                            }
                            else
                            {
                                firstMeals = "null";
                            }
                        }
                        else if (textbox.ID == "SecondsTextBox" + dayCounter)
                        {
                            if (((TextBox)textbox).Text != "")
                            {
                                seconds = ((TextBox)textbox).Text;
                            }
                            else
                            {
                                seconds = "null";
                            }
                        }
                        else if (textbox.ID == "ProgAdultsTextBox" + dayCounter)
                        {
                            if (((TextBox)textbox).Text != "")
                            {
                                progAdults = ((TextBox)textbox).Text;
                            }
                            else
                            {
                                progAdults = "null";
                            }
                        }
                        else if (textbox.ID == "NutritionEducationDropDownListBox" + dayCounter)
                        {
                            nutritionEducation = ((DropDownList)textbox).SelectedItem.Text;
                        }
                        else if (textbox.ID == "DisallowedTextBox" + dayCounter)
                        {
                            if (((TextBox)textbox).Text != "")
                            {
                                disallowed = ((TextBox)textbox).Text;
                            }
                            else
                            {
                                disallowed = "null";
                            }
                        }
                    }

                    m_SQL = "SELECT SiteID FROM Site WHERE SiteName = '" + SiteNameLabel.Text + "'";
                    DataSet siteNameDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                    string siteID = GCFDGlobals.dbGetValue(siteNameDataSet.Tables[0].Rows[0], "SiteID");

                    m_SQL = "EXEC spInsertDailyCountInformation " + siteID + ", " + DeliveryTypeDropDownList.SelectedItem.Value + ", '" + servingDate + "', " + attendance + ", " + firstMeals + ", " + seconds + ", " + progAdults + ", '" + nutritionEducation + "', " + disallowed;
                    DataSet dailyCountReportCreation = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);
                    
                    dailyCountID = "";

                    dayCounter = dayCounter + 1;
                }
                else if (ctrl.ID == "MealsSentLabel" + dayCounter && ((Label)ctrl).Text == "N/A")
                {
                    dayCounter = dayCounter + 1;
                }
            //}
        }

        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "message", "alert('Daily count has been saved.');", true);
    }

    protected void SiteName_OnClick(object sender, EventArgs e)
    {
        int dayCounter = 1;
        string mealTypeID = DeliveryTypeDropDownList.SelectedItem.Value;
        string attendance = "";
        string firstMeals = "";
        string seconds = "";
        string progAdults = "";
        string disallowed = "";
        bool displayData = false;
        string servingDate = "";
        string dailyCountID = "";
        string nutritionEducation = "";
        DateTime dailyCountSelectedDate = Convert.ToDateTime(DeliveryWeekTextBox.Text);

        foreach (Control ctrl in test.Controls)
        {
            if (ctrl.GetType() == typeof(TextBox))
            {
                ((TextBox)ctrl).Enabled = false;

                ((TextBox)ctrl).Text = "";

                if (ctrl.ID.Substring(0, 5) != "Total")
                {
                    ((TextBox)ctrl).BackColor = System.Drawing.Color.Gray;
                }                
            }
            else if (ctrl.GetType() == typeof(HiddenField))
            {
                ((HiddenField)ctrl).Value = "";
            }
            else if (ctrl.GetType() == typeof(Label))
            {
                if (ctrl.ID.Substring(0, 6) == "MealsS")
                {
                    ((Label)ctrl).Text = "N/A";
                }   
            }
            else if (ctrl.GetType() == typeof(DropDownList))
            {
                ((DropDownList)ctrl).SelectedIndex = ((DropDownList)ctrl).Items.IndexOf(((DropDownList)ctrl).Items.FindByText("No"));

                ((DropDownList)ctrl).Enabled = false;
            }
        }

        SiteNameLabel.Text = ((LinkButton)sender).Text;

        if (DeliveryTypeDropDownList.SelectedItem.Text != "SELECT DELIVERY TYPE")
        {
            MealTypeLabel.Text =DeliveryTypeDropDownList.SelectedItem.Text + " Delivery Counts";
        }

        while (dailyCountSelectedDate.DayOfWeek.ToString() != "Monday")
        {
            dailyCountSelectedDate = dailyCountSelectedDate.AddDays(-1);
        }

        DeliveryDateRangeLabel.Text = dailyCountSelectedDate.ToString("MM/dd/yyyy") + "-" +
                                        dailyCountSelectedDate.AddDays(6).ToString("MM/dd/yyyy");

        if (mealTypeID != "Select Delivery Type")
        {
            foreach (Control ctrl in test.Controls)
            {
                if (ctrl.ID == "MealDateLabel" + dayCounter)
                {
                    m_SQL = "SELECT MealCount FROM vwDelivery WHERE DeliveryDate <> '01/01/1900' AND ServingDate = '" + ((Label)ctrl).Text +
                            "' AND MealTypeID = " + mealTypeID + " AND SiteName = '" + ((LinkButton)sender).Text + "' AND DeliveryTypeName <> 'Cancelled'";
                    DataSet servingDateDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                    int MealCount = 0;

                    foreach (DataRow servingDateDataRow in servingDateDataSet.Tables[0].Rows)
                    {
                        foreach (Control label in test.Controls)
                        {
                            if (label.GetType() == typeof(Label) && label.ID == "MealsSentLabel" + dayCounter)
                            {
                                MealCount = Convert.ToInt32(GCFDGlobals.dbGetValue(servingDateDataRow, "MealCount")) + MealCount;

                                this.usedTextBox = this.Master.FindControl("MainContent").FindControl("TotalUsedTextBox" + dayCounter.ToString()) as TextBox;
                                this.usedTextBox.Text = "0";

                                this.unusedTextBox = this.Master.FindControl("MainContent").FindControl("TotalUnusedTextBox" + dayCounter.ToString()) as TextBox;
                                this.unusedTextBox.Text = Convert.ToString(MealCount);

                                ((Label)label).Text = Convert.ToString(MealCount);
                            }
                            else if (label.GetType() == typeof(TextBox) &&
                                     label.ID.Substring(label.ID.Length - 1, 1) == dayCounter.ToString())
                            {
                                ((TextBox)label).Enabled = true;

                                if (label.ID.Substring(0, 5) != "Total")
                                {
                                    ((TextBox)label).Text = "";

                                    ((TextBox)label).BackColor = System.Drawing.Color.White;
                                }
                            }
                            else if (label.GetType() == typeof(DropDownList) && label.ID.Substring(label.ID.Length - 1, 1) == dayCounter.ToString())
                            {
                                ((DropDownList)label).Enabled = true;
                            }
                        }
                    }

                    dayCounter = dayCounter + 1;
                }
            }

            dayCounter = 1;

            foreach (Control label in test.Controls)
            {
                if (label.ID == "MealsSentLabel" + dayCounter && ((Label)label).Text != "N/A")
                {
                    foreach (Control ctrl in test.Controls)
                    {
                        if (ctrl.ID == "MealDateLabel" + dayCounter)
                        {
                            servingDate = ((Label)ctrl).Text;

                            break;
                        }
                    }

                    m_SQL = "SELECT d.NutritionEducation, d.DailyCountID, d.Attendance, d.FirstMeals, d.Seconds, d.ProgAdults, d.Disallowed FROM DailyCount d, Site s WHERE d.SiteID = s.SiteID AND d.ServingDate = '" + servingDate +
                            "' AND d.MealTypeID = " + mealTypeID + " AND s.SiteName = '" + ((LinkButton)sender).Text + "'";
                    DataSet dailyCountDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                    if (dailyCountDataSet.Tables[0].Rows.Count > 0)
                    {
                        dailyCountID = GCFDGlobals.dbGetValue(dailyCountDataSet.Tables[0].Rows[0], "DailyCountID");
                        attendance = GCFDGlobals.dbGetValue(dailyCountDataSet.Tables[0].Rows[0], "Attendance");
                        firstMeals = GCFDGlobals.dbGetValue(dailyCountDataSet.Tables[0].Rows[0], "FirstMeals");
                        seconds = GCFDGlobals.dbGetValue(dailyCountDataSet.Tables[0].Rows[0], "Seconds");
                        progAdults = GCFDGlobals.dbGetValue(dailyCountDataSet.Tables[0].Rows[0], "ProgAdults");
                        disallowed = GCFDGlobals.dbGetValue(dailyCountDataSet.Tables[0].Rows[0], "Disallowed");
                        nutritionEducation = GCFDGlobals.dbGetValue(dailyCountDataSet.Tables[0].Rows[0], "NutritionEducation");
                            
                        displayData = true;
                    }
                    else
                    {
                        dayCounter = dayCounter + 1;
                    }
                }
                else if (label.ID == "MealsSentLabel" + dayCounter && ((Label)label).Text == "N/A")
                {
                    dayCounter = dayCounter + 1;
                }

                if (displayData)
                {
                    if (label.ID == "DailyCountHiddenField" + dayCounter)
                    {
                        ((HiddenField)label).Value = dailyCountID;
                    }
                    else if (label.ID == "AttendanceTextBox" + dayCounter)
                    {
                        ((TextBox)label).Text = attendance;
                    }
                    else if (label.ID == "FirstMealTextBox" + dayCounter)
                    {
                        UpdateTotals(dayCounter, firstMeals);

                        ((TextBox)label).Text = firstMeals;
                    }
                    else if (label.ID == "SecondsTextBox" + dayCounter)
                    {
                        UpdateTotals(dayCounter, seconds);

                        ((TextBox)label).Text = seconds;
                    }
                    else if (label.ID == "ProgAdultsTextBox" + dayCounter)
                    {
                        UpdateTotals(dayCounter, progAdults);

                        ((TextBox)label).Text = progAdults;
                    }
                    else if (label.ID == "DisallowedTextBox" + dayCounter)
                    {
                        UpdateTotals(dayCounter, disallowed);

                        ((TextBox)label).Text = disallowed;                        
                    }
                    else if (label.ID == "NutritionEducationDropDownListBox" + dayCounter)
                    {
                        ((DropDownList)label).SelectedIndex = ((DropDownList)label).Items.IndexOf(((DropDownList)label).Items.FindByText(nutritionEducation));

                        displayData = false;

                        dayCounter = dayCounter + 1;
                    }
                }
            }
        }
    }

    private void UpdateTotals(int dayCounter, string itemCount)
    {
        if (itemCount == "")
        {
            itemCount = "0";
        }

        this.usedTextBox = this.Master.FindControl("MainContent").FindControl("TotalUsedTextBox" + dayCounter.ToString()) as TextBox;
        this.usedTextBox.Text = Convert.ToString(Convert.ToInt32(this.usedTextBox.Text) + Convert.ToInt32(itemCount));
        
        this.label = this.Master.FindControl("MainContent").FindControl("MealsSentLabel" + dayCounter.ToString()) as Label;

        this.unusedTextBox = this.Master.FindControl("MainContent").FindControl("TotalUnusedTextBox" + dayCounter.ToString()) as TextBox;
        this.unusedTextBox.Text = Convert.ToString(Convert.ToInt32(this.label.Text) - Convert.ToInt32(this.usedTextBox.Text));
    }

    private string[] DeliverySiteListItems
    {
        get
        {
            //We assume the array of items will be small, so we use viewstate
            //  If the array were big you may need to use session, the cache API, 
            //  or even a database or filesystem to store the items between postbacks.

            object items = ViewState["DeliverySiteListItems"];

            if (items == null) // items are not in viewstate, read from data store
            {
                items = GetDeliverySiteListFromDb(); //get values from the data store

                ViewState["DeliverySiteListItems"] = items;//shove into viewstate
            }

            return (string[])items;
        }
        set
        {
            ViewState["DeliverySiteListItems"] = value;
        }
    }

    private string[] GetDeliverySiteListFromDb()
    {
        DateTime dailyCountSelectedDate = Convert.ToDateTime(DeliveryWeekTextBox.Text);

        while (dailyCountSelectedDate.DayOfWeek.ToString() != "Monday")
        {
            dailyCountSelectedDate = dailyCountSelectedDate.AddDays(-1);
        }     

        m_SQL = "SELECT DISTINCT SiteName FROM vwDelivery WHERE DeliveryDate <> '01/01/1900' AND ServingDate BETWEEN '" + dailyCountSelectedDate +
                           "' AND '" + dailyCountSelectedDate.AddDays(6) + "' AND MealTypeID = " + DeliveryTypeDropDownList.SelectedItem.Value + " AND DeliveryTypeName <> 'Cancelled'";
        DataSet servingDateDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        string[] ret = new string[servingDateDataSet.Tables[0].Rows.Count];

        int i = 0;

        foreach (DataRow SiteDataRow in servingDateDataSet.Tables[0].Rows)
        {
            ret[i] = GCFDGlobals.dbGetValue(SiteDataRow, "SiteName");

            i++;
        }

        return ret;
    }

    protected void DeliveryWeekTextBox_TextChanged(object sender, EventArgs e)
    {
        DateTime dailyCountSelectedDate = Convert.ToDateTime(DeliveryWeekTextBox.Text);
        int dayCounter = 1;

        SiteNameLabel.Text = "";

        while (dailyCountSelectedDate.DayOfWeek.ToString() != "Monday")
        {
            dailyCountSelectedDate = dailyCountSelectedDate.AddDays(-1);
        }

        DeliveryDateRangeLabel.Text = dailyCountSelectedDate.ToString("MM/dd/yyyy") + "-" +
                                                dailyCountSelectedDate.AddDays(6).ToString("MM/dd/yyyy");

        ViewState["DeliverySiteListItems"] = null;

        if (DeliveryTypeDropDownList.SelectedItem.Text != "SELECT DELIVERY TYPE")
        {
            DeliverySiteListView.DataSource = DeliverySiteListItems;

            DeliverySiteListView.DataBind();
        }

        foreach (Control ctrl in test.Controls)
        {
            if (ctrl.GetType() == typeof(Label))
            {
                if (ctrl.ID == "MealDateLabel" + dayCounter)
                {
                    ((Label)ctrl).Text =
                        dailyCountSelectedDate.AddDays(dayCounter - 1).ToString("MM/dd/yyyy");
                }
                else if (ctrl.ID == "MealsSentLabel" + dayCounter)
                {
                    ((Label)ctrl).Text = "N/A";
                }
            }
            else if (ctrl.GetType() == typeof(TextBox))
            {
                ((TextBox)ctrl).Enabled = false;

                ((TextBox)ctrl).Text = "";

                if (ctrl.ID.Substring(0, 5) != "Total")
                {
                    ((TextBox)ctrl).BackColor = System.Drawing.Color.Gray;
                }               

                if (ctrl.ID == "DisallowedTextBox" + dayCounter)
                {
                    dayCounter = dayCounter + 1;
                }
            }
            else if (ctrl.GetType() == typeof(HiddenField))
            {
                ((HiddenField)ctrl).Value = "";
            }
        }
    }
    
    protected void CancelDailyCountsButton_Click(object sender, EventArgs e)
    {
        int dayCounter = 1;
        string mealTypeID = DeliveryTypeDropDownList.SelectedItem.Value;
        string attendance = "";
        string firstMeals = "";
        string seconds = "";
        string progAdults = "";
        string disallowed = "";
        bool displayData = false;
        string servingDate = "";
        string dailyCountID = "";
        DateTime dailyCountSelectedDate = Convert.ToDateTime(DeliveryWeekTextBox.Text);

        foreach (Control ctrl in test.Controls)
        {
            if (ctrl.GetType() == typeof(TextBox))
            {
                ((TextBox)ctrl).Enabled = false;

                ((TextBox)ctrl).Text = "";                    

                if (ctrl.ID.Substring(0, 5) != "Total")
                {
                    ((TextBox)ctrl).BackColor = System.Drawing.Color.Gray;
                }                
            }
            else if (ctrl.GetType() == typeof(HiddenField))
            {
                ((HiddenField)ctrl).Value = "";
            }
            else if (ctrl.GetType() == typeof(Label))
            {
                if (ctrl.ID.Substring(0, 6) == "MealsS")
                {
                    ((Label)ctrl).Text = "N/A";
                }   
            }
        }

        if (mealTypeID != "Select Delivery Type")
        {
            foreach (Control ctrl in test.Controls)
            {
                if (ctrl.ID == "MealDateLabel" + dayCounter)
                {
                    m_SQL = "SELECT MealCount FROM vwDelivery WHERE DeliveryDate <> '01/01/1900' AND ServingDate = '" + ((Label)ctrl).Text +
                            "' AND MealTypeID = " + mealTypeID + " AND SiteName = '" + SiteNameLabel.Text + "' AND DeliveryTypeName <> 'Cancelled'";
                    DataSet servingDateDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                    int MealCount = 0;

                    foreach (DataRow servingDateDataRow in servingDateDataSet.Tables[0].Rows)
                    {
                        foreach (Control label in test.Controls)
                        {
                            if (label.GetType() == typeof(Label) && label.ID == "MealsSentLabel" + dayCounter)
                            {
                                MealCount = Convert.ToInt32(GCFDGlobals.dbGetValue(servingDateDataRow, "MealCount")) + MealCount;

                                this.usedTextBox = this.Master.FindControl("MainContent").FindControl("TotalUsedTextBox" + dayCounter.ToString()) as TextBox;
                                this.usedTextBox.Text = "0";

                                this.unusedTextBox = this.Master.FindControl("MainContent").FindControl("TotalUnusedTextBox" + dayCounter.ToString()) as TextBox;
                                this.unusedTextBox.Text = Convert.ToString(MealCount);

                                ((Label)label).Text = Convert.ToString(MealCount);
                            }
                            else if (label.GetType() == typeof(TextBox) &&
                                     label.ID.Substring(label.ID.Length - 1, 1) == dayCounter.ToString())
                            {
                                ((TextBox)label).Enabled = true;

                                if (label.ID.Substring(0, 5) != "Total")
                                {
                                    ((TextBox)label).Text = "";

                                    ((TextBox)label).BackColor = System.Drawing.Color.White;
                                }
                            }
                        }
                    }

                    dayCounter = dayCounter + 1;
                }
            }
        }
    }
}
