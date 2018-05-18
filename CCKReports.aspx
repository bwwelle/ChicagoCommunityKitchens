<%@ Page Language="C#" Title="CCK Application Reports" AutoEventWireup="true" MasterPageFile="~/Site.master"
    CodeFile="CCKReports.aspx.cs" Inherits="CCKReports" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        var launch = false;

        function launchModal() {
            launch = true;
        }

        function pageLoad() {
            if (launch) {
                $find("ReportDatePopupBehavior").show();
            }
        }

        //Sets default date for delivery rescheduling popup calendar
        function setDate() {
            var reportStartDate = document.getElementById('ctl00_MainContent_ReportStartDateTextBox').value;
            reportEndDate = new Date(eval('"' + reportStartDate + '"'));
            $find("ReportEndDateBehaviorID").set_selectedDate(reportEndDate);
        }

        function hideMealTypeCommunityArea() {
            if (document.getElementById('ctl00_MainContent_SiteNameDropDownList').selectedIndex != 0) {
                document.getElementById('ctl00_MainContent_MealTypeDiv').style.visibility = "hidden";
                document.getElementById('ctl00_MainContent_CommunityAreaDiv').style.visibility = "hidden";
            }

            if (document.getElementById('ctl00_MainContent_SiteNameDropDownList').selectedIndex == 0) {
                document.getElementById('ctl00_MainContent_MealTypeDiv').style.visibility = "visible";
                document.getElementById('ctl00_MainContent_CommunityAreaDiv').style.visibility = "visible";
            }
        }

        function hideSite() {
            if (document.getElementById('ctl00_MainContent_MealTypeDropDownList').selectedIndex != 0) {
                document.getElementById('ctl00_MainContent_SiteNameDiv').style.visibility = "hidden";
            }

            if (document.getElementById('ctl00_MainContent_CommunityAreaDropDownList').selectedIndex != 0) {
                document.getElementById('ctl00_MainContent_SiteNameDiv').style.visibility = "hidden";
            }

            if (document.getElementById('ctl00_MainContent_CommunityAreaDropDownList').selectedIndex == 0 && document.getElementById('ctl00_MainContent_MealTypeDropDownList').selectedIndex == 0) {
                document.getElementById('ctl00_MainContent_SiteNameDiv').style.visibility = "visible";
            }
        }

    </script>
    <style type="text/css">
        .style9
        {
            font-size: medium;
        }
        </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="asm" runat="server" />
    <div>
        <table style="width: 100%; text-align: center;">
            <tr>
                <td align="center">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" Text="CCK Application Reports"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" style="font-weight: bold; font-size: small;">
                    Click The Name Of The Desired Report Below And Enter Report Parameter(s) If/When
                    Prompted
                </td>
            </tr>
            <tr>
                <td align="left" style="border: 1px double #ffffff; font-weight: bold; font-size: small;
                    color: #004d45; font-style: Calibri; background-color: #d28000;">
                    View Kitchen's Reports
                </td>
            </tr>
            <tr>
                <td align="left" style="font-weight: bold; font-size: small;">
                    <ul>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton4" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="AdjustedRecipesReportButton_Click" Width="235px">Adjusted Recipes Report</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="RecipesReportButton_Click" Width="195px">All Recipes Report</asp:LinkButton></li>
                       <li class="style9">
                            <asp:LinkButton ID="AverageRecipeCostPerDateRange" runat="server" ForeColor="Gray"
                                Enabled="False" Font-Size="small" OnClick="AverageRecipeCostPerDateRange_Click"
                                Width="337px">Average Recipe/Meal Cost Per Date Range</asp:LinkButton></li>
                       <li class="style9">
                            <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Gray" Enabled="True"
                                Font-Size="small" OnClick="DeliveryLabelsButton_Click" Width="195px">Delivery Labels</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton36" runat="server" ForeColor="White" Enabled="True"
                                Font-Size="small" OnClick="ColdLabelsButton_Click" Width="195px">Cold Delivery Labels</asp:LinkButton></li>
                          <li class="style9">
                            <asp:LinkButton ID="LinkButton37" runat="server" ForeColor="White" Enabled="True"
                                Font-Size="small" OnClick="LunchBusLabelsButton_Click" Width="195px">Cold Lunchbus Delivery Labels</asp:LinkButton></li>
                       
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton32" runat="server" ForeColor="White" Enabled="True"
                                Font-Size="small" OnClick="DeliveryLabelsHotButton_Click" Width="195px">Hot Lunch Delivery Labels</asp:LinkButton></li>
                       
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton33" runat="server" ForeColor="White" Enabled="True"
                                Font-Size="small" OnClick="DeliveryLabelsBreakfastButton_Click" Width="195px">Breakfast Delivery Labels</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton6" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="DeliveryReceiptButton_Click" Width="195px">Delivery Receipts-Hot CCK</asp:LinkButton></li>
                                <li class="style9">
                            <asp:LinkButton ID="LinkButton35" runat="server" ForeColor="White" Enabled="True"
                                Font-Size="small" OnClick="DeliveryReceiptButtonCold_Click" Width="195px">Delivery Receipts-Cold CCK</asp:LinkButton></li>
                         <li class="style9">
                            <asp:LinkButton ID="LinkButton7" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="InventoryOrderReport_Click" Width="218px">Inventory Order Report</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton5" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="InventoryReport_Click" Width="195px" Style="height: 15px">Inventory Report</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="ProductionSheets_Click" Width="195px">Production Sheets</asp:LinkButton></li>

                        <li class="style9">
                            <asp:LinkButton ID="LinkButton31" runat="server" ForeColor="White" Enabled="True"
                                Font-Size="small" OnClick="NumberPans_Click" Width="195px">Number Of Pans (Filo's Report)</asp:LinkButton></li>
                    </ul>
                </td>
            </tr>
            <tr>
                <td align="left" style="border: 1px double #ffffff; font-weight: bold; font-size: small;
                    color: #004d45; font-style: Calibri; background-color: #d28000;">
                    View Transportation/Inventory's Reports
                </td>
            </tr>
            <tr>
                <td align="left" style="font-weight: bold; font-size: small;">
                    <ul>
                        <%--                        <li class="style9">
                            <asp:LinkButton ID="LinkButton8" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="DeliveryReportButton_Click" Width="167px">Create Delivery Report</asp:LinkButton></li>--%>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton9" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="DeliveryReportByRangeButton_Click" Width="372px">Delivery Report By Date Range</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton26" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="CACFPDeliveryReportButton_Click" Width="287px">Delivery Report (CACFP)</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton25" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="SFSPDeliveryReportButton_Click" Width="287px" Style="height: 17px">Delivery Report (SFSP)</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton15" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="PoundageReportButton_Click" Width="181px">Poundage Report</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton10" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="PoundageByRangeButton_Click" Width="284px">Poundage Report By Date Range</asp:LinkButton>
                        </li>
                    </ul>
                </td>
            </tr>
            <tr>
                <td align="left" style="border: 1px double #ffffff; font-weight: bold; font-size: small;
                    color: #004d45; font-style: Calibri; background-color: #d28000;">
                    View Program's/Compliance&#39;s Reports
                </td>
            </tr>
            <tr>
                <td align="left" style="font-weight: bold; font-size: small;">
                    <ul>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton22" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="CACFPComplianceSiteInformationButton_Click" Width="445px">Compliance Site Information Report (CACFP)</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton16" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="SFSPComplianceSiteInformationButton_Click" Width="453px"
                                Style="margin-bottom: 0px">Compliance Site Information Report (SFSP)</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton11" runat="server" ForeColor="Gray" Enabled="false"
                                Font-Size="small" OnClick="ColdMealScheduleButton_Click" Width="246px" 
                                style="height: 17px">Cold Meal Schedule Report</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton14" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="DailyCountReport_Click" Width="290px">Daily Count Report</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton8" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="DistributionByCommunity_Click" Width="290px">Distribution By Community</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton24" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="IndividualCACFPSiteInformationSheet_Click" Width="358px">Individual Site Information Sheet (CACFP)</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton23" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="IndividualSFSPSiteInformationSheet_Click" Width="353px">Individual Site Information Sheet (SFSP)</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton19" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="CACFPMealInformationReport_Click" Width="267px">Meal Information Report (CACFP)</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton12" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="SFSPMealInformationReport_Click" Width="267px" Style="margin-bottom: 0px">Meal Information Report (SFSP) </asp:LinkButton></li>
                                 <li class="style9">
                            <asp:LinkButton ID="LinkButton30" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="MissingMealCountsReportByDateRange_Click" Width="314px" 
                                         Style="margin-bottom: 0px">Missing Meal Counts Report By Date Range</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="NutritionEducationReportButton" runat="server" 
                                ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="NutritionEducationCountReport_Click" 
                                Width="310px">Nutrition Education Count Report</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="CACFPProgramsLabelReport" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="CACFPProgramsLabelReport_Click" Width="358px">Programs Label Report (CACFP)</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="SFSPProgramsLabelReport" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="SFSPProgramsLabelReport_Click" Width="358px">Programs Label Report (SFSP)</asp:LinkButton></li>
                                <li class="style9">
                        <asp:LinkButton ID="LinkButton13" runat="server" ForeColor="Gray"
                                Enabled="False" Font-Size="small" OnClick="SiteCodeAndCommunityAreaReport_Click"
                                Width="432px">Site Code/Community Area Report</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="SiteCountMealTotalReportByRange" runat="server" ForeColor="Gray"
                                Enabled="False" Font-Size="small" OnClick="SiteCountMealTotalReportByRange_Click"
                                Width="432px">Site Count Meal Total Report By Range Report</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton20" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="CACFPSiteInformationReport_Click" Width="267px">Site Information Report (CACFP)</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton21" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="SFSPSiteInformationReport_Click" Width="267px">Site Information Report (SFSP)</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton17" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="CACFPSiteMealCountForm_Click" Width="286px">Site Meal Count Form (CACFP)</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton18" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="SFSPSiteMealCountForm_Click" Width="286px" 
                                Height="17px">Site Meal Count Form (SFSP)</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton29" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="SitesReceivingMealsByDateRange_Click" Width="286px" 
                                Height="17px">Sites Receiving Meals Report By Date Range</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="UnduplicatedParticipationReportByRange" runat="server" ForeColor="Gray"
                                Enabled="False" Font-Size="small" OnClick="UnduplicatedParticipationReportByRange_Click"
                                Width="358px">Unduplicated Participation Report By Range</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton27" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="CACFPWeeklyOrderReport_Click" Width="284px">Weekly Order Report (CACFP)</asp:LinkButton></li>
                        <li class="style9">
                            <asp:LinkButton ID="LinkButton28" runat="server" ForeColor="Gray" Enabled="False"
                                Font-Size="small" OnClick="SFSPWeeklyOrderReport_Click" Width="207px">Weekly Order Report (SFSP)</asp:LinkButton></li> 
                                 <li class="style9">
                            <asp:LinkButton ID="LinkButton34" runat="server" ForeColor="White" Enabled="True"
                                Font-Size="small" OnClick="CACFPWeeklyOrderReportCold_Click" Width="284px">Weekly Order Report (Cold)</asp:LinkButton></li>
                        <li class="style9">                       
                    </ul>
                </td>
            </tr>
            <tr>
                <td align="left" style="font-weight: bold; font-size: small;">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <cc1:CalendarExtender ID="StartDateCalendarExtender" runat="server" Format="MM/dd/yyyy"
        TargetControlID="ReportStartDateTextBox">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender BehaviorID="ReportEndDateBehaviorID" ID="EndDateCalendarExtender"
        runat="server" Format="MM/dd/yyyy" TargetControlID="ReportEndDateTextBox" OnClientShowing="setDate">
    </cc1:CalendarExtender>
    <cc1:ModalPopupExtender ID="ReportDateModelPopupExtender" runat="server" BehaviorID="ReportDatePopupBehavior"
        CancelControlID="CancelButton" PopupControlID="ReportDatePanel" TargetControlID="ReportDateTriggerButton">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" Visible="False">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" ZoomMode="PageWidth"
            ZoomPercent="50">
        </rsweb:ReportViewer>
    </asp:Panel>
    <asp:Panel ID="ReportDatePanel" BackColor="#00C7B3" runat="server" Width="456px"
        HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
        ForeColor="Black">
        <table width="100%">
            <tr>
                <td align="center" style="font-weight: bold; text-decoration: underline;">
                    Enter Report Date Parameter(s) Below
                </td>
            </tr>
            <tr valign="top">
                <td>
                    <div id="ReportStartDateDiv" style="display: none; width: 100%; height: 1px;" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td align="right" style="width: 50%;">
                                    <asp:Label ID="ReportStartDateLabel" runat="server" Text="Report Start Date:"></asp:Label>
                                </td>
                                <td align="left" style="width: 50%;">
                                    <asp:TextBox ID="ReportStartDateTextBox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="ReportEndDateDiv" style="display: none; width: 100%; height: 1px;" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td align="right" style="width: 50%;">
                                    <asp:Label ID="ReportEndDateLabel" runat="server" Text="Report End Date:"></asp:Label>
                                </td>
                                <td align="left" style="width: 50%;">
                                    <asp:TextBox ID="ReportEndDateTextBox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="MealTypeDiv" style="display: none; width: 100%; height: 1px;" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td align="right" style="width: 50%;">
                                    <asp:Label ID="MealTypeLabel" runat="server" Text="Meal Type:"></asp:Label>
                                </td>
                                <td align="left" style="width: 50%;">
                                    <asp:DropDownList ID="MealTypeDropDownList" runat="server" Width="187px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="ScheduleTypeDiv" style="display: none; width: 100%; height: 1px;" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td align="right" style="width: 50%;">
                                    <asp:Label ID="ScheduleTypeLabel" runat="server" Text="Schedule Type:"></asp:Label>
                                </td>
                                <td align="left" style="width: 50%;">
                                    <asp:DropDownList ID="ScheduleTypeDropDownList" runat="server" Width="187px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="CommunityAreaDiv" style="display: none; width: 100%; height: 1px;" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td align="right" style="width: 50%;">
                                    <asp:Label ID="CommunityAreaLabel" runat="server" Text="Community Area:"></asp:Label>
                                </td>
                                <td align="left" style="width: 50%;">
                                    <asp:DropDownList ID="CommunityAreaDropDownList" runat="server" Width="187px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="SiteNameDiv" style="display: none; width: 100%; height: 1px;" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td align="right" style="width: 50%;">
                                    <asp:Label ID="SiteNameLabel" runat="server" Text="Site Name:"></asp:Label>
                                </td>
                                <td align="left" style="width: 50%;">
                                    <asp:DropDownList ID="SiteNameDropDownList" runat="server" Width="187px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="ReportTextDiv" style="display: none; width: 100%; height: 1px;" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td align="right" style="width: 50%;" valign="top">
                                    <asp:Label ID="Label2" runat="server" Text="Label Text:"></asp:Label>
                                </td>
                                <td align="left" style="width: 50%;" valign="top">
                                    <asp:TextBox ID="LabelTextTextBox" runat="server" Width="217px" TextMode="MultiLine"
                                        Font-Size="Small"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:HiddenField ID="ReportNameHiddenField" runat="server" />
                </td>
            </tr>
           
                
             
            <tr>
                <td>
                    <div id="DeliveryReportParametersDiv" style="display: inline; width: 100%;" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr style="width:100%">
                                <td align="right" style="width:50%">
                                    <asp:Label ID="Label3" runat="server" Text="Number of Slices Per Loaf:"></asp:Label>
                                </td>
                                <td align="left"  style="width:50%"><asp:TextBox ID="LoafSliceCountTextBox" runat="server" Width="65px">20</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="text-align: right">
                                    <asp:Label ID="Label4" runat="server" Text="Number of Buns Per Bag:"></asp:Label>
                                </td>
                                <td align="center" style="text-align: left">
                                    <asp:TextBox ID="BagBunCountTextBox" runat="server" Width="65px">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="text-align: right">
                                    <asp:Label ID="Label5" runat="server" Text="Number of Loaves Per Container:"></asp:Label>
                                </td>
                                <td align="center" style="text-align: left">
                                    <asp:TextBox ID="LoafCountTextBox" runat="server" Width="65px">8</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="text-align: right">
                                    <asp:Label ID="Label6" runat="server" Text="Number of Bags Per Container:"></asp:Label>
                                </td>
                                <td align="center" style="text-align: left">
                                    <asp:TextBox ID="BagCountTextBox" runat="server" Width="65px">0</asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
             <tr>
                <td>
                    <div id="mealsPerBoxDiv" style="display: inline; width: 100%;" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td align="center" style="text-align: right">
                                    <asp:Label ID="lblMealsPerBox" runat="server" Text="Number of meals per box:"></asp:Label>
                                </td>
                                <td align="center" style="text-align: left">
                                    <asp:TextBox ID="txtMealsPerBox" runat="server" Width="65px">20</asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="ReportFormatDiv" style="display: inline; width: 100%;" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td align="center">
                                    <asp:RadioButtonList ID="ReportTypeRadioButtonList" runat="server" Font-Bold="True"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True">PDF</asp:ListItem>
                                        <asp:ListItem>Excel</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="CancelButton" runat="server" Font-Bold="True" Text="Cancel" />
                    <asp:Button ID="CreateReportButton" runat="server" Font-Bold="True" OnClick="CreateReportButton_Click"
                        Text="Create Report" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Button ID="ReportDateTriggerButton" runat="server" Style="display: none" Text="Button" />
</asp:Content>
