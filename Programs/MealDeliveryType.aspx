<%@ Page Language="VB" EnableEventValidation="false"  Title = "CCK Meal Type" AutoEventWireup="false" MasterPageFile="~/Site.master"  CodeFile="MealDeliveryType.aspx.vb" Inherits="MealDeliveryType" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<script type="text/javascript" language="javascript">
    var buttonID;
    var hiddenTextBoxName;

    function SetButtonValueName(hiddenTextBoxName, buttonID) {
        document.getElementById('ctl00_MainContent_ButtonNameHiddenField').value = buttonID;

        document.forms.aspnetForm.submit();
    }
</script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div align="left">
        <asp:Label ID="Label1" runat="server" Text="View Meal Type"></asp:Label>&nbsp;</div>
        <table style="width: 100%">
            <tr>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="AllButton" value="All" onclick="SetButtonValueName('ButtonNameHiddenField','allbutton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="AButton" value="A" onclick="SetButtonValueName('ButtonNameHiddenField','AButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="BButton" value="B" onclick="SetButtonValueName('ButtonNameHiddenField','BButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="CButton" value="C" onclick="SetButtonValueName('ButtonNameHiddenField','CButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="DButton" value="D" onclick="SetButtonValueName('ButtonNameHiddenField','DButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="EButton" value="E" onclick="SetButtonValueName('ButtonNameHiddenField','EButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="FButton" value="F" onclick="SetButtonValueName('ButtonNameHiddenField','FButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="GButton" value="G" onclick="SetButtonValueName('ButtonNameHiddenField','GButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="HButton" value="H" onclick="SetButtonValueName('ButtonNameHiddenField','HButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="IButton" value="I" onclick="SetButtonValueName('ButtonNameHiddenField','IButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="JButton" value="J" onclick="SetButtonValueName('ButtonNameHiddenField','JButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="KButton" value="K" onclick="SetButtonValueName('ButtonNameHiddenField','KButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="LButton" value="L" onclick="SetButtonValueName('ButtonNameHiddenField','LButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="MButton" value="M" onclick="SetButtonValueName('ButtonNameHiddenField','MButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="NButton" value="N" onclick="SetButtonValueName('ButtonNameHiddenField','NButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="OButton" value="O" onclick="SetButtonValueName('ButtonNameHiddenField','OButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="PButton" value="P" onclick="SetButtonValueName('ButtonNameHiddenField','PButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="QButton" value="Q" onclick="SetButtonValueName('ButtonNameHiddenField','QButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="RButton" value="R" onclick="SetButtonValueName('ButtonNameHiddenField','RButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="SButton" value="S" onclick="SetButtonValueName('ButtonNameHiddenField','SButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="TButton" value="T" onclick="SetButtonValueName('ButtonNameHiddenField','TButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="UButton" value="U" onclick="SetButtonValueName('ButtonNameHiddenField','UButton')"/></td>
                    <td style="width: 100px; height: 26px">
                    <input type="button" id="VButton" value="V" onclick="SetButtonValueName('ButtonNameHiddenField','VButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="WButton" value="W" onclick="SetButtonValueName('ButtonNameHiddenField','WButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="XButton" value="X" onclick="SetButtonValueName('ButtonNameHiddenField','XButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="YButton" value="Y" onclick="SetButtonValueName('ButtonNameHiddenField','YButton')"/></td>
                <td style="width: 100px; height: 26px">
                    <input type="button" id="ZButton" value="Z" onclick="SetButtonValueName('ButtonNameHiddenField','ZButton')"/></td>
            </tr>
            <tr>
                <td align="right" colspan="13" class="style1">
                    <asp:TextBox ID="MealDeliveryTypeSearchTextBox" runat="server" Width="279px"></asp:TextBox>
                </td>
                <td align="left" colspan="13" class="style1">
                    <asp:Button ID="MealDeliveryTypeSearchButton" runat="server" Text="Search" Width="106px" 
                        Font-Bold="True" style="margin-bottom: 0px"/>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="26" style="height: 26px">
                    <asp:Button ID="CreateNewMealDeliveryTypeButton" runat="server" 
                        Text="Create New Meal Type" style="font-weight: 700" /></td>
            </tr>
            <tr>
                <td align="center" colspan="26" style="height: 22px">
                    <asp:GridView ID="MealDeliveryTypeGridView" runat="server" OnRowDataBound="MealDeliveryTypeSearchTextBox_RowDataBound" OnSelectedIndexChanging="MealDeliveryTypeGridView_SelectedIndexChanging">
                        <SelectedRowStyle Font-Bold="True" Font-Italic="False" />
                    </asp:GridView>
                    <asp:HiddenField ID="SearchLetterHiddenField" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        &nbsp;
        <input id="ButtonNameHiddenField" runat="server" type="hidden" />
</asp:Content>