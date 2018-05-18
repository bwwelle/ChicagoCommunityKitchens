using System;
using System.Data;
using System.Web.UI.WebControls;
using GCFDGlobalsNamespace;

public partial class FoodItemInventoryDetails : System.Web.UI.Page
{
    public string m_SQL;

    public void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated && User.IsInRole("Kitchen-Admin"))
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

                InvoicedCostTextBox.Attributes["onkeyup"] = "javascript:updatecost()";
                RecipeUnitInUnitTextBox.Attributes["onkeyup"] = "javascript:updatecost()";
                YieldTextBox.Attributes["onkeyup"] = "javascript:updatecost()";
                UnitDropDownList.Attributes["onclick"] = "javascript:updateparlabel()";

                IngredientNameTextBox.Focus();

                Load += (Page_Load);

                if (!IsPostBack)
                {
                    BuildDropDownLists();
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
        IngredientIDHiddenField.Value = Request.QueryString.Get("IngredientID");
        IngredientModeHiddenField.Value = Request.QueryString.Get("IngredientMode");

        if (IngredientModeHiddenField.Value == "Update")
        {
            m_SQL = "SELECT IngredientName, RecipeUnit, Vendor, CONVERT(varchar, CONVERT(money, LastInvoicedCost), 1) AS LastInvoicedCost, PurchaseUnit, RecipeUnitsInPurchaseUnit, RecipeUnitAfterWaste, CONVERT(varchar, CONVERT(money, CostPerRecipeUnit), 1) AS CostPerRecipeUnit, Yield, DateLastUpdated, Notes, Par, SubCategory, Category FROM Ingredient WHERE IngredientID = " + IngredientIDHiddenField.Value;
            DataSet ingredientDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            foreach (DataRow ingredientData in ingredientDataSet.Tables[0].Rows)
            {
                IngredientNameTextBox.Text = GCFDGlobals.dbGetValue(ingredientData, "IngredientName");
                OriginalIngredientNameHiddenField.Value = GCFDGlobals.dbGetValue(ingredientData, "IngredientName");

                UnitDropDownList.SelectedIndex =
                        UnitDropDownList.Items.IndexOf(
                            UnitDropDownList.Items.FindByText(GCFDGlobals.dbGetValue(ingredientData,
                                                                                              "RecipeUnit")));
                VendorDropDownList.SelectedIndex =
                        VendorDropDownList.Items.IndexOf(
                            VendorDropDownList.Items.FindByText(GCFDGlobals.dbGetValue(ingredientData,
                                                                                              "Vendor")));

                CategoryDropDownList.SelectedIndex =
                        CategoryDropDownList.Items.IndexOf(
                            CategoryDropDownList.Items.FindByText(GCFDGlobals.dbGetValue(ingredientData,
                                                                                              "Category")));

                InvoicedCostTextBox.Text = GCFDGlobals.dbGetValue(ingredientData, "LastInvoicedCost");
                PurchasedUnitTextBox.Text = GCFDGlobals.dbGetValue(ingredientData, "PurchaseUnit");
                RecipeUnitInUnitTextBox.Text = GCFDGlobals.dbGetValue(ingredientData, "RecipeUnitsInPurchaseUnit");
                RecipeUnitAfterWasteLabel.Text = GCFDGlobals.dbGetValue(ingredientData, "RecipeUnitAfterWaste");
                CostPerRecipeUnitLabel.Text = "$" + GCFDGlobals.dbGetValue(ingredientData, "CostPerRecipeUnit");
                ParTextBox.Text = GCFDGlobals.dbGetValue(ingredientData, "Par");
                RecipeUnitLabel.Text = GCFDGlobals.dbGetValue(ingredientData, "RecipeUnit");

                RecipeUnitHiddenField.Value = GCFDGlobals.dbGetValue(ingredientData, "RecipeUnit");
                RecipeUnitAfterWasteHiddenField.Value = GCFDGlobals.dbGetValue(ingredientData, "RecipeUnitAfterWaste");
                CostPerRecipeUnitHiddenField.Value = GCFDGlobals.dbGetValue(ingredientData, "CostPerRecipeUnit");

                if(String.IsNullOrEmpty(GCFDGlobals.dbGetValue(ingredientData, "Yield")))
                {
                    YieldTextBox.Text = "0";
                }
                else
                {
                    double yield = Convert.ToDouble(GCFDGlobals.dbGetValue(ingredientData, "Yield"));
                    int convertedYield = Convert.ToInt16(yield * 100);
                    
                    YieldTextBox.Text = convertedYield.ToString();
                }

                NotesTextBox.Text = GCFDGlobals.dbGetValue(ingredientData, "Notes");
                DateLastUpdatedLabel.Text = GCFDGlobals.dbGetValue(ingredientData, "DateLastUpdated");

                SubCategoryListBox.Items.Clear();

                m_SQL = "SELECT * FROM vwCategory WHERE CategoryName = '" + CategoryDropDownList.SelectedItem.Value + "'";
                DataSet categoryDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                SubCategoryListBox.Items.Add(new ListItem("Select A SubCategory", "Select A SubCategory"));

                foreach (DataRow categoryDataRow in categoryDataSet.Tables[0].Rows)
                {
                    SubCategoryListBox.Items.Add(new ListItem(GCFDGlobals.dbGetValue(categoryDataRow, "SubCategoryName"), GCFDGlobals.dbGetValue(categoryDataRow, "SubCategoryName")));
                }

                SubCategoryListBox.SelectedIndex =
                        SubCategoryListBox.Items.IndexOf(
                            SubCategoryListBox.Items.FindByText(GCFDGlobals.dbGetValue(ingredientData,
                                                                                              "SubCategory")));
            }

            SaveAsNewButton.Enabled = true;

            DeleteIngredientButton.Enabled = true;
            DeleteIngredientButton.Attributes.Add("onclick", "return confirm('Are you sure you want to PERMANENTLY DELETE the " + OriginalIngredientNameHiddenField.Value + " food inventory item?')");
        }
        else if (IngredientModeHiddenField.Value == "New")
        {
            SaveAsNewButton.Visible = false;

            DeleteIngredientButton.Enabled = false;
        }
    }

	protected void SaveButton_Click(object sender, EventArgs e)
	{
        if (Page.IsValid)
        {
            try
            {
                string lastInvoiceCost = InvoicedCostTextBox.Text;
                string recipeUnitsInUnit = RecipeUnitInUnitTextBox.Text;
                string costPerUnit = CostPerRecipeUnitHiddenField.Value;
                string recipeUnitWaste = RecipeUnitAfterWasteHiddenField.Value;
                double yield = Convert.ToDouble(YieldTextBox.Text);
                string par = ParTextBox.Text;

                if (par == "")
                {
                    par = "null";
                }

                if (String.IsNullOrEmpty(YieldTextBox.Text))
                {
                    yield = 0;
                }
                else
                {
                    yield = Convert.ToDouble(YieldTextBox.Text) / 100;
                }

                if (lastInvoiceCost == "")
                {
                    lastInvoiceCost = "null";
                }

                if (recipeUnitsInUnit == "")
                {
                    recipeUnitsInUnit = "null";
                }

                if (costPerUnit == "")
                {
                    costPerUnit = "null";
                }

                if (recipeUnitWaste == "")
                {
                    recipeUnitWaste = "null";
                }

                if ((IngredientModeHiddenField.Value == "New" && CreateItem() == true) || (IngredientModeHiddenField.Value == "Update" && IngredientNameTextBox.Text != OriginalIngredientNameHiddenField.Value && CreateItem() == true) || (IngredientModeHiddenField.Value == "Update" && IngredientNameTextBox.Text == OriginalIngredientNameHiddenField.Value))
                {
                    m_SQL = "UPDATE Ingredient SET IngredientName = '" + IngredientNameTextBox.Text + "', RecipeUnit = '" +
                                 UnitDropDownList.SelectedItem.Value + "', ";
                    m_SQL = m_SQL + "Vendor = '" + VendorDropDownList.SelectedItem.Value + "', LastInvoicedCost = " + lastInvoiceCost + ", Par = " + par + ", PurchaseUnit = '" + PurchasedUnitTextBox.Text + "', RecipeUnitsInPurchaseUnit = " + recipeUnitsInUnit + ", RecipeUnitAfterWaste = " + recipeUnitWaste + ", CostPerRecipeUnit = " + costPerUnit + ", Yield = " + yield + ", Notes = '" + NotesTextBox.Text + "', Active = 1, SubCategory = '" + SubCategoryListBox.SelectedItem.Value + "', Category = '" + CategoryDropDownList.SelectedItem.Value + "', DateLastUpdated = GETDATE() WHERE IngredientID = " +
                            IngredientIDHiddenField.Value;
                    GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                    SaveAsNewButton.Visible = true;

                    IngredientModeHiddenField.Value = "Update";

                    OriginalIngredientNameHiddenField.Value = IngredientNameTextBox.Text;
                    
                    DeleteIngredientButton.Enabled = true;
                    DeleteIngredientButton.Attributes.Add("onclick", "return confirm('Are you sure you want to PERMANENTLY DELETE the " + IngredientNameTextBox.Text + " food inventory item?')");
       
                    MessageBox.Show("The " + IngredientNameTextBox.Text + " Ingredient Has Been Saved");
                }
                else
                {
                    MessageBox.Show("There already is an ingredient/food item within the system with the same name as the ingredient/food item you're attempting to currently save. Change the current ingredient/food item's name before attempting to save.");
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

        m_SQL = "SELECT * FROM Ingredient WHERE IngredientName = '" + IngredientNameTextBox.Text + "'";
        DataSet ingredientNameTestDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        if (ingredientNameTestDataSet.Tables.Count > 0)
        {
            if (ingredientNameTestDataSet.Tables[0].Rows.Count > 0)
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
        if (IngredientModeHiddenField.Value == "New")
        {
            m_SQL = "DELETE Ingredient WHERE IngredientID = " + IngredientIDHiddenField.Value;
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
        }

		Response.Redirect("FoodItemInventory.aspx", false);
    }

    protected void SaveAsNewButton_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string lastInvoiceCost = InvoicedCostTextBox.Text;
            string recipeUnitsInUnit = RecipeUnitInUnitTextBox.Text;
            string costPerUnit = CostPerRecipeUnitLabel.Text;
            double yield;
            string recipeUnitWaste = RecipeUnitAfterWasteLabel.Text;

            if (String.IsNullOrEmpty(YieldTextBox.Text))
            {
                yield = 0;
            }
            else
            {
                yield = Convert.ToInt16(YieldTextBox.Text) / 100;
            }

            if (lastInvoiceCost == "")
            {
                lastInvoiceCost = "null";
            }

            if (recipeUnitsInUnit == "")
            {
                recipeUnitsInUnit = "null";
            }

            if (costPerUnit == "")
            {
                costPerUnit = "null";
            }

            if (recipeUnitWaste == "")
            {
                recipeUnitWaste = "null";
            }

            if (CreateItem() == true)
            {
                try
                {
                    m_SQL = "INSERT INTO Ingredient (IngredientName, RecipeUnit, Vendor, LastInvoicedCost, PurchaseUnit, RecipeUnitsInPurchaseUnit, RecipeUnitAfterWaste, CostPerRecipeUnit, Yield, Par, Notes, Active, DateLastUpdated, Category, SubCategory, DateCreated) VALUES('" +
                                        IngredientNameTextBox.Text + "', '" + UnitDropDownList.SelectedItem.Value + "', '";
                    m_SQL = m_SQL + VendorDropDownList.SelectedItem.Value + "', " + lastInvoiceCost + ", '" + PurchasedUnitTextBox.Text + "', " + recipeUnitsInUnit + ", " + recipeUnitWaste + ", " + costPerUnit + ", " + yield + ", " + ParTextBox.Text + ", '" + NotesTextBox.Text + "', 1, GETDATE(), '" + CategoryDropDownList.SelectedItem.Value + "', '" + SubCategoryListBox.SelectedItem.Value + "', GETDATE())";
                    GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                    m_SQL = "SELECT SCOPE_IDENTITY() AS IngredientID";
                    DataSet newIngredientIDDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                    IngredientIDHiddenField.Value = GCFDGlobals.dbGetValue(newIngredientIDDataSet.Tables[0].Rows[0], "IngredientID");

                    OriginalIngredientNameHiddenField.Value = IngredientNameTextBox.Text;

                    MessageBox.Show("The " + IngredientNameTextBox.Text + " Ingredient Has Been Saved.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                    throw;
                }
            }
            else
            {
                MessageBox.Show("There already is an ingredient/food item saved with the same name as the ingredient/food item you're attempting to currently save. Change the current ingredient/food item's name before attempting to save.");
            }
        }
    }

    protected void DeleteIngredientButton_Click(object sender, EventArgs e)
    {
        m_SQL = "DELETE Ingredient WHERE IngredientID = " + IngredientIDHiddenField.Value;
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

        Response.Redirect("FoodItemInventory.aspx", false);
    }

    protected void CategoryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubCategoryListBox.Items.Clear();

        m_SQL = "SELECT * FROM vwCategory WHERE CategoryName = '" + CategoryDropDownList.SelectedItem.Value + "'";
        DataSet categoryDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        SubCategoryListBox.Items.Add(new ListItem("Select A SubCategory", "Select A SubCategory"));

        foreach(DataRow categoryDataRow in categoryDataSet.Tables[0].Rows)
        {
            SubCategoryListBox.Items.Add(new ListItem(GCFDGlobals.dbGetValue(categoryDataRow, "SubCategoryName"), GCFDGlobals.dbGetValue(categoryDataRow, "SubCategoryName")));    
        }

        RecipeUnitLabel.Text = RecipeUnitHiddenField.Value;
        RecipeUnitAfterWasteLabel.Text = RecipeUnitAfterWasteHiddenField.Value;
        CostPerRecipeUnitLabel.Text = CostPerRecipeUnitHiddenField.Value;
    }
}