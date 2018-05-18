<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecipeReport_Prompt.aspx.cs" Inherits="RecipeReport_Prompt" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Recipe Report With Prompt</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="Panel1" runat="server" Height="440px" Width="100%">
            <rsweb:ReportViewer id="ReportViewer1" runat="server" font-names="Verdana" font-size="8pt"
                height="445px" processingmode="Remote" width="100%">
<ServerReport ReportServerUrl="http://gcfd-intranet/reportserver" ReportPath="/CCKReports/RecipeReport_NamePrompt"></ServerReport>
</rsweb:ReportViewer>
            <asp:Button ID="BackToReportsButton" runat="server" Font-Bold="True" Font-Italic="False"
                OnClick="BackToReportsButton_Click" Text="Back To Reports" Width="239px" /></asp:Panel>
    </div>
    </form>
</body>
</html>
