using System;
using System.Data;
using GCFDGlobalsNamespace;

public partial class MealDeliveryTypeDetails : System.Web.UI.Page
{
    public string m_SQL;

    public void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated && User.IsInRole("Programs-Admin"))
        {
            if (Session["SessionID"] == null)
            {
                System.Web.Security.FormsAuthentication.SignOut();

                Response.Redirect("~/Account/Login.aspx", false);

                Response.End();
            }
            else
            {
                Page.Master.Page.Form.DefaultFocus = SaveButton.UniqueID;

                MealDeliveryTypeNameTextBox.Focus();

                Load += (Page_Load);

                ProhibitPastActivityCheckBox.Attributes["onclick"] = "javascript:ProhibitPastActivityClick()";

                if (!IsPostBack)
                {
                    BuildDropDownLists();

                    DeleteMealDeliveryTypeButton.Attributes.Add("onclick", "return confirm('Are you sure you want to PERMANENTLY DELETE the " + MealDeliveryTypeNameTextBox.Text + " meal/delivery type?')");
                }
            }
        }
        else
        {
            Response.Redirect("Default.aspx", false);

            Response.End();
        }
    }

    public void BuildDropDownLists()
    {
        MealDeliveryTypeIDHiddenField.Value = Request.QueryString.Get("MealDeliveryTypeID");
        MealDeliveryTypeModeHiddenField.Value = Request.QueryString.Get("MealDeliveryTypeMode");

        if (MealDeliveryTypeModeHiddenField.Value == "Update")
        {
            m_SQL = "SELECT * FROM MealTypeDict WHERE MealTypeID = " + Request.QueryString.Get("MealDeliveryTypeID");
            DataSet mealDeliveryTypeDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            foreach (DataRow mealDeliveryTypeData in mealDeliveryTypeDataSet.Tables[0].Rows)
            {
                MealDeliveryTypeNameTextBox.Text = GCFDGlobals.dbGetValue(mealDeliveryTypeData, "MealTypeName");

                OriginalMealDeliveryTypeNameHiddenField.Value = GCFDGlobals.dbGetValue(mealDeliveryTypeData, "MealTypeName");

                ServingDayIntervalDropDownList.SelectedIndex = ServingDayIntervalDropDownList.Items.IndexOf(ServingDayIntervalDropDownList.Items.FindByValue(GCFDGlobals.dbGetValue(mealDeliveryTypeData, "ServingDayInterval")));

                if (GCFDGlobals.dbGetValue(mealDeliveryTypeData, "HasWeekendActivity") == "1")
                {
                    WeekendActivityCheckBox.Checked = true;
                }

                if (GCFDGlobals.dbGetValue(mealDeliveryTypeData, "ProhibitPastModifications") == "1")
                { 
                    ProhibitPastActivityCheckBox.Checked = true;
                }

                FutureIntervalTextBox.Text = GCFDGlobals.dbGetValue(mealDeliveryTypeData, "FutureActivityInterval");

                if (ProhibitPastActivityCheckBox.Checked)
                {
                    FutureIntervalTextBox.Enabled = true;

                    FutureActivityIntervalLabel.Enabled = true;

                    FutureActivityIntervalLabel.ForeColor = System.Drawing.Color.White;
                }
            }

            SaveAsNewButton.Visible = true;

            DeleteMealDeliveryTypeButton.Enabled = true;
            DeleteMealDeliveryTypeButton.Attributes.Add("onclick", "return confirm('Are you sure you want to PERMANENTLY DELETE the " + OriginalMealDeliveryTypeNameHiddenField.Value + " meal type?')");
        }
        else if (Request.QueryString.Get("MealDeliveryTypeMode") == "New")
        {
            SaveAsNewButton.Visible = false;

            DeleteMealDeliveryTypeButton.Enabled = false;
        }
    }

	protected void SaveButton_Click(object sender, EventArgs e)
	{
        if (Page.IsValid)
        {
            try
            {
                if ((MealDeliveryTypeModeHiddenField.Value == "New" && CreateItem() == true) || (MealDeliveryTypeModeHiddenField.Value == "Update" && MealDeliveryTypeNameTextBox.Text != OriginalMealDeliveryTypeNameHiddenField.Value && CreateItem() == true) || (MealDeliveryTypeModeHiddenField.Value == "Update" && MealDeliveryTypeNameTextBox.Text == OriginalMealDeliveryTypeNameHiddenField.Value))
                {
                    int weekendActivityEnabled = 0;
                    int prohibitPastActivity = 0;
                    string servingDayInterval = ServingDayIntervalDropDownList.SelectedItem.Value;

                    if (WeekendActivityCheckBox.Checked)
                    {
                        weekendActivityEnabled = 1;
                    }

                    if (ProhibitPastActivityCheckBox.Checked)
                    {
                        prohibitPastActivity = 1;
                    }

                    m_SQL = "UPDATE MealTypeDict SET MealTypeName = '" + MealDeliveryTypeNameTextBox.Text + "', ServingDayInterval = " + servingDayInterval + ", HasWeekendActivity = " + weekendActivityEnabled + ", ProhibitPastModifications = " + prohibitPastActivity + ", FutureActivityInterval = " + FutureIntervalTextBox.Text + " WHERE MealTypeID = " + MealDeliveryTypeIDHiddenField.Value;
                    GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                    MealDeliveryTypeModeHiddenField.Value = "Update";

                    DeleteMealDeliveryTypeButton.Enabled = true;

                    SaveAsNewButton.Visible = true;

                    OriginalMealDeliveryTypeNameHiddenField.Value = MealDeliveryTypeNameTextBox.Text;

                    DeleteMealDeliveryTypeButton.Attributes.Add("onclick", "return confirm('Are you sure you want to PERMANENTLY DELETE the " + OriginalMealDeliveryTypeNameHiddenField.Value + " meal type?')");

                    m_SQL = "INSERT INTO UserAction (UserName, Action) VALUES('" + User.Identity.Name + "', 'Meal Type Name= " + MealDeliveryTypeNameTextBox.Text + " Has Been Saved With The Following Values;ServingDayInterval=" + servingDayInterval + ", HasWeekendActivity=" + weekendActivityEnabled + ", ProhibitPastModifications = " + prohibitPastActivity + ", FutureActivityInterval = " + FutureIntervalTextBox.Text + "')";
                    GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                    MessageBox.Show("The " + MealDeliveryTypeNameTextBox.Text + " Meal Type Has Been Saved");
                }
                else
                {
                    MessageBox.Show("There already is an meal delivery type saved with the same name as the meal delivery type you're attempting to currently save. Change the current meal delivery type's name before attempting to save.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                throw;
            }
        }
	}

    protected Boolean CreateItem()
    {
        Boolean createItem = true;

        m_SQL = "SELECT * FROM MealTypeDict WHERE MealTypeName = '" + MealDeliveryTypeNameTextBox.Text + "'";
        DataSet mealDeliveryTypeNameTestDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        if (mealDeliveryTypeNameTestDataSet.Tables.Count > 0)
        {
            if (mealDeliveryTypeNameTestDataSet.Tables[0].Rows.Count > 0)
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

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        if (MealDeliveryTypeModeHiddenField.Value == "New")
        {
            m_SQL = "DELETE MealTypeDict WHERE MealTypeID = " + MealDeliveryTypeIDHiddenField.Value;
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
        }

        Response.Redirect("MealDeliveryType.aspx", false);
    }

    protected void SaveAsNewButton_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                if (CreateItem() == true)
                {
                    int weekendActivityEnabled = 0;
                    int prohibitPastActivity = 0;
                    string servingDayInterval = ServingDayIntervalDropDownList.SelectedItem.Value;

                    if (WeekendActivityCheckBox.Checked)
                    {
                        weekendActivityEnabled = 1;
                    }

                    if (ProhibitPastActivityCheckBox.Checked)
                    {
                        prohibitPastActivity = 1;
                    }

                    m_SQL = "INSERT INTO MealTypeDict (MealTypeName, ServingDayInterval, HasWeekendActivity, ProhibitPastModifications, FutureActivityInterval, DateCreated) VALUES('" + MealDeliveryTypeNameTextBox.Text + "', " + servingDayInterval + ", " + weekendActivityEnabled + ", " + prohibitPastActivity + ", " + FutureIntervalTextBox.Text + ", GETDATE())";
                    GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                    m_SQL = "SELECT SCOPE_IDENTITY() AS MealTypeID";
                    DataSet newMealTypeDataSet  = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                    MealDeliveryTypeIDHiddenField.Value = GCFDGlobals.dbGetValue(newMealTypeDataSet.Tables[0].Rows[0], "MealTypeID");

                    OriginalMealDeliveryTypeNameHiddenField.Value = MealDeliveryTypeNameTextBox.Text;

                    m_SQL = "INSERT INTO UserAction (UserName, Action) VALUES('" + User.Identity.Name + "', 'Meal Type Name= " + MealDeliveryTypeNameTextBox.Text + " Has Been Saved With The Following Values;ServingDayInterval=" + servingDayInterval + ", HasWeekendActivity=" + weekendActivityEnabled + ", ProhibitPastModifications = " + prohibitPastActivity + ", FutureActivityInterval = " + FutureIntervalTextBox.Text + "')";
                    GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                    MessageBox.Show("The " + MealDeliveryTypeNameTextBox.Text + " Meal Type Has Been Saved");
                }
                else
                {
                    MessageBox.Show("There already is an meal delivery type saved with the same name as the meal delivery type you're attempting to currently save. Change the current meal delivery type's name before attempting to save.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                throw;
            }
        }
    }

    protected void DeleteMealDeliveryTypeButton_Click(object sender, EventArgs e)
    {
        m_SQL = "DELETE MealTypeDict WHERE MealTypeID = " + MealDeliveryTypeIDHiddenField.Value;
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
        
        m_SQL = "INSERT INTO UserAction (UserName, Action) VALUES('" + User.Identity.Name + "', 'Meal Type Name= " + MealDeliveryTypeNameTextBox.Text + " Has Been Deleted')";
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

        Response.Redirect("MealDeliveryType.aspx", false);
    }
}