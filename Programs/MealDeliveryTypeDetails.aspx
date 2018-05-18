<%@ Page Language="C#" AutoEventWireup="true" Title = "CCK Meal Delivery Type Details" EnableEventValidation="false"  MasterPageFile="~/Site.master" CodeFile="MealDeliveryTypeDetails.aspx.cs" Inherits="MealDeliveryTypeDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<script type="text/javascript" language="JavaScript">
    function ProhibitPastActivityClick() {

        if (document.getElementById('ctl00_MainContent_ProhibitPastActivityCheckBox').checked) {
            document.getElementById('ctl00_MainContent_FutureIntervalTextBox').disabled = false;
            myObj = document.getElementById('ctl00_MainContent_FutureActivityIntervalLabel');
            myObj.style.color = "white";
        }
        else {
            document.getElementById('ctl00_MainContent_FutureIntervalTextBox').disabled = true;
            myObj = document.getElementById('ctl00_MainContent_FutureActivityIntervalLabel');
            myObj.style.color = "silver";
        }
    }

function disableEnterKey(e)
{
     var key;      
     if(window.event)
          key = window.event.keyCode; //IE
     else
          key = e.which; //firefox      

     return (key != 13);
}      
</script>
    <style type="text/css">
        .style1
        {
            text-align: center;
        }
        .style2
        {
            width: 486px;
            height: 20px;
        }
        .style3
        {
            width: 50%;
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="asm" runat="server" />
        <table id="IngredientDetailsTable" style="width: 100%; height: 20px">
            <tr>
                <td align="center" colspan="2" style="height: 6%" valign="bottom">
                    <asp:Button ID="SaveButton" runat="server" 
                        OnClick="SaveButton_Click" Text="Save Meal Delivery Type Changes" 
                        Font-Bold="True" Width="226px" /><asp:Button ID="SaveAsNewButton" 
                        runat="server" OnClick="SaveAsNewButton_Click"
                        Text="Save As New Meal Delivery Type" Font-Bold="True" Width="235px" />
                    <asp:Button ID="DeleteMealDeliveryTypeButton" runat="server" OnClick="DeleteMealDeliveryTypeButton_Click"
                        Text="Delete Meal Delivery Type" Font-Bold="True" Width="188px" /></td>
            </tr>
            <tr>
                <td align="right" style="width: 486px; height: 17px" valign="top">
                </td>
                <td align="left" style="width: 50%; height: 17px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 486px; height: 17px" valign="top">
                    <asp:Label ID="Label1" runat="server" Text="Meal Delivery Type Name:"></asp:Label></td>
                <td align="left" style="width: 50%; height: 17px" valign="top">
                    <asp:TextBox ID="MealDeliveryTypeNameTextBox" runat="server" Width="230px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="MealDeliveryTypeNameTextBoxRequiredFieldValidator" 
                    runat="server" ControlToValidate="MealDeliveryTypeNameTextBox" Text="Required Field!"
                    ErrorMessage="RequiredFieldValidator" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right" valign="top" class="style2">
                    Serving Day Interval:</td>
                <td align="left" valign="top" class="style2">
                    <asp:DropDownList ID="ServingDayIntervalDropDownList" runat="server" 
                        Height="20px" Width="35px">
                        <asp:ListItem>0</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 17px; font-size: x-small; text-align: center;" 
                    valign="top" colspan="2">
                    (How many days from the delivery day.&nbsp; i.e. Value of 0 means serving day is 
                    the same as delivery day.)</td>
            </tr>
            <tr>
                <td align="right" style="width: 486px; height: 17px" valign="top">
                    Enable Weekend Scheduling Defaults(deliveries/servings):</td>
                <td align="left" style="width: 50%; height: 17px" valign="top">
                    <asp:CheckBox ID="WeekendActivityCheckBox" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="height: 17px; font-size: x-small;" valign="top" class="style1" 
                    colspan="2">
                    (Sets the default serving day equal to a weekend day if appropriate during the 
                    delivery recurrence creation/modification process.)</td>
            </tr>
            <tr>
                <td align="right" style="width: 486px; height: 17px" valign="top">
                    Prohibit The Modification/Adding/Deleting of Scheduling Activity in the Past:</td>
                <td align="left" style="width: 50%; height: 17px" valign="top">
                    <asp:CheckBox ID="ProhibitPastActivityCheckBox" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 486px; height: 17px" valign="top">
                    <asp:Label ID="FutureActivityIntervalLabel" runat="server" 
                        
                        Text="Interval of Days in the Future An Activity Can Be Modified/Added/Deleted:" 
                        Enabled="False" ForeColor="Silver"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 17px" valign="top">
                    <asp:TextBox ID="FutureIntervalTextBox" runat="server" Width="24px" 
                        Enabled="False">2</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2" style="height: 7px" valign="top">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="height: 7px" valign="top">
                    <asp:HiddenField ID="OriginalMealDeliveryTypeNameHiddenField" runat="server" />
                    <asp:HiddenField ID="MealDeliveryTypeModeHiddenField" runat="server" />
                    <asp:HiddenField ID="MealDeliveryTypeIDHiddenField" runat="server" />
                </td>
            </tr>
            </table>
</asp:Content>