Imports System
Imports System.Data
Imports GCFDGlobalsNamespace

Partial Class Sites
    Inherits System.Web.UI.Page
    Public strSQL As String
    Public m_Table As New Table
    Public m_TableRow As New TableRow
    Public m_TableCell As New TableCell
    Public m_LinkButton As New LinkButton

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If User.Identity.IsAuthenticated And (User.IsInRole("Programs") Or User.IsInRole("Compliance")) Then
            If Session("SessionID") = Nothing Then
                System.Web.Security.FormsAuthentication.SignOut()

                Response.Redirect("~/Account/Login.aspx", False)

                Response.End()
            Else
                If Not IsPostBack Then
                    Me.ButtonNameHiddenField.Value = ""

                    SearchLetterHiddenField.Value = ""
                End If

                If Me.ButtonNameHiddenField.Value.ToString <> "" Then
                    BuildSiteGridView(Me.ButtonNameHiddenField.Value.ToString())

                    SearchLetterHiddenField.Value = Me.ButtonNameHiddenField.Value.ToString()

                    Me.ButtonNameHiddenField.Value = ""
                ElseIf SearchLetterHiddenField.Value <> "" Then
                    BuildSiteGridView(SearchLetterHiddenField.Value)
                End If
            End If
        Else
            Response.Redirect("Default.aspx", False)

            Response.End()
        End If
    End Sub

    Protected Sub BuildSiteGridView(ByVal buttonName As String)
        Dim searchLetter As String
        Dim strSQL As String
        Dim ActiveSiteDataSet As New DataSet

        If buttonName = "allbutton" Then
            strSQL = "SELECT SiteID, SiteName AS 'Site Name' FROM Site ORDER BY SiteName"
            ActiveSiteDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(strSQL)
        Else
            searchLetter = buttonName.Substring(0, 1)

            ActiveSiteDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.GetSitesByLetter(searchLetter)
        End If

        FillSiteDataGridView(ActiveSiteDataSet)
    End Sub

    Public Sub FillSiteDataGridView(ByVal siteDataSet As DataSet)
        Try
            If siteDataSet.Tables(0).Rows.Count = 0 Then
                AddDummySiteData()

                'Get the number of columns to know what the Column Span should be
                Dim columnCount As Integer = SiteGridView.Rows(0).Cells.Count

                'Call the clear method to clear out any controls that you use in the columns.  I use a dropdown list in one of the column so this was necessary.
                SiteGridView.Rows(0).Cells.Clear()

                SiteGridView.Rows(0).Cells.Add(New TableCell())

                SiteGridView.Rows(0).Cells(0).ColumnSpan = columnCount

                SiteGridView.Rows(0).Cells(0).Text = "No Sites Start With That Letter"
            Else
                SiteGridView.DataMember = siteDataSet.Tables(0).TableName

                SiteGridView.DataSource = siteDataSet

                SiteGridView.DataBind()
            End If
        Catch ex As Exception
            MessageBox.Show("Error retrieving site information - " + ex.Message)
        End Try
    End Sub

    Private Sub AddDummySiteData()
        'Add a dummy row 
        Dim siteDataTable As System.Data.DataTable = New DataTable("DummyTable")

        siteDataTable.Columns.Add("Site Name")

        Dim newRow As DataRow = siteDataTable.NewRow()

        siteDataTable.Rows.Add(newRow)

        SiteGridView.DataSource = siteDataTable

        SiteGridView.DataBind()
    End Sub

    Public Sub SiteGridView_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onmouseover") = "this.style.cursor='hand';this.style.textDecoration='underline';"
            e.Row.Attributes("onmouseout") = "this.style.textDecoration='none';"

            e.Row.Attributes("onclick") = ClientScript.GetPostBackClientHyperlink(Me.SiteGridView, "Select$" + e.Row.RowIndex.ToString)
        End If

        e.Row.Cells(0).Visible = False
    End Sub

    Public Sub SiteGridView_SelectedIndexChanging(ByVal sender As Object, ByVal e As GridViewSelectEventArgs)
        Dim row As GridViewRow = SiteGridView.Rows(e.NewSelectedIndex)
    End Sub

    Public Sub SiteGridView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles SiteGridView.SelectedIndexChanged

        Response.Redirect("SiteDetails.aspx?SiteID=" + SiteGridView.SelectedRow.Cells(0).Text + "&SiteMode=Update&CalendarDate=" + DateTime.Today, False)
    End Sub

    Protected Sub CreateNewSiteButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CreateNewSiteButton.Click
        strSQL = "INSERT INTO Site (SiteState, DateCreated) VALUES('Illinos', GETDATE())"
        GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbExecuteSQLCmd(strSQL)

        strSQL = "SELECT SCOPE_IDENTITY() AS SiteID"
        Dim newSiteDataSet As DataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(strSQL)

        Response.Redirect("SiteDetails.aspx?SiteID=" + GCFDGlobals.dbGetValue(newSiteDataSet.Tables(0).Rows(0), "SiteID") + "&SiteMode=New&CalendarDate=" + DateTime.Today, False)
    End Sub

    Protected Sub SiteSearchButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SiteSearchButton.Click
        Dim strSQL As String
        Dim SiteDataSet As New DataSet
        Dim ActiveRecipeDataSet As New DataSet

        If SiteNameSearchTextBox.Text <> "" Then
            strSQL = "SELECT SiteID, SiteName AS 'Site Name' FROM Site WHERE SiteName Like '%" + SiteNameSearchTextBox.Text + "%' ORDER BY SiteName"
            SiteDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(strSQL)

            FillSiteDataGridView(SiteDataSet)
        End If
    End Sub
End Class