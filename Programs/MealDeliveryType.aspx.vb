Imports System
Imports System.Data
Imports GCFDGlobalsNamespace

Partial Class MealDeliveryType
    Inherits System.Web.UI.Page
    Public strSQL As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If User.Identity.IsAuthenticated And User.IsInRole("Programs-Admin") Then
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
                        BuildMealDeliveryTypeGridView(Me.ButtonNameHiddenField.Value.ToString())

                        SearchLetterHiddenField.Value = Me.ButtonNameHiddenField.Value.ToString()

                        Me.ButtonNameHiddenField.Value = ""
                    ElseIf SearchLetterHiddenField.Value <> "" Then
                        BuildMealDeliveryTypeGridView(SearchLetterHiddenField.Value)
                    End If
                End If
            End If
        Else
            Response.Redirect("Default.aspx", False)

            Response.End()
        End If
    End Sub

    Protected Sub BuildMealDeliveryTypeGridView(ByVal buttonName As String)
        Dim searchLetter As String
        Dim strSQL As String
        Dim mealTypeDataSet As New DataSet

        If buttonName = "allbutton" Then
            strSQL = "SELECT MealTypeID, MealTypeName as 'Meal Type Name' FROM MealTypeDict ORDER BY MealTypeName"

            mealTypeDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(strSQL)
        Else
            searchLetter = buttonName.Substring(0, 1)

            mealTypeDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.GetActiveMealDeliveryTypeByLetter(searchLetter)
        End If

        FillMealDeliveryTypeDataGridView(mealTypeDataSet)
    End Sub

    Public Sub FillMealDeliveryTypeDataGridView(ByVal ingredientDataSet As DataSet)
        Try
            If ingredientDataSet.Tables(0).Rows.Count = 0 Then
                AddDummyMealDeliveryTypeData()

                'Get the number of columns to know what the Column Span should be
                Dim columnCount As Integer = MealDeliveryTypeGridView.Rows(0).Cells.Count

                'Call the clear method to clear out any controls that you use in the columns.  I use a dropdown list in one of the column so this was necessary.
                MealDeliveryTypeGridView.Rows(0).Cells.Clear()

                MealDeliveryTypeGridView.Rows(0).Cells.Add(New TableCell())

                MealDeliveryTypeGridView.Rows(0).Cells(0).ColumnSpan = columnCount

                MealDeliveryTypeGridView.Rows(0).Cells(0).Text = "There currently are no meal types to modify"
            Else
                MealDeliveryTypeGridView.DataMember = ingredientDataSet.Tables(0).TableName

                MealDeliveryTypeGridView.DataSource = ingredientDataSet

                MealDeliveryTypeGridView.DataBind()
            End If
        Catch ex As Exception
            MessageBox.Show("Error retrieving meal type information - " + ex.Message)
        End Try
    End Sub

    Private Sub AddDummyMealDeliveryTypeData()
        'Add a dummy row 
        Dim mealDeliveryTypeDataTable As System.Data.DataTable = New DataTable("DummyTable")

        mealDeliveryTypeDataTable.Columns.Add("Meal Type Name")

        Dim newRow As DataRow = mealDeliveryTypeDataTable.NewRow()

        mealDeliveryTypeDataTable.Rows.Add(newRow)

        MealDeliveryTypeGridView.DataSource = mealDeliveryTypeDataTable

        MealDeliveryTypeGridView.DataBind()
    End Sub

    Public Sub MealDeliveryTypeSearchTextBox_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onmouseover") = "this.style.cursor='hand';this.style.textDecoration='underline';"
            e.Row.Attributes("onmouseout") = "this.style.textDecoration='none';"

            e.Row.Attributes("onclick") = ClientScript.GetPostBackClientHyperlink(Me.MealDeliveryTypeGridView, "Select$" + e.Row.RowIndex.ToString)
        End If

        e.Row.Cells(0).Visible = False
    End Sub

    Public Sub MealDeliveryTypeGridView_SelectedIndexChanging(ByVal sender As Object, ByVal e As GridViewSelectEventArgs)
        Dim row As GridViewRow = MealDeliveryTypeGridView.Rows(e.NewSelectedIndex)
    End Sub

    Public Sub MealDeliveryTypeGridView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MealDeliveryTypeGridView.SelectedIndexChanged
        Response.Redirect("MealDeliveryTypeDetails.aspx?MealDeliveryTypeMode=Update&MealDeliveryTypeID=" + MealDeliveryTypeGridView.SelectedRow.Cells(0).Text, False)
    End Sub

    Protected Sub CreateNewMealDeliveryTypeButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CreateNewMealDeliveryTypeButton.Click
        strSQL = "INSERT INTO MealTypeDict (DateCreated) VALUES(GETDATE())"
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(strSQL)

        strSQL = "SELECT SCOPE_IDENTITY() AS MealTypeID"
        Dim newMealTypeDataSet As DataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(strSQL)

        Response.Redirect("MealDeliveryTypeDetails.aspx?MealDeliveryTypeID=" + GCFDGlobals.dbGetValue(newMealTypeDataSet.Tables(0).Rows(0), "MealTypeID") + "&MealDeliveryTypeMode=New", False)
    End Sub

    Protected Sub MealDeliveryTypeSearchButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MealDeliveryTypeSearchButton.Click
        Dim strSQL As String
        Dim MealDeliveryTypeDataSet As New DataSet

        If MealDeliveryTypeSearchTextBox.Text <> "" Then
            strSQL = "SELECT MealTypeID, MealTypeName as 'Meal Type Name' FROM MealTypeDict WHERE MealTypeName Like '%" + MealDeliveryTypeSearchTextBox.Text + "%' ORDER BY MealTypeName"
            MealDeliveryTypeDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(strSQL)

            FillMealDeliveryTypeDataGridView(MealDeliveryTypeDataSet)
        End If
    End Sub
End Class