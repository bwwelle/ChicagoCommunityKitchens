<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"  Title = "CCK Food Item Inventory Details" MasterPageFile="~/Site.master"
    CodeFile="FoodItemInventoryDetails.aspx.cs" Inherits="FoodItemInventoryDetails" %>

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

            if (UnitSize == "Select Recipe Unit") {
                document.getElementById('ctl00_MainContent_RecipeUnitLabel').innerText = '';
            }
            else {

                document.getElementById('ctl00_MainContent_RecipeUnitLabel').innerText = UnitSize;
                document.getElementById('ctl00_MainContent_RecipeUnitHiddenField').value = UnitSize;
            }
        }

        function updatecost() {
            if (document.getElementById('ctl00_MainContent_InvoicedCostTextBox').value != '' && document.getElementById('ctl00_MainContent_RecipeUnitInUnitTextBox').value != '') {
                var yieldValue = document.getElementById('ctl00_MainContent_YieldTextBox').value / 100;
                yieldValue = yieldValue * document.getElementById('ctl00_MainContent_RecipeUnitInUnitTextBox').value;

                document.getElementById('ctl00_MainContent_RecipeUnitAfterWasteLabel').innerText = formatCurrency(yieldValue);
                document.getElementById('ctl00_MainContent_RecipeUnitAfterWasteHiddenField').value = formatCurrency(yieldValue);

                yieldValue = document.getElementById('ctl00_MainContent_InvoicedCostTextBox').value / yieldValue;

                document.getElementById('ctl00_MainContent_CostPerRecipeUnitLabel').innerText = '$' + formatCurrency(yieldValue);
                document.getElementById('ctl00_MainContent_CostPerRecipeUnitHiddenField').value = formatCurrency(yieldValue);
            }
        }

        function formatCurrency(num) {
            num = isNaN(num) || num === '' || num === null ? 0.00 : num;
            return parseFloat(num).toFixed(2);
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="asm" runat="server" />
    <table id="IngredientDetailsTable" style="width: 100%; height: 20px">
        <tr>
            <td align="center" colspan="7" style="height: 6%" valign="bottom">
                <asp:Button ID="SaveButton" runat="server" OnClick="SaveButton_Click"
                        Text="Save Ingredient Changes" Font-Bold="True" /><asp:Button ID="SaveAsNewButton"
                            runat="server" OnClick="SaveAsNewButton_Click" Text="Save As New Ingredient"
                            Font-Bold="True" /><asp:Button ID="DeleteIngredientButton" runat="server" OnClick="DeleteIngredientButton_Click"
                                Text="Delete Ingredient"  Font-Bold="True" OnClientClick="return confirm('Do you really want to delete this ingredient?');" />
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="width: 50%; height: 7px" valign="top">
            </td>
            <td align="left" colspan="4" style="width: 50%; height: 17px" valign="top">
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="width: 50%; height: 7px" valign="top">
                <asp:Label ID="Label1" runat="server" Text="Ingredient Name:"></asp:Label>
            </td>
            <td align="left" colspan="4" style="width: 50%; height: 17px" valign="top">
                <asp:TextBox ID="IngredientNameTextBox" runat="server" Width="230px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="IngredientNameRequiredFieldValidator" 
                    runat="server" ControlToValidate="IngredientNameTextBox" Text="The name field is required!"
                    ErrorMessage="RequiredFieldValidator" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:HiddenField ID="OriginalIngredientNameHiddenField" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="height: 7px; width: 50%;" valign="top">
                <asp:Label ID="Label2" runat="server" Text="Recipe Unit:"></asp:Label>
            </td>
            <td align="left" colspan="4" style="height: 17px; width: 50%;" valign="top">
                <asp:DropDownList ID="UnitDropDownList" runat="server" Width="152px">
                   <asp:ListItem>#10 can(s)</asp:ListItem>
                   <asp:ListItem>#2.5 can(s)</asp:ListItem>
                   <asp:ListItem>bag(s), cookchill</asp:ListItem>
                   <asp:ListItem>bucket(s)</asp:ListItem>
                   <asp:ListItem>bushel(s)</asp:ListItem>
                   <asp:ListItem>gallon(s)</asp:ListItem>
                   <asp:ListItem>gram(s)</asp:ListItem>
                   <asp:ListItem>ounce(s)</asp:ListItem>
                   <asp:ListItem>ounce(s), fluid</asp:ListItem>
                   <asp:ListItem>piece(s)</asp:ListItem>
                   <asp:ListItem>pint(s)</asp:ListItem>
                   <asp:ListItem>pound(s)</asp:ListItem>
                   <asp:ListItem>quart(s)</asp:ListItem>
                   <asp:ListItem>sheet(s)</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RecipeUnitRequiredFieldValidator" 
                    runat="server" ControlToValidate="UnitDropDownList" Text="The recipe unit field is required!"
                    ErrorMessage="RequiredFieldValidator" Font-Bold="True" ForeColor="Red" InitialValue="Select Recipe Unit"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="height: 7px; width: 50%;" valign="top">
                <asp:Label ID="Label3" runat="server" Text="Vendor:"></asp:Label>
            </td>
            <td align="left" colspan="4" style="height: 17px; width: 50%;" valign="top">
                <asp:DropDownList ID="VendorDropDownList" runat="server" Width="150px">
                    <asp:ListItem>Select Vendor</asp:ListItem>
                    <asp:ListItem>CCK</asp:ListItem>
                    <asp:ListItem>Chi Sweeteners</asp:ListItem>
                    <asp:ListItem>Finer Foods</asp:ListItem>
                    <asp:ListItem>GFS</asp:ListItem>
                    <asp:ListItem>GFS-CMA</asp:ListItem>
                    <asp:ListItem>S11.00</asp:ListItem>
                    <asp:ListItem>SYSCO</asp:ListItem>
                    <asp:ListItem>US Foodservice</asp:ListItem>
                    <asp:ListItem>UNFI</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="VendorRequiredFieldValidator0" 
                    runat="server" ControlToValidate="VendorDropDownList" Text="The vendor field is required!"
                    ErrorMessage="RequiredFieldValidator" Font-Bold="True" ForeColor="Red" 
                    InitialValue="Select Vendor"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="height: 7px; width: 50%;" valign="bottom">
                <asp:Label ID="Label4" runat="server" Text="Last Invoiced Cost:"></asp:Label>
            </td>
            <td align="left" colspan="4" style="height: 17px; width: 50%;" valign="bottom">
                <asp:TextBox ID="TextBox1" runat="server" BorderStyle="None" Width="16px" ReadOnly="True"
                    Wrap="False" BackColor="Transparent">$</asp:TextBox><asp:TextBox ID="InvoicedCostTextBox" runat="server"></asp:TextBox>
                <asp:CompareValidator ID="CheckFormat1" runat="server" ControlToValidate="InvoicedCostTextBox"
                    Operator="DataTypeCheck" Type="Currency" Display="Dynamic" Font-Bold="True" ForeColor="Red" ErrorMessage="Invalid Character!" /><asp:RequiredFieldValidator ID="InvoicedCostRequiredFieldValidator" 
                    runat="server" ControlToValidate="InvoicedCostTextBox" Text="The last invoiced cost field is required!"
                    ErrorMessage="RequiredFieldValidator" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="height: 7px; width: 50%;" valign="top">
                <asp:Label ID="Label5" runat="server" Text="Purchase Unit:"></asp:Label>
            </td>
            <td align="left" colspan="4" style="height: 17px; width: 50%;" valign="top">
                <asp:TextBox ID="PurchasedUnitTextBox" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="PurchasedUnitRequiredFieldValidator" 
                    runat="server" ControlToValidate="PurchasedUnitTextBox" Text="The purchase unit field is required!"
                    ErrorMessage="RequiredFieldValidator" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="height: 7px; width: 50%;" valign="top">
                <asp:Label ID="Label6" runat="server" Text="Recipe Units In Purchase Unit:"></asp:Label>
            </td>
            <td align="left" colspan="4" style="height: 17px; width: 50%;" valign="bottom">
                <asp:TextBox ID="RecipeUnitInUnitTextBox" runat="server"></asp:TextBox><asp:CompareValidator
                    ID="CompareValidator2" runat="server" ControlToValidate="RecipeUnitInUnitTextBox"
                    Operator="DataTypeCheck" Type="Currency" Display="Dynamic" ErrorMessage="Invalid Character!" Font-Bold="True" ForeColor="Red" /><asp:RequiredFieldValidator ID="RecipeUnitInUnitRequiredFieldValidator" 
                    runat="server" ControlToValidate="RecipeUnitInUnitTextBox" Text="The recipe units in purchase unit field is required!"
                    ErrorMessage="RequiredFieldValidator" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="width: 50%; height: 7px" valign="top">
                <asp:Label ID="Label12" runat="server" Text="Recipe Units In Purchase Unit After Waste:"></asp:Label>
            </td>
            <td align="left" colspan="4" style="height: 17px; width: 50%;" valign="top">
                <asp:Label ID="RecipeUnitAfterWasteLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="height: 7px; width: 50%;" valign="top">
                <asp:Label ID="Label7" runat="server" Text="Cost Per Recipe Unit Based On Yield:"></asp:Label>
            </td>
            <td align="left" colspan="4" style="height: 17px; width: 50%;" valign="top">
                <asp:Label ID="CostPerRecipeUnitLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="height: 7px; width: 50%;" valign="bottom">
                <asp:Label ID="Label8" runat="server" Text="Yield:"></asp:Label>
            </td>
            <td align="left" colspan="4" style="height: 17px; width: 50%;" valign="bottom">
                <asp:TextBox ID="YieldTextBox" runat="server" Width="30px"></asp:TextBox><asp:Label
                    ID="Label10" runat="server" Text="%" Width="18px"></asp:Label><asp:CompareValidator
                        ID="CompareValidator3" runat="server" ControlToValidate="YieldTextBox" Operator="DataTypeCheck"
                        Type="Currency" Display="Dynamic" ErrorMessage="Invalid Character!" Font-Bold="True" ForeColor="Red"/><asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                    runat="server" ControlToValidate="YieldTextBox" Text="The yield field is required!"
                    ErrorMessage="RequiredFieldValidator" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="width: 50%; height: 7px" valign="bottom">
                Par:
            </td>
            <td align="left" colspan="4" style="height: 17px; width: 50%;" valign="bottom">
                <asp:TextBox ID="ParTextBox" runat="server" Width="47px"></asp:TextBox>
                <asp:Label ID="RecipeUnitLabel" runat="server"></asp:Label>
                <asp:CompareValidator ID="CompareValidator4"
                        runat="server" ControlToValidate="ParTextBox" Operator="DataTypeCheck" Type="Currency"
                        Display="Dynamic" ErrorMessage="Invalid Character!" Font-Bold="True" ForeColor="Red"/><asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                    runat="server" ControlToValidate="ParTextBox" Text="The par field is required!"
                    ErrorMessage="RequiredFieldValidator" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="width: 50%; height: 7px" valign="top">
                <asp:Label ID="Label13" runat="server" Text="Category:"></asp:Label>
            </td>
            <td align="left" colspan="4" style="height: 17px; width: 50%;" valign="bottom">
                <asp:DropDownList ID="CategoryDropDownList" runat="server" Width="178px" AutoPostBack="True"
                    OnSelectedIndexChanged="CategoryDropDownList_SelectedIndexChanged">
                    <asp:ListItem>Select A Category</asp:ListItem>
                    <asp:ListItem>Dry Goods</asp:ListItem>
                    <asp:ListItem>Meats</asp:ListItem>
                    <asp:ListItem>Walk-In</asp:ListItem>
                </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                    runat="server" ControlToValidate="CategoryDropDownList" Text="The category field is required!"
                    ErrorMessage="RequiredFieldValidator" Font-Bold="True" InitialValue="Select A Category" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="width: 50%; height: 7px" valign="top">
                <asp:Label ID="Label14" runat="server" Text="SubCategory:"></asp:Label>
            </td>
            <td align="left" colspan="4" style="height: 17px; width: 50%;" valign="bottom">
                <asp:DropDownList ID="SubCategoryListBox" runat="server" Width="178px">
                    <asp:ListItem>Select A SubCategory</asp:ListItem>
                    <asp:ListItem>General</asp:ListItem>
                    <asp:ListItem>Dairy</asp:ListItem>
                    <asp:ListItem>Baking</asp:ListItem>
                    <asp:ListItem>Stock Base</asp:ListItem>
                    <asp:ListItem>Vegetables</asp:ListItem>
                    <asp:ListItem>Fruit</asp:ListItem>
                    <asp:ListItem>Frozen</asp:ListItem>
                    <asp:ListItem>Herbs</asp:ListItem>
                    <asp:ListItem>Assorted</asp:ListItem>
                    <asp:ListItem>Spices</asp:ListItem>
                    <asp:ListItem>Can Goods</asp:ListItem>
                </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                    runat="server" ControlToValidate="SubCategoryListBox" Text="The subcategory field is required!"
                    ErrorMessage="RequiredFieldValidator" Font-Bold="True" InitialValue="Select A SubCategory" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="height: 7px; width: 50%;" valign="top">
                <asp:Label ID="Label11" runat="server" Text="Date Last Updated:"></asp:Label>
            </td>
            <td align="left" colspan="4" style="height: 17px; width: 50%;" valign="bottom">
                <asp:Label ID="DateLastUpdatedLabel" runat="server" Width="245px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="7" style="height: 7px" valign="top">
                <asp:Label ID="Label9" runat="server" Text="Notes:"></asp:Label>
                <asp:HiddenField ID="IngredientIDHiddenField" runat="server" />
                <asp:HiddenField ID="IngredientModeHiddenField" runat="server" />
                <asp:HiddenField ID="RecipeUnitHiddenField" runat="server" />
                <asp:HiddenField ID="RecipeUnitAfterWasteHiddenField" runat="server" />
                <asp:HiddenField ID="CostPerRecipeUnitHiddenField" runat="server" />
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
