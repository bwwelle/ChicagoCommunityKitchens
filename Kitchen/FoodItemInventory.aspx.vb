Imports System
Imports System.Data
Imports GCFDGlobalsNamespace

Partial Class FoodItemInventory
    Inherits System.Web.UI.Page
    Public strSQL As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If User.Identity.IsAuthenticated And User.IsInRole("Kitchen-Admin") Then
            If Session("SessionID") = Nothing Then
                System.Web.Security.FormsAuthentication.SignOut()

                Response.Redirect("~/Account/Login.aspx", False)

                Response.End()
            Else
                If Not IsPostBack Then
                    Me.ButtonNameHiddenField.Value = ""

                    SearchLetterHiddenField.Value = ""
                Else
                    If Me.ButtonNameHiddenField.Value.ToString <> "" Then
                        BuildIngredientGridView(Me.ButtonNameHiddenField.Value.ToString())

                        SearchLetterHiddenField.Value = Me.ButtonNameHiddenField.Value.ToString()

                        Me.ButtonNameHiddenField.Value = ""
                    ElseIf SearchLetterHiddenField.Value <> "" Then
                        BuildIngredientGridView(SearchLetterHiddenField.Value)
                    End If
                End If
            End If
        Else
            Response.Redirect("Default.aspx", False)

            Response.End()
        End If
    End Sub

    Protected Sub BuildIngredientGridView(ByVal buttonName As String)
        Dim searchLetter As String
        Dim strSQL As String
        Dim ActiveIngredientDataSet As New DataSet

        If buttonName = "allbutton" Then
            strSQL = "SELECT IngredientID, IngredientName as 'Ingredient Name' FROM Ingredient WHERE Active = 1 ORDER BY IngredientName"

            ActiveIngredientDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(strSQL)
        Else
            searchLetter = buttonName.Substring(0, 1)

            ActiveIngredientDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.GetActiveIngredientsByLetter(searchLetter)
        End If

        FillIngredientDataGridView(ActiveIngredientDataSet)
    End Sub

    Public Sub FillIngredientDataGridView(ByVal ingredientDataSet As DataSet)
        Try
            If ingredientDataSet.Tables(0).Rows.Count = 0 Then
                AddDummyIngredientData()

                'Get the number of columns to know what the Column Span should be
                Dim columnCount As Integer = IngredientGridView.Rows(0).Cells.Count

                'Call the clear method to clear out any controls that you use in the columns.  I use a dropdown list in one of the column so this was necessary.
                IngredientGridView.Rows(0).Cells.Clear()

                IngredientGridView.Rows(0).Cells.Add(New TableCell())

                IngredientGridView.Rows(0).Cells(0).ColumnSpan = columnCount

                IngredientGridView.Rows(0).Cells(0).Text = "There currently are no ingredients to modify"
            Else
                IngredientGridView.DataMember = ingredientDataSet.Tables(0).TableName

                IngredientGridView.DataSource = ingredientDataSet

                IngredientGridView.DataBind()
            End If
        Catch ex As Exception
            MessageBox.Show("Error retrieving ingredient information - " + ex.Message)
        End Try
    End Sub

    Private Sub AddDummyIngredientData()
        'Add a dummy row 
        Dim ingredientDataTable As System.Data.DataTable = New DataTable("DummyTable")

        ingredientDataTable.Columns.Add("Ingredient Name")

        Dim newRow As DataRow = ingredientDataTable.NewRow()

        ingredientDataTable.Rows.Add(newRow)

        IngredientGridView.DataSource = ingredientDataTable

        IngredientGridView.DataBind()
    End Sub

    Public Sub IngredientGridView_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onmouseover") = "this.style.cursor='hand';this.style.textDecoration='underline';"
            e.Row.Attributes("onmouseout") = "this.style.textDecoration='none';"

            e.Row.Attributes("onclick") = ClientScript.GetPostBackClientHyperlink(Me.IngredientGridView, "Select$" + e.Row.RowIndex.ToString)
        End If

        e.Row.Cells(0).Visible = False
    End Sub

    Public Sub IngredientGridView_SelectedIndexChanging(ByVal sender As Object, ByVal e As GridViewSelectEventArgs)
        Dim row As GridViewRow = IngredientGridView.Rows(e.NewSelectedIndex)
    End Sub

    Public Sub IngredientGridView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles IngredientGridView.SelectedIndexChanged
        Response.Redirect("FoodItemInventoryDetails.aspx?IngredientMode=Update&IngredientID=" + IngredientGridView.SelectedRow.Cells(0).Text, False)
    End Sub

    Protected Sub CreateNewIngredientButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CreateNewIngredientButton.Click
        strSQL = "INSERT INTO Ingredient (DateCreated) VALUES(GETDATE())"
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(strSQL)

        strSQL = "SELECT SCOPE_IDENTITY() AS IngredientID"
        Dim newIngredientDataSet As DataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(strSQL)

        Response.Redirect("FoodItemInventoryDetails.aspx?IngredientID=" + GCFDGlobals.dbGetValue(newIngredientDataSet.Tables(0).Rows(0), "IngredientID") + "&IngredientMode=New", False)
    End Sub

    Protected Sub FoodItemSearchButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FoodItemSearchButton.Click
        Dim strSQL As String
        Dim FoodItemDataSet As New DataSet

        If FoodItemNameSearchTextBox.Text <> "" Then
            strSQL = "SELECT IngredientID, IngredientName as 'Ingredient Name' FROM Ingredient WHERE Active = 1 AND IngredientName Like '%" + FoodItemNameSearchTextBox.Text + "%' ORDER BY IngredientName"
            FoodItemDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(strSQL)

            FillIngredientDataGridView(FoodItemDataSet)
        End If
    End Sub
End Class