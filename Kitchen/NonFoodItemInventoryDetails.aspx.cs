using System;
using System.Data;
using GCFDGlobalsNamespace;

public partial class NonFoodItemInventoryDetails : System.Web.UI.Page
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
                PiecesPerPurchasedUnitTextBox.Attributes["onkeyup"] = "javascript:updatecost()";
                UnitDropDownList.Attributes["onclick"] = "javascript:updateparlabel()";

                ItemNameTextBox.Focus();

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
        NonFoodItemIDHiddenField.Value = Request.QueryString.Get("NonFoodItemID");
        NonFoodItemModeHiddenField.Value = Request.QueryString.Get("NonFoodItemMode");

        if (NonFoodItemModeHiddenField.Value == "Update")
        {
            m_SQL = "SELECT ItemName, PurchaseUnit, Vendor, CONVERT(varchar, CONVERT(money, LastInvoicedCost), 1) AS LastInvoicedCost, PiecesPerPurchaseUnit, CONVERT(varchar, CONVERT(money, CostPerPiece), 1) AS CostPerPiece, SubCategory, Par, DateLastUpdated, Notes FROM NonFoodItem WHERE NonFoodItemID = " + NonFoodItemIDHiddenField.Value;
            DataSet nonFoodItemDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            foreach (DataRow nonFoodItemData in nonFoodItemDataSet.Tables[0].Rows)
            {
                ItemNameTextBox.Text = GCFDGlobals.dbGetValue(nonFoodItemData, "ItemName");

                OriginalNonFoodItemNameHiddenField.Value = GCFDGlobals.dbGetValue(nonFoodItemData, "ItemName");

                UnitDropDownList.SelectedIndex =
                        UnitDropDownList.Items.IndexOf(
                            UnitDropDownList.Items.FindByText(GCFDGlobals.dbGetValue(nonFoodItemData,
                                                                                              "PurchaseUnit")));
                VendorDropDownList.SelectedIndex =
                        VendorDropDownList.Items.IndexOf(
                            VendorDropDownList.Items.FindByText(GCFDGlobals.dbGetValue(nonFoodItemData,
                                                                                              "Vendor")));
                SubCategoryListBox.SelectedIndex =
                    SubCategoryListBox.Items.IndexOf(
                        SubCategoryListBox.Items.FindByText(GCFDGlobals.dbGetValue(nonFoodItemData,
                                                                              "SubCategory")));
                ParTextBox.Text = GCFDGlobals.dbGetValue(nonFoodItemData, "Par");
                InvoicedCostTextBox.Text = GCFDGlobals.dbGetValue(nonFoodItemData, "LastInvoicedCost");
                PiecesPerPurchasedUnitTextBox.Text = GCFDGlobals.dbGetValue(nonFoodItemData, "PiecesPerPurchaseUnit");
                CostPerPurchaseUnitLabel.Text = GCFDGlobals.dbGetValue(nonFoodItemData, "CostPerPiece");
                CostPerPurchaseUnitHiddenField.Value = GCFDGlobals.dbGetValue(nonFoodItemData, "CostPerPiece");

                NotesTextBox.Text = GCFDGlobals.dbGetValue(nonFoodItemData, "Notes");
                DateLastUpdatedLabel.Text = GCFDGlobals.dbGetValue(nonFoodItemData, "DateLastUpdated");

                PiecesPerPurchasedUnitTextBox.Text = GCFDGlobals.dbGetValue(nonFoodItemData, "PurchaseUnit");
            }

            SaveAsNewButton.Enabled = true;

            DeleteItemButton.Enabled = true;
            DeleteItemButton.Attributes.Add("onclick", "return confirm('Are you sure you want to PERMANENTLY DELETE the " + OriginalNonFoodItemNameHiddenField.Value + " non-food inventory item?')");
        }
        else
        {
            SaveAsNewButton.Visible = false;
        }

        if (NonFoodItemModeHiddenField.Value == "New")
        {
            SaveAsNewButton.Enabled = false;

            DeleteItemButton.Enabled = false;
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                string lastInvoiceCost = InvoicedCostTextBox.Text;
                string costPerUnit = CostPerPurchaseUnitHiddenField.Value;

                if (lastInvoiceCost == "")
                {
                    lastInvoiceCost = "null";
                }

                if (costPerUnit == "")
                {
                    costPerUnit = "null";
                }

                if ((NonFoodItemModeHiddenField.Value == "New" && CreateItem() == true) || (NonFoodItemModeHiddenField.Value == "Update" && ItemNameTextBox.Text != OriginalNonFoodItemNameHiddenField.Value && CreateItem() == true) || (NonFoodItemModeHiddenField.Value == "Update" && ItemNameTextBox.Text == OriginalNonFoodItemNameHiddenField.Value))
                {
                    m_SQL = "UPDATE NonFoodItem SET ItemName = '" + ItemNameTextBox.Text + "', PurchaseUnit = '" +
                           UnitDropDownList.SelectedItem.Value + "', ";
                    m_SQL = m_SQL + "Vendor = '" + VendorDropDownList.SelectedItem.Value + "', LastInvoicedCost = " + lastInvoiceCost + ", PiecesPerPurchaseUnit = '" + PiecesPerPurchasedUnitTextBox.Text + "', CostPerPiece = '" + costPerUnit + "', Notes = '" + NotesTextBox.Text + "', Active = 1, SubCategory = '" + SubCategoryListBox.SelectedItem.Value + "', DateLastUpdated = GETDATE(), Par = " + ParTextBox.Text + " WHERE NonFoodItemID = " +
                            NonFoodItemIDHiddenField.Value;
                    GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                    SaveAsNewButton.Enabled = true;

                    NonFoodItemModeHiddenField.Value = "Update";

                    OriginalNonFoodItemNameHiddenField.Value = ItemNameTextBox.Text;

                    DeleteItemButton.Enabled = true;
                    DeleteItemButton.Attributes.Add("onclick", "return confirm('Are you sure you want to PERMANENTLY DELETE the " + OriginalNonFoodItemNameHiddenField.Value + " non-food inventory item?')");

                    MessageBox.Show("The " + ItemNameTextBox.Text + " Non-Food Item Has Been Saved");

                    CostPerPurchaseUnitLabel.Text = costPerUnit;
                }
                else
                {
                    MessageBox.Show("There already is an non-food item within the system with the same name as the non-food item you're attempting to currently save. Change the current non-food item's name before attempting to save.");
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

        m_SQL = "SELECT * FROM NonFoodItem WHERE ItemName = '" + ItemNameTextBox.Text + "'";
        DataSet nonFoodItemNameTestDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        if (nonFoodItemNameTestDataSet.Tables.Count > 0)
        {
            if (nonFoodItemNameTestDataSet.Tables[0].Rows.Count > 0)
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
        if (NonFoodItemModeHiddenField.Value == "New")
        {
            m_SQL = "DELETE NonFoodItem WHERE NonFoodItemID = " + NonFoodItemIDHiddenField.Value;
            GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
        }

        Response.Redirect("NonFoodItemInventory.aspx", false);
    }

    protected void SaveAsNewButton_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string lastInvoiceCost = InvoicedCostTextBox.Text;
            string costPerUnit = CostPerPurchaseUnitHiddenField.Value;

            if (lastInvoiceCost == "")
            {
                lastInvoiceCost = "null";
            }
            if (costPerUnit == "")
            {
                costPerUnit = "null";
            }

            if (CreateItem() == true)
            {
                try
                {
                    m_SQL =
                       "INSERT INTO NonFoodItem (ItemName, PurchaseUnit, Vendor, LastInvoicedCost, PiecesPerPurchaseUnit, CostPerPiece, SubCategory, Par, Notes, Active, DateLastUpdated, DateCreated) VALUES('" +
                       ItemNameTextBox.Text + "', '" + UnitDropDownList.SelectedItem.Value + "', '";
                    m_SQL = m_SQL + VendorDropDownList.SelectedItem.Value + "', " + lastInvoiceCost + ", '" + PiecesPerPurchasedUnitTextBox.Text + "', '" + costPerUnit + "', '" + SubCategoryListBox.SelectedItem.Value + "', " + ParTextBox.Text + ", '" + NotesTextBox.Text + "', 1, GETDATE(), GETDATE())";
                    GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);

                    m_SQL = "SELECT SCOPE_IDENTITY() AS IngredientID";
                    DataSet newIngredientIDDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                    NonFoodItemIDHiddenField.Value = GCFDGlobals.dbGetValue(newIngredientIDDataSet.Tables[0].Rows[0], "IngredientID");

                    OriginalNonFoodItemNameHiddenField.Value = ItemNameTextBox.Text;

                    MessageBox.Show("The " + ItemNameTextBox.Text + " Non-food Item Has Been Saved.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                    throw;
                }
            }
            else
            {
                MessageBox.Show("There already is an non-food item within the system with the same name as the non-food item you're attempting to currently save. Change the current non-food item's name before attempting to save.");
            }
        }
    }

    protected void DeleteNonFoodItemButton_Click(object sender, EventArgs e)
    {
        m_SQL = "DELETE NonFoodItem WHERE NonFoodItemID = " + NonFoodItemIDHiddenField.Value;
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(m_SQL);
        
        Response.Redirect("NonFoodItemInventory.aspx", false);
    }
}