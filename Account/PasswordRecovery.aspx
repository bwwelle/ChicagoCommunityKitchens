<%@ Page Title="Recover Password" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PasswordRecovery.aspx.cs" Inherits="PasswordRecovery" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Recover password</h2>
    <p>
        &nbsp;<asp:PasswordRecovery ID="PasswordRecovery1" runat="server">
        </asp:PasswordRecovery>
    </p>
    </asp:Content>