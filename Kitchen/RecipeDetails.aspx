<%@ Page Language="C#" Title="CCK Recipe Details" AutoEventWireup="true" MasterPageFile="~/Site.master"
    EnableEventValidation="false" CodeFile="RecipeDetails.aspx.cs" Inherits="RecipeDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        var launchIngredientPopup = false;
        var launchCondimentPopup = false;
        var launchDirectionPopup = false;
        var launchCookChillYieldPopup = false;
        var launchRegularYieldPopup = false;

        function launchIngredientPopupModal() {
            launchIngredientPopup = true;
        }

        function launchCondimentPopupModal() {
            launchCondimentPopup = true;
        }

        function launchDirectionPopupModal() {
            launchDirectionPopup = true;
        }

        function launchRegularYieldPopupModal() {
            launchRegularYieldPopup = true;
        }

        function launchCookChillYieldPopupModal() {
            launchCookChillYieldPopup = true;
        }

        function fnClickOK(sender, e) {
            __doPostBack(sender, e);
        }

        function pageLoad() {
            if (launchIngredientPopup) {
                $find("IngredientModalPopupBehavior").show();

                document.getElementById('ctl00_MainContent_IngredientDropDownList').focus();
            }

            if (launchCondimentPopup) {
                $find("CondimentModalPopupBehavior").show();
            }

            if (launchDirectionPopup) {
                $find("DirectionModalPopupBehavior").show();

                document.getElementById('ctl00_MainContent_DirectionTextBox').focus();
            }

            if (launchRegularYieldPopup) {
                $find("RegularYieldPopupBehavior").show();
            }

            if (launchCookChillYieldPopup) {
                $find("CookChillYieldPopupBehavior").show();
            }
        }
    </script>
    <script type="text/javascript" language="JavaScript">

        function disableEnterKey(e) {
            var key;
            if (window.event)
                key = window.event.keyCode; //IE
            else
                key = e.which; //firefox      

            return (key != 13);
        }

        function updatecookchillyield() {
            var numberOfServingsPerPackage;
            var numberOfServingsPerBatch;
            var batchYieldInPounds;
            var recipeCostPerServing;

            var ingredientCostPerServingSum = document.getElementById('ctl00_MainContent_CookChillIngredientCostHiddenField').value;

            var oPackageType = document.all('ctl00_MainContent_PackageDropdownList');
            var packageType = oPackageType.options[oPackageType.selectedIndex].text;

            var oVolumeServingSize = document.all('ctl00_MainContent_VolumeServingSizeDropDownList');
            var servingSize = oVolumeServingSize.options[oVolumeServingSize.selectedIndex].value;

            var numberofPackagesPerBatch = document.getElementById('ctl00_MainContent_NumberofPackagesTextBox').value;

            var oWeightEquivalent = document.all('ctl00_MainContent_CookChillWeightDropDownList');
            var weightEquivalent = oWeightEquivalent.options[oWeightEquivalent.selectedIndex].value;

            if (numberofPackagesPerBatch != "" && weightEquivalent != "") {
                if (packageType == '1 gallon souper bag(s) (16c)') {
                    numberOfServingsPerPackage = 16 / servingSize;
                }
                else {
                    numberOfServingsPerPackage = 12 / servingSize;
                }
                
                // 09/16/2010 AJ -> Base number of servings on server per package without decimal places
                numberOfServingsPerBatch = numberofPackagesPerBatch * Math.floor(numberOfServingsPerPackage);

                numberOfServingsPerBatch = formatCurrency(numberOfServingsPerBatch);

                batchYieldInPounds =  numberOfServingsPerBatch * weightEquivalent / 16;

                batchYieldInPounds = formatCurrency(batchYieldInPounds);

                document.getElementById('ctl00_MainContent_ServingsPerPackageLabel').innerText = Math.round(numberOfServingsPerPackage);
                document.getElementById('ctl00_MainContent_ServingsPerBatchLabel').innerText = numberOfServingsPerBatch;
                document.getElementById('ctl00_MainContent_BatchYieldInPoundsLabel').innerText = batchYieldInPounds;

                recipeCostPerServing = ingredientCostPerServingSum / Math.floor(numberOfServingsPerBatch);

                document.getElementById('ctl00_MainContent_RecipeCostPerServingCookChillLabel').innerText = '$' + formatCurrency(recipeCostPerServing);
            }
        }

        function updateregularyield() {
            var recipeYield;
            var recipeCostPerServing;

            var numberofServings = document.getElementById('ctl00_MainContent_NumberOfServingsTextBox').value;

            var ingredientCostPerServingSum = document.getElementById('ctl00_MainContent_RegularIngredientCostHiddenField').value;

            var oDDL = document.all("ctl00_MainContent_ServingSizeTypeDropdownList");
            var servingSizeMeasurementType = oDDL.options[oDDL.selectedIndex].text;

            var oServingSize = document.all("ctl00_MainContent_RegularServingSizeDropDownList");
            var servingSize = oServingSize.options[oServingSize.selectedIndex].value;

            var oVolumeWeight = document.all("ctl00_MainContent_RegularVolumeDropDownList");
            var volumeWeight = oVolumeWeight.options[oVolumeWeight.selectedIndex].value;

            if (numberofServings != "") {
                if (servingSizeMeasurementType == 'ounce(s)') {
                    recipeYield = numberofServings * servingSize;
                }
                else {
                    recipeYield = numberofServings * volumeWeight;
                }

                recipeYield = recipeYield / 16;

                recipeYield = formatCurrency(recipeYield);

                recipeCostPerServing = ingredientCostPerServingSum / numberofServings;

                document.getElementById('ctl00_MainContent_RecipeYieldLabel').innerText = recipeYield;
                document.getElementById('ctl00_MainContent_RecipeCostPerServingLabel').innerText = '$' + formatCurrency(recipeCostPerServing);
            }
            else {
                document.getElementById('ctl00_MainContent_RecipeYieldLabel').innerText = '0';
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
            width: 50%;
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="asm" runat="server" />
    <asp:HiddenField ID="RecipeIDHiddenField" runat="server" />
    <table id="RecipeDetailsTable" style="width: 100%; height: 20px">
        <tr>
            <td align="center" colspan="4" style="height: 6%" valign="bottom">
                <asp:Button ID="SaveRecipeDetailButton" runat="server" OnClick="SaveRecipeDetailButton_Click"
                    Text="Save Recipe Changes" Font-Bold="True" /><asp:Button ID="SaveAsNewButton" runat="server"
                        OnClick="SaveAsNewButton_Click" Text="Save As New Recipe" Font-Bold="True" Enabled="False" /><asp:Button
                            ID="DeleteRecipeButton" runat="server" OnClick="DeleteRecipeButton_Click" Text="Delete Recipe"
                            Font-Bold="True" />
            </td>
        </tr>
        <tr>
            <td align="right" colspan="1" style="width: 25%; height: 6%" valign="bottom">
                <asp:Label ID="RecipeNameLabel" runat="server" Text="Recipe Name:"></asp:Label>
            </td>
            <td align="left" colspan="1" style="width: 25%; height: 7px" valign="bottom">
                <asp:TextBox ID="RecipeNameTextbox" runat="server" Width="220px"></asp:TextBox>
            </td>
            <td align="left" colspan="2" style="width: 50%;" rowspan="6">
                <asp:Panel ID="CondimentsGridViewPanel" ScrollBars="Both" Height="95%" Width="99%"
                    runat="server" HorizontalAlign="Center">
                    <asp:GridView ID="CondimentsGridView" runat="server" OnRowDataBound="CondimentsGridView_RowDataBound"
                        OnSelectedIndexChanging="CondimentsGridView_SelectedIndexChanging" Font-Size="XX-Small"
                        Height="95%" HorizontalAlign="Center" Width="99%" Caption="Condiments" CaptionAlign="Top">
                    </asp:GridView>
                    <cc1:ModalPopupExtender BehaviorID="CondimentModalPopupBehavior" ID="CondimentModalPopupExtender"
                        PopupControlID="CondimentPanel" runat="server" TargetControlID="CondimentsPopupButton"
                        OkControlID="CondimentCancelButton" DynamicServicePath="" Enabled="True">
                    </cc1:ModalPopupExtender>
                </asp:Panel>
                <asp:Button ID="AddNewCondimentButton" runat="server" Font-Bold="True" Text="Add New Condiment"
                    OnClick="AddNewCondimentButton_Click" Width="99%" />
            </td>
        </tr>
        <tr>
            <td align="right" colspan="1" style="width: 25%; height: 6%" valign="top">
                <asp:Label ID="MealComponentLabel" runat="server" Text="Recipe Type:"></asp:Label>
            </td>
            <td align="left" colspan="1" style="width: 25%; height: 6%" valign="top">
                <asp:DropDownList ID="RecipeTypeDropDownList" runat="server" Width="156px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="1" style="width: 25%; height: 6%" valign="top">
                <asp:Label ID="Label2" runat="server" Text="Recipe Serving Notes:"></asp:Label>
            </td>
            <td align="left" colspan="1" rowspan="2" style="width: 25%" valign="top">
                <asp:TextBox ID="RecipeNotesTextBox" runat="server" Height="32px" TextMode="MultiLine"
                    Width="229px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="1" style="width: 25%; height: 6%" valign="top">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="height: 6%" valign="bottom">
                <asp:RadioButton ID="RegularYieldRadioButton" runat="server" Checked="True" OnCheckedChanged="RegularYieldRadioButton_CheckedChanged"
                    Text="Regular Recipe" GroupName="YieldType" /><asp:RadioButton ID="CookChillYieldRadioButton"
                        runat="server" OnCheckedChanged="CookChillYieldRadioButton_CheckedChanged" Text="Cook Chill Recipe"
                        GroupName="YieldType" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="height: 6%" valign="top">
                <asp:Button ID="ModifyYieldDetailsButton" runat="server" Font-Bold="True" Text="View/Modify Yield Details"
                    Width="173px" OnClick="ModifyYieldDetailsButton_Click" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="width: 50%; height: 6%" valign="top">
            </td>
            <td align="left" colspan="2" rowspan="1" style="width: 50%; height: 6%">
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2" style="width: 50%; height: 2575%" valign="bottom">
                <asp:Panel ID="IngredientsGridViewPanel" runat="server" Height="95%" ScrollBars="Both"
                    Width="99%" HorizontalAlign="Center">
                    <asp:GridView ID="IngredientsGridView" runat="server" Font-Size="Small" HorizontalAlign="Center"
                        Width="99%" OnRowDataBound="IngredientsGridView_RowDataBound" OnSelectedIndexChanging="IngredientsGridView_SelectedIndexChanging"
                        Height="95%" Caption="Ingredients" CaptionAlign="Top">
                    </asp:GridView>
                </asp:Panel>
                <asp:Button ID="AddNewIngredientButton" runat="server" Font-Bold="True" OnClick="AddNewIngredientButton_Click"
                    Text="Add New Ingredient" Width="99%" Height="24px" />
            </td>
            <td align="left" colspan="2" style="width: 50%; height: 2575%" valign="bottom">
                <asp:Panel ID="DirectionsGridViewPanel" runat="server" Height="95%" ScrollBars="Both"
                    Width="99%" HorizontalAlign="Center">
                    <asp:GridView ID="DirectionsGridView" runat="server" Font-Size="Small" HorizontalAlign="Center"
                        Width="99%" OnRowDataBound="DirectionsGridView_RowDataBound" OnSelectedIndexChanging="DirectionsGridView_SelectedIndexChanging"
                        Height="95%" Caption="Directions" CaptionAlign="Top">
                    </asp:GridView>
                </asp:Panel>
                <asp:Button ID="AddNewDirectionButton" runat="server" Font-Bold="True" OnClick="AddNewDirectionButton_Click"
                    Text="Add New Direction" Width="99%" />
            </td>
        </tr>
    </table>
    <div>
        <asp:Panel ID="CondimentPanel" runat="server" Width="627px" BackColor="#00C7B3" BorderColor="Black"
            BorderStyle="Solid" BorderWidth="1px" Style="display: none;">
            <table width="100%">
                <tr>
                    <td align="right" style="height: 21px; width: 47%;">
                        <asp:Label ID="Label6" runat="server" Text="Condiments:"></asp:Label>
                    </td>
                    <td align="left" style="height: 21px; width: 355px;">
                        <asp:DropDownList ID="CondimentDropDownList" runat="server">
                        </asp:DropDownList>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right" style="height: 21px; width: 47%;">
                        <asp:Label ID="Label5" runat="server" Text="Per Serving:"></asp:Label>
                    </td>
                    <td align="left" style="height: 21px; width: 355px;">
                        <asp:TextBox ID="CondimentAmountTextBox" runat="server" Width="43px"></asp:TextBox><asp:DropDownList
                            ID="CondimentUnitDropDownList" runat="server">
                            <asp:ListItem>Packet(s)</asp:ListItem>
                            <asp:ListItem>Slice(s)</asp:ListItem>
                            <asp:ListItem>Ounce(s)</asp:ListItem>
                            <asp:ListItem>Piece(s)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 47%; height: 8px">
                        <asp:Label ID="Label8" runat="server" Text="Condiment Delivery Unit:"></asp:Label>
                    </td>
                    <td align="left" style="height: 8px; width: 355px;">
                        <asp:DropDownList ID="CondimentDeliveryUnitDropDownList" runat="server" OnSelectedIndexChanged="DeliveryPackageDropDownList_SelectedIndexChanged"
                            Width="173px">
                            <asp:ListItem>Pan</asp:ListItem>
                            <asp:ListItem>1/2 Pan</asp:ListItem>
                            <asp:ListItem>Bag</asp:ListItem>
                            <asp:ListItem>Box</asp:ListItem>
                            <asp:ListItem>Can</asp:ListItem>
                            <asp:ListItem>Bucket</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 47%; height: 7px">
                        <asp:Label ID="CondimentUnitLabel" runat="server" Text="Packet(s)"></asp:Label>
                        <asp:Label ID="Label3" runat="server" Text=" Per "></asp:Label>
                        <asp:Label ID="CondimentDeliveryUnitLabel" runat="server" Text="Pan:"></asp:Label>
                    </td>
                    <td align="left" style="width: 355px; height: 7px">
                        <asp:TextBox ID="CondimentUnitPerDeliveryUnitTextBox" runat="server" Width="52px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 11px" align="left" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td style="height: 21px" align="center" colspan="2">
                        <asp:Button ID="SaveAddCondimentButton" runat="server" Text="Save And Add Another Condiment"
                            OnClick="SaveAddCondimentChangesButton_Click" Font-Bold="True" Width="237px" />
                        <asp:Button ID="SaveCondimentButton" runat="server" Text="Save Condiment" OnClick="SaveCondimentChangesButton_Click"
                            Font-Bold="True" />
                        <asp:Button ID="DeleteCondimentButton" runat="server" Height="24px" Text="Delete Condiment"
                            Width="146px" OnClick="DeleteCondimentButton_Click" Font-Bold="True" />
                        <asp:Button ID="CondimentCancelButton" runat="server" Font-Bold="True" Text="Cancel"
                            OnClick="CondimentCancelButton_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        &nbsp;</div>
    <asp:Panel ID="IngredientPanel" runat="server" Width="617px" BackColor="#00C7B3"
        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right">
        <table width="100%">
            <tr>
                <td align="right" style="width: 50%; height: 21px">
                    <asp:Label ID="IngredientNumberLabel" runat="server" Font-Size="Medium" Text="Ingredient Number:"></asp:Label>
                </td>
                <td align="left" colspan="2" style="width: 50%; height: 21px">
                    <asp:TextBox ID="IngredientNumberTextBox" runat="server" Width="57px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 21px; width: 50%;">
                    <asp:Label ID="IngredientNameLabel" runat="server" Text="Ingredient Name:" Font-Size="Medium"></asp:Label>
                </td>
                <td align="left" colspan="2" style="height: 21px; width: 50%;">
                    <asp:DropDownList ID="IngredientDropDownList" runat="server" Width="98%" OnSelectedIndexChanged="IngredientDropDownList_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 21px; width: 50%;">
                    <asp:Label ID="IngredientAmountLabel" runat="server" Text="Ingredient Amount:" Font-Size="Medium"
                        Width="99%"></asp:Label>
                </td>
                <td align="left" style="width: 10%; height: 21px;">
                    <asp:TextBox ID="IngredientMeasureTextBox" runat="server" Width="99%"></asp:TextBox>
                </td>
                <td style="width: 40%; height: 21px" align="left">
                    &nbsp;<asp:Label ID="IngredientRecipeUnitLabel" runat="server" Width="225px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 21px">
                    Prep Info:
                </td>
                <td align="left" colspan="2" style="height: 21px">
                    <asp:TextBox ID="PrepInfoTextBox" runat="server" Width="303px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3" style="height: 23px">
                    <asp:Label ID="NotesLabel" runat="server" Text="Notes:" Font-Size="Medium"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 16px" align="left" colspan="3">
                    <asp:TextBox ID="IngredientNoteTextBox" runat="server" Rows="2" TextMode="MultiLine"
                        Width="98%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 16px" align="center" colspan="3" valign="top">
                    <asp:Button ID="SaveAddIngredientButton" runat="server" Text="Save And Add Another Ingredient"
                        OnClick="SaveAddIngredientChangesButton_Click" Font-Bold="True" Width="251px" />
                    <asp:Button ID="SaveIngredientChangesButton" runat="server" Text="Save Ingredient"
                        OnClick="SaveIngredientChangesButton_Click" Font-Bold="True" />
                    <asp:Button ID="DeleteIngredientButton" runat="server" Text="Delete Ingredient" Width="146px"
                        OnClick="DeleteIngredientButton_Click" Font-Bold="True" />
                    <asp:Button ID="IngredientCancelButton" runat="server" Font-Bold="True" Text="Cancel"
                        OnClick="IngredientCancelButton_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="DirectionPanel" runat="server" BorderColor="Black" BorderStyle="Solid"
        BorderWidth="1px" Width="511px" BackColor="#00C7B3" Style="display: none;">
        <table id="DirectionsTable" width="100%">
            <tr>
                <td align="right" style="width: 50%; height: 6px;">
                    <asp:Label ID="DirectionNumberLabel" runat="server" Text="Direction Step Number:"></asp:Label>
                </td>
                <td align="left" style="width: 51%; height: 6px;">
                    <asp:TextBox ID="DirectionStepNumberTextBox" runat="server" Width="10%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="width: 100%; height: 21px">
                    <asp:Label ID="DirectionStepLabel" runat="server" Text="Enter Direction Below"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="width: 100%; height: 41px" valign="top">
                    <asp:TextBox ID="DirectionTextBox" runat="server" TextMode="MultiLine" Width="98%"
                        Height="38px"></asp:TextBox>&nbsp;
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="width: 100%; height: 26px">
                    <asp:Button ID="SaveAddDirectionButton" runat="server" Text="Save And Add Another Direction"
                        OnClick="SaveAddDirectionChangesButton_Click" Font-Bold="True" Width="223px" />
                    <asp:Button ID="SaveDirection" runat="server" OnClick="SaveDirection_Click" Text="Save Direction"
                        Font-Bold="True" />
                    <asp:Button ID="DeleteDirection" runat="server" Text="Delete Direction" OnClick="DeleteDirection_Click"
                        Font-Bold="True" />
                    <asp:Button ID="DirectionCancelButton" runat="server" Font-Bold="True" Text="Cancel"
                        OnClick="DirectionCancelButton_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="RegularYieldPanel" runat="server" BorderColor="Black" BorderStyle="Solid"
        BorderWidth="1px" Width="714px" BackColor="#00C7B3">
        <table id="Table1" width="100%">
            <tr>
                <td align="right" style="width: 50%; height: 26px;">
                    <asp:Label ID="Label10" runat="server" Text="Number of Servings:"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 26px;">
                    <asp:TextBox ID="NumberOfServingsTextBox" runat="server" Width="67px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 15px">
                    <asp:Label ID="Label11" runat="server" Text="Serving Size:"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 15px">
                    <asp:DropDownList ID="RegularServingSizeDropDownList" runat="server" Width="120px">
                        <asp:ListItem Value="0.50">1/2</asp:ListItem>
                        <asp:ListItem Value="1.00">1</asp:ListItem>
                        <asp:ListItem Value="1.50">1 1/2</asp:ListItem>
                        <asp:ListItem Value="2.00">2</asp:ListItem>
                        <asp:ListItem Value="2.50">2 1/2</asp:ListItem>
                        <asp:ListItem Value="3.00">3</asp:ListItem>
                        <asp:ListItem Value="3.50">3 1/2</asp:ListItem>
                        <asp:ListItem Value="4.00">4</asp:ListItem>
                        <asp:ListItem Value="4.50">4 1/2</asp:ListItem>
                        <asp:ListItem Value="5.00">5</asp:ListItem>
                        <asp:ListItem Value="5.50">5 1/2</asp:ListItem>
                        <asp:ListItem Value="6.00">6</asp:ListItem>
                        <asp:ListItem Value="6.50">6 1/2</asp:ListItem>
                        <asp:ListItem Value="7.00">7</asp:ListItem>
                        <asp:ListItem Value="7.50">7 1/2</asp:ListItem>
                        <asp:ListItem Value="8.00">8</asp:ListItem>
                        <asp:ListItem Value="8.50">8 1/2</asp:ListItem>
                        <asp:ListItem Value="9.00">9</asp:ListItem>
                        <asp:ListItem Value="9.50">9 1/2</asp:ListItem>
                        <asp:ListItem Value="10.00">10</asp:ListItem>
                        <asp:ListItem Value="10.50">10 1/2</asp:ListItem>
                        <asp:ListItem Value="11.00">11</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ServingSizeTypeDropdownList" runat="server" Width="143px" AutoPostBack="true"
                        OnSelectedIndexChanged="ServingSizeTypeDropdownList_SelectedIndexChanged">
                        <asp:ListItem Value="1">ounce(s)</asp:ListItem>
                        <asp:ListItem Value="2">piece(s)</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 21px">
                    <asp:Label ID="VolumeWeightLabel" runat="server">Volume Equivalent:</asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 21px">
                    <asp:DropDownList ID="RegularVolumeDropDownList" runat="server" Width="120px">
                        <asp:ListItem Value="0.25">1/4</asp:ListItem>
                        <asp:ListItem Value="0.75">3/4</asp:ListItem>
                        <asp:ListItem Value="0.50">1/2</asp:ListItem>
                        <asp:ListItem Value="1.00">1</asp:ListItem>
                        <asp:ListItem Value="1.25">1 1/4</asp:ListItem>
                        <asp:ListItem Value="1.50">1 1/2</asp:ListItem>
                        <asp:ListItem Value="1.75">1 3/4</asp:ListItem>
                        <asp:ListItem Value="2.00">2</asp:ListItem>
                        <asp:ListItem Value="2.25">2 1/4</asp:ListItem>
                        <asp:ListItem Value="2.50">2 1/2</asp:ListItem>
                        <asp:ListItem Value="2.75">2 3/4</asp:ListItem>
                        <asp:ListItem Value="3.00">3</asp:ListItem>
                        <asp:ListItem Value="3.25">3 1/4</asp:ListItem>
                        <asp:ListItem Value="3.50">3 1/2</asp:ListItem>
                        <asp:ListItem Value="3.75">3 3/4</asp:ListItem>
                        <asp:ListItem Value="4.00">4</asp:ListItem>
                        <asp:ListItem Value="4.25">4 1/4</asp:ListItem>
                        <asp:ListItem Value="4.50">4 1/2</asp:ListItem>
                        <asp:ListItem Value="4.75">4 3/4</asp:ListItem>
                        <asp:ListItem Value="5.00">5</asp:ListItem>
                        <asp:ListItem Value="5.25">5 1/4</asp:ListItem>
                        <asp:ListItem Value="5.50">5 1/2</asp:ListItem>
                        <asp:ListItem Value="5.75">5 3/4</asp:ListItem>
                        <asp:ListItem Value="6.00">6</asp:ListItem>
                        <asp:ListItem Value="6.25">6 1/4</asp:ListItem>
                        <asp:ListItem Value="6.50">6 1/2</asp:ListItem>
                        <asp:ListItem Value="6.75">6 3/4</asp:ListItem>
                        <asp:ListItem Value="7.00">7</asp:ListItem>
                        <asp:ListItem Value="7.25">7 1/4</asp:ListItem>
                        <asp:ListItem Value="7.50">7 1/2</asp:ListItem>
                        <asp:ListItem Value="7.75">7 3/4</asp:ListItem>
                        <asp:ListItem Value="8.00">8</asp:ListItem>
                        <asp:ListItem Value="8.25">8 1/4</asp:ListItem>
                        <asp:ListItem Value="8.50">8 1/2</asp:ListItem>
                        <asp:ListItem Value="8.75">8 3/4</asp:ListItem>
                        <asp:ListItem Value="9.00">9</asp:ListItem>
                        <asp:ListItem Value="9.25">9 1/4</asp:ListItem>
                        <asp:ListItem Value="9.50">9 1/2</asp:ListItem>
                        <asp:ListItem Value="9.75">9 3/4</asp:ListItem>
                        <asp:ListItem Value="10.00">10</asp:ListItem>
                        <asp:ListItem Value="10.25">10 1/4</asp:ListItem>
                        <asp:ListItem Value="10.50">10 1/2</asp:ListItem>
                        <asp:ListItem Value="10.75">10 3/4</asp:ListItem>
                        <asp:ListItem Value="11.00">11</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="VolumeWeightTypeLabel" runat="server">Cup(s)</asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 21px">
                    <asp:Label ID="RecipeYield" runat="server" Font-Bold="True" Text="Original Recipe Yield In Pounds:"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 21px">
                    <asp:Label ID="RecipeYieldLabel" runat="server" Font-Bold="True" ForeColor="Black"
                        Width="81px">0</asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 21px">
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Text="Delivery Package:"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 21px">
                    <asp:DropDownList ID="DeliveryPackageDropDownList" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="DeliveryPackageDropDownList_SelectedIndexChanged" Width="173px">
                        <asp:ListItem Value="1">Pan(s)</asp:ListItem>
                        <asp:ListItem Value="2">1/2 Pan(s)</asp:ListItem>
                        <asp:ListItem Value="3">Bag(s)</asp:ListItem>
                        <asp:ListItem Value="4">Box(es)</asp:ListItem>
                        <asp:ListItem Value="5">Can(s)</asp:ListItem>
                        <asp:ListItem Value="6">Bucket(s)</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 20px">
                    <asp:Label ID="PerPanLabel" runat="server" Font-Bold="False" Text="# of Pounds Per  "></asp:Label>
                    <asp:Label ID="DeliveryPackageTypeLabel" runat="server" Font-Bold="False" Text="Pan:"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 20px">
                    <asp:TextBox ID="NumberServingsPerPackagesTextBox" runat="server" Width="53px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 20px">
                    Recipe Cost Per Serving:
                </td>
                <td align="left" style="width: 50%; height: 20px">
                    <asp:Label ID="RecipeCostPerServingLabel" runat="server" ForeColor="Black" Width="81px">0</asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="width: 100%; height: 2px" rowspan="1">
                    <asp:HiddenField ID="RegularIngredientCostHiddenField" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="width: 100%; height: 24px">
                    <asp:Button ID="SaveRegularYieldDetailButton" runat="server" Font-Bold="True" OnClick="SaveRegularYieldDetailButton_Click"
                        Text="Save Yield Details" />
                    <asp:Button ID="CancelRegularYieldDetailButton" runat="server" Font-Bold="True" OnClick="CancelRegularYieldDetailButton_Click"
                        Text="Cancel" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="CookChillYieldPanel" runat="server" BorderColor="Black" BorderStyle="Solid"
        BorderWidth="1px" Width="714px" BackColor="#00C7B3">
        <table id="Table2" width="100%">
            <tr>
                <td align="right" style="width: 50%; height: 26px;">
                    <asp:Label ID="Label12" runat="server" Text="Package:"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 26px;">
                    <asp:DropDownList ID="PackageDropdownList" runat="server" Width="188px" AutoPostBack="true"
                        OnSelectedIndexChanged="PackageDropdownList_SelectedIndexChanged">
                        <asp:ListItem Value="7">1 gallon souper bag(s) (16c)</asp:ListItem>
                        <asp:ListItem Value="8">3/4 gallon souper bag(s) (12c)</asp:ListItem>
                        <asp:ListItem Value="9">meat bag(s)</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 19px">
                    <asp:Label ID="Label18" runat="server">Number of </asp:Label>
                    <asp:Label ID="PackageTypeLabel" runat="server">1 gallon souper bag(s) (16c)</asp:Label>
                    <asp:Label ID="Label13" runat="server">Per Batch:</asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 19px">
                    <asp:TextBox ID="NumberofPackagesTextBox" runat="server" Width="67px" OnTextChanged="NumberofPackagesTextBox_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 22px">
                    <asp:Label ID="Label14" runat="server">Volume Serving Size:</asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 22px">
                    <asp:DropDownList ID="VolumeServingSizeDropDownList" runat="server" Width="108px"
                        AutoPostBack="True">
                        <asp:ListItem Value="0.50">1/2 Cup</asp:ListItem>
                        <asp:ListItem Value="0.75">3/4 Cup</asp:ListItem>
                        <asp:ListItem Value="1.00">1 Cup</asp:ListItem>
                        <asp:ListItem Value="1.50">1 1/2 Cup</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 22px">
                    <asp:Label ID="Label16" runat="server" Font-Bold="False" Text="Weight Equivalent:"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 22px">
                    <asp:DropDownList ID="CookChillWeightDropDownList" runat="server" Width="120px">
                        <asp:ListItem Value="0.25">1/4</asp:ListItem>
                        <asp:ListItem Value="0.75">3/4</asp:ListItem>
                        <asp:ListItem Value="0.50">1/2</asp:ListItem>
                        <asp:ListItem Value="1.00">1</asp:ListItem>
                        <asp:ListItem Value="1.25">1 1/4</asp:ListItem>
                        <asp:ListItem Value="1.50">1 1/2</asp:ListItem>
                        <asp:ListItem Value="1.75">1 3/4</asp:ListItem>
                        <asp:ListItem Value="2.00">2</asp:ListItem>
                        <asp:ListItem Value="2.25">2 1/4</asp:ListItem>
                        <asp:ListItem Value="2.50">2 1/2</asp:ListItem>
                        <asp:ListItem Value="2.75">2 3/4</asp:ListItem>
                        <asp:ListItem Value="3.00">3</asp:ListItem>
                        <asp:ListItem Value="3.25">3 1/4</asp:ListItem>
                        <asp:ListItem Value="3.50">3 1/2</asp:ListItem>
                        <asp:ListItem Value="3.75">3 3/4</asp:ListItem>
                        <asp:ListItem Value="4.00">4</asp:ListItem>
                        <asp:ListItem Value="4.25">4 1/4</asp:ListItem>
                        <asp:ListItem Value="4.50">4 1/2</asp:ListItem>
                        <asp:ListItem Value="4.75">4 3/4</asp:ListItem>
                        <asp:ListItem Value="5.00">5</asp:ListItem>
                        <asp:ListItem Value="5.25">5 1/4</asp:ListItem>
                        <asp:ListItem Value="5.50">5 1/2</asp:ListItem>
                        <asp:ListItem Value="5.75">5 3/4</asp:ListItem>
                        <asp:ListItem Value="6.00">6</asp:ListItem>
                        <asp:ListItem Value="6.25">6 1/4</asp:ListItem>
                        <asp:ListItem Value="6.50">6 1/2</asp:ListItem>
                        <asp:ListItem Value="6.75">6 3/4</asp:ListItem>
                        <asp:ListItem Value="7.00">7</asp:ListItem>
                        <asp:ListItem Value="7.25">7 1/4</asp:ListItem>
                        <asp:ListItem Value="7.50">7 1/2</asp:ListItem>
                        <asp:ListItem Value="7.75">7 3/4</asp:ListItem>
                        <asp:ListItem Value="8.00">8</asp:ListItem>
                        <asp:ListItem Value="8.25">8 1/4</asp:ListItem>
                        <asp:ListItem Value="8.50">8 1/2</asp:ListItem>
                        <asp:ListItem Value="8.75">8 3/4</asp:ListItem>
                        <asp:ListItem Value="9.00">9</asp:ListItem>
                        <asp:ListItem Value="9.25">9 1/4</asp:ListItem>
                        <asp:ListItem Value="9.50">9 1/2</asp:ListItem>
                        <asp:ListItem Value="9.75">9 3/4</asp:ListItem>
                        <asp:ListItem Value="10.00">10</asp:ListItem>
                        <asp:ListItem Value="10.25">10 1/4</asp:ListItem>
                        <asp:ListItem Value="10.50">10 1/2</asp:ListItem>
                        <asp:ListItem Value="10.75">10 3/4</asp:ListItem>
                        <asp:ListItem Value="11.00">11</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Label15" runat="server" Text="ounce(s)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 21px">
                    <asp:Label ID="Label17" runat="server" Text="Number of Servings Per Package:"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 21px">
                    <asp:Label ID="ServingsPerPackageLabel" runat="server" Width="31px" ForeColor="Black">0</asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 21px">
                    <asp:Label ID="Label19" runat="server" Text="Number of Servings (per batch):"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 21px">
                    <asp:Label ID="ServingsPerBatchLabel" runat="server" Width="63px" ForeColor="Black">0</asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 21px">
                    <asp:Label ID="Label20" runat="server" Text="Batch Yield In Pounds:"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 21px">
                    <asp:Label ID="BatchYieldInPoundsLabel" runat="server" Width="69px" ForeColor="Black">0</asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    Recipe Cost Per Serving:
                </td>
                <td align="left" class="style1">
                    <asp:Label ID="RecipeCostPerServingCookChillLabel" runat="server" ForeColor="Black"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="width: 100%; height: 2px" rowspan="1">
                    <asp:HiddenField ID="CookChillIngredientCostHiddenField" runat="server"/>
                    <asp:Label ID="RecipeCost" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="width: 100%; height: 24px">
                    <asp:Button ID="SaveCookChillYieldDetailButton" runat="server" Font-Bold="True" OnClick="SaveCookChillYieldDetailButton_Click"
                        Text="Save Yield Details" />
                    <asp:Button ID="CancelCookChillYieldDetailButton" runat="server" Font-Bold="True"
                        OnClick="CancelCookChillYieldDetailButton_Click" Text="Cancel" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cc1:ModalPopupExtender BehaviorID="IngredientModalPopupBehavior" ID="IngredientModalPopupExtender"
        PopupControlID="IngredientPanel" runat="server" TargetControlID="IngredientsPopupButton"
        OkControlID="IngredientCancelButton" X="400" Y="200">
    </cc1:ModalPopupExtender>
    <asp:Button ID="IngredientsPopupButton" runat="server" Text="Button" Style="display: none;" />
    <asp:Button ID="RegularYieldPopupButton" runat="server" Text="Button" Style="display: none;" />
    <asp:Button ID="CookChillYieldPopupButton" runat="server" Text="Button" Style="display: none;" />
    <asp:Button ID="DirectionPopupButton" runat="server" Text="Button" Style="display: none;" />
    <asp:Button ID="CondimentsPopupButton" runat="server" Text="Button" Style="display: none;" />
    <cc1:ModalPopupExtender BehaviorID="DirectionModalPopupBehavior" ID="DirectionModalPopupExtender"
        PopupControlID="DirectionPanel" runat="server" TargetControlID="DirectionPopupButton"
        OkControlID="DirectionCancelButton" X="0" Y="200">
    </cc1:ModalPopupExtender>
    <cc1:ModalPopupExtender BehaviorID="RegularYieldPopupBehavior" ID="RegularYieldPopupExtender"
        PopupControlID="RegularYieldPanel" runat="server" TargetControlID="RegularYieldPopupButton"
        OkControlID="CancelRegularYieldDetailButton">
    </cc1:ModalPopupExtender>
    <cc1:ModalPopupExtender BehaviorID="CookChillYieldPopupBehavior" ID="CookChillYieldPopupExtender"
        PopupControlID="CookChillYieldPanel" runat="server" TargetControlID="CookChillYieldPopupButton"
        OkControlID="CancelCookChillYieldDetailButton">
    </cc1:ModalPopupExtender>
</asp:Content>
