Imports System
Imports System.Data
Imports GCFDGlobalsNamespace

Partial Class Recipes
    Inherits System.Web.UI.Page
    Public strSQL As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If User.Identity.IsAuthenticated And User.IsInRole("Kitchen-Staff") Then
            If Session("SessionID") = Nothing Then
                System.Web.Security.FormsAuthentication.SignOut()

                Response.Redirect("~/Account/Login.aspx", False)

                Response.End()
            Else
                If User.IsInRole("Kitchen-Staff") And Not User.IsInRole("Kitchen-Admin") Then
                    CreateNewRecipeButton.Enabled = False
                End If

                If Not IsPostBack Then
                    Me.ButtonNameHiddenField.Value = ""

                    Session("SearchLetter") = ""
                End If

                If Session("RecipeMode") = "RecipeSaved" Then
                    MessageBox.Show("The " + Session("NewRecipeName") + " Recipe Has Been Saved")
                End If

                If Session("RecipeMode").ToString() = "RecipeDeleted" Then
                    MessageBox.Show(Session("OriginalRecipeName") + " Recipe Has Been Deleted")

                    Session("RecipeID") = "-1"

                    Session("OriginalRecipeName") = ""
                End If

                If Me.ButtonNameHiddenField.Value.ToString <> "" Then
                    BuildRecipeGridView(Me.ButtonNameHiddenField.Value.ToString())

                    GCFDGlobals.m_GCFDPlannerDatabaseLibrary.ClearSessionVariables()

                    Session("SearchLetter") = Me.ButtonNameHiddenField.Value.ToString()

                    Me.ButtonNameHiddenField.Value = ""
                ElseIf Session("SearchLetter") <> "" Then
                    BuildRecipeGridView(Session("SearchLetter"))
                End If

                GCFDGlobals.m_GCFDPlannerDatabaseLibrary.ClearSessionVariables()
            End If
        Else
            Response.Redirect("Default.aspx", False)

            Response.End()
        End If
    End Sub

    Protected Sub BuildRecipeGridView(ByVal buttonName As String)
        Dim searchLetter As String
        Dim strSQL As String
        Dim ActiveRecipeDataSet As New DataSet

        If buttonName = "allbutton" Then
            strSQL = "SELECT RecipeID, RecipeName AS 'Recipe Name' FROM Recipe WHERE RecipeName IS NOT NULL ORDER BY RecipeName"
            ActiveRecipeDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(strSQL)
        Else
            searchLetter = buttonName.Substring(0, 1)

            ActiveRecipeDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.GetActiveRecipesByLetter(searchLetter)
        End If

        FillRecipeDataGridView(ActiveRecipeDataSet)
    End Sub

    Public Sub FillRecipeDataGridView(ByVal recipeDataSet As DataSet)
        Try
            If recipeDataSet.Tables(0).Rows.Count = 0 Then
                AddDummyRecipeData()

                'Get the number of columns to know what the Column Span should be
                Dim columnCount As Integer = RecipeGridView.Rows(0).Cells.Count

                'Call the clear method to clear out any controls that you use in the columns.  I use a dropdown list in one of the column so this was necessary.
                RecipeGridView.Rows(0).Cells.Clear()

                RecipeGridView.Rows(0).Cells.Add(New TableCell())

                RecipeGridView.Rows(0).Cells(0).ColumnSpan = columnCount

                RecipeGridView.Rows(0).Cells(0).Text = "There currently are no recipes to modify"
            Else
                RecipeGridView.DataMember = recipeDataSet.Tables(0).TableName

                RecipeGridView.DataSource = recipeDataSet

                RecipeGridView.DataBind()
            End If
        Catch ex As Exception
            MessageBox.Show("Error retrieving recipe information - " + ex.Message)
        End Try
    End Sub

    Private Sub AddDummyRecipeData()
        'Add a dummy row 
        Dim recipeDataTable As System.Data.DataTable = New DataTable("DummyTable")

        recipeDataTable.Columns.Add("Recipe Name")

        Dim newRow As DataRow = recipeDataTable.NewRow()

        recipeDataTable.Rows.Add(newRow)

        RecipeGridView.DataSource = recipeDataTable

        RecipeGridView.DataBind()
    End Sub

    Public Sub RecipeGridView_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onmouseover") = "this.style.cursor='hand';this.style.textDecoration='underline';"
            e.Row.Attributes("onmouseout") = "this.style.textDecoration='none';"

            e.Row.Attributes("onclick") = ClientScript.GetPostBackClientHyperlink(Me.RecipeGridView, "Select$" + e.Row.RowIndex.ToString)
        End If

        e.Row.Cells(0).Visible = False
    End Sub

    Public Sub RecipeGridView_SelectedIndexChanging(ByVal sender As Object, ByVal e As GridViewSelectEventArgs)
        Dim row As GridViewRow = RecipeGridView.Rows(e.NewSelectedIndex)
    End Sub

    Public Sub RecipeGridView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles RecipeGridView.SelectedIndexChanged
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.ClearSessionVariables()

        Session("RecipeID") = RecipeGridView.SelectedRow.Cells(0).Text

        Session("RecipeMode") = "RecipeUpdate"

        Response.Redirect("RecipeDetails.aspx", False)
    End Sub

    Protected Sub CreateNewRecipeButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CreateNewRecipeButton.Click
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.ClearSessionVariables()

        Session("RecipeMode") = "New"

        Response.Redirect("RecipeDetails.aspx", False)
    End Sub

    Protected Sub RecipeSearchButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RecipeSearchButton.Click
        Dim strSQL As String
        Dim ActiveRecipeDataSet As New DataSet

        If RecipeNameSearchTextBox.Text <> "" Then
            strSQL = "SELECT RecipeID, RecipeName AS 'Recipe Name' FROM Recipe WHERE RecipeName Like '%" + RecipeNameSearchTextBox.Text + "%' ORDER BY RecipeName"
            ActiveRecipeDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(strSQL)

            FillRecipeDataGridView(ActiveRecipeDataSet)
        End If
    End Sub
End Class