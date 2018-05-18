<%@ Page Language="C#" AutoEventWireup="true" Title = "CCK Non-Food Item Inventory Details" MasterPageFile="~/Site.master" EnableEventValidation="false"
    CodeFile="NonFoodItemInventoryDetails.aspx.cs" Inherits="NonFoodItemInventoryDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" language="JavaScript">
        function disableEnterKey(e) {
            var key;
            if (window.event)
                key = window.event.keyCode; //IE
            else
                key = e.which; //firefox      

            return (key != 13);
        }

        function updateparlabel() {
            var oUnitSize = document.all("ctl00_MainContent_UnitDropDownList");
            var UnitSize = oUnitSize.options[oUnitSize.selectedIndex].value;

            if (UnitSize == "Select Purchase Unit") {
                document.getElementById('ctl00_MainContent_PurchaseUnitLabel').innerText = '';
            }
            else {

                document.getElementById('ctl00_MainContent_PurchaseUnitLabel').innerText = UnitSize;
                document.getElementById('ctl00_MainContent_PurchaseUnitHiddenField').value = UnitSize;
            }

            //var oUnitSize = document.all("UnitDropDownList");
            //var UnitSize = oUnitSize.options[oUnitSize.selectedIndex].value;
            //document.form1.elements['PurchaseUnitTextBox'].value = UnitSize;
        }

        function updatecost() {
            if (document.getElementById('ctl00_MainContent_InvoicedCostTextBox').value != '' && document.getElementById('ctl00_MainContent_PiecesPerPurchasedUnitTextBox').value != '') {
                var yieldValue = document.getElementById('ctl00_MainContent_PiecesPerPurchasedUnitTextBox').value;

                yieldValue = document.getElementById('ctl00_MainContent_InvoicedCostTextBox').value / yieldValue;

                document.getElementById('ctl00_MainContent_CostPerPurchaseUnitLabel').innerText = '$' + formatCurrency(yieldValue);
                document.getElementById('ctl00_MainContent_CostPerPurchaseUnitHiddenField').value = formatCurrency(yieldValue);
            }
        }

        function formatCurrency(num) {
            num = isNaN(num) || num === '' || num === null ? 0.00 : num;
            return parseFloat(num).toFixed(2);
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 486px;
        }
        .style3
        {
            width: 50%;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="asm" runat="server" />
    <table id="IngredientDetailsTable" style="width: 100%; height: 20px">
        <tr>
            <td align="center" colspan="7" style="height: 6%" valign="bottom">
                <asp:Button ID="SaveButton" runat="server" OnClick="SaveButton_Click"
                        Text="Save Non-Food Item Changes" Font-Bold="True" Width="226px" /><asp:Button ID="SaveAsNewButton"
                            runat="server" OnClick="SaveAsNewButton_Click" Text="Save As New Non-Food Item"
                            Font-Bold="True" Width="235px" /><asp:Button ID="DeleteItemButton" runat="server"
                                OnClick="DeleteNonFoodItemButton_Click" Text="Delete Non-Food Item" Font-Bold="True"
                                Width="188px" />
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" valign="top" class="style1">
            </td>
            <td align="left" colspan="4" valign="top" class="style3">
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="width: 486px; height: 17px" 
                valign="bottom">
                <asp:Label ID="Label1" runat="server" Text="Non-Food Item Name:"></asp:Label>
            </td>
            <td align="left" colspan="4" style="width: 50%; height: 17px" valign="top">
                <asp:TextBox ID="ItemNameTextBox" runat="server" Width="230px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="NonFoodItemNameRequiredFieldValidator" 
                    runat="server" ControlToValidate="ItemNameTextBox" 
                    ErrorMessage="RequiredFieldValidator" Font-Bold="True" ForeColor="Red" 
                    Text="The name field is required!"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="height: 10px; width: 486px;" 
                valign="bottom">
                <asp:Label ID="Label2" runat="server" Text="Purchase Unit:"></asp:Label>
            </td>
            <td align="left" colspan="4" style="height: 10px" valign="top">
                <asp:DropDownList ID="UnitDropDownList" runat="server" Width="152px" ons>
                    <asp:ListItem>Select Purchase Unit</asp:ListItem>
                    <asp:ListItem>each</asp:ListItem>
                    <asp:ListItem>gallon</asp:ListItem>
                    <asp:ListItem>case</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="height: 8px; width: 486px;" 
                valign="bottom">
                <asp:Label ID="Label3" runat="server" Text="Vendor:"></asp:Label>
            </td>
            <td align="left" colspan="4" style="height: 8px" valign="top">
                <asp:DropDownList ID="VendorDropDownList" runat="server" Width="150px">
                    <asp:ListItem>Select Vendor</asp:ListItem>
                    <asp:ListItem>CCK</asp:ListItem>
                    <asp:ListItem>Chi Sweeteners</asp:ListItem>
                    <asp:ListItem>GFS</asp:ListItem>
                    <asp:ListItem>GFS-CMA</asp:ListItem>
                    <asp:ListItem>S11.00</asp:ListItem>
                    <asp:ListItem>SYSCO</asp:ListItem>
                    <asp:ListItem>US Foodservice</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="height: 2px; width: 486px;" valign="bottom">
                <asp:Label ID="Label4" runat="server" Text="Last Invoiced Cost:"></asp:Label>
            </td>
            <td align="left" colspan="4" style="height: 2px" valign="bottom">
                <asp:TextBox ID="TextBox1" runat="server" BorderStyle="None" Width="16px" ReadOnly="True"
                    Wrap="False" BackColor="Transparent">$ </asp:TextBox>
                <asp:TextBox ID="InvoicedCostTextBox" runat="server" ForeColor="Black"></asp:TextBox><asp:CompareValidator
                        ID="CompareValidator1" runat="server" ControlToValidate="InvoicedCostTextBox"
                        Operator="DataTypeCheck" Type="Currency" Display="Dynamic" 
                    ErrorMessage="Invalid Character!" Font-Bold="True" ForeColor="Red" />
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" valign="bottom" class="style1">
                <asp:Label ID="Label5" runat="server" Text="Pieces Per Purchase Unit:"></asp:Label>
            </td>
            <td align="left" colspan="4" valign="bottom">
                <asp:TextBox ID="PiecesPerPurchasedUnitTextBox" runat="server"></asp:TextBox><asp:CompareValidator
                    ID="CompareValidator3" runat="server" ControlToValidate="PiecesPerPurchasedUnitTextBox"
                    Operator="DataTypeCheck" Type="Currency" Display="Dynamic" ErrorMessage="Invalid Character!" Font-Bold="True" ForeColor="Red" />
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="height: 7px; width: 486px;" 
                valign="bottom">
                <asp:Label ID="Label7" runat="server" Text="Cost Per Piece:"></asp:Label>
            </td>
            <td align="left" colspan="4" style="height: 7px" valign="top">
                <asp:Label ID="CostPerPurchaseUnitLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="width: 486px; height: 7px" valign="bottom">
                <asp:Label ID="Label14" runat="server" Text="SubCategory:"></asp:Label>
            </td>
            <td align="left" colspan="4" style="height: 7px" valign="bottom">
                <asp:DropDownList ID="SubCategoryListBox" runat="server" Width="178px">
                    <asp:ListItem>Select A SubCategory</asp:ListItem>
                    <asp:ListItem>Cleaning Supplies</asp:ListItem>
                    <asp:ListItem>Paper Goods</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="width: 486px; height: 7px" valign="bottom">
                Par:
            </td>
            <td align="left" colspan="4" style="height: 7px" valign="bottom">
                <asp:TextBox ID="ParTextBox" runat="server" Width="47px"></asp:TextBox><asp:Label ID="PurchaseUnitLabel" runat="server"></asp:Label>
                <asp:CompareValidator ID="CompareValidator2"
                        runat="server" ControlToValidate="ParTextBox" Operator="DataTypeCheck" Type="Currency"
                        Display="Dynamic" ErrorMessage="Invalid Character!" Font-Bold="True" ForeColor="Red" />
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="height: 7px; width: 486px;" 
                valign="bottom">
                <asp:Label ID="Label11" runat="server" Text="Date Last Updated:"></asp:Label>
            </td>
            <td align="left" colspan="4" style="height: 7px" valign="bottom">
                <asp:Label ID="DateLastUpdatedLabel" runat="server" Text="N/A" Width="245px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="7" style="height: 7px" valign="top">
                <asp:Label ID="Label9" runat="server" Text="Notes:"></asp:Label>
                <asp:HiddenField ID="NonFoodItemIDHiddenField" runat="server" />
                <asp:HiddenField ID="NonFoodItemModeHiddenField" runat="server" />
                <asp:HiddenField ID="OriginalNonFoodItemNameHiddenField" runat="server" />
                <asp:HiddenField ID="PurchaseUnitHiddenField" runat="server" />
                <asp:HiddenField ID="CostPerPurchaseUnitHiddenField" runat="server" />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="7" style="height: 7px" valign="top">
                &nbsp;<asp:TextBox ID="NotesTextBox" runat="server" Height="40px" TextMode="MultiLine"
                    Width="878px"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
