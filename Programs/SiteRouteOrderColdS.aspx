<%@ Page Language="C#" AutoEventWireup="true"  Title = "CCK Site Route Order" CodeFile="SiteRouteOrderColdS.aspx.cs" Inherits="SiteRouteOrder"  MasterPageFile="~/Site.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .dragHandle
        {
            width: 10px;
            height: 15px;
            background-color: Blue;
            background-image: url(~/Images/bg-menu-main.png);
            cursor: move;
            border: outset thin white;
        }
        
        .callbackStyle
        {
            border: thin blue inset;
        }
        
        .callbackStyle table
        {
            background-color: #5377A9;
            color: Black;
        }
        
        
        .reorderListDemo li
        {
            list-style: none;
            margin: 2px;
            background-image: url(images/bg_nav.gif);
            background-repeat: repeat-x;
            color: #FFF;
        }
        
        .reorderListDemo li a
        {
            color: #FFF !important;
            font-weight: bold;
        }
        
        .reorderCue
        {
            border: dashed thin black;
            width: 100%;
            height: 25px;
        }
        
        .itemArea
        {
            margin-left: 15px;
            font-family: Arial, Verdana, sans-serif;
            font-size: 1em;
            text-align: left;
        }
        
        .reorderStyle li
        {
            list-style-type: none;
            font: Verdana;
            font-size: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <table width="100%">
            <tr>
                <td align="center" colspan="6" style="font-weight: bold; font-size: x-large; height: 21px">
                    Site Route Order
                </td>
            </tr>
            <tr>
                <td align="center" colspan="6" style="font-weight: bold; font-size: x-large; height: 21px">
                    <asp:Button ID="SaveOrder" runat="server" Text="Save Route Order Changes" OnClick="SaveOrder_Click"
                        Width="165px" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3" style="font-weight: bold; font-size: large; width: 50%;
                    height: 21px">
                    CCK-Cold Route South
                </td>
                <td align="center" colspan="3" style="font-weight: bold; font-size: large; width: 50%;
                    height: 21px">
                   
                </td>
            </tr>
            <tr>
                <td align="right" colspan="1" style="width: 10%; height: 21px" valign="top">
                    <ajaxToolkit:ReorderList ID="SouthOrderNumberList" runat="server" CssClass="reorderStyle">
                    </ajaxToolkit:ReorderList>
                </td>
                <td align="left" colspan="2" style="height: 21px; width: 3%;" valign="top">

                    <ajaxToolkit:ReorderList ID="SouthRouteOrderList" runat="server" AllowReorder="true"
                        LayoutType="Table" OnItemReorder="SouthRouteOrderItems_ItemReorder" PostBackOnReorder="true">
                        <ItemTemplate>
                            <span style="cursor: pointer;">
                                <asp:Label ID="ItemName" runat="server" Text='<%# Container.DataItem %>' />
                            </span>
                        </ItemTemplate>
                    </ajaxToolkit:ReorderList>
                    &nbsp;
                </td>
                <td align="right" colspan="1" style="width: 10%; height: 21px" valign="top">
                   
                </td>
                <td align="left" colspan="2" style="height: 21px; width: 40%;" valign="top">
                   
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
