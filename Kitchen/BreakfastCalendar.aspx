<%@ Page Language="C#" AutoEventWireup="true" Title="CCK Breakfast Planner" MasterPageFile="~/Site.master" EnableEventValidation="false" CodeFile="BreakfastCalendar.aspx.cs"
    Inherits="BreakfastCalendar" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="GCFDGlobalsNamespace" %>
<%@ MasterType TypeName="SiteMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <style type="text/css">
        .modalBackground
        {

        }
        .style1
        {
            width: 212px;
            height: 10px;
        }
        .style2
        {
            width: 687px;
            height: 10px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('._title').parent().attr('colspan', '5'); // title row initially has a colspan of seven
            $('._dayheader:first, ._dayheader:last', $('#<%= DeliveryCalendarControl.ClientID %>')).hide(); // remove first and last cells from day header row
            $('._weekendday').hide(); // remove all the cells marked weekends
        });
    </script>
    <script type="text/javascript">

        var launch = false;
        var launchCookChill = false;
        var launchCookChillYieldPopup = false;
        var launchRegularYieldPopup = false;

        function launchMealDetailModal() {
            launch = true;
        }

        function launchCookChillModal() {
            launchCookChill = true;
        }

        function fnClickOK(sender, e) {
            __doPostBack(sender, e);
        }

        function fnOpenMealDetail(sender, e) {
            $find("mpe").show();
        }
        
        function launchRegularYieldPopupModal() {            
            launchRegularYieldPopup = true;
        }

        function launchCookChillYieldPopupModal() {
            launchCookChillYieldPopup = true;
        }

        function pageLoad() {
            if (launch) {
                $find("mpe").show();
            }

            if (launchRegularYieldPopup) {
                $find("RegularYieldPopupExtender").show();
            }

            if (launchCookChillYieldPopup) {
                $find("CookChillYieldPopupExtender").show();
            }

            if (launchCookChill) {
                $find("CookChillPopupExtender").show();
            }
        }     
    </script>
    <script language="C#" runat="server">
        void CalendarDateClick(Object source, EventArgs e)
        {
            MealDetailForNewMeal(DeliveryCalendarControl.SelectedDate.ToString("MM/dd/yyyy"), "Scheduled");
        }
        
    </script>
    <script type="text/javascript" language="JavaScript">
        function updatecookchillyield() {
            var cca = document.form1.elements['PackageTypeTextBox'].value;

            var oVolumeServingSize = document.all('VolumeServingSizeDropDownList');
            var ccc = oVolumeServingSize.options[oVolumeServingSize.selectedIndex].value;

            var ccb = document.form1.elements['NumberofPackageTypeTextBox'].value;

            var oWeightEquivalent = document.all('CookChillWeightDropDownList');
            var ccd = oWeightEquivalent.options[oWeightEquivalent.selectedIndex].value;

            var RecipeYield;
            var ConversionFactor;
            var OriginalYield = document.form1.elements['RecipeOriginalCCYieldHiddenField'].value;
            var OriginalVolume = document.form1.elements['OriginalCCVolumeHiddenField'].value;
            var OriginalServingSize = document.form1.elements['OriginalCCServingSizeHiddenField'].value;

            var cce;
            var ccf;
            var ccg;

            if (ccc != OriginalServingSize) {
                ccd = ccc / OriginalServingSize * OriginalVolume;

                ccd = Math.round(ccd * 4) / 4;

                for (var i = 0; i <= document.getElementById("CookChillWeightDropDownList").length - 1; i = i + 1) {
                    var e = document.getElementById("CookChillWeightDropDownList");
                    var ddlText = e.options[i].value;

                    if (ddlText == ccd.toFixed(2)) {
                        document.getElementById("CookChillWeightDropDownList").selectedIndex = i;

                        break;
                    }
                }
            }
            else {
                ccc = ccd / OriginalVolume * OriginalServingSize;

                ccc = Math.round(ccc * 2) / 2;

                for (var i = 0; i <= document.getElementById("VolumeServingSizeDropDownList").length - 1; i = i + 1) {
                    var e = document.getElementById("VolumeServingSizeDropDownList");
                    var ddlText = e.options[i].value;

                    if (ddlText == ccc) {
                        document.getElementById("VolumeServingSizeDropDownList").selectedIndex = i;

                        break;
                    }
                }
            }

            if (ccb != "" && ccd != "") {
                if (cca == '1 gallon souper bag (16c)') {
                    cce = 16 / ccc;
                }
                else {
                    cce = 12 / ccc;
                }

                ccf = ccb * cce;

                ccf = Math.round(ccf * Math.pow(10, 2)) / Math.pow(10, 2);

                ccg = ccf * ccd / 16;

                ccg = Math.round(ccg * Math.pow(10, 2)) / Math.pow(10, 2);

                document.form1.elements['ServingsPerPackageTextBox'].value = cce;
                document.form1.elements['ServingsPerBatchTextBox'].value = ccf;
                document.form1.elements['BatchYieldInPoundsTextBox'].value = ccg;

                if (ccg == OriginalYield) {
                    document.form1.elements['CookChillConversionFactorTextBox'].value = 'N/A';
                }
                else {
                    ConversionFactor = ccg / OriginalYield;

                    document.form1.elements['CookChillConversionFactorTextBox'].value = ConversionFactor;
                }
            }
            else {
                document.form1.elements['CookChillConversionFactorTextBox'].value = 'N/A';
            }
        }

        function updateregularyield() {
            var oDDL = document.all("ServingSizeTypeDropdownList");
            var curText = oDDL.options[oDDL.selectedIndex].text;

            var NumberofServings = document.form1.elements['RecipeNumberofServings'].value;

            var oServingSize = document.all("RegularServingSizeDropDownList");
            var ServingSize = oServingSize.options[oServingSize.selectedIndex].value;

            var oVolumeWeight = document.all("RegularVolumeDropDownList");
            var VolumeWeight = oVolumeWeight.options[oVolumeWeight.selectedIndex].value;

            var RecipeYield;
            var ConversionFactor;
            var OriginalYield = document.form1.elements['RecipeOriginalYieldHiddenField'].value;
            var OriginalVolume = document.form1.elements['OriginalVolumeHiddenField'].value;
            var OriginalServingSize = document.form1.elements['OriginalServingSizeHiddenField'].value;

            if (ServingSize != OriginalServingSize) {
                VolumeWeight = ServingSize / OriginalServingSize * OriginalVolume;

                VolumeWeight = Math.round(VolumeWeight * 4) / 4;

                for (var i = 0; i <= document.getElementById("RegularVolumeDropDownList").length - 1; i = i + 1) {
                    var e = document.getElementById("RegularVolumeDropDownList");
                    var ddlText = e.options[i].value;

                    if (ddlText == VolumeWeight.toFixed(2)) {
                        document.getElementById("RegularVolumeDropDownList").selectedIndex = i;

                        break;
                    }
                }
            }
            else {
                ServingSize = VolumeWeight / OriginalVolume * OriginalServingSize;

                ServingSize = Math.round(ServingSize * 2) / 2;

                for (var i = 0; i <= document.getElementById("RegularServingSizeDropDownList").length - 1; i = i + 1) {
                    var e = document.getElementById("RegularServingSizeDropDownList");
                    var ddlText = e.options[i].value;

                    if (ddlText == ServingSize) {
                        document.getElementById("RegularServingSizeDropDownList").selectedIndex = i;

                        break;
                    }
                }
            }

            if (document.form1.elements['RecipeNumberofServings'].value != "") {
                if (curText == 'ounce(s)') {
                    RecipeYield = NumberofServings * ServingSize;
                }
                else {
                    RecipeYield = NumberofServings * VolumeWeight;
                }

                RecipeYield = RecipeYield / 16;

                RecipeYield = Math.round(RecipeYield * Math.pow(10, 2)) / Math.pow(10, 2);

                if (RecipeYield == OriginalYield) {
                    document.form1.elements['ConversionFactorTextBox'].value = 'N/A';
                }
                else {
                    ConversionFactor = RecipeYield / OriginalYield;

                    document.form1.elements['ConversionFactorTextBox'].value = ConversionFactor;
                }
            }
            else {
                document.form1.elements['ConversionFactorTextBox'].value = 'N/A';
            }
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="asm" runat="server" EnablePartialRendering="true" />
    <asp:Panel ID="CalendarPanel" runat="server" Width="100%">
        <table width="100%">
        <tr>
                <td align="center" style="width: 100%;">
                    <asp:DropDownList ID="drpCalMonth" runat="Server" OnSelectedIndexChanged="drpCalMonth_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:DropDownList ID="drpCalYear" runat="Server" OnSelectedIndexChanged="drpCalYear_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr> 
            <tr>
                <td rowspan="3" style="height: 486px; width: 100%;">
                    <asp:Calendar ID="DeliveryCalendarControl" runat="server" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px" FirstDayOfWeek="Sunday" Height="100%" OnDayRender="MenuDeliveryCalendarControl_DayRender"
                        OnSelectionChanged="CalendarDateClick" OnVisibleMonthChanged="MenuDeliveryCalendarControl_VisibleMonthChanged"
                        ShowGridLines="True" UseAccessibleHeader="False" Width="100%" 
                        NextMonthText="&gt;" NextPrevFormat="ShortMonth" PrevMonthText="&lt;" >
                        <DayStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                        ForeColor="Black" Font-Bold="True"  />
                        <NextPrevStyle Font-Bold="True" ForeColor="#004D45" />
                        <OtherMonthDayStyle BackColor="#E0E0E0" ForeColor="Black" />
                        <TitleStyle Font-Bold="True" ForeColor="#004D45" />
                    </asp:Calendar>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="MenuComponentsPanel" runat="server" BackColor="#00C7B3" Width="900px" CssClass="modalBackground" ForeColor="Black">
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <table style="width: 899px">
                    <tr>
                        <td align="right" style="height: 11px; width: 446px;" valign="bottom">
                            <asp:Label ID="MenuDayLabelLabel" runat="server" Font-Bold="True" Text="Date of Breakfast:"
                                Width="231px"></asp:Label>
                        </td>
                        <td align="left" style="width: 50%; height: 18px" valign="bottom">
                            <asp:Label ID="MealDateLabel" runat="server" Width="22%" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="bottom" style="width: 446px; height: 11px">
                            <asp:Label ID="TotalMealCountLabel" runat="server" Font-Bold="True" 
                                Text="Total Regular Breakfast Count For This Date:"></asp:Label>
                        </td>
                        <td align="left" style="width: 50%; height: 18px;" valign="bottom">
                            <asp:Label ID="TotalMealCountForDateLabel" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="height: 11px" valign="bottom">
                            <asp:Button ID="Button1" ControlID="Button1" runat="server" Text="->" CausesValidation="False"
                                UseSubmitBehavior="False" OnClick="Button1_Click" />
                            <asp:Button ID="Button2" ControlID="Button2"
                                    runat="server" Text="<-" OnClick="Button2_Click" Width="25px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 446px; height: 15px" valign="bottom">
                            <div id="Div1" style="overflow: auto; width: 85%; height: 86px">
                                <asp:ListBox ID="lstMain" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </td>
                        <td align="left" style="width: 50%; height: 15px" valign="bottom">
                            <div id="lstSelectDiv" style="overflow: auto; width: 85%; height: 86px">
                                <asp:ListBox ID="lstSelect" runat="server" AppendDataBoundItems="True" 
                                    SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 446px; height: 15px" valign="bottom">
                            Site(s) Above <b>Will Not</b> Receive This Meal</td>
                        <td align="center" style="width: 50%; height: 15px" valign="bottom">
                            Site(s) Above <b>Will</b> Receive This Meal</td>
                    </tr>
                    <tr>
                        <td align="right" style="height: 18px; width: 446px;" valign="bottom">
                            <asp:Label ID="Label5" runat="server" Text="Number of Kids This Breakfast:"></asp:Label>
                        </td>
                        <td align="left" style="width: 50%; height: 18px" valign="bottom">
                            <asp:Label ID="CurrentMealCountLabel" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height: 21px; width: 446px;" valign="bottom">
                            <asp:Label ID="Label21" runat="server" 
                                Text="Rounded Production Number This Breakfast:"></asp:Label>
                        </td>
                        <td align="left" style="width: 50%; height: 21px" valign="bottom">
                            <asp:Label ID="RoundedCurrentMealCountLabel" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="right" colspan="1" style="height: 11px; width: 212px;" valign="bottom">
                            <asp:Label ID="Label97" runat="server" Text="Juice/Fruit/Vegetable:"></asp:Label>
                        </td>
                        <td align="left" style="height: 11px; width: 687px;" valign="top">
                            <asp:DropDownList ID="VegetableDropDownList" runat="server" Width="190px" AutoPostBack="True"
                                OnSelectedIndexChanged="VegetableDropDownList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="Label96" runat="server" Text="Serving Size:"></asp:Label>
                            <asp:Label ID="VegetableServingSizeLabel" runat="server" Text="0 " Width="52px"></asp:Label><asp:Label
                                ID="VegetableServingSizeTypeLabel" runat="server" Text="ounce(s)" Width="60px"></asp:Label>
                                <asp:Button
                                    ID="ViewVegetableYieldDetailButton" runat="server" 
                                Text="View/Modify Yield/Notes" Width="172px"
                                    Font-Bold="True" OnClick="ViewVegetableYieldDetailButton_Click"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="1" style="height: 9px; width: 212px;" valign="bottom">
                            Bread/Grain/Frozen Pastry:
                        </td>
                        <td align="left" style="height: 9px; width: 687px;" valign="top">
                            <asp:DropDownList ID="BreadDropDownList" runat="server" Width="190px" AutoPostBack="True"
                                OnSelectedIndexChanged="BreadDropDownList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="Label95" runat="server" Text="Serving Size:"></asp:Label>
                            <asp:Label ID="BreadServingSizeLabel" runat="server" Text="0 " Width="52px"></asp:Label>
                            <asp:Label
                                ID="BreadServingSizeTypeLabel" runat="server" Text="ounce(s)" Width="60px"></asp:Label>
                            <asp:Button
                                    ID="ViewBreadYieldDetailButton" runat="server" 
                                Text="View/Modify Yield/Notes" Width="172px"
                                    Font-Bold="True" OnClick="ViewBreadYieldDetailButton_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="1" style="height: 11px; width: 212px;" valign="bottom">
                            Other:
                        </td>
                        <td align="left" style="height: 11px; width: 687px;" valign="top">
                            <asp:DropDownList ID="Other1DropDownList" runat="server" Width="190px" AutoPostBack="True"
                                OnSelectedIndexChanged="Other1DropDownList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="Label18" runat="server" Text="Serving Size:"></asp:Label>
                            <asp:Label ID="Other1ServingSizeLabel" runat="server" Text="0 " Width="52px"></asp:Label><asp:Label
                                ID="Other1ServingSizeTypeLabel" runat="server" Text="ounce(s)" Width="60px"></asp:Label><asp:Button
                                    ID="ViewOther1YieldDetailButton" runat="server" 
                                Text="View/Modify Yield/Notes" Width="172px"
                                    Font-Bold="True" OnClick="ViewOther1YieldDetailButton_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="1" valign="bottom" class="style1">
                            Other 2:
                        </td>
                        <td align="left" valign="top" class="style2">
                            <asp:DropDownList ID="Other2DropDownList" runat="server" Width="190px" AutoPostBack="True"
                                OnSelectedIndexChanged="Other2DropDownList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="Label94" runat="server" Text="Serving Size:"></asp:Label>
                            <asp:Label ID="Other2ServingSizeLabel" runat="server" Text="0 " Width="52px"></asp:Label><asp:Label
                                ID="Other2ServingSizeTypeLabel" runat="server" Text="ounce(s)" Width="60px"></asp:Label><asp:Button
                                    ID="ViewOther2YieldDetailButton" runat="server" 
                                Text="View/Modify Yield/Notes" Width="172px"
                                    Font-Bold="True" OnClick="ViewOther2YieldDetailButton_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="1" style="width: 212px;" valign="bottom">
                            Other 3:
                        </td>
                        <td align="left" valign="top" style="width: 687px">
                            <asp:DropDownList ID="Other3DropDownList" runat="server" Width="190px" AutoPostBack="True"
                                OnSelectedIndexChanged="Other3DropDownList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="Label93" runat="server" Text="Serving Size:"></asp:Label>
                            <asp:Label ID="Other3ServingSizeLabel" runat="server" Text="0 " Width="52px"></asp:Label><asp:Label
                                ID="Other3ServingSizeTypeLabel" runat="server" Text="ounce(s)" Width="60px"></asp:Label><asp:Button
                                    ID="ViewOther3YieldDetailButton" runat="server" 
                                Text="View/Modify Yield/Notes" Width="172px"
                                    Font-Bold="True" OnClick="ViewOther3YieldDetailButton_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="1" style="width: 212px" valign="bottom">
                        </td>
                        <td align="left" style="width: 687px" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="1" style="width: 212px; height: 20px" valign="top">
                            <asp:Label ID="Label4" runat="server" Text="Meal Notes:" Font-Bold="False"></asp:Label>
                        </td>
                        <td align="left" rowspan="2" style="width: 687px" valign="top">
                            <asp:TextBox ID="NotesTextBox" runat="server" TextMode="MultiLine" Width="498px">Store all food except canned fruit in refrigerator until ready to reheat (hot meal items) or serve (fresh fruit, cold salads, etc.) Before serving, reheat hot meal items within 2 hours to an internal temperature of 165ºF for at least 15 seconds, or discard. Serve reheated food within 2 hours. While serving, hot meal items must be kept at 135°F or higher. Discard unused portions.</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="1" style="width: 212px" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="height: 8px" valign="top">
                                               <asp:HiddenField ID="MealIDHiddenTextBox" runat="server" />
                    <asp:HiddenField ID="VegetableRecipeDetailIDHiddenField" runat="server" />
                    <asp:HiddenField ID="BreadRecipeDetailIDHiddenField" runat="server" />
                    <asp:HiddenField ID="Other1RecipeDetailIDHiddenField" runat="server" />
                    <asp:HiddenField ID="Other2RecipeDetailIDHiddenField" runat="server" />
                    <asp:HiddenField ID="Other3RecipeDetailIDHiddenField" runat="server" />
                    <asp:HiddenField ID="YieldDetailRecipeTypeNameHiddenField" runat="server" />
                    <asp:HiddenField ID="YieldDetailRecipeDetailIDHiddenField" runat="server" /></td>
                    </tr>
                </table>                
            </ContentTemplate>
        </asp:UpdatePanel>
        <table style="width: 899px">
            <tr>
                <td align="center" style="height: 15px">
                   <asp:Button ID="SaveMealDetailButton" runat="server"
                            Font-Bold="True" Text="Save Meal Details" OnClick="SaveMealDetailButton_Click"  Enabled = "false"/><asp:Button
                                ID="CancelMealDetailButton" runat="server" Font-Bold="True" Text="Cancel" Width="64px" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="CookChillYieldPanel" runat="server" BackColor="#00C7B3" BorderColor="Black"
        BorderStyle="Solid" BorderWidth="1px" Width="714px"  ForeColor="Black">
        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
         <ContentTemplate>
        <table id="Table2" width="100%">
            <tr>
                <td align="right" style="width: 50%; height: 12px">
                    <asp:Label ID="Label26" runat="server" Text="Rounded Production Number This Meal:"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 12px">
                    <asp:TextBox ID="CookchillRoundMealCountTextBox" runat="server" BackColor="Transparent"
                        BorderStyle="None" Width="71px">0</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 5px">
                    <asp:Label ID="Label13" runat="server" Text="Package Type:"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 5px">
                    <asp:TextBox ID="PackageTypeTextBox" runat="server" BackColor="Transparent" BorderStyle="None"
                        Width="168px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 19px">
                    <asp:Label ID="Label99" runat="server">Number of </asp:Label>
                    <asp:Label ID="PackageTypeLabel" runat="server">1 gallon super bag (16c)</asp:Label>
                    <asp:Label ID="Label23" runat="server">Per Batch:</asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 19px">
                    &nbsp;<asp:TextBox ID="NumberofPackageTypeTextBox" runat="server" BackColor="Transparent"
                        BorderStyle="None" Width="71px">0</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 21px">
                    <asp:Label ID="Label16" runat="server" Font-Bold="False" Text="Serving Size (Weight):"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 21px">
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
                    <asp:Label ID="Label14" runat="server">Serving Size (Volume):</asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 21px">
                    <asp:DropDownList ID="VolumeServingSizeDropDownList" runat="server" Width="108px">
                        <asp:ListItem Value="0.25">1/4</asp:ListItem>
                        <asp:ListItem Value="0.50">1/2</asp:ListItem>
                        <asp:ListItem Value="0.75">3/4</asp:ListItem>
                        <asp:ListItem Value="1.00">1</asp:ListItem>
                        <asp:ListItem Value="1.25">1 1/4</asp:ListItem>
                        <asp:ListItem Value="1.50">1 1/2</asp:ListItem>
                        <asp:ListItem Value="1.75">1 3/4</asp:ListItem>
                        <asp:ListItem Value="2.00">2</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="CookChillServingSizeTypeLabel" runat="server" Text="cup(s)" 
                        Width="53px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 23px">
                    <asp:Label ID="Label17" runat="server" Text="Number of Servings Per Package:"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 23px">
                    <asp:TextBox ID="ServingsPerPackageTextBox" runat="server" BackColor="Transparent"
                        BorderStyle="None" Width="39px">0</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 21px">
                    <asp:Label ID="Label19" runat="server" Text="Number of Servings (per batch):"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 21px">
                    <asp:TextBox ID="ServingsPerBatchTextBox" runat="server" BackColor="Transparent"
                        BorderStyle="None">0</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 21px">
                    <asp:Label ID="Label20" runat="server" Text="Batch Yield In Pounds:"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 21px">
                    <asp:TextBox ID="BatchYieldInPoundsTextBox" runat="server" BackColor="Transparent"
                        BorderStyle="None">0</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 21px">
                    <asp:Label ID="Label28" runat="server" Font-Bold="True" Text="Conversion Factor:"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 21px">
                    <asp:TextBox ID="CookChillConversionFactorTextBox" runat="server" BackColor="Transparent"
                        BorderStyle="None" Font-Bold="True" Width="184px">0</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" rowspan="1" style="width: 100%; height: 2px">
                    <asp:HiddenField ID="PackageTypeIDHiddenField" runat="server" />
                    <asp:HiddenField ID="ServingSizeTypeIDHiddenField" runat="server" />
                    <asp:HiddenField ID="RecipeOriginalCCYieldHiddenField" runat="server" />
                    <asp:HiddenField ID="OriginalCCVolumeHiddenField" runat="server" />
                    <asp:HiddenField ID="OriginalCCServingSizeHiddenField" runat="server" />
                </td>
            </tr>
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>
            <table width= "100%">
                        <tr>
                <td align="center" colspan="2" style="width: 100%;">
                    <asp:Button ID="SaveCookChillYieldDetailButton" runat="server" Font-Bold="True" OnClick="SaveCookChillYieldDetail_Click"
                        Text="Save Yield Details"  Enabled = "false"/>
                    <asp:Button ID="CancelCookChillYieldDetailButton" runat="server"
                            Font-Bold="True" Text="Cancel" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="RegularYieldPanel" runat="server" BackColor="#00C7B3" BorderColor="Black"
        BorderStyle="Solid" BorderWidth="1px" Width="714px"  ForeColor="Black">
     <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
        <table id="Table1" width="100%">
            <tr>
                <td align="right" style="width: 50%; height: 10px">
                    <asp:Label ID="Label24" runat="server" Text="Rounded Production Number This Meal:"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 10px">
                    <asp:TextBox ID="CurrentMealRoundedMealCountTextBox" runat="server" BackColor="Transparent"
                        BorderStyle="None" Width="71px" Height="18px">0</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 8px">
                    <asp:Label ID="Label27" runat="server" Text="Recipe's Number Of Servings:"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 8px">
                    <asp:TextBox ID="RecipeNumberofServings" runat="server" BackColor="Transparent" BorderStyle="None"
                        Width="71px">0</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 15px">
                    <asp:Label ID="Label25" runat="server" Text="Serving Size:"></asp:Label>
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
                    <asp:DropDownList ID="ServingSizeTypeDropdownList" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ServingSizeTypeDropdownList_SelectedIndexChanged" Width="143px">
                        <asp:ListItem Value="1">ounce(s)</asp:ListItem>
                        <asp:ListItem Value="2">piece(s)</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 21px">
                    <asp:Label ID="VolumeWeightLabel" runat="server">Serving Size (Volume):</asp:Label>
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
                    <asp:Label ID="VolumeWeightTypeLabel" runat="server">cup(s)</asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 21px">
                    <asp:Label ID="Label22" runat="server" Font-Bold="True" Text="Conversion Factor:"></asp:Label>
                </td>
                <td align="left" style="width: 50%; height: 21px">
                    <asp:TextBox ID="ConversionFactorTextBox" runat="server" BackColor="Transparent"
                        BorderStyle="None" Font-Bold="True" Width="184px" Height="18px">0</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 21px">
                    Recipe Serving Notes:
                </td>
                <td align="left" rowspan="2" style="width: 50%" valign="top">
                    <asp:TextBox ID="RecipeNotesTextBox" runat="server" TextMode="MultiLine" Width="331px"
                        MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50%; height: 21px">
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 21px" colspan="2">
                    <asp:HiddenField ID="RecipeOriginalYieldHiddenField" runat="server" />
                    <asp:HiddenField ID="OriginalVolumeHiddenField" runat="server" />
                    <asp:HiddenField ID="OriginalServingSizeHiddenField" runat="server" />
                </td>
            </tr></table>
        </ContentTemplate>
        </asp:UpdatePanel>
        <table id="Table3" width="100%">
            <tr>
                <td align="center" colspan="2" style="width: 100%; height: 22px">
                    <asp:Button ID="SaveRegularYieldDetailButton" runat="server" Font-Bold="True" OnClick="SaveRegularYieldDetailButton_Click"
                        Text="Save Yield Details"  Enabled = "false" /><asp:Button ID="CancelRegularYieldDetailButton" runat="server"
                            Font-Bold="True" Text="Cancel" />
                </td>
            </tr>
        </table>
    </asp:Panel>
   <asp:Button ID="CookChillYieldPopupButton" runat="server" Style="display: none" Text="Button" />
   <asp:Button ID="RegularYieldPopupButton" runat="server" Style="display: none" Text="Button" />
    <cc1:ModalPopupExtender ID="CookChillYieldPopupExtender" runat="server" BehaviorID="CookChillYieldPopupExtender"
        OkControlID="CancelCookChillYieldDetailButton" PopupControlID="CookChillYieldPanel"
        TargetControlID="CookChillYieldPopupButton">
    </cc1:ModalPopupExtender>
    <cc1:ModalPopupExtender ID="RegularYieldPopupExtender" runat="server" BehaviorID="RegularYieldPopupExtender"
        PopupControlID="RegularYieldPanel" OkControlID="CancelRegularYieldDetailButton"
        TargetControlID="RegularYieldPopupButton">
    </cc1:ModalPopupExtender>
    <cc1:ModalPopupExtender BehaviorID="mpe" ID="mpe" runat="server"
        CancelControlID="CancelMealDetailButton" PopupControlID="MenuComponentsPanel" BackgroundCssClass="modalBackground"
        TargetControlID="ClientButton" Drag="true" PopupDragHandleControlID="MenuComponentsPanel">
    </cc1:ModalPopupExtender>
    <asp:Button ID="ClientButton" runat="server" Style="display: none" Text="Launch Modal Popup (Client)" />
</asp:Content>
