<%@ Page Title="CCK Application Home Page" Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" MasterPageFile="~/Site.master"  Inherits="Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div>
        <table border="1" width="100%" style="border-color: #d28000">
            <tr>
                <td style="border-color: #d28000; border-width: 1px; font-family: Calibri; font-size: large; font-weight: bold;" 
                    align="center" width="50%">
                    Kitchen Functionality</td>
                <td style="border-color: #d28000; border-width: 1px; font-size: large; font-weight: bold;" 
                    align="center" width="50%" style="font-family: Calibri">Programs Functionality</td>
            </tr>
            <tr>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%" valign="middle">
                    <asp:LinkButton ID="MealPlannerLinkButton" runat="server" OnClick="MenuPlannerLinkButton_Click"
                        Width="159px" Font-Bold="True" ForeColor="Gray" Enabled = "False">Meal Planner</asp:LinkButton>
                </td>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%">
                    <asp:LinkButton ID="DeliveryPlannerLinkButton" runat="server" ForeColor="Gray" Enabled = "False" ToolTip="Click to Plan Upcoming Menus"
                        OnClick="LinkButton6_Click" Width="161px" style="font-weight: 700">Delivery Planner</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%" valign="middle">
                    <asp:LinkButton ID="BreakfastPlannerLinkButton" runat="server" OnClick="BreakfastPlannerLinkButton_Click"
                        Width="159px" Font-Bold="True" ForeColor="Gray" Enabled = "False">Breakfast Planner</asp:LinkButton>
                </td>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%">
                    <asp:LinkButton ID="DeliverySitesLinkButton" runat="server" 
                        OnClick="LinkButton5_Click" ForeColor="Gray" Enabled = "False"
                        Width="130px" style="font-weight: 700">Delivery 
                    Sites</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%" valign="middle">
                    <asp:LinkButton ID="SnackPlannerLinkButton" runat="server"
                        Width="159px" Font-Bold="True" ForeColor="Gray" Enabled = "False">Snack Planner</asp:LinkButton>
                </td>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%">
                    <asp:LinkButton ID="SiteRouteLinkButton" runat="server" OnClick="LinkButton2_Click1" 
                        Width="130px" ForeColor="Gray" Enabled = "False" Height="16px" style="font-weight: 700">Site Route Order</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%" valign="middle">
                    <asp:LinkButton ID="RecipesLinkButton" runat="server" ForeColor="Gray" Enabled = "False" ToolTip="Click to Manage Recipes"
                        OnClick="LinkButton1_Click" Font-Bold="True">Recipes</asp:LinkButton>
                </td>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%">
                    <asp:LinkButton ID="MilkOrderingCalculatorLinkButton" runat="server" OnClick="MilkOrderingCalculator_Click" 
                        Width="253px" ForeColor="Gray" Enabled = "False" Height="19px" style="font-weight: 700">Milk Ordering Calculator</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%" valign="middle">
                    <asp:LinkButton ID="FoodItemInventoryLinkButton" runat="server" 
                        ForeColor="Gray" Enabled = "False" OnClick="LinkButton3_Click"
                        ToolTip="Click to Manage Food Item Inventory" Height="18px" 
                        Font-Bold="True">Food Item Inventory</asp:LinkButton>
                </td>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%">
                    <asp:LinkButton ID="MealDeliveryTypesLinkButton" runat="server" OnClick="MealDeliveryType_Click" 
                        Width="250px" ForeColor="Gray" Enabled = "False" Height="16px" style="font-weight: 700">Meal Delivery Types</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%" valign="middle">
                    <asp:LinkButton ID="NonFoodItemInventoryLinkButton" runat="server" 
                        ForeColor="Gray" Enabled = "False" Height="18px" OnClick="LinkButton69_Click"
                        ToolTip="Click to Manage Non-Food Item Inventory" Font-Bold="True">Non-Food Item Inventory</asp:LinkButton>
                </td>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%">
                    <asp:LinkButton ID="SiteDeliveryCountsLinkButton" runat="server" 
                        ForeColor="Gray" Enabled = "False" ToolTip="Click to Plan Upcoming Menus"
                        OnClick="SiteDeliveryCounts_Click" Width="182px" style="font-weight: 700" 
                        Height="19px">Site Delivery Counts</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%" valign="middle">
                    &nbsp;</td>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style4" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" colspan="2" valign="middle">
                    <asp:LinkButton ID="ReportsLinkButton" runat="server" 
                        OnClick="ReportsLinkButton_Click" ForeColor="White" 
                        style="font-weight: 700" Font-Bold="True">Reports</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style4" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" colspan="2" valign="middle">
                    <asp:LinkButton ID="UserActionLogButton" runat="server" OnClick="UserActionLogButton_Click" 
                        Width="346px" Font-Bold="True" ForeColor="White" Height="16px" 
                        style="font-weight: 700">CCK Application User Action Log</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style1" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" colspan="2" valign="middle">
                    <asp:LinkButton ID="ViewChangeLogLinkButton" runat="server" OnClick="ViewChangeLogLinkButton_Click" 
                        Width="346px" Font-Bold="True" ForeColor="White" Height="16px" 
                        style="font-weight: 700">CCK Application Development Changes</asp:LinkButton>
                </td>
            </tr>
            </table>
    </div>
