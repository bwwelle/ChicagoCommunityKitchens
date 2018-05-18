Imports System
Imports System.Data
Imports GCFDGlobalsNamespace

Partial Class NonFoodItemInventory
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
                        BuildNonFoodItemGridView(Me.ButtonNameHiddenField.Value.ToString())

                        SearchLetterHiddenField.Value = Me.ButtonNameHiddenField.Value.ToString()

                        Me.ButtonNameHiddenField.Value = ""
                    ElseIf SearchLetterHiddenField.Value <> "" Then
                        BuildNonFoodItemGridView(SearchLetterHiddenField.Value)
                    End If
                End If
            End If
        Else
            Response.Redirect("Default.aspx", False)

            Response.End()
        End If
    End Sub

    Protected Sub BuildNonFoodItemGridView(ByVal buttonName As String)
        Dim searchLetter As String
        Dim strSQL As String
        Dim ActiveNonFoodItemDataSet As New DataSet

        If buttonName = "allbutton" Then
            strSQL = "SELECT NonFoodItemID, ItemName AS 'Item Name' FROM NonFoodItem WHERE Active = 1 ORDER BY ItemName"

            ActiveNonFoodItemDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(strSQL)
        Else
            searchLetter = buttonName.Substring(0, 1)

            ActiveNonFoodItemDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.GetActiveNonFoodItemByLetter(searchLetter)
        End If

        FillNonFoodItemDataGridView(ActiveNonFoodItemDataSet)
    End Sub

    Public Sub FillNonFoodItemDataGridView(ByVal nonFoodItemDataSet As DataSet)
        Try
            If nonFoodItemDataSet.Tables(0).Rows.Count = 0 Then
                AddDummyNonFoodItemData()

                'Get the number of columns to know what the Column Span should be
                Dim columnCount As Integer = NonFoodItemGridView.Rows(0).Cells.Count

                'Call the clear method to clear out any controls that you use in the columns.  I use a dropdown list in one of the column so this was necessary.
                NonFoodItemGridView.Rows(0).Cells.Clear()

                NonFoodItemGridView.Rows(0).Cells.Add(New TableCell())

                NonFoodItemGridView.Rows(0).Cells(0).ColumnSpan = columnCount

                NonFoodItemGridView.Rows(0).Cells(0).Text = "There currently are no non-food items to modify"
            Else
                NonFoodItemGridView.DataMember = nonFoodItemDataSet.Tables(0).TableName

                NonFoodItemGridView.DataSource = nonFoodItemDataSet

                NonFoodItemGridView.DataBind()
            End If
        Catch ex As Exception
            MessageBox.Show("Error retrieving non-food items information - " + ex.Message)
        End Try
    End Sub

    Private Sub AddDummyNonFoodItemData()
        'Add a dummy row 
        Dim nonFoodItemDataTable As System.Data.DataTable = New DataTable("DummyTable")

        nonFoodItemDataTable.Columns.Add("Item Name")

        Dim newRow As DataRow = nonFoodItemDataTable.NewRow()

        nonFoodItemDataTable.Rows.Add(newRow)

        NonFoodItemGridView.DataSource = nonFoodItemDataTable

        NonFoodItemGridView.DataBind()
    End Sub

    Public Sub NonFoodItemGridView_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onmouseover") = "this.style.cursor='hand';this.style.textDecoration='underline';"
            e.Row.Attributes("onmouseout") = "this.style.textDecoration='none';"

            e.Row.Attributes("onclick") = ClientScript.GetPostBackClientHyperlink(Me.NonFoodItemGridView, "Select$" + e.Row.RowIndex.ToString)
        End If

        e.Row.Cells(0).Visible = False
    End Sub

    Public Sub NonFoodItemGridView_SelectedIndexChanging(ByVal sender As Object, ByVal e As GridViewSelectEventArgs)
        Dim row As GridViewRow = NonFoodItemGridView.Rows(e.NewSelectedIndex)
    End Sub

    Public Sub NonFoodItemGridView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles NonFoodItemGridView.SelectedIndexChanged
        Response.Redirect("NonFoodItemInventoryDetails.aspx?NonFoodItemMode=Update&NonFoodItemID=" + NonFoodItemGridView.SelectedRow.Cells(0).Text, False)
    End Sub

    Protected Sub CreateNewNonFoodItemButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CreateNewNonFoodItemButton.Click
        strSQL = "INSERT INTO NonFoodItem (DateCreated) VALUES(GETDATE())"
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(strSQL)

        strSQL = "SELECT SCOPE_IDENTITY() AS NonFoodItemID"
        Dim newNonFoodItemDataSet As DataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(strSQL)

        Response.Redirect("NonFoodItemInventoryDetails.aspx?NonFoodItemID=" + GCFDGlobals.dbGetValue(newNonFoodItemDataSet.Tables(0).Rows(0), "NonFoodItemID") + "&NonFoodItemMode=New", False)
    End Sub

    Protected Sub NonFoodItemSearchButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NonFoodItemSearchButton.Click
        Dim strSQL As String
        Dim NonFoodItemDataSet As New DataSet

        If NonFoodItemNameSearchTextBox.Text <> "" Then
            strSQL = "SELECT NonFoodItemID, ItemName AS 'Item Name' FROM NonFoodItem WHERE Active = 1 AND ItemName LIKE '%" + NonFoodItemNameSearchTextBox.Text + "%' ORDER BY ItemName"
            NonFoodItemDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(strSQL)

            FillNonFoodItemDataGridView(NonFoodItemDataSet)
        End If
    End Sub
End Class