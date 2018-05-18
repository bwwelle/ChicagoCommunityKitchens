using System;
using System.Data;
using System.Web.UI.WebControls;
using GCFDGlobalsNamespace;
using System.Web.UI;

public partial class RecipeDetails : System.Web.UI.Page
{
    public string m_SQL;
    public string m_RecipeTypeID;
    public DataSet measurementsDataSet =
		GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet("SELECT DISTINCT RecipeUnit FROM Ingredient");
    public DataSet recipeTypeDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.GetRecipeType();
    public DataSet condimentDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet("SELECT * FROM Condiment");
    public DataSet ingredientDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet("SELECT * FROM Ingredient ORDER BY IngredientName");
    public DataSet condimentTypeDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet("SELECT * FROM CondimentType");
	public DataSet directionsDataSet;
    public DataSet recipeIngredientsDataSet;
    public DataSet recipeCondimentsDataSet;

    public void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated && User.IsInRole("Kitchen-Staff"))
        {
            if (Session["SessionID"] == null)
            {
                System.Web.Security.FormsAuthentication.SignOut();

                Response.Redirect("~/Account/Login.aspx", false);

                Response.End();
            }
            else
            {
                if (User.IsInRole("Kitchen-Staff") && !User.IsInRole("Kitchen-Admin"))
                {
                    SaveRecipeDetailButton.Enabled = false;

                    SaveAsNewButton.Enabled = false;

                    DeleteRecipeButton.Enabled = false;

                    AddNewCondimentButton.Enabled = false;

                    SaveAddIngredientButton.Enabled = false;

                    SaveIngredientChangesButton.Enabled = false;

                    DeleteIngredientButton.Enabled = false;

                    SaveRegularYieldDetailButton.Enabled = false;

                    SaveCookChillYieldDetailButton.Enabled = false;
                }

                NumberOfServingsTextBox.Attributes["onkeyup"] = "javascript:updateregularyield()";
                RegularServingSizeDropDownList.Attributes["onchange"] = "javascript:updateregularyield()";
                RegularVolumeDropDownList.Attributes["onchange"] = "javascript:updateregularyield()";
                NumberofPackagesTextBox.Attributes["onkeyup"] = "javascript:updatecookchillyield()";
                CookChillWeightDropDownList.Attributes["onchange"] = "javascript:updatecookchillyield()";

                Page.Master.Page.Form.DefaultFocus = SaveRecipeDetailButton.UniqueID;

                RecipeNameTextbox.Focus();

                Load += (Page_Load);

                CondimentCancelButton.OnClientClick = String.Format("fnClickOK('{0}','{1}')", CondimentCancelButton.UniqueID, "");

                DirectionCancelButton.OnClientClick = String.Format("fnClickOK('{0}','{1}')", DirectionCancelButton.UniqueID, "");

                IngredientCancelButton.OnClientClick = String.Format("fnClickOK('{0}','{1}')", IngredientCancelButton.UniqueID, "");

                CancelRegularYieldDetailButton.OnClientClick = String.Format("fnClickOK('{0}','{1}')", CancelRegularYieldDetailButton.UniqueID, "");

                CancelCookChillYieldDetailButton.OnClientClick = String.Format("fnClickOK('{0}','{1}')", CancelCookChillYieldDetailButton.UniqueID, "");

                if (!IsPostBack)
                {
                    BuildDropDownLists();

                    DeleteRecipeButton.Attributes.Add("onclick", "return confirm('Are you sure you want to PERMANENTLY DELETE the " + RecipeNameTextbox.Text + " recipe?')");

                    FillIngredientsDataGridView();

                    FillDirectionsDataGridView();

                    FillCondimentsDataGridView();
                }

                BuildIngredients();

                BuildDirections();

                BuildCondiments();

                switch (Session["YieldEditMode"].ToString())
                {
                    case ("RegularMode"):
                        ClientScript.RegisterStartupScript(GetType(), "key", "launchRegularYieldPopupModal();", true);

                        GetRegularRecipeYieldInPounds();

                        break;
                    case ("CookChillMode"):
                        ClientScript.RegisterStartupScript(GetType(), "key", "launchCookChillYieldPopupModal();", true);

                        GetCookChillRecipeYieldInPounds();

                        break;
                    case ("CancelRegularYield"):
                        RegularYieldRadioButton.Checked = true;

                        break;
                    case ("CancelCookChillYield"):
                        CookChillYieldRadioButton.Checked = true;

                        break;
                    case ("SavedRegularYield"):
                        RegularYieldRadioButton.Checked = true;

                        break;
                    case ("SavedCookChillYield"):
                        CookChillYieldRadioButton.Checked = true;

                        break;
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
		RecipeTypeDropDownList.DataValueField = "RecipeTypeID";
        RecipeTypeDropDownList.DataTextField = "RecipeTypeName";
        RecipeTypeDropDownList.DataSource = recipeTypeDataSet.Tables[0];
        RecipeTypeDropDownList.DataBind();
        RecipeTypeDropDownList.Items.Insert(0, new ListItem("SELECT RECIPE TYPE", "-1"));

		CondimentDropDownList.DataValueField = "CondimentID";
		CondimentDropDownList.DataTextField = "CondimentName";
		CondimentDropDownList.DataSource = condimentDataSet.Tables[0];
		CondimentDropDownList.DataBind();
		CondimentDropDownList.Items.Insert(0, new ListItem("SELECT CONDIMENT", "-1"));

		IngredientDropDownList.DataValueField = "IngredientID";
		IngredientDropDownList.DataTextField = "IngredientName";
		IngredientDropDownList.DataSource = ingredientDataSet.Tables[0];
		IngredientDropDownList.DataBind();
		IngredientDropDownList.Items.Insert(0, new ListItem("SELECT INGREDIENT", "-1"));

		CondimentUnitDropDownList.DataValueField = "CondimentTypeID";
		CondimentUnitDropDownList.DataTextField = "CondimentTypeName";
		CondimentUnitDropDownList.DataSource = condimentTypeDataSet.Tables[0];
		CondimentUnitDropDownList.DataBind();
		CondimentUnitDropDownList.Items.Insert(0, new ListItem("SELECT CONDIMENT TYPE", "-1"));

		if (Session["RecipeMode"].ToString() == "RecipeUpdate" || Session["RecipeMode"].ToString() == "RecipeSaved")
		{	  
			try                                               
			{
                SaveAsNewButton.Enabled = true;

				m_SQL = "SELECT DISTINCT RecipeName, YieldTypeID, RecipeTypeID, RecipeTypeName, Notes FROM vwRecipeDetail WHERE RecipeID = " + Session["RecipeID"] + " AND IsDefault = 'true'";
				DataSet recipeDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL); 

				foreach(DataRow recipeData in recipeDataSet.Tables[0].Rows)
				{
					RecipeNameTextbox.Text = GCFDGlobals.dbGetValue(recipeData, "RecipeName");

                    RecipeNotesTextBox.Text = GCFDGlobals.dbGetValue(recipeData, "Notes");

					if(GCFDGlobals.dbGetValue(recipeData, "YieldTypeID") == "1")
					{
						RegularYieldRadioButton.Checked = true;

						CookChillYieldRadioButton.Checked = false;
					}
					else
					{
						CookChillYieldRadioButton.Checked = true;

						RegularYieldRadioButton.Checked = false;
					}

					Session["OriginalRecipeName"] = GCFDGlobals.dbGetValue(recipeData, "RecipeName");

                    DeleteRecipeButton.Enabled = true;

					m_RecipeTypeID = GCFDGlobals.dbGetValue(recipeData, "RecipeTypeID");
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				
				throw;
			}

			foreach(DataRow recipeTypeData in recipeTypeDataSet.Tables[0].Rows)
			{
				if(m_RecipeTypeID == GCFDGlobals.dbGetValue(recipeTypeData, "RecipeTypeID"))
				{
					RecipeTypeDropDownList.SelectedIndex =
                        RecipeTypeDropDownList.Items.IndexOf(
                            RecipeTypeDropDownList.Items.FindByText(GCFDGlobals.dbGetValue(recipeTypeData,
																							  "RecipeTypeName")));
				}
			}
		}
		else
		{
			SaveAsNewButton.Enabled = false;

            DeleteRecipeButton.Enabled = false;
		}

		if(Session["RecipeTypeName"].ToString() != "None")
		{
			RecipeTypeDropDownList.SelectedIndex =
                        RecipeTypeDropDownList.Items.IndexOf(
                            RecipeTypeDropDownList.Items.FindByText(Session["RecipeTypeName"].ToString()));

			Session["RecipeTypeName"] = "None";
		}

		if(Session["NewRecipeName"].ToString() != "None")
		{
			RecipeNameTextbox.Text = Session["NewRecipeName"].ToString();
		}
    }

    public void BuildIngredients()
    {
		recipeIngredientsDataSet =
		  GCFDGlobals.m_GCFDPlannerDatabaseLibrary.GetRecipeIngredientsById(Session["RecipeID"].ToString());
    	
		string recipeIngredientID = "";
    	string ingredientName = "";

        switch (Session["RecipeIngredientMode"].ToString())
        {
            case "RecipeIngredientUpdate":
                foreach (DataRow recipeIngredientsData in recipeIngredientsDataSet.Tables[0].Rows)
                {
                    if (GCFDGlobals.dbGetValue(recipeIngredientsData, "RecipeIngredientID") == Session["RecipeIngredientID"].ToString())
                    {
                        IngredientMeasureTextBox.Text = GCFDGlobals.dbGetValue(recipeIngredientsData, "Measure");

                        ingredientName = GCFDGlobals.dbGetValue(recipeIngredientsData,
                                                                       "Ingredient Name").TrimEnd();

						recipeIngredientID = GCFDGlobals.dbGetValue(recipeIngredientsData,
																	   "RecipeIngredientID").TrimEnd();

						IngredientDropDownList.SelectedIndex =
                            IngredientDropDownList.Items.IndexOf(
                                IngredientDropDownList.Items.FindByText(ingredientName));

                        IngredientNoteTextBox.Text = GCFDGlobals.dbGetValue(recipeIngredientsData, "Notes");

                    	PrepInfoTextBox.Text = GCFDGlobals.dbGetValue(recipeIngredientsData, "Prep Info");

						IngredientNumberTextBox.Text = GCFDGlobals.dbGetValue(recipeIngredientsData, "Number");
					}
                }

				m_SQL = "SELECT RecipeUnit FROM Ingredient WHERE IngredientName = '" + ingredientName + "'";
				DataSet recipeUnitDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        		IngredientRecipeUnitLabel.Text = GCFDGlobals.dbGetValue(recipeUnitDataSet.Tables[0].Rows[0], "RecipeUnit");

				//IngredientDropDownList.Enabled = false;

				//IngredientNumberTextBox.Enabled = false;

                Page.Master.Page.Form.DefaultFocus = SaveAddIngredientButton.UniqueID;

        		DeleteIngredientButton.Enabled = true;

				Session["RecipeIngredientMode"] = "RecipeIngredientUpdating";

				ClientScript.RegisterStartupScript(GetType(), "key", "launchIngredientPopupModal();", true);

                break;

            case "RecipeIngredientDelete":
        		Session["RecipeIngredientID"] = "-1";

                Session["RecipeIngredientMode"] = "None";

                MessageBox.Show(Session["RecipeIngredientName"] + " Was Deleted As An Ingredient From The Recipe, " + Session["OriginalRecipeName"]);

                break;

            case "RecipeIngredientSaved":
				Session["RecipeIngredientID"] = "-1";

        		Session["RecipeIngredientMode"] = "None";

                MessageBox.Show(Session["RecipeIngredientName"] + " Was Saved As An Ingredient For The Recipe, " + Session["OriginalRecipeName"]);

                break;

            case "RecipeIngredientAdd":
				m_SQL = "SELECT * FROM RecipeIngredient WHERE RecipeID = " + Session["RecipeID"] + " ORDER BY IngredientNumber";
				DataSet recipeIngredientDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

				IngredientNumberTextBox.Text = Convert.ToString(recipeIngredientDataSet.Tables[0].Rows.Count + 1);

                Page.Master.Page.Form.DefaultFocus  = SaveAddIngredientButton.UniqueID;

				DeleteIngredientButton.Enabled = false;

				Session["RecipeIngredientMode"] = "RecipeIngredientAdding";

				ClientScript.RegisterStartupScript(GetType(), "key", "launchIngredientPopupModal();", true);

                break;
			case "RecipeIngredientCancel":
        		Session["RecipeIngredientID"] = "-1";

				Session["RecipeIngredientMode"] = "None";

        		break;
        }
    }

    public void BuildDirections()
    {
		directionsDataSet =
		 GCFDGlobals.m_GCFDPlannerDatabaseLibrary.GetRecipeDirectionsById(Session["RecipeID"].ToString());
        
		switch (Session["RecipeDirectionMode"].ToString())
        {
            case "RecipeDirectionUpdate":
                foreach (DataRow recipeDirectionsData in directionsDataSet.Tables[0].Rows)
                {
                    if (GCFDGlobals.dbGetValue(recipeDirectionsData, "RecipeDirectionID") == Session["RecipeDirectionID"].ToString())
                    {
                        DirectionStepNumberTextBox.Text = GCFDGlobals.dbGetValue(recipeDirectionsData, "Number").TrimEnd();

                        DirectionTextBox.Text = GCFDGlobals.dbGetValue(recipeDirectionsData, "Direction").TrimEnd();
                    }
                }

                Page.Master.Page.Form.DefaultFocus = SaveAddDirectionButton.UniqueID;

				DeleteDirection.Enabled = true;

				Session["RecipeDirectionMode"] = "RecipeDirectionUpdating";

				ClientScript.RegisterStartupScript(GetType(), "key", "launchDirectionPopupModal();", true);
				
				break;

            case "RecipeDirectionSaved":
				Session["RecipeDirectionID"] = "-1";

				Session["RecipeDirectionMode"] = "None";

                MessageBox.Show("Direction Number " + Session["RecipeDirectionNumber"] + " Was Saved As A Direction For The Recipe, " + Session["OriginalRecipeName"]);

                break;
            
            case "RecipeDirectionDelete":
				Session["RecipeDirectionID"] = "-1";

				Session["RecipeDirectionMode"] = "None";

                MessageBox.Show("Direction Number " + Session["RecipeDirectionNumber"] + " Was Deleted As A Direction For The Recipe, " + Session["OriginalRecipeName"]);

        		break;

            case "RecipeDirectionAdd":
        		m_SQL = "SELECT * FROM RecipeDirection WHERE RECIPEID = " + Session["RecipeID"] + " ORDER BY DirectionNumber";
				DataSet recipeDirectionDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        		DirectionStepNumberTextBox.Text = Convert.ToString(recipeDirectionDataSet.Tables[0].Rows.Count + 1);

                Page.Master.Page.Form.DefaultFocus = SaveAddDirectionButton.UniqueID;

				DeleteDirection.Enabled = false;

				Session["RecipeDirectionMode"] = "RecipeDirectionAdding";

				ClientScript.RegisterStartupScript(GetType(), "key", "launchDirectionPopupModal();", true);
				
                break;
			case "RecipeDirectionCancel":
        		Session["RecipeDirectionID"] = "-1";

				Session["RecipeDirectionMode"] = "None";

        		break;
        }
    }

    public void BuildCondiments()
	{
		recipeCondimentsDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.GetRecipeCondimentsById(Session["RecipeID"].ToString());

        switch (Session["RecipeCondimentMode"].ToString())
        {
			case "RecipeCondimentUpdate":
				foreach(DataRow recipeCondimentsData in recipeCondimentsDataSet.Tables[0].Rows)
				{
					if(GCFDGlobals.dbGetValue(recipeCondimentsData, "RecipeCondimentID") == Session["RecipeCondimentID"].ToString())
					{
						CondimentAmountTextBox.Text = GCFDGlobals.dbGetValue(recipeCondimentsData, "Amount");

						string condimentName = GCFDGlobals.dbGetValue(recipeCondimentsData,
																	   "Condiment");

						CondimentDropDownList.SelectedIndex =
							CondimentDropDownList.Items.IndexOf(
								CondimentDropDownList.Items.FindByText(condimentName));

						CondimentUnitDropDownList.SelectedIndex =
							CondimentUnitDropDownList.Items.IndexOf(
								CondimentUnitDropDownList.Items.FindByText(GCFDGlobals.dbGetValue(recipeCondimentsData,
																								"Units")));

						CondimentDeliveryUnitDropDownList.SelectedIndex =
							CondimentDeliveryUnitDropDownList.Items.IndexOf(
								CondimentDeliveryUnitDropDownList.Items.FindByText(GCFDGlobals.dbGetValue(recipeCondimentsData,
																								"Delivery Package")));
					}
				}

				CondimentDropDownList.Enabled = false;

				CondimentDropDownList.Focus();

                Page.Master.Page.Form.DefaultFocus = SaveAddCondimentButton.UniqueID;
    
				DeleteCondimentButton.Enabled = true;

				Session["RecipeCondimentMode"] = "RecipeCondimentUpdating";

				ClientScript.RegisterStartupScript(GetType(), "key", "launchCondimentPopupModal();", true);

        		break;
            case "RecipeCondimentDeleted":
        		Session["RecipeCondimentID"] = "-1"; 

				Session["RecipeCondimentMode"] = "None";

                MessageBox.Show(Session["RecipeCondimentName"] + " Has Been Deleted From The Recipe, '" + Session["OriginalRecipeName"] + "'");

                break;

            case "RecipeCondimentSaved":
				Session["RecipeCondimentID"] = "-1"; 

				Session["RecipeCondimentMode"] = "None";

                MessageBox.Show(Session["RecipeCondimentName"] + " Has Been Saved To The Recipe, '" + Session["OriginalRecipeName"] + "'");

                break;

			case "RecipeCondimentAdd":
				CondimentDropDownList.SelectedIndex =
							CondimentDropDownList.Items.IndexOf(
								CondimentDropDownList.Items.FindByText("SELECT CONDIMENT"));

        		CondimentAmountTextBox.Text = "";

				CondimentUnitDropDownList.SelectedIndex =
							CondimentUnitDropDownList.Items.IndexOf(
								CondimentUnitDropDownList.Items.FindByText("SELECT CONDIMENT TYPE"));

				CondimentDropDownList.Focus();

                Page.Master.Page.Form.DefaultFocus = SaveAddCondimentButton.UniqueID;

				DeleteCondimentButton.Enabled = false;

				Session["RecipeCondimentMode"] = "RecipeCondimentAdding";

				ClientScript.RegisterStartupScript(GetType(), "key", "launchCondimentPopupModal();", true);
                
				break;

			case "RecipeCondimentCancel": 

				Session["RecipeCondimentMode"] = "None";

        		Session["RecipeCondimentID"] = "-1";

        		break;
        }
    }																	  
    
    public void FillIngredientsDataGridView()
    {
        try
        {
			recipeIngredientsDataSet =
						GCFDGlobals.m_GCFDPlannerDatabaseLibrary.GetRecipeIngredientsById(Session["RecipeID"].ToString());

            if (recipeIngredientsDataSet.Tables[0].Rows.Count == 0)
            {
                AddDummyIngredientsData();

				int columnCount = IngredientsGridView.Rows[0].Cells.Count;

                IngredientsGridView.Rows[0].Cells.Clear();

				IngredientsGridView.Rows[0].Cells.Add(new TableCell());

				IngredientsGridView.Rows[0].Cells[0].ColumnSpan = columnCount;

				IngredientsGridView.Rows[0].Cells[0].Text = "Click Here or 'Add New Ingredient' Button Below To Add New Ingredients.";
            }
            else
            {
				IngredientsGridView.DataMember = recipeIngredientsDataSet.Tables[0].TableName;

				IngredientsGridView.DataSource = recipeIngredientsDataSet;

				IngredientsGridView.DataBind();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving ingredients information - " + ex.Message);
        }
    }

    private void AddDummyIngredientsData()
    {
        DataTable ingredientDataTable = new DataTable("DummyIngredientsTable");

        ingredientDataTable.Columns.Add("Ingredient Name");

        DataRow newRow = ingredientDataTable.NewRow();

        ingredientDataTable.Rows.Add(newRow);

		IngredientsGridView.DataSource = ingredientDataTable;

		IngredientsGridView.DataBind();
    }

    public void FillCondimentsDataGridView()
    {
        try
		{
			recipeCondimentsDataSet =
						GCFDGlobals.m_GCFDPlannerDatabaseLibrary.GetRecipeCondimentsById(Session["RecipeID"].ToString());

            if (recipeCondimentsDataSet.Tables[0].Rows.Count == 0)
            {
                AddDummyCondimentsData();

                int columnCount = CondimentsGridView.Rows[0].Cells.Count;

                CondimentsGridView.Rows[0].Cells.Clear();

                CondimentsGridView.Rows[0].Cells.Add(new TableCell());

                CondimentsGridView.Rows[0].Cells[0].ColumnSpan = columnCount;

				CondimentsGridView.Rows[0].Cells[0].Text = "Click Here or 'Add New Condiment' Button Below To Add New Condiments.";
            }
            else
            {
                CondimentsGridView.DataMember = recipeCondimentsDataSet.Tables[0].TableName;

                CondimentsGridView.DataSource = recipeCondimentsDataSet;

                CondimentsGridView.DataBind();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving condiments information - " + ex.Message);
        }
    }

    private void AddDummyCondimentsData()
    {
        try
        {
            DataTable condimentsDataTable = new DataTable("DummyCondimentTable");

            condimentsDataTable.Columns.Add("Condiments");

            DataRow newRow = condimentsDataTable.NewRow();

            condimentsDataTable.Rows.Add(newRow);

            CondimentsGridView.DataSource = condimentsDataTable;

            CondimentsGridView.DataBind();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving condiments information - " + ex.Message);
        }
    }

    public void FillDirectionsDataGridView()
    {
		directionsDataSet =
		   GCFDGlobals.m_GCFDPlannerDatabaseLibrary.GetRecipeDirectionsById(Session["RecipeID"].ToString());
        
		try
        {
            if (directionsDataSet.Tables[0].Rows.Count == 0)
            {
                AddDummyDirectionsData();

                int columnCount = DirectionsGridView.Rows[0].Cells.Count;

                DirectionsGridView.Rows[0].Cells.Clear();

                DirectionsGridView.Rows[0].Cells.Add(new TableCell());

                DirectionsGridView.Rows[0].Cells[0].ColumnSpan = columnCount;

				DirectionsGridView.Rows[0].Cells[0].Text = "Click Here or 'Add New Direction' Button Below To Add New Directions.";
            }
            else
            {
                DirectionsGridView.DataMember = directionsDataSet.Tables[0].TableName;

                DirectionsGridView.DataSource = directionsDataSet;

                DirectionsGridView.DataBind();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving directions information - " + ex.Message);
        }
    }

    private void AddDummyDirectionsData()
    {
        try
        {
            DataTable directionsDataTable = new DataTable("DummyDirectionsTable");

            directionsDataTable.Columns.Add("Directions");

            DataRow newRow = directionsDataTable.NewRow();

            directionsDataTable.Rows.Add(newRow);

            DirectionsGridView.DataSource = directionsDataTable;

            DirectionsGridView.DataBind();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving directions information - " + ex.Message);
        }
    }

	protected void SaveIngredientChangesButton_Click(object sender, EventArgs e)
	{
		switch (Session["RecipeIngredientMode"].ToString())
		{
			case "RecipeIngredientAdding":
				try
				{
					m_SQL = "SELECT COUNT(RecipeIngredientID) AS 'Count' FROM RecipeIngredient WHERE RecipeID = " + Session["RecipeID"] + " AND IngredientNumber = " + IngredientNumberTextBox.Text;
					ingredientDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

					if(Convert.ToInt32(GCFDGlobals.dbGetValue(ingredientDataSet.Tables[0].Rows[0], "Count")) + 1 != Convert.ToInt32(IngredientNumberTextBox.Text))
					{
						m_SQL = "UPDATE RecipeIngredient SET IngredientNumber = IngredientNumber + 1 WHERE IngredientNumber >= " +
							        IngredientNumberTextBox.Text + " AND RecipeID = " + Session["RecipeID"];
						GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
					}

                    string yieldType = "";

                    if (RegularYieldRadioButton.Checked)
                    {
                        yieldType = "1";
                    }
                    else
                    {
                        yieldType = "2";
                    }

                    m_SQL = "DECLARE @NewRecipeID int EXEC spInsertRecipeIngredient " + Session["RecipeID"] + ", '" + RecipeNameTextbox.Text + "', " + yieldType + ", '" + RecipeNotesTextBox.Text + "', " + 
                                    IngredientDropDownList.SelectedItem.Value + ", " + IngredientNumberTextBox.Text + ", " + IngredientMeasureTextBox.Text + ", '" + PrepInfoTextBox.Text + "', '" + IngredientNoteTextBox.Text +
                                    "', @NewRecipeID = @NewRecipeID OUTPUT SELECT @NewRecipeID as 'RecipeID'";
                    DataSet ingredientInsertDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

					Session["RecipeID"] = GCFDGlobals.dbGetValue(ingredientInsertDataSet.Tables[0].Rows[0], "RecipeID");

					Session["RecipeMode"] = "Update";
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);

					throw;
				}

				break;
			case "RecipeIngredientUpdating":
				m_SQL = "SELECT IngredientNumber FROM RecipeIngredient WHERE RecipeIngredientID = " + Session["RecipeIngredientID"];
				DataSet ingredientNumberDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

				if(GCFDGlobals.dbGetValue(ingredientNumberDataSet.Tables[0].Rows[0], "IngredientNumber") != IngredientNumberTextBox.Text)
				{
					if(Convert.ToInt32(IngredientNumberTextBox.Text) > Convert.ToInt32(GCFDGlobals.dbGetValue(ingredientNumberDataSet.Tables[0].Rows[0], "IngredientNumber")))
					{
						m_SQL = "UPDATE RecipeIngredient SET IngredientNumber = IngredientNumber - 1 WHERE IngredientNumber <= " +
						        IngredientNumberTextBox.Text + " AND IngredientNumber > " + GCFDGlobals.dbGetValue(ingredientNumberDataSet.Tables[0].Rows[0], "IngredientNumber") + " AND RecipeID = " + Session["RecipeID"];
						GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
					}
					else
					{
						m_SQL = "UPDATE RecipeIngredient SET IngredientNumber = IngredientNumber + 1 WHERE IngredientNumber >= " +
							   IngredientNumberTextBox.Text + " AND IngredientNumber < " + GCFDGlobals.dbGetValue(ingredientNumberDataSet.Tables[0].Rows[0], "IngredientNumber") + " AND RecipeID = " + Session["RecipeID"];
						GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
					}
				}

				m_SQL = "UPDATE RecipeIngredient SET IngredientID = " + IngredientDropDownList.SelectedItem.Value + ", IngredientNumber = " + IngredientNumberTextBox.Text + ", Measure = " +
						IngredientMeasureTextBox.Text + ", PrepInfo = '" + PrepInfoTextBox.Text + "', Notes = '" +
						IngredientNoteTextBox.Text + "', Active = 1 WHERE RecipeIngredientID = " + Session["RecipeIngredientID"];
					GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

				break;
		}

        Session["RecipeTypeName"] = RecipeTypeDropDownList.SelectedItem.Text;

		Session["NewRecipeName"] = RecipeNameTextbox.Text;

		Session["RecipeIngredientName"] = IngredientDropDownList.SelectedItem.Text;

		Session["RecipeIngredientMode"] = "RecipeIngredientSaved";

		Response.Redirect("RecipeDetails.aspx", false);
	}			 

	protected void SaveCondimentChangesButton_Click(object sender, EventArgs e)
	{
		if(Session["RecipeCondimentMode"].ToString() == "RecipeCondimentAdding")
		{
			try
			{
				m_SQL =
					"DECLARE @NewRecipeID int EXEC spInsertRecipeCondiment " + Session["RecipeID"] + ", " + CondimentDropDownList.SelectedItem.Value +
						", " + CondimentUnitDropDownList.SelectedItem.Value + ", '" + CondimentAmountTextBox.Text + "', '" + CondimentDeliveryUnitDropDownList.SelectedItem.Text + "', @NewRecipeID = @NewRecipeID OUTPUT SELECT @NewRecipeID as 'RecipeID'";
				DataSet condimentInsertDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

				Session["RecipeID"] = GCFDGlobals.dbGetValue(condimentInsertDataSet.Tables[0].Rows[0], "RecipeID");

				Session["RecipeMode"] = "Update";
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);

				throw;
			}
		}
		else
		{
			m_SQL =
				"UPDATE RecipeCondiment SET CondimentID = " + CondimentDropDownList.SelectedItem.Value + ", CondimentTypeID = " + CondimentUnitDropDownList.SelectedItem.Value + ", CondimentAmount = '" + CondimentAmountTextBox.Text +
				"', PackageType = '" + CondimentDeliveryUnitDropDownList.SelectedItem.Text + "' WHERE RecipeCondimentID = " + Session["RecipeCondimentID"];
			GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
		}

        Session["RecipeTypeName"] = RecipeTypeDropDownList.SelectedItem.Text;

		Session["NewRecipeName"] = RecipeNameTextbox.Text;

		Session["RecipeCondimentName"] = CondimentDropDownList.SelectedItem.Text.Trim();

		Session["RecipeCondimentMode"] = "RecipeCondimentSaved";

		Response.Redirect("RecipeDetails.aspx", false);
	}

	protected void IngredientsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";

            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(IngredientsGridView, "Select$" + e.Row.RowIndex);
        }

        e.Row.Cells[0].Visible = false;
    }

    protected void SaveDirection_Click(object sender, EventArgs e)
	{
		switch(Session["RecipeDirectionMode"].ToString())
		{
			case "RecipeDirectionAdding":
				try
				{
					m_SQL = "SELECT Count(RecipeDirectionID) AS 'Count' FROM RecipeDirection WHERE RecipeID = " + Session["RecipeID"] + " AND DirectionNumber = " + DirectionStepNumberTextBox.Text;
					directionsDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

					if(Convert.ToInt32(GCFDGlobals.dbGetValue(directionsDataSet.Tables[0].Rows[0], "Count")) + 1 != Convert.ToInt32(DirectionStepNumberTextBox.Text))
					{
						m_SQL = "UPDATE RecipeDirection SET DirectionNumber = DirectionNumber + 1 WHERE DirectionNumber >= " +
							        DirectionStepNumberTextBox.Text + " AND RecipeID = " + Session["RecipeID"];
						GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
					}

                    m_SQL =
                        "DECLARE @NewRecipeID int EXEC spInsertRecipeDirection " + Session["RecipeID"] + ", '" + RecipeNameTextbox.Text + "', " + DirectionStepNumberTextBox.Text +
                            ", '" + DirectionTextBox.Text + "', @NewRecipeID = @NewRecipeID OUTPUT SELECT @NewRecipeID as 'RecipeID'";
                    DataSet directionAddDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

					Session["RecipeID"] = GCFDGlobals.dbGetValue(directionAddDataSet.Tables[0].Rows[0], "RecipeID");

					Session["RecipeMode"] = "Update";
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message);

					throw;
				}

				break;
			case "RecipeDirectionUpdating":
				m_SQL = "SELECT DirectionNumber FROM RecipeDirection WHERE RecipeDirectionID = " + Session["RecipeDirectionID"];
				DataSet directionNumberDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

				if(GCFDGlobals.dbGetValue(directionNumberDataSet.Tables[0].Rows[0], "DirectionNumber") != DirectionStepNumberTextBox.Text)
		        {
					if(Convert.ToInt32(DirectionStepNumberTextBox.Text) > Convert.ToInt32(GCFDGlobals.dbGetValue(directionNumberDataSet.Tables[0].Rows[0], "DirectionNumber")))
		            {
						m_SQL = "UPDATE RecipeDirection SET DirectionNumber = DirectionNumber - 1 WHERE DirectionNumber <= " +
							DirectionStepNumberTextBox.Text + " AND DirectionNumber > " + GCFDGlobals.dbGetValue(directionNumberDataSet.Tables[0].Rows[0], "DirectionNumber") + " AND RecipeID = " + Session["RecipeID"];
						GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
		            }
		            else
		            {
						m_SQL = "UPDATE RecipeDirection SET DirectionNumber = DirectionNumber + 1 WHERE DirectionNumber >= " +
							DirectionStepNumberTextBox.Text + " AND DirectionNumber < " + GCFDGlobals.dbGetValue(directionNumberDataSet.Tables[0].Rows[0], "DirectionNumber") + " AND RecipeID = " + Session["RecipeID"];
						GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
					}
				}

				m_SQL = "UPDATE RecipeDirection SET DirectionNumber = " + DirectionStepNumberTextBox.Text + ", RecipeDirection = '" +
						DirectionTextBox.Text + "' WHERE RecipeDirectionID = " + Session["RecipeDirectionID"];
				GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

				break;
		}

        Session["RecipeTypeName"] = RecipeTypeDropDownList.SelectedItem.Text;

		Session["NewRecipeName"] = RecipeNameTextbox.Text;

        Session["RecipeDirectionNumber"] = DirectionStepNumberTextBox.Text;

        Session["RecipeDirectionMode"] = "RecipeDirectionSaved";

        Response.Redirect("RecipeDetails.aspx", false);
    }

    protected void DirectionsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";

            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(DirectionsGridView, "Select$" + e.Row.RowIndex);
        }

        e.Row.Cells[0].Visible = false;
    }

    protected void CondimentsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if(e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
			e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";

			e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(CondimentsGridView, "Select$" + e.Row.RowIndex);
		}

		e.Row.Cells[0].Visible = false; 
	}

    public void IngredientsGridView_SelectedIndexChanging(Object sender, GridViewSelectEventArgs e)
    {
		GridViewRow row = IngredientsGridView.Rows[e.NewSelectedIndex];

		if(row.Cells[0].Text == "Click Here or 'Add New Ingredient' Button Below To Add New Ingredients.")
        {
			Session["RecipeIngredientID"] = "-1";

            Session["RecipeIngredientMode"] = "RecipeIngredientAdd";
        }
        else
        {
            Session["RecipeIngredientID"] = row.Cells[0].Text;

            Session["RecipeIngredientMode"] = "RecipeIngredientUpdate";
        }

        Session["RecipeTypeName"] = RecipeTypeDropDownList.SelectedItem.Text;

		Session["NewRecipeName"] = RecipeNameTextbox.Text;
            
        Response.Redirect("RecipeDetails.aspx", false);
    }

    public void DirectionsGridView_SelectedIndexChanging(Object sender, GridViewSelectEventArgs e)
    {
        GridViewRow row = DirectionsGridView.Rows[e.NewSelectedIndex];

		if(row.Cells[0].Text == "Click Here or 'Add New Direction' Button Below To Add New Directions.")
        {
            Session["RecipeDirectionID"] = "-1";

            Session["RecipeDirectionMode"] = "RecipeDirectionAdd";
        }
        else
        {
            Session["RecipeDirectionID"] = row.Cells[0].Text;

            Session["RecipeDirectionMode"] = "RecipeDirectionUpdate";
        }

        Session["RecipeTypeName"] = RecipeTypeDropDownList.SelectedItem.Text;

		Session["NewRecipeName"] = RecipeNameTextbox.Text;

        Response.Redirect("RecipeDetails.aspx", false);
    }

	public void CondimentsGridView_SelectedIndexChanging(Object sender, GridViewSelectEventArgs e)
	{
		GridViewRow row = CondimentsGridView.Rows[e.NewSelectedIndex];

		if(row.Cells[0].Text == "Click Here or 'Add New Condiment' Button Below To Add New Condiments.")
		{
			Session["RecipeCondimentID"] = "-1";

			Session["RecipeCondimentMode"] = "RecipeCondimentAdd";
		}
		else
		{
			Session["RecipeCondimentID"] = row.Cells[0].Text;

			Session["RecipeCondimentMode"] = "RecipeCondimentUpdate";
		}

        Session["RecipeTypeName"] = RecipeTypeDropDownList.SelectedItem.Text;

		Session["NewRecipeName"] = RecipeNameTextbox.Text;

		Response.Redirect("RecipeDetails.aspx", false);
	}
    
    protected void DeleteIngredientButton_Click(object sender, EventArgs e)
    {
        Session["RecipeIngredientName"] = IngredientDropDownList.SelectedItem.Text;

		m_SQL = "UPDATE RecipeIngredient SET IngredientNumber = IngredientNumber - 1 WHERE IngredientNumber >= " + IngredientNumberTextBox.Text + " AND RecipeID = " + Session["RecipeID"];
		GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

        m_SQL = "DELETE RecipeIngredient WHERE RecipeIngredientID = " + Session["RecipeIngredientID"];
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

        Session["RecipeIngredientMode"] = "RecipeIngredientDelete";

        Session["RecipeTypeName"] = RecipeTypeDropDownList.SelectedItem.Text;

		Session["NewRecipeName"] = RecipeNameTextbox.Text;

        Response.Redirect("RecipeDetails.aspx", false);
    }

	protected void DeleteCondimentButton_Click(object sender, EventArgs e)
	{
		Session["RecipeCondimentName"] = CondimentDropDownList.SelectedItem.Text.Trim();

		m_SQL = "DELETE RecipeCondiment WHERE RecipeID = " + Session["RecipeID"] + " AND CondimentID = " + CondimentDropDownList.SelectedItem.Value;
		GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

		Session["RecipeCondimentMode"] = "RecipeCondimentDeleted";

		Response.Redirect("RecipeDetails.aspx", false);
	}

    protected void DeleteDirection_Click(object sender, EventArgs e)
    {
        Session["RecipeDirectionNumber"] = DirectionStepNumberTextBox.Text;

		m_SQL ="UPDATE RecipeDirection SET DirectionNumber = DirectionNumber - 1 WHERE DirectionNumber >= " + DirectionStepNumberTextBox.Text + " AND RecipeID = " + Session["RecipeID"];
		GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

        m_SQL = "DELETE RecipeDirection WHERE RecipeDirectionID = " + Session["RecipeDirectionID"];
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

        Session["RecipeDirectionMode"] = "RecipeDirectionDelete";

        Response.Redirect("RecipeDetails.aspx", false);
    }

    protected void AddNewIngredientButton_Click(object sender, EventArgs e)
    {
        Session["RecipeTypeName"] = RecipeTypeDropDownList.SelectedItem.Text;

		Session["NewRecipeName"] = RecipeNameTextbox.Text;

        Session["RecipeIngredientID"] = "-1";

        Session["RecipeIngredientMode"] = "RecipeIngredientAdd";

        Response.Redirect("RecipeDetails.aspx", false);
    }

    protected void AddNewDirectionButton_Click(object sender, EventArgs e)
    {
        Session["RecipeTypeName"] = RecipeTypeDropDownList.SelectedItem.Text;

		Session["NewRecipeName"] = RecipeNameTextbox.Text;

        Session["RecipeDirectionID"] = "-1";

        Session["RecipeDirectionMode"] = "RecipeDirectionAdd";

        Response.Redirect("RecipeDetails.aspx", false);
    }

    protected void AddNewCondimentButton_Click(object sender, EventArgs e)
    {
        Session["RecipeTypeName"] = RecipeTypeDropDownList.SelectedItem.Text;

		Session["NewRecipeName"] = RecipeNameTextbox.Text;

		Session["RecipeCondimentID"] = "-1";

		Session["RecipeCondimentMode"] = "RecipeCondimentAdd";

		Response.Redirect("RecipeDetails.aspx", false);
    }

    protected void CondimentCancelButton_Click(object sender, EventArgs e)
    {
		Session["RecipeCondimentMode"] = "RecipeCondimentCancel";

		Response.Redirect("RecipeDetails.aspx", false);
    }

    protected void DirectionCancelButton_Click(object sender, EventArgs e)
    {
		Session["RecipeDirectionMode"] = "RecipeDirectionCancel";

		Response.Redirect("RecipeDetails.aspx", false);
    }

    protected void IngredientCancelButton_Click(object sender, EventArgs e)
    {
		Session["RecipeIngredientMode"] = "RecipeIngredientCancel";

		Response.Redirect("RecipeDetails.aspx", false);
    }
    
	protected void ServingSizeTypeDropdownList_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (ServingSizeTypeDropdownList.SelectedItem.Text == "ounce(s)")
		{
			PerPanLabel.Text = "# of Pounds Per ";

			VolumeWeightLabel.Text = "Volume Equivalent:";

			VolumeWeightTypeLabel.Text = "cup(s)";
		}
		else
		{
			PerPanLabel.Text = "# of Pieces Per ";

			VolumeWeightLabel.Text = "Weight Equivalent:";

			VolumeWeightTypeLabel.Text = "ounce(s)";	
		}
	}

	protected void GetCookChillRecipeYieldInPounds()
	{
		decimal numberOfServingsPerPackage;
		decimal numberOfServingsPerBatch;
		decimal batchYieldInPounds;
        string packageType = PackageDropdownList.SelectedItem.Text;
        decimal volumeServingSize = Convert.ToDecimal(VolumeServingSizeDropDownList.SelectedItem.Value);		
		
		if(NumberofPackagesTextBox.Text != "")
        {
        	int numberOfPackages = Convert.ToInt32(NumberofPackagesTextBox.Text);
			decimal weightEquivalent = Convert.ToDecimal(CookChillWeightDropDownList.SelectedItem.Value);

			if(packageType == "1 gallon souper bag(s) (16c)")
			{
				numberOfServingsPerPackage = 16 / volumeServingSize;
            }
			else
			{
				numberOfServingsPerPackage = 12 / volumeServingSize;
			}

			numberOfServingsPerBatch = numberOfPackages * Convert.ToInt32(numberOfServingsPerPackage);
        	batchYieldInPounds = numberOfServingsPerBatch * weightEquivalent / 16;

        	ServingsPerPackageLabel.Text = Convert.ToString(Math.Round(numberOfServingsPerPackage, 2));
        	ServingsPerBatchLabel.Text = Convert.ToString(Math.Round(numberOfServingsPerBatch, 2));
        	BatchYieldInPoundsLabel.Text = Convert.ToString(Math.Round(batchYieldInPounds, 2));
		} 
	}

	protected void GetRegularRecipeYieldInPounds()
	{
		Decimal recipeYieldValue = 0;

		if(NumberOfServingsTextBox.Text != "")
		{
			if(ServingSizeTypeDropdownList.SelectedItem.Text == "ounce(s)")
			{
                recipeYieldValue = Convert.ToInt32(NumberOfServingsTextBox.Text) * Convert.ToDecimal(RegularServingSizeDropDownList.SelectedItem.Value);
			}
			else
			{
                recipeYieldValue = Convert.ToInt32(NumberOfServingsTextBox.Text) * Convert.ToDecimal(RegularVolumeDropDownList.SelectedItem.Value); 	
			}

            recipeYieldValue = recipeYieldValue / 16;

            RecipeYieldLabel.Text = Convert.ToString(Math.Round(recipeYieldValue, 2));
		}
	}

	protected string DecimalToEntityConversion(decimal valueForConversion)
	{
		string newValueForDropDown = "";
		string valueForFraction = "";
		string valueConvertedToString = Convert.ToString(valueForConversion);

		string decimalValueConvertedToString = valueConvertedToString.Substring(valueConvertedToString.Length - 3, 3);

		switch (decimalValueConvertedToString)
		{
			case(".25"):
				valueForFraction = "1/4";

				break;
			case(".50"):
				valueForFraction = "1/2";

				break;
			case(".75"):
				valueForFraction = "3/4";

				break;
		}

		if(decimalValueConvertedToString == ".00")
		{
			newValueForDropDown = valueConvertedToString.Substring(0, valueConvertedToString.Length - 3);
		}
		else
		{
			newValueForDropDown = valueConvertedToString.Substring(0, valueConvertedToString.Length - 3) + " " + valueForFraction;
		}

		return newValueForDropDown;
	}
    	
	protected void PackageDropdownList_SelectedIndexChanged(object sender, EventArgs e)
	{
		switch(PackageDropdownList.SelectedItem.Text)
		{
			case "1 gallon souper bag(s) (16c)":
				PackageTypeLabel.Text = "1 gallon souper bag(s) (16c)";

				break;

			case "3/4 gallon souper bag(s) (12c)":
				PackageTypeLabel.Text = "3/4 gallon souper bag(s) (12c)";

				break;
		}

		GetCookChillRecipeYieldInPounds();

		ClientScript.RegisterStartupScript(GetType(), "key", "launchCookChillYieldPopupModal();", true);
	}
    	
	protected void RegularYieldRadioButton_CheckedChanged(object sender, EventArgs e)
	{
		if (CookChillYieldRadioButton.Checked)
		{
			CookChillYieldRadioButton.Checked = false;
		}
	}

	protected void CookChillYieldRadioButton_CheckedChanged(object sender, EventArgs e)
	{
		if(RegularYieldRadioButton.Checked)
		{
			RegularYieldRadioButton.Checked = false;
		}
	}

	protected void IngredientDropDownList_SelectedIndexChanged(object sender, EventArgs e)
	{
		m_SQL = "SELECT RecipeUnit FROM Ingredient Where IngredientID = " + IngredientDropDownList.SelectedItem.Value;
		DataSet recipeUnitIDDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

		IngredientRecipeUnitLabel.Text = GCFDGlobals.dbGetValue(recipeUnitIDDataSet.Tables[0].Rows[0], "RecipeUnit");

        Page.Master.Page.Form.DefaultFocus = SaveAddIngredientButton.UniqueID;

		ClientScript.RegisterStartupScript(GetType(), "key", "launchIngredientPopupModal();", true);
	}
	
	protected void NumberofPackagesTextBox_TextChanged(object sender, EventArgs e)
	{

	}

	protected void SaveAddIngredientChangesButton_Click(object sender, EventArgs e)
	{
		switch(Session["RecipeIngredientMode"].ToString())
		{
			case "RecipeIngredientAdding":
				try
				{
					m_SQL = "SELECT COUNT(RecipeIngredientID) AS 'Count' FROM RecipeIngredient WHERE RECIPEID = " + Session["RecipeID"] + " AND IngredientNumber = " + IngredientNumberTextBox.Text;
					ingredientDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

					if(Convert.ToInt32(GCFDGlobals.dbGetValue(ingredientDataSet.Tables[0].Rows[0], "Count")) + 1 != Convert.ToInt32(IngredientNumberTextBox.Text))
					{
						m_SQL = "UPDATE RecipeIngredient SET IngredientNumber = IngredientNumber + 1 WHERE IngredientNumber >= " +
									IngredientNumberTextBox.Text + " AND RecipeID = " + Session["RecipeID"];
						GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
					}

                    string yieldType = "";

                    if (RegularYieldRadioButton.Checked)
                    {
                        yieldType = "1";
                    }
                    else
                    {
                        yieldType = "2";
                    }

                    m_SQL = "DECLARE @NewRecipeID int EXEC spInsertRecipeIngredient " + Session["RecipeID"] + ", '" + RecipeNameTextbox.Text + "', " + yieldType + ", '" + RecipeNotesTextBox.Text + "', " +
									IngredientDropDownList.SelectedItem.Value + ", " + IngredientNumberTextBox.Text + ", " + IngredientMeasureTextBox.Text + ", '" + PrepInfoTextBox.Text + "', '" + IngredientNoteTextBox.Text +
									"', @NewRecipeID = @NewRecipeID OUTPUT SELECT @NewRecipeID as 'RecipeID'";
					DataSet ingredientInsertDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

					Session["RecipeID"] = GCFDGlobals.dbGetValue(ingredientInsertDataSet.Tables[0].Rows[0], "RecipeID");

					Session["RecipeMode"] = "Update";
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message);

					throw;
				}

				break;
			case "RecipeIngredientUpdating":
				m_SQL = "SELECT IngredientNumber FROM RecipeIngredient WHERE RecipeIngredientID = " + Session["RecipeIngredientID"];
				DataSet ingredientNumberDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

				if(GCFDGlobals.dbGetValue(ingredientNumberDataSet.Tables[0].Rows[0], "IngredientNumber") != IngredientNumberTextBox.Text)
				{
					if(Convert.ToInt32(IngredientNumberTextBox.Text) > Convert.ToInt32(GCFDGlobals.dbGetValue(ingredientNumberDataSet.Tables[0].Rows[0], "IngredientNumber")))
					{
						m_SQL = "UPDATE RecipeIngredient SET IngredientNumber = IngredientNumber - 1 WHERE IngredientNumber <= " +
								IngredientNumberTextBox.Text + " AND IngredientNumber > " + GCFDGlobals.dbGetValue(ingredientNumberDataSet.Tables[0].Rows[0], "IngredientNumber") + " AND RecipeID = " + Session["RecipeID"];
						GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
					}
					else
					{
						m_SQL = "UPDATE RecipeIngredient SET IngredientNumber = IngredientNumber + 1 WHERE IngredientNumber >= " +
							   IngredientNumberTextBox.Text + " AND IngredientNumber < " + GCFDGlobals.dbGetValue(ingredientNumberDataSet.Tables[0].Rows[0], "IngredientNumber") + " AND RecipeID = " + Session["RecipeID"];
						GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
					}
				}

				m_SQL = "UPDATE RecipeIngredient SET IngredientID = " + IngredientDropDownList.SelectedItem.Value + ", IngredientNumber = " + IngredientNumberTextBox.Text + ", Measure = " +
						IngredientMeasureTextBox.Text + ", PrepInfo = '" + PrepInfoTextBox.Text + "', Notes = '" +
						IngredientNoteTextBox.Text + "', Active = 1 WHERE RecipeIngredientID = " + Session["RecipeIngredientID"];
				GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

				break;
		}

        Session["RecipeTypeName"] = RecipeTypeDropDownList.SelectedItem.Text;

		Session["NewRecipeName"] = RecipeNameTextbox.Text;

		Session["RecipeIngredientMode"] = "RecipeIngredientAdd";

		Response.Redirect("RecipeDetails.aspx", false);

	}
	
	protected void SaveAddCondimentChangesButton_Click(object sender, EventArgs e)
	{
		if(Session["RecipeCondimentID"].ToString() == "-1" || Session["RecipeCondimentMode"].ToString() == "RecipeCondimentAdd")
		{
			m_SQL = "INSERT INTO RecipeCondiment (CondimentID, RecipeID, CondimentTypeID, CondimentAmount) VALUES(" +
					CondimentDropDownList.SelectedItem.Value + ", " + Session["RecipeID"] + ", " + CondimentUnitDropDownList.SelectedItem.Value + ", '" + CondimentAmountTextBox.Text + "')";
			GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
		}
		else
		{
			m_SQL =
				"UPDATE RecipeCondiment SET CondimentID = " + CondimentDropDownList.SelectedItem.Value + ", CondimentTypeID = " + CondimentUnitDropDownList.SelectedItem.Value + ", CondimentAmount = '" + CondimentAmountTextBox.Text +
				"' WHERE RecipeCondimentID = " + Session["RecipeCondimentID"];
			GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
		}

		Session["MealComponentType"] = RecipeTypeDropDownList.SelectedItem.Text;

		Session["NewRecipeName"] = RecipeNameTextbox.Text;

		Session["RecipeCondimentMode"] = "RecipeCondimentAdd";

		Response.Redirect("RecipeDetails.aspx", false);
	}
	
	protected void SaveAddDirectionChangesButton_Click(object sender, EventArgs e)
	{
		switch(Session["RecipeDirectionMode"].ToString())
		{
			case "RecipeDirectionAdding":
				try
				{
					m_SQL = "SELECT Count(RecipeDirectionID) AS 'Count' FROM RecipeDirection WHERE RECIPEID = " + Session["RecipeID"] + " AND DirectionNumber = " + DirectionStepNumberTextBox.Text;
					directionsDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

					if(Convert.ToInt32(GCFDGlobals.dbGetValue(directionsDataSet.Tables[0].Rows[0], "Count")) + 1 != Convert.ToInt32(DirectionStepNumberTextBox.Text))
					{
						m_SQL = "UPDATE RecipeDirection SET DirectionNumber = DirectionNumber + 1 WHERE DirectionNumber >= " +
									DirectionStepNumberTextBox.Text + " AND RecipeID = " + Session["RecipeID"];
						GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
					}

                    m_SQL =
                        "DECLARE @NewRecipeID int EXEC spInsertRecipeDirection " + Session["RecipeID"] + ", '" + RecipeNameTextbox.Text + "', " + DirectionStepNumberTextBox.Text +
                            ", '" + DirectionTextBox.Text + "', @NewRecipeID = @NewRecipeID OUTPUT SELECT @NewRecipeID as 'RecipeID'";
                    DataSet directionAddDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

					Session["RecipeID"] = GCFDGlobals.dbGetValue(directionAddDataSet.Tables[0].Rows[0], "RecipeID");

					Session["RecipeMode"] = "Update";
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message);

					throw;
				}

				break;
			case "RecipeDirectionUpdating":
				m_SQL = "SELECT DirectionNumber FROM RecipeDirection WHERE RecipeDirectionID = " + Session["RecipeDirectionID"];
				DataSet directionNumberDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

				if(GCFDGlobals.dbGetValue(directionNumberDataSet.Tables[0].Rows[0], "DirectionNumber") != DirectionStepNumberTextBox.Text)
				{
					if(Convert.ToInt32(DirectionStepNumberTextBox.Text) > Convert.ToInt32(GCFDGlobals.dbGetValue(directionNumberDataSet.Tables[0].Rows[0], "DirectionNumber")))
					{
						m_SQL = "UPDATE RecipeDirection SET DirectionNumber = DirectionNumber - 1 WHERE DirectionNumber <= " +
							DirectionStepNumberTextBox.Text + " AND DirectionNumber > " + GCFDGlobals.dbGetValue(directionNumberDataSet.Tables[0].Rows[0], "DirectionNumber") + " AND RecipeID = " + Session["RecipeID"];
						GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
					}
					else
					{
						m_SQL = "UPDATE RecipeDirection SET DirectionNumber = DirectionNumber + 1 WHERE DirectionNumber >= " +
							DirectionStepNumberTextBox.Text + " AND DirectionNumber < " + GCFDGlobals.dbGetValue(directionNumberDataSet.Tables[0].Rows[0], "DirectionNumber") + " AND RecipeID = " + Session["RecipeID"];
						GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
					}
				}

				m_SQL = "UPDATE RecipeDirection SET DirectionNumber = " + DirectionStepNumberTextBox.Text + ", RecipeDirection = '" +
						DirectionTextBox.Text + "' WHERE RecipeDirectionID = " + Session["RecipeDirectionID"];
				GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

				break;
		}

		Session["RecipeTypeName"] = RecipeTypeDropDownList.SelectedItem.Text;

		Session["NewRecipeName"] = RecipeNameTextbox.Text;

		Session["RecipeDirectionMode"] = "RecipeDirectionAdd";

		Response.Redirect("RecipeDetails.aspx", false);

	}
	
	protected void ViewReportButton_Click(object sender, EventArgs e)
	{
		Response.Redirect("RecipeReport.aspx", false);	
	}

	protected void DeliveryPackageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
	{
		DeliveryPackageTypeLabel.Text = DeliveryPackageDropDownList.SelectedItem.Text + ":";
	}
    
    protected void ModifyYieldDetailsButton_Click(object sender, EventArgs e)
    {
        Session["RecipeTypeName"] = RecipeTypeDropDownList.SelectedItem.Text;

        Session["NewRecipeName"] = RecipeNameTextbox.Text;

        if (RegularYieldRadioButton.Checked)
        {
            RegularYield();

            NumberOfServingsTextBox.Focus();

            Page.Master.Page.Form.DefaultFocus = SaveRegularYieldDetailButton.UniqueID;

            ClientScript.RegisterStartupScript(GetType(), "key", "launchRegularYieldPopupModal();", true);

            Session["YieldEditMode"] = "RegularMode";
        }

        if (CookChillYieldRadioButton.Checked)
        {
            CookChillYield();

            PackageDropdownList.Focus();

            Page.Master.Page.Form.DefaultFocus = SaveCookChillYieldDetailButton.UniqueID;

            ClientScript.RegisterStartupScript(GetType(), "key", "launchCookChillYieldPopupModal();", true);

            Session["YieldEditMode"] = "CookChillMode";
        }
    }

    public void RegularYield()
    {
        RecipeDetail recipeYield = new RecipeDetail();

        recipeYield.RecipeID = Session["RecipeID"].ToString();

        recipeYield.GetDefaultRecipeDetail();

        ServingSizeTypeDropdownList.SelectedIndex =
                    ServingSizeTypeDropdownList.Items.IndexOf(
                        ServingSizeTypeDropdownList.Items.FindByText(recipeYield.ServingSizeTypeName));

        if (recipeYield.ServingSizeTypeName == "ounce(s)")
        {
            PerPanLabel.Text = "# of Pounds Per ";
        }
        else
        {
            PerPanLabel.Text = "# of Pieces Per ";
        }

        if (!String.IsNullOrEmpty(recipeYield.PackageTypeName))
        {
            if (recipeYield.PackageTypeName.IndexOf("gallon", 0) > 0)
            {
                DeliveryPackageDropDownList.SelectedIndex =
                    DeliveryPackageDropDownList.Items.IndexOf(
                        DeliveryPackageDropDownList.Items.FindByText("Pan"));

                DeliveryPackageTypeLabel.Text = "Pan:";
            }
            else
            {
                DeliveryPackageDropDownList.SelectedIndex =
                    DeliveryPackageDropDownList.Items.IndexOf(
                        DeliveryPackageDropDownList.Items.FindByText(recipeYield.PackageTypeName));

                DeliveryPackageTypeLabel.Text = recipeYield.PackageTypeName + ":";
            }
        }

        NumberServingsPerPackagesTextBox.Text = recipeYield.ServingsPerPackage;

        if (recipeYield.ServingSizeTypeName == "ounce(s)")
        {
            VolumeWeightLabel.Text = "Volume Equivalent:";

            VolumeWeightTypeLabel.Text = "cup(s)";
        }
        else
        {
            VolumeWeightLabel.Text = "Weight Equivalent:";

            VolumeWeightTypeLabel.Text = "ounce(s)";
        }

        RegularServingSizeDropDownList.SelectedIndex =
            RegularServingSizeDropDownList.Items.IndexOf(
                RegularServingSizeDropDownList.Items.FindByValue(recipeYield.ServingSize));

        RegularVolumeDropDownList.SelectedIndex =
            RegularVolumeDropDownList.Items.IndexOf(
                RegularVolumeDropDownList.Items.FindByValue(recipeYield.VolumeWeight));

        NumberOfServingsTextBox.Text = recipeYield.NumberOfServings;

        RecipeYieldLabel.Text = recipeYield.YieldInPounds;

        m_SQL = "SELECT SUM(ric.IngredientCost) AS SumIngredientCost, ROUND(SUM(ric.IngredientCost)/r.NumberofServings, 2) AS RecipeIngredientCost FROM vwRecipeIngredientCost ric INNER JOIN vwRecipeDetail r ON r.RecipeID = ric.RecipeID WHERE r.RecipeID = " + Session["RecipeID"] + " AND r.IsDefault = 'true' GROUP BY r.NumberofServings";
        DataSet recipeCostDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        if (recipeCostDataSet.Tables[0].Rows.Count > 0)
        {
            if (!String.IsNullOrEmpty(GCFDGlobals.dbGetValue(recipeCostDataSet.Tables[0].Rows[0], "SumIngredientCost")))
            {
                RegularIngredientCostHiddenField.Value = GCFDGlobals.dbGetValue(recipeCostDataSet.Tables[0].Rows[0],
                                                                                "SumIngredientCost");
            }

            if (
                !String.IsNullOrEmpty(GCFDGlobals.dbGetValue(recipeCostDataSet.Tables[0].Rows[0], "RecipeIngredientCost")))
            {
                RecipeCostPerServingLabel.Text = "$" +
                                                       Convert.ToDouble(
                                                           GCFDGlobals.dbGetValue(recipeCostDataSet.Tables[0].Rows[0],
                                                                                  "RecipeIngredientCost")).ToString();
            }
            else
            {
                RecipeCostPerServingLabel.Text = "$0.00";
            }
        }
        else
        {
            RecipeCostPerServingLabel.Text = "$0.00";
        }
    }

    public void CookChillYield()
    {
        RecipeDetail recipeYield = new RecipeDetail();
        
        recipeYield.RecipeID = Session["RecipeID"].ToString();

        recipeYield.GetDefaultRecipeDetail();

        PackageDropdownList.SelectedIndex =
                PackageDropdownList.Items.IndexOf(
                    PackageDropdownList.Items.FindByText(recipeYield.PackageTypeName));

        switch (PackageDropdownList.SelectedItem.Text)
        {
            case "1 gallon souper bag(s) (16c)":
                PackageTypeLabel.Text = "1 gallon souper bag(s) (16c)";

                break;

            case "3/4 gallon souper bag(s) (12c)":
                PackageTypeLabel.Text = "3/4 gallon souper bag(s) (12c)";

                break;
        }

        NumberofPackagesTextBox.Text = recipeYield.PackagesPerBatch;

        VolumeServingSizeDropDownList.SelectedIndex =
            VolumeServingSizeDropDownList.Items.IndexOf(
                VolumeServingSizeDropDownList.Items.FindByValue(recipeYield.ServingSize));

        CookChillWeightDropDownList.SelectedIndex =
            CookChillWeightDropDownList.Items.IndexOf(
                CookChillWeightDropDownList.Items.FindByValue(recipeYield.VolumeWeight));

        ServingsPerPackageLabel.Text = recipeYield.ServingsPerPackage;

        ServingsPerBatchLabel.Text = recipeYield.ServingsPerBatch;

        BatchYieldInPoundsLabel.Text = recipeYield.YieldInPounds;

        if (String.IsNullOrEmpty(recipeYield.ServingsPerPackage))
        {
            CookChillIngredientCostHiddenField.Value = "0";
            RecipeCostPerServingCookChillLabel.Text = "$0.00";
        }
        else
        {
            m_SQL = "SELECT SUM(ric.IngredientCost) AS SumIngredientCost, ROUND(SUM(ric.IngredientCost)/r.ServingsPerBatch, 2) AS RecipeIngredientCost FROM vwRecipeIngredientCost ric INNER JOIN vwRecipeDetail r ON r.RecipeID = ric.RecipeID WHERE r.RecipeID = " + Session["RecipeID"] + " AND r.IsDefault = 'true' GROUP BY r.NumberofServings,r.ServingsPerBatch";
            DataSet recipeCostDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            if (recipeCostDataSet.Tables[0].Rows.Count > 0)
            {
                RecipeCost.Text = GCFDGlobals.dbGetValue(recipeCostDataSet.Tables[0].Rows[0], "SumIngredientCost");
                CookChillIngredientCostHiddenField.Value = GCFDGlobals.dbGetValue(recipeCostDataSet.Tables[0].Rows[0], "SumIngredientCost");
                RecipeCostPerServingCookChillLabel.Text = "$0.00";
                //RecipeCostPerServingCookChillLabel.Text = "$" + Convert.ToDouble(GCFDGlobals.dbGetValue(recipeCostDataSet.Tables[0].Rows[0], "RecipeIngredientCost")).ToString();
            }
            else
            {
                CookChillIngredientCostHiddenField.Value = "0";
                RecipeCostPerServingCookChillLabel.Text = "$0.00";
            }
        }
    }

    protected void SaveRegularYieldDetailButton_Click(object sender, EventArgs e)
    {
        m_SQL = "SELECT DISTINCT RecipeDetailID FROM vwRecipeDetail WHERE RecipeID = " + Session["RecipeID"].ToString() + " AND IsDefault = 'true'";
        DataSet recipeDetailDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        if (Session["RecipeMode"].ToString() == "New" || recipeDetailDataSet.Tables[0].Rows.Count == 0)
        {
            m_SQL = "DECLARE @NewRecipeID int EXEC spInsertRecipeRegularYield " + Session["RecipeID"] + ", '" + RecipeNameTextbox.Text + "', " + RegularServingSizeDropDownList.SelectedItem.Value + ", " + ServingSizeTypeDropdownList.SelectedItem.Value + ", " +
                    RegularVolumeDropDownList.SelectedItem.Value + ", " + RecipeYieldLabel.Text + ", " + NumberOfServingsTextBox.Text + ", " + DeliveryPackageDropDownList.SelectedItem.Value + ", " + NumberServingsPerPackagesTextBox.Text + ", @NewRecipeID = @NewRecipeID OUTPUT SELECT @NewRecipeID as 'RecipeID'";
            DataSet regularYieldAddDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            Session["RecipeID"] = GCFDGlobals.dbGetValue(regularYieldAddDataSet.Tables[0].Rows[0], "RecipeID");

            Session["RecipeMode"] = "Update";
        }
        else
        {
            m_SQL = "UPDATE RecipeDetail SET YieldTypeID = 1, ServingSize = " + RegularServingSizeDropDownList.SelectedItem.Value + ", ServingSizeTypeID = " + ServingSizeTypeDropdownList.SelectedItem.Value + ", VolumeWeight = " + RegularVolumeDropDownList.SelectedItem.Value + ", YieldInPounds = " + RecipeYieldLabel.Text + ", NumberOfServings = " + NumberOfServingsTextBox.Text + ", PackageTypeID = " + DeliveryPackageDropDownList.SelectedItem.Value + ", ServingsPerPackage = " + NumberServingsPerPackagesTextBox.Text + " WHERE RecipeID = " + Session["RecipeID"] + " AND IsDefault = 'true'";
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
        }

        Session["YieldEditMode"] = "SavedRegularYield";

        Response.Redirect("RecipeDetails.aspx", false);
    }

    protected void CancelRegularYieldDetailButton_Click(object sender, EventArgs e)
    {
        Session["YieldEditMode"] = "CancelRegularYield";

        Response.Redirect("RecipeDetails.aspx", false);
    }

    protected void SaveCookChillYieldDetailButton_Click(object sender, EventArgs e)
    {
        m_SQL = "SELECT DISTINCT RecipeDetailID FROM vwRecipeDetail WHERE RecipeID = " + Session["RecipeID"].ToString() + " AND IsDefault = 'true'";
        DataSet recipeDetailDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        if (Session["RecipeMode"].ToString() == "New" || recipeDetailDataSet.Tables[0].Rows.Count == 0)
        {
            m_SQL = "DECLARE @NewRecipeID int EXEC spInsertRecipeCookChillYield " + Session["RecipeID"] + ", '" + RecipeNameTextbox.Text + "', 3, " + PackageDropdownList.SelectedItem.Value + ", " + VolumeServingSizeDropDownList.SelectedItem.Value + ", " + CookChillWeightDropDownList.SelectedItem.Value + ", " +
                    NumberofPackagesTextBox.Text + ", " + ServingsPerPackageLabel.Text + ", " + ServingsPerBatchLabel.Text + ", " + BatchYieldInPoundsLabel.Text + ", @NewRecipeID = @NewRecipeID OUTPUT SELECT @NewRecipeID as 'RecipeID'";
            DataSet cookChillYieldAddDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            Session["RecipeID"] = GCFDGlobals.dbGetValue(cookChillYieldAddDataSet.Tables[0].Rows[0], "RecipeID");

            Session["RecipeMode"] = "Update";
        }
        else
        {
            m_SQL = "UPDATE RecipeDetail SET YieldTypeID = 2, PackageTypeID = " + PackageDropdownList.SelectedItem.Value + ", ServingSize = " + VolumeServingSizeDropDownList.SelectedItem.Value + ", VolumeWeight = " + CookChillWeightDropDownList.SelectedItem.Value + ", PackagesPerBatch = " +
                    NumberofPackagesTextBox.Text + ", ServingsPerPackage = " + ServingsPerPackageLabel.Text + ", ServingsPerBatch = " + ServingsPerBatchLabel.Text + ", YieldInPounds = " + BatchYieldInPoundsLabel.Text + " WHERE RecipeID = " + Session["RecipeID"] + " AND IsDefault = 'true'";
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
        }

        Session["YieldEditMode"] = "SavedCookChillYield";

        Response.Redirect("RecipeDetails.aspx", false);
    }

    protected void CancelCookChillYieldDetailButton_Click(object sender, EventArgs e)
    {
        Session["YieldEditMode"] = "CancelCookChillYield";

        Response.Redirect("RecipeDetails.aspx", false);
    }

    protected void SaveRecipeDetailButton_Click(object sender, EventArgs e)
    {
        string yieldType = "";

        if (RegularYieldRadioButton.Checked)
        {
            yieldType = "1";
        }
        else
        {
            yieldType = "2";
        }
        
        try
        {
            if (Session["RecipeMode"].ToString() == "New")
            {
                string recipeID;

                m_SQL = "DECLARE @RecipeID int EXEC spUpdateCreateRecipe 'CreateNew', '', '" + RecipeNameTextbox.Text + "', '" + RecipeTypeDropDownList.SelectedItem.Value + "', '" + RecipeNotesTextBox.Text + "', '" + yieldType + "', @RecipeID OUTPUT SELECT @RecipeID as 'RecipeID'";
                DataSet recipeCreationDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                recipeID = GCFDGlobals.dbGetValue(recipeCreationDataSet.Tables[0].Rows[0], "RecipeID");

                Session["OriginalRecipeName"] = RecipeNameTextbox.Text;
            }
            else
            {
                string recipeID;

                m_SQL = "DECLARE @RecipeID int EXEC spUpdateCreateRecipe 'Update', '" + Session["RecipeID"].ToString() + "', '" + RecipeNameTextbox.Text + "', '" + RecipeTypeDropDownList.SelectedItem.Value + "', '" + RecipeNotesTextBox.Text + "', '" + yieldType + "', @RecipeID OUTPUT SELECT @RecipeID as 'RecipeID'";
                DataSet recipeCreationDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                recipeID = GCFDGlobals.dbGetValue(recipeCreationDataSet.Tables[0].Rows[0], "RecipeID");
            }

            Session["RecipeMode"] = "RecipeSaved";

            Session["NewRecipeName"] = RecipeNameTextbox.Text;

            MessageBox.Show("The " + RecipeNameTextbox.Text + " Recipe Has Been Saved");

            Response.Redirect("Recipes.aspx", false);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);

            throw;
        }
    }

    protected void SaveAsNewButton_Click(object sender, EventArgs e)
    {
        string yieldType = "";

        if (RegularYieldRadioButton.Checked)
        {
            yieldType = "1";
        }
        else
        {
            yieldType = "2";
        }

        if (Session["OriginalRecipeName"].ToString() == RecipeNameTextbox.Text)
        {
            MessageBox.Show("Two recipes cannot have the same name. Change current recipe's name before saving as a new recipe.");
        }
        else
        {
            try
            {
                string recipeID;

                m_SQL = "DECLARE @RecipeID int EXEC spUpdateCreateRecipe 'CreateNewBasedOnOld', '" + Session["RecipeID"].ToString() + "', '" + RecipeNameTextbox.Text + "', '" + RecipeTypeDropDownList.SelectedItem.Value + "', '" + RecipeNotesTextBox.Text + "', '" + yieldType + "', @RecipeID OUTPUT SELECT @RecipeID as 'RecipeID'";
                DataSet recipeCreationDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                recipeID = GCFDGlobals.dbGetValue(recipeCreationDataSet.Tables[0].Rows[0], "RecipeID");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                throw;
            }

            Session["OriginalRecipeName"] = RecipeNameTextbox.Text;

            MessageBox.Show("New Recipe Has Been Saved.");
        }
    }

    protected void DeleteRecipeButton_Click(object sender, EventArgs e)
    {
        m_SQL = "DELETE Recipe WHERE RecipeID = " + Session["RecipeID"];
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

        m_SQL = "DELETE RecipeCondiment WHERE RecipeID = " + Session["RecipeID"];
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

        m_SQL = "DELETE RecipeDirection WHERE RecipeID = " + Session["RecipeID"];
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

        m_SQL = "DELETE RecipeIngredient WHERE RecipeID = " + Session["RecipeID"];
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

        Session["RecipeMode"] = "RecipeDeleted";

        Response.Redirect("Recipes.aspx", false);
    }
    
    protected void CancelRecipeDetailButton_Click(object sender, EventArgs e)
    {
        m_SQL = "DELETE RecipeCondiment WHERE RecipeID = -1";
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

        m_SQL = "DELETE RecipeDirection WHERE RecipeID = -1";
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

        m_SQL = "DELETE RecipeIngredient WHERE RecipeID = -1";
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

        Session["RecipeMode"] = "None";

        Session["RecipeID"] = "-1";

        Response.Redirect("Recipes.aspx", false);
    }
}