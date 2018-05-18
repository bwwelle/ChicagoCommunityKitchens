<%@ Page Language="C#" Title="CCK Delivery Calendar" AutoEventWireup="true" EnableEventValidation="false"
    MasterPageFile="~/Site.master" CodeFile="DeliveryCalendar.aspx.cs" Inherits="DeliveryCalendar" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="GCFDGlobalsNamespace" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .cssLstQueries
        {
            width: auto;
            height: 290px;
        }
        .modalBackground
        {
            background-color: #000;
            filter: alpha(opacity=80);
            opacity: 0.5;
        }
        
    </style>
    <script type="text/javascript">
        var launch = false;
        var launchDeliveryDayDetail = false;
        var launchGroupCancellation = false;
        var launchContinue = false;

        function launchDeliveryDayDetailModal() {
            launchDeliveryDayDetail = true;
        }

        function launchGroupCancellationModal() {
            launchGroupCancellation = true;
        }

        function launchContinueModal() {
            launchContinue = true;
        }

        function fnClickOK(sender, e) {
            __doPostBack(sender, e);
        }

        function pageLoad() {
            if (launchDeliveryDayDetail) {
                $find("DayDetailModelPopupExtender").show();
            }

            if (launchGroupCancellation) {
                $find("CancellationModalPopupExtender").show();
            }

            if (launchContinue) {
                $find("ContinueModalPopupExtender").show();
            }
        }

        function setDate() {
            var reportStartDate = document.getElementById('ctl00_MainContent_CancellationRangeStartDateTextBox').value;
            reportEndDate = new Date(eval('"' + reportStartDate + '"'));
            $find("CancellationRangeEndCalendarBehaviorID").set_selectedDate(reportEndDate);
        }
    </script>
    <script language="C#" runat="server">
        
        void DayRender(Object source, EventArgs e)
        {
            ScheduledDeliveryWeekdayLabel.Text = DeliveryCalendarControl.SelectedDate.ToShortDateString();

            DayOfScheduledTaskLabel.Text = "Date of Delivery:";

            Session["DeliveryCalendarMode"] = "DayView";

            m_SQL = "SELECT * FROM Site";
            m_DeliveriesDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

            FillSiteDataGridView(m_DeliveriesDataSet, DeliveryCalendarControl.SelectedDate, "DayClick");

            GetCalendarData();

            ClientScript.RegisterStartupScript(GetType(), "key", "launchDeliveryDayDetailModal();", true);

            DeliveryCalendarControl.SelectedDates.Clear();
        }

    </script>
    <style type="text/css">
        .style1
        {
            width: 299px;
            height: 9px;
        }
        .style2
        {
            width: 283px;
            height: 9px;
        }
        .style3
        {
            height: 203px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="asm" runat="server" EnablePartialRendering="true" />
    <asp:Panel ID="CalendarPanel" runat="server" Width="100%">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" rowspan="1">
                    <asp:Button ID="CreateRangeCancellationButton" runat="server" OnClick="CreateRangeCancellationButton_Click"
                        Text="Cancel/Reschedule Deliveries For A Date Range" Enabled="True" Font-Bold="True" />
                </td>
            </tr>
                        <tr>
                <td align="center" colspan="4" style="border-style: solid solid none solid; border-width: thin;
                    border-color: #FFFFFF; width: 100%;" valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="left" bgcolor="Silver" class="style25" style="width: 25%">
                                &nbsp;<asp:LinkButton ID="PreviousMonthLinkButton" runat="server" Font-Bold="true" 
                                    ForeColor="#004D45" OnClick="PreviousMonthLinkButton_Click">LinkButton</asp:LinkButton>
                                <asp:HiddenField ID="PreviousMonthHiddenField" runat="server" />
                            </td>
                            <td align="right" bgcolor="Silver" class="style24" style="width: 25%">
                                <asp:DropDownList ID="drpCalMonth" runat="Server" AutoPostBack="true" 
                                    BackColor="Silver" Font-Bold="True" Font-Names="Arial Black" 
                                    ForeColor="#004D45" OnSelectedIndexChanged="drpCalMonth_SelectedIndexChanged" 
                                    Width="105px">
                                </asp:DropDownList>
                            </td>
                            <td align="left" bgcolor="Silver" class="style24" style="width: 25%">
                                <asp:DropDownList ID="drpCalYear" runat="Server" AutoPostBack="true" 
                                    BackColor="Silver" Font-Bold="True" Font-Names="Arial Black" 
                                    ForeColor="#004D45" OnSelectedIndexChanged="drpCalYear_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right" bgcolor="Silver" class="style24" style="width: 25%">
                                <asp:HiddenField ID="NextMonthHiddenField" runat="server" />
                                <asp:LinkButton ID="NextMonthLinkButton" runat="server" Font-Bold="true" 
                                    ForeColor="#004D45" OnClick="NextMonthLinkButton_Click">LinkButton</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="width: 100%; border-style: none solid solid solid; border-width: thin;
                border-color: #FFFFFF;">
                <td colspan="4" valign="top" style="border-style: none solid solid solid; border-width: thin;
                    border-color: #FFFFFF;">
                    <asp:Calendar ID="DeliveryCalendarControl" runat="server" CellPadding="0" OnSelectionChanged="DayRender"  
                        DayNameFormat="Full" FirstDayOfWeek="Monday" NextMonthText="&gt;" 
                        NextPrevFormat="ShortMonth" OnDayRender="MenuDeliveryCalendarControl_DayRender"
                        OnVisibleMonthChanged="MenuDeliveryCalendarControl_VisibleMonthChanged" 
                        PrevMonthText="&lt;" SelectMonthText="" ShowTitle="False" 
                        UseAccessibleHeader="False" Width="100%">
                        <DayStyle BackColor="Transparent" BorderColor="Black" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Bold="True" ForeColor="White" />
                        <NextPrevStyle ForeColor="#004D45" />
                        <OtherMonthDayStyle BackColor="#E0E0E0" Font-Bold="True" ForeColor="#004D45" />
                        <TitleStyle Font-Bold="True" ForeColor="#004D45" />
                    </asp:Calendar>
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 709px;">
                    <asp:Panel ID="DayDetailsPanel" runat="server" BackColor="#00C7B3" Width="714px"
                        ForeColor="Black" CssClass="modalBackground">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%;">
                                    <asp:Label ID="DayOfScheduledTaskLabel" runat="server" Text="Day of Scheduled Delivery:"
                                        Font-Bold="true"></asp:Label>
                                </td>
                                <td align="left" style="width: 50%;">
                                    <asp:Label ID="ScheduledDeliveryWeekdayLabel" runat="server" Width="25%" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" valign="top" class="style3">
                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Width="100%" Height="200px">
                                        <asp:GridView ID="DayDetailsGridView" runat="server" OnRowDataBound="DayDetailsGridView_RowDataBound"
                                            OnSelectedIndexChanged="DayDetailsGridView_SelectedIndexChanged" OnSelectedIndexChanging="DayDetailsGridView_SelectedIndexChanging"
                                            Width="99%">
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%;">
                                    <asp:Button ID="ViewSiteListButton" runat="server" Font-Bold="True" OnClick="ViewSiteListButton_Click"
                                        Text="View Site List" />
                                    <asp:Button ID="CloseDayDetailButton" runat="server" Font-Bold="True" Text="Close" />
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="ChoosenDateHiddenField" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="GroupExceptionPanel" runat="server" BackColor="#00C7B3" Width="850px"
                        ForeColor="Black" CssClass="modalBackground">
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td align="right" style="width: 50%; height: 14px">
                                            <asp:Label ID="Label2" runat="server" Text="Start of Cancellation Date Range:"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 50%; height: 14px">
                                            <asp:Label ID="CancellationRangeStartDateLabel" runat="server" Text="Label" Width="156px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 50%; height: 14px">
                                            <asp:Label ID="Label4" runat="server" Text="End of Cancellation Date Range:"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 50%; height: 14px">
                                            <asp:Label ID="CancellationRangeEndDateLabel" runat="server" Text="Label" Width="156px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 50%; height: 14px">
                                            <asp:Label ID="Label5" runat="server" Text="Meal Type:"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 50%; height: 14px">
                                            <asp:Label ID="CancellationMealTypeLabel" runat="server" Text="Label" Width="156px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 50%; height: 14px">
                                            <asp:Label ID="Label7" runat="server" Text="Schedule:"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 50%; height: 14px">
                                            <asp:Label ID="CancellationScheduleTypeLabel" runat="server" Text="Label" 
                                                Width="383px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 50%; height: 14px">
                                            <asp:Button ID="AddExceptionButton" runat="server" OnClick="Button1_Click" 
                                                Text="-&gt;" />
                                        </td>
                                        <td align="left" style="width: 50%; height: 14px">
                                            <asp:Button ID="RemoveExceptionButton" runat="server" OnClick="Button2_Click" 
                                                Text="&lt;-" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 50%;">
                                            <div id="RemovedDiv" style="overflow: auto; width: 400px; height: 300px">
                                                <asp:ListBox ID="RemovedDeliverySiteList" runat="server" SelectionMode="Multiple">
                                                </asp:ListBox>
                                            </div>
                                        </td>
                                        <td align="left" style="width: 50%;">
                                            <div id="AddedDiv" runat="server" style="overflow-x: auto; width: 400px; height: 300px;">
                                                <asp:ListBox ID="AddedDeliverySiteList" runat="server" SelectionMode="Multiple" CssClass="cssLstQueries">
                                                </asp:ListBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 50%;">
                                            Site(s) Above <b>Will Not</b> Have Their Meals Cancelled
                                        </td>
                                        <td align="center" style="width: 50%;">
                                            Site(s) Above <b>Will</b> Have Their Meals Cancelled
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 50%; height: 12px">
                                            <asp:Label ID="Label3" runat="server" Text="Reschedule Date:"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 50%; height: 12px">
                                            <asp:TextBox ID="RescheduleDateTextBox" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2" style="height: 10px" valign="top">
                                            <asp:Label ID="Label6" runat="server" Text="Notes:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2" style="height: 10px" valign="top">
                                            <asp:TextBox ID="NotesTextBox" runat="server" TextMode="MultiLine" Width="789px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <cc1:CalendarExtender ID="RescheduleDateCalendarExtender" runat="server" TargetControlID="RescheduleDateTextBox">
                                </cc1:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <table width="100%">
                            <tr>
                                <td style="width: 100%; height: 100px">
                                    <asp:Button ID="SaveGroupExceptionButton" runat="server" Font-Bold="True" OnClick="SaveGroupCancellations_Click"
                                        Text="Save/Edit Group Cancellations" Width="220px" />
                                    <asp:Button ID="CloseGroupCancellationButton" runat="server" Font-Bold="True" Text="Close" />
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="DeliveryEventIDHiddenTextBox" runat="server" />
                        <asp:HiddenField ID="MealDeliveryTypeHiddenTextBox" runat="server" />
                        <asp:HiddenField ID="GroupCancellationRescheduledDateHiddenTextBox" 
                            runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="CancellationRangeDatePanel" runat="server" BackColor="#00C7B3" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" Width="450px"
                        ForeColor="Black">
                        <table>
                            <tr>
                                <td align="center" colspan="2" style="font-weight: bold; height: 21px; text-decoration: underline">
                                    Enter Cancellation Range Parameter(s) Below
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 299px; height: 26px">
                                    <asp:Label ID="ReportStartDateLabel" runat="server" Text="Cancellation Range Start Date:"
                                        Width="182px"></asp:Label>
                                </td>
                                <td align="left" style="width: 283px; height: 26px">
                                    <asp:TextBox ID="CancellationRangeStartDateTextBox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 299px; height: 20px">
                                    <asp:Label ID="Label1" runat="server" Text="Cancellation Range End Date:" Width="182px"></asp:Label>
                                </td>
                                <td align="left" style="width: 283px; height: 20px">
                                    <asp:TextBox ID="CancellationRangeEndDateTextBox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" class="style1">
                                    <asp:Label ID="MealTypeLabel" runat="server" Text="Meal Delivery Type:"></asp:Label>
                                </td>
                                <td align="left" valign="top" class="style2">
                                    <asp:DropDownList ID="MealDeliveryTypeDropDownList" runat="server" Width="205px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="style1" rowspan="3" valign="middle">
                                    Schedule:</td><td align="left" class="style26" valign="top">
                                <asp:CheckBox ID="CACFPCheckbox" Text="CACFP" runat="server">
                                </asp:CheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style26" valign="top">
                                <asp:CheckBox ID="SFSPCheckbox" Text="SFSP" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style26" valign="top">
                                <asp:CheckBox ID="NACheckbox" Text = "N/A" runat="server" />
                            </td>
                        </tr>
                            <tr>
                                <td align="right" style="width: 299px; height: 20px">
                                    <asp:Button ID="CancelButton" runat="server" Font-Bold="True" Text="Cancel" />
                                </td>
                                <td align="left" style="width: 283px; height: 20px">
                                    <asp:Button ID="ContinueCancellationButton" runat="server" Font-Bold="True" OnClick="ContinueButton_Click"
                                        Text="Continue" />
                                </td>
                            </tr>
                        </table>
                        <cc1:CalendarExtender ID="CancellationRangeStartCalendarExtender" runat="server"
                            TargetControlID="CancellationRangeStartDateTextBox">
                        </cc1:CalendarExtender>
                        <cc1:CalendarExtender ID="CancellationRangeEndCalendarExtender" runat="server" TargetControlID="CancellationRangeEndDateTextBox" OnClientShowing="setDate" BehaviorID="CancellationRangeEndCalendarBehaviorID">
                        </cc1:CalendarExtender>
                        <cc1:ModalPopupExtender ID="DayDetailModelPopupExtender" BehaviorID="DayDetailModelPopupExtender"
                            PopupControlID="DayDetailsPanel" runat="server" TargetControlID="DayDetailPopupTriggerButton"
                            OkControlID="CloseDayDetailButton" BackgroundCssClass="modalBackground">
                        </cc1:ModalPopupExtender>
                        <cc1:ModalPopupExtender ID="ContinueModalPopupExtender" BehaviorID="ContinueModalPopupExtender"
                            PopupControlID="CancellationRangeDatePanel" runat="server" TargetControlID="ContinueTriggerButton"
                            OkControlID="CancelButton">
                        </cc1:ModalPopupExtender>
                        <cc1:ModalPopupExtender ID="CancellationModalPopupExtender" runat="server" PopupControlID="GroupExceptionPanel"
                            TargetControlID="CancellationTriggerButton" BehaviorID="CancellationModalPopupExtender"
                            OkControlID="CloseGroupCancellationButton" BackgroundCssClass="modalBackground">
                        </cc1:ModalPopupExtender>
                        <asp:Button ID="DayDetailPopupTriggerButton" runat="server" Text="DayDetailTriggerButton"
                            Style="display: none;" />
                        <asp:Button ID="ContinueTriggerButton" runat="server" Style="display: none;" Text="ContinueTriggerButton" />
                        <asp:Button ID="CancellationTriggerButton" runat="server" Style="display: none;"
                            Text="CancellationTriggerButton" />
                        <asp:Button ID="ClientButton" runat="server" Style="display: none" Text="Launch Modal Popup (Client)" />
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
