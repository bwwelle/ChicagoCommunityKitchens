<%@ Page Language="C#" Title = "CCK Application User Action Log" AutoEventWireup="true" CodeFile="CCKUserActionLog.aspx.cs" MasterPageFile="~/Site.master" Inherits="CCKUserActionLog" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <asp:GridView ID="UserActionsGridView" runat="server" Width="100%">
        </asp:GridView>
    </div>
</asp:Content>