</asp:Content>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GCFD Kitchen</title>
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
        .style3
        {
            width: 38%;
            height: 29px;
        }
        .style4
        {
            width: 100%;
            height: 29px;
        }
        .style6
        {
            width: 38%;
            height: 28px;
            font-weight: bold;
        }
        .style8
        {
            width: 100%;
            height: 28px;
        }
        .style9
        {
            width: 38%;
            height: 28px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="asm" runat="server" />
    <div>
        <table border="1" width="100%" style="border-color: #d28000">
            <tr>
                <td align="center" class="style2" colspan="2" bgcolor="#004D45">
                    <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Welcome To The CCK Scheduling Application Version 2.0"
                        Width="422px" BackColor="#004D45" ForeColor="White" 
                        style="margin-bottom: 0px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style6" style="border-color: #d28000; border-width: 1px; font-family: Calibri;" 
                    align="center" width="50%">
                    Kitchen Fuctionality</td>
                <td class="style9" style="border-color: #d28000; border-width: 1px;" 
                    align="center" width="50%">
                    <b style="font-family: Calibri">Programs/Operations Functionality</b></td>
            </tr>
            <tr>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%" valign="middle">
                    <asp:LinkButton ID="MenuPlannerLinkButton" runat="server" OnClick="MenuPlannerLinkButton_Click"
                        Width="159px" Font-Bold="True" ForeColor="White">Meal Planner</asp:LinkButton>
                </td>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%">
                    <asp:LinkButton ID="LinkButton6" runat="server" ForeColor="White" ToolTip="Click to Plan Upcoming Menus"
                        OnClick="LinkButton6_Click" Width="161px" style="font-weight: 700">Delivery Planner</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%" valign="middle">
                    <asp:LinkButton ID="BreakfastPlannerLinkButton" runat="server" OnClick="BreakfastPlannerLinkButton_Click"
                        Width="159px" Font-Bold="True" ForeColor="White">Breakfast Planner</asp:LinkButton>
                </td>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%">
                    <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click" ForeColor="White"
                        Width="130px" style="font-weight: 700">Delivery 
                    Sites</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%" valign="middle">
                    <asp:LinkButton ID="SnackPlannerLinkButton" runat="server" OnClick="MenuPlannerLinkButton_Click"
                        Width="159px" Font-Bold="True" ForeColor="White" Enabled="False">Snack Planner</asp:LinkButton>
                </td>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%">
                    <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton2_Click1" 
                        Width="130px" ForeColor="White" Height="16px" style="font-weight: 700">Site Route Order</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%" valign="middle">
                    <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="White" ToolTip="Click to Manage Recipes"
                        OnClick="LinkButton1_Click" Font-Bold="True">Recipes</asp:LinkButton>
                </td>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%">
                    <asp:LinkButton ID="LinkButton8" runat="server" OnClick="MilkOrderingCalculator_Click" 
                        Width="253px" ForeColor="White" Height="19px" style="font-weight: 700">Milk Ordering Calculator</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%" valign="middle">
                    <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="White" OnClick="LinkButton3_Click"
                        ToolTip="Click to Manage Food Item Inventory" Height="18px" 
                        Font-Bold="True">Food Item Inventory</asp:LinkButton>
                </td>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%">
                    <asp:LinkButton ID="LinkButton9" runat="server" OnClick="MealDeliveryType_Click" 
                        Width="250px" ForeColor="White" Height="16px" style="font-weight: 700">Meal Delivery Types</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%" valign="middle">
                    <asp:LinkButton ID="LinkButton4" runat="server" ForeColor="White" Height="18px" OnClick="LinkButton69_Click"
                        ToolTip="Click to Manage Non-Food Item Inventory" Font-Bold="True">Non-Food Item Inventory</asp:LinkButton>
                </td>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%">
                    <asp:LinkButton ID="LinkButton10" runat="server" ForeColor="White" ToolTip="Click to Plan Upcoming Menus"
                        OnClick="SiteDeliveryCounts_Click" Width="182px" style="font-weight: 700" 
                        Height="19px">Site Delivery Counts</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%" valign="middle">
                    &nbsp;</td>
                <td class="style3" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" width="50%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style4" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" colspan="2" valign="middle">
                    <asp:LinkButton ID="ReportsLinkButton" runat="server" 
                        OnClick="ReportsLinkButton_Click" ForeColor="White" 
                        style="font-weight: 700" Font-Bold="True">Reports</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style8" style="border-color: #FFFFFF;" align="center" 
                    bgcolor="#004D45" colspan="2" valign="middle">
                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click1" 
                        Width="190px" Font-Bold="True" ForeColor="White" Height="16px" 
                        style="font-weight: 700">View Change Log</asp:LinkButton>
                </td>
            </tr>
            </table>
    </div>
    </form>
</body></html>--%>
