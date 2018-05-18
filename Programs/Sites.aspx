<%@ Page Title = "CCK Sites" Language="VB" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/Site.master" CodeFile="Sites.aspx.vb" Inherits="Sites" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" language="javascript">
     var buttonID;
     var hiddenTextBoxName;

     function SetButtonValueName(hiddenTextBoxName, buttonID) {
         document.getElementById("ctl00_MainContent_ButtonNameHiddenField").value = buttonID;

         document.forms.aspnetForm.submit();
     }
</script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <table style="width: 100%">
            <tr>
                <td style="width: 100px; height: 26px">
                    <input id="AllButton" onclick="SetButtonValueName('ButtonNameHiddenField','allbutton')"
                        type="button" value="All" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="AButton" onclick="SetButtonValueName('ButtonNameHiddenField','AButton')"
                        type="button" value="A" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="BButton" onclick="SetButtonValueName('ButtonNameHiddenField','BButton')"
                        type="button" value="B" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="CButton" onclick="SetButtonValueName('ButtonNameHiddenField','CButton')"
                        type="button" value="C" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="DButton" onclick="SetButtonValueName('ButtonNameHiddenField','DButton')"
                        type="button" value="D" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="EButton" onclick="SetButtonValueName('ButtonNameHiddenField','EButton')"
                        type="button" value="E" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="FButton" onclick="SetButtonValueName('ButtonNameHiddenField','FButton')"
                        type="button" value="F" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="GButton" onclick="SetButtonValueName('ButtonNameHiddenField','GButton')"
                        type="button" value="G" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="HButton" onclick="SetButtonValueName('ButtonNameHiddenField','HButton')"
                        type="button" value="H" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="IButton" onclick="SetButtonValueName('ButtonNameHiddenField','IButton')"
                        type="button" value="I" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="JButton" onclick="SetButtonValueName('ButtonNameHiddenField','JButton')"
                        type="button" value="J" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="KButton" onclick="SetButtonValueName('ButtonNameHiddenField','KButton')"
                        type="button" value="K" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="LButton" onclick="SetButtonValueName('ButtonNameHiddenField','LButton')"
                        type="button" value="L" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="MButton" onclick="SetButtonValueName('ButtonNameHiddenField','MButton')"
                        type="button" value="M" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="NButton" onclick="SetButtonValueName('ButtonNameHiddenField','NButton')"
                        type="button" value="N" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="OButton" onclick="SetButtonValueName('ButtonNameHiddenField','OButton')"
                        type="button" value="O" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="PButton" onclick="SetButtonValueName('ButtonNameHiddenField','PButton')"
                        type="button" value="P" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="QButton" onclick="SetButtonValueName('ButtonNameHiddenField','QButton')"
                        type="button" value="Q" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="RButton" onclick="SetButtonValueName('ButtonNameHiddenField','RButton')"
                        type="button" value="R" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="SButton" onclick="SetButtonValueName('ButtonNameHiddenField','SButton')"
                        type="button" value="S" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="TButton" onclick="SetButtonValueName('ButtonNameHiddenField','TButton')"
                        type="button" value="T" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="UButton" onclick="SetButtonValueName('ButtonNameHiddenField','UButton')"
                        type="button" value="U" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="VButton" onclick="SetButtonValueName('ButtonNameHiddenField','VButton')"
                        type="button" value="V" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="WButton" onclick="SetButtonValueName('ButtonNameHiddenField','WButton')"
                        type="button" value="W" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="XButton" onclick="SetButtonValueName('ButtonNameHiddenField','XButton')"
                        type="button" value="X" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="YButton" onclick="SetButtonValueName('ButtonNameHiddenField','YButton')"
                        type="button" value="Y" /></td>
                <td style="width: 100px; height: 26px">
                    <input id="ZButton" onclick="SetButtonValueName('ButtonNameHiddenField','ZButton')"
                        type="button" value="Z" /></td>
            </tr>
            <tr>
                <td align="right" colspan="13" class="style1">
                    <asp:TextBox ID="SiteNameSearchTextBox" runat="server" Width="279px"></asp:TextBox>
                </td>
                <td align="left" colspan="13" class="style1">
                    <asp:Button ID="SiteSearchButton" runat="server" Text="Search" Width="106px" 
                        Font-Bold="True" style="margin-bottom: 0px"/>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="26" style="height: 26px">
                    <asp:Button ID="CreateNewSiteButton" runat="server" Text="Create New Site" 
                        style="font-weight: 700" /></td>
            </tr>
            <tr>
                <td align="center" colspan="26" style="height: 22px">
                    <asp:GridView ID="SiteGridView" runat="server" OnRowDataBound="SiteGridView_RowDataBound"
                        OnSelectedIndexChanging="SiteGridView_SelectedIndexChanging">
                        <SelectedRowStyle Font-Bold="True" Font-Italic="False" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
        &nbsp;<asp:HiddenField ID="SearchLetterHiddenField" runat="server" />
&nbsp;<asp:HiddenField ID="ButtonNameHiddenField" runat="server"  />    
    </div>
</asp:Content>
