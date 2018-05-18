<%@ Page Language="C#" AutoEventWireup="true" Title="CCK Site Details" CodeFile="SiteDetails.aspx.cs"
    EnableEventValidation="false" Inherits="SiteDetails" MasterPageFile="~/Site.master" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="GCFDGlobalsNamespace" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        /* Accordion */
        .accordionHeader
        {
            border: 1px solid #2F4F4F;
            color: white;
            background-color: #2E4d7B;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
        }
        
        #master_content .accordionHeader a
        {
            color: #FFFFFF;
            background: none;
            text-decoration: none;
        }
        
        #master_content .accordionHeader a:hover
        {
            background: none;
            text-decoration: underline;
        }
        
        .accordionHeaderSelected
        {
            border: 1px solid #2F4F4F;
            color: white;
            background-color: #5078B3;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
        }
        
        #master_content .accordionHeaderSelected a
        {
            color: #FFFFFF;
            background: none;
            text-decoration: none;
        }
        
        #master_content .accordionHeaderSelected a:hover
        {
            background: none;
            text-decoration: underline;
        }
        
        .accordionContent
        {
            background-color: #D3DEEF;
            border: 1px dashed #2F4F4F;
            border-top: none;
            padding: 5px;
            padding-top: 10px;
            color: #2E4d7B;
        }
        .style6
        {
            width: 100%;
        }
        .TopSiteInfoLargeColumnTextStyle
        {
            height: 26px;
            width: 40%;
            text-align: left;
        }
        .TopSiteInfoSmallColumnTextStyle
        {
            height: 15px;
            width: 30%;
            text-align: left;
        }
        .TopSiteInfoLargeTextBoxStyle
        {
            width: 375px;
        }
        .PhoneTextBoxStyle
        {
            width: 100px;
            margin-top: 0px;
        }
        .TopSiteInfoColumnLabelStyle
        {
            height: 26px;
            width: 15%;
            text-align: right;
        }
        .TopSiteInfoColumnAccordianLabelStyle
        {
            height: 26px;
            width: 20%;
            text-align: right;
            vertical-align: top;
        }
        .TopSiteInfoAccordianColumnTextStyle
        {
            height: 26px;
            width: 35%;
            text-align: left;
        }
        .TopSiteInfoColumnSmallAccordianLabelStyle
        {
            height: 26px;
            width: 20%;
            text-align: right;
            vertical-align: top;
        }
        .TopSiteInfoSmallAccordianColumnTextStyle
        {
            height: 15px;
            width: 25%;
            text-align: left;
        }
        .TopSiteInfoLargeAccordianTextBoxStyle
        {
            width: 320px;
        }
        .style11
        {
            height: 15px;
            width: 30%;
        }
        .style17
        {
            height: 15px;
        }
        .cssLstQueries
        {
        }
        .style21
        {
            height: 24px;
        }
        
        .popupHover
        {
            color: White;
        }
        .style23
        {
            height: 27px;
        }
        .style24
        {
            height: 26px;
        }
        .style25
        {
            height: 26px;
            width: 132px;
        }
        .modalBackground
        {
            background-color: #e6e6e6;
            filter: alpha(opacity=60);
            opacity: 0.60;
        }
        .style26
        {
            width: 205px;
        }
        .style27
        {
            height: 20px;
            width: 205px;
        }
        .style32
        {
            height: 12px;
        }
        .style37
        {
            width: 3%;
        }
        .style38
        {
            width: 35%;
        }
        .style39
        {
            width: 38%;
        }
        .style40
        {
            height: 12px;
            width: 38%;
        }
        .style41
        {
            width: 3%;
            height: 11px;
        }
        .style1
        {
            text-align: right;
        }
    </style>
    <script type="text/javascript">
        var launch = false;
        var launchDeliveryRecurrencePopup = false;
        var launchDeliveryQuestionPopup = false;
        var launchGroupCancellation = false;
        var launchContinue = false;
        var lauchCancelReschedule = false;




        function launchDeliveryQuestionPopupModal() {
            launchDeliveryQuestionPopup = true;
        }

        function launchDeliveryRecurrencePopupModal() {
            launchDeliveryRecurrencePopup = true;
        }

        function launchGroupCancellationModal() {
            launchGroupCancellation = true;
        }

        function launchCancelRescheduleModal() {
            lauchCancelReschedule = true;
        }

        function launchContinueModal() {
            launchContinue = true;
        }

        function launchModal() {
            launch = true;
        }

        function fnClickOK(sender, e) {
            __doPostBack(sender, e);
        }

        function conditionClick(number, display) {
            var elementName = "ctl00_MainContent_DeliveryRecurrenceDetailPanel";

            document.getElementById(elementName).style.visibility = "visible";
        }

        function pageLoad() {

            if (launchDeliveryQuestionPopup) {
                $find("DeliveryQuestionPopupExtender").show();
            }

            if (lauchCancelReschedule) {
                $find("CancellationRescheduleModalPopupExtender").show();
            }

            if (launch) {
                $find("mpe").show();
            }

            if (launchDeliveryRecurrencePopup) {
                $find("RecurrencePopupExtender").show();
            }

            if (launchGroupCancellation) {
                $find("CancellationModalPopupExtender").show();
            }

            if (launchContinue) {
                $find("ContinueModalPopupExtender").show();
            }
        }
    </script>
    <script type="text/javascript">
        function setRecurrenceEndDateMonth() {
            if (document.getElementById('ctl00_MainContent_RecurrenceEndDateTextBox').value == '') {
                var reportStartDate = document.getElementById('ctl00_MainContent_RecurrenceStartDateTextBox').value;

                reportEndDate = new Date(eval('"' + reportStartDate + '"'));

                $find("RecurrenceEndDateCalendarExtenderBehaviorID").set_selectedDate(reportEndDate);

            }
        }

        function setDefaultServingDay(DeliveryDayDropDownListName) {
            var oDeliveryDay = document.all(DeliveryDayDropDownListName);

            var deliveryDay = oDeliveryDay.options[oDeliveryDay.selectedIndex].text;

            var mealType = document.all('ctl00_MainContent_RecurrenceMealTypeLabel').innerText;

            var servingDayDropDownListName = DeliveryDayDropDownListName.replace("DeliveryDayDropDownList", "ServingDayDropDownList");

            if (mealType == 'Hot' || mealType == "Breakfast" || mealType == "Cold Breakfast") {
                if (deliveryDay == 'Friday') {
                    document.getElementById(servingDayDropDownListName).value = 'Monday';
                }
                else if (deliveryDay == 'Monday') {
                    document.getElementById(servingDayDropDownListName).value = 'Tuesday';
                }
                else if (deliveryDay == 'Tuesday') {
                    document.getElementById(servingDayDropDownListName).value = 'Wednesday';
                }
                else if (deliveryDay == 'Wednesday') {
                    document.getElementById(servingDayDropDownListName).value = 'Thursday';
                }
                else if (deliveryDay == 'Thursday') {
                    document.getElementById(servingDayDropDownListName).value = 'Friday';
                }
            }
            else if (mealType == 'Cold' || mealType == "Lunch Bus" || mealType.indexOf("Locker Mate") != -1) {
                document.getElementById(servingDayDropDownListName).value = deliveryDay;
            }
        }

        //Sets default date for delivery rescheduling popup calendar
        function setCancellationDate(sender, args) {
            var deliveryDate = document.getElementById("ctl00_MainContent_DeliveryDateLabel").innerText;
            deliveryWeekDay = new Date(eval('"' + deliveryDate + '"'));

            if (deliveryWeekDay.getDay() == 5) {
                deliveryWeekDay.setDate(deliveryWeekDay.getDate() + 3)
            }
            else {
                deliveryWeekDay.setDate(deliveryWeekDay.getDate() + 1)
            }

            $find("DeliveryCancellationBehaviorID").set_selectedDate(deliveryWeekDay);
        }

        function setCancellationRangeDate() {
            var reportStartDate = document.getElementById('ctl00_MainContent_CancellationRangeStartDateTextBox').value;

            var reportEndDate = new Date(eval('"' + reportStartDate + '"'));

            $find("CancellationRangeEndCalendar").set_selectedDate(reportEndDate);
        }

        function setCancellationRangeRescheduleDate(sender, args) {
            var reportStartDate = document.getElementById('ctl00_MainContent_CancellationRangeEndDateTextBox').value;

            sel = document.getElementById('ctl00_MainContent_AddedDeliverySiteList');

            var str = sel[sel.length - 1].text;

            var deliveryDate = str.substring(str.lastIndexOf("(") + 1, str.lastIndexOf("(") + 11);

            var reportEndDate = new Date(eval('"' + deliveryDate + '"'));

            reportEndDate.setDate(reportEndDate.getDate() + 1);

            $find("RescheduleDateCalendarExtender").set_selectedDate(reportEndDate);
        }

        function setCancellationIndividualDate(sender, args) {
            var reportStartDate = document.getElementById('ctl00_MainContent_CancellationDateTextBox').value;
            reportEndDate = new Date(eval('"' + reportStartDate + '"'));
            reportEndDate.setDate(reportEndDate.getDate() + 1);
            $find("CalendarExtender9").set_selectedDate(reportEndDate);
        }
    </script>
    <script language="C#" runat="server">

        void QuestionAnswer(Object source, EventArgs e)
        {
            if (EditRecurRadioButton.Checked)
            {
                m_SQL = "SELECT DISTINCT DeliveryRecurrenceID, MealTypeName, MealTypeID, ScheduleTypeID, StartDate, EndDate, CONVERT(varchar, DeliveryRecurrenceLastModified, 121) AS DeliveryRecurrenceLastModified FROM vwDelivery WHERE DeliveryID = " + DeliveryIDHiddenField.Value;
                DataSet deliveryDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                if (deliveryDataSet.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Delivery recurrence that was chosen has been deleted by another user.");
                }
                else
                {
                    ScheduleTypeLabel.Visible = true;
                    SFSPRadioButton.Visible = false;
                    CACFPRadioButton.Visible = false;

                    if (GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "ScheduleTypeID") == "1")
                    {
                        ScheduleTypeLabel.Text = "SFSP";
                    }
                    else if (GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "ScheduleTypeID") == "2")
                    {
                        ScheduleTypeLabel.Text = "CACFP";
                    }
                    else
                    {
                        ScheduleTypeLabel.Text = "N/A";
                    }

                    MealTypeIDHiddenField.Value = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "MealTypeID");

                    DeliveryRecurrenceIDHiddenField.Value = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "DeliveryRecurrenceID");

                    RecurrenceLastModifiedHiddenField.Value = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "DeliveryRecurrenceLastModified");

                    RecurrenceMealTypeLabel.Text = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "MealTypeName");

                    RecurrenceStartDateTextBox.Text = Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "StartDate")).ToString("MM/dd/yyyy");

                    RecurrenceEndDateTextBox.Text = Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "EndDate")).ToString("MM/dd/yyyy");

                    string mealTypeID = GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "MealTypeID");

                    int endDate = Convert.ToInt32(Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "EndDate")).ToString("yyyyMMdd"));
                    int startDate = Convert.ToInt32(Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "StartDate")).ToString("yyyyMMdd"));

                    int permittedStartDate = 0;

                    FillDeliveryRecurrenceDetailInGrid(GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "DeliveryRecurrenceID"));

                    if (DateTime.Now.DayOfWeek.ToString() == "Friday")
                    {
                        permittedStartDate = Convert.ToInt32(DateTime.Now.AddDays(4).ToString("yyyyMMdd"));
                    }
                    else
                    {
                        permittedStartDate = Convert.ToInt32(DateTime.Now.AddDays(2).ToString("yyyyMMdd"));
                    }

                    if (mealTypeID != "10" && mealTypeID != "11" && mealTypeID != "12")
                    {
                        if (startDate < permittedStartDate)
                            RecurrenceStartDateTextBox.Enabled = false;
                        else
                            RecurrenceStartDateTextBox.Enabled = true;

                        if (endDate < permittedStartDate)
                        {
                            RecurrenceEndDateTextBox.Enabled = false;

                            DeleteRecurrenceButton.Enabled = false;

                            SaveDeliveryRecurrenceButton.Enabled = false;

                            foreach (GridViewRow row in DeliveryRecurrenceDetailGridView.Rows)
                            {
                                Panel popupPanel = (Panel)row.FindControl("PopupMenu");

                                popupPanel.Visible = false;
                            }
                        }
                        else
                        {
                            RecurrenceEndDateTextBox.Enabled = true;

                            DeleteRecurrenceButton.Enabled = true;
                        }
                    }

                    MealTypeDropDownList.Visible = false;

                    RecurrenceMealTypeLabel.Visible = true;

                    ClientScript.RegisterStartupScript(GetType(), "key", "launchDeliveryRecurrencePopupModal();", true);
                }
            }
            else
            {
                m_SQL = "SELECT DISTINCT DeliveryDate, MealTypeName FROM vwDelivery WHERE DeliveryID = " + DeliveryIDHiddenField.Value;
                DataSet deliveryDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

                if (deliveryDataSet.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Delivery that was chosen has been edited by another user.  Please try again.");
                }
                else
                {
                    DeleteExceptionButton.Enabled = false;

                    DeliveryDateLabel.Text = Convert.ToDateTime(GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "DeliveryDate")).ToString("MM/dd/yyyy");

                    DeliveryExceptionTypeLabel.Text = "Cancelled " + GCFDGlobals.dbGetValue(deliveryDataSet.Tables[0].Rows[0], "MealTypeName") + " Delivery";

                    DeliveryCancellationTextBox.Text = "";

                    DeliveryCancellationTextBox.Enabled = true;

                    DeliveryEventDetailsLabelLabel.Text = "Delivery Rescheduled Date:";

                    AddExceptionButton.Enabled = true;

                    ClientScript.RegisterStartupScript(GetType(), "key", "launchModal();", true);
                }
            }

            GetCalendarData();
        }

    </script>
    <script type="text/javascript" language="JavaScript">
        function SFSPclick() {
            if (document.getElementById('ctl00_MainContent_SFSPRadioButton').checked) {
                document.getElementById('ctl00_MainContent_CACFPRadioButton').checked = false;
            }
        }

        function CACFPclick() {
            if (document.getElementById('ctl00_MainContent_CACFPRadioButton').checked) {
                document.getElementById('ctl00_MainContent_SFSPRadioButton').checked = false;
            }
        }



        function exceptiononclick() {
            if (document.getElementById('ctl00_MainContent_ExceptionRadioButton').checked) {
                document.getElementById('ctl00_MainContent_EditRecurRadioButton').checked = false;
            }
        }

        function recurrenceonclick() {
            if (document.getElementById('ctl00_MainContent_EditRecurRadioButton').checked) {
                document.getElementById('ctl00_MainContent_ExceptionRadioButton').checked = false;
            }
        } 
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="asm" runat="server" EnablePartialRendering="true" />
    <asp:Panel ID="pnlProgress" runat="server" Style="background-color: #ffffff; display: none;
        width: 400px; text-align: center">
        <div style="padding: 8px">
            <table border="0" cellpadding="2" cellspacing="0" style="width: 100%">
                <tbody>
                    <tr>
                        <td style="width: 50%">
                        </td>
                        <td style="text-align: right">
                            <img id="myAnimatedImage" alt="Loading" src="~/Images/indicator-big.gif" runat="server" />
                        </td>
                        <td style="text-align: left; white-space: nowrap">
                            <span style="font-size: larger; color: #000000; font-weight: bold;">Loading, Please
                                Wait...</span>
                        </td>
                        <td style="width: 50%">
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="SiteIDHiddenField" runat="server" />
    <asp:HiddenField ID="OriginalSiteNameHiddenField" runat="server" />
    <asp:HiddenField ID="SiteModeHiddenField" runat="server" />
    <asp:HiddenField ID="DeliveryIDHiddenField" runat="server" />
    <cc1:ModalPopupExtender ID="mpeProgress" runat="server" TargetControlID="pnlProgress"
        PopupControlID="pnlProgress" BackgroundCssClass="modalBackground" DropShadow="true" />
    <table style="width: 100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td align="left" style="width: 25%">
                <asp:Button ID="PreviousSiteButton" runat="server" Text="<< Previous Site" Font-Bold="true"
                    OnClick="PreviousSiteButton_Click" />
            </td>
            <td align="right" style="width: 25%">
                <asp:Button ID="SaveSiteChangesButton" runat="server" Font-Bold="True" OnClick="SaveSiteChangesButton_Click"
                    Text="Save Site Changes" TabIndex="3" />
            </td>
            <td style="width: 25%">
                <asp:Button ID="DeleteSiteButton" runat="server" Font-Bold="True" OnClick="DeleteSiteButton_Click"
                    Text="Delete Site" TabIndex="4" CausesValidation="false" />
            </td>
            <td style="width: 25%" align="right">
                <asp:Button ID="NextSiteButton" runat="server" Text="Next Site >>" Font-Bold="true"
                    OnClick="NextSiteButton_Click" />
            </td>
        </tr>
    </table>
    <table style="width: 100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td class="TopSiteInfoColumnLabelStyle">
                &nbsp;
            </td>
            <td class="TopSiteInfoLargeColumnTextStyle">
                <asp:RequiredFieldValidator ID="SiteNameFieldValidator" runat="server" ControlToValidate="SiteNameTextBox"
                    ErrorMessage="test" Font-Bold="True" ForeColor="Red" Text="The site name field is required!"></asp:RequiredFieldValidator>
            </td>
            <td class="TopSiteInfoColumnLabelStyle">
                &nbsp;
            </td>
            <td class="TopSiteInfoSmallColumnTextStyle">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="TopSiteInfoColumnLabelStyle">
                <asp:Label ID="SiteNameLabel" runat="server" Text="Site Name:"></asp:Label>
            </td>
            <td class="TopSiteInfoLargeColumnTextStyle">
                <asp:TextBox ID="SiteNameTextBox" runat="server" CssClass="TopSiteInfoLargeTextBoxStyle"
                    TabIndex="5"></asp:TextBox>
            </td>
            <td class="TopSiteInfoColumnLabelStyle">
                <asp:Label ID="Label59" runat="server" Text="Site Phone:"></asp:Label>
            </td>
            <td class="TopSiteInfoSmallColumnTextStyle">
                <asp:TextBox ID="SitePhoneTextBox" runat="server" CssClass="PhoneTextBoxStyle" TabIndex="6"></asp:TextBox><cc1:MaskedEditExtender
                    runat="server" TargetControlID="SitePhoneTextBox" Mask="(999)999-9999" MaskType="Number"
                    InputDirection="LeftToRight" ClearMaskOnLostFocus="false" />
            </td>
        </tr>
        <tr>
            <td class="TopSiteInfoColumnLabelStyle">
                <asp:Label ID="Label7" runat="server" Text="Address:"></asp:Label>
            </td>
            <td class="style10">
                <asp:TextBox ID="SiteAddressTextBox" runat="server" CssClass="TopSiteInfoLargeTextBoxStyle"
                    TabIndex="7"></asp:TextBox>
            </td>
            <td class="TopSiteInfoColumnLabelStyle">
                <asp:Label ID="Label8" runat="server" Text="City:"></asp:Label>
            </td>
            <td class="TopSiteInfoSmallColumnTextStyle">
                <asp:TextBox ID="SiteCityTextBox" runat="server" Width="208px" TabIndex="8"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="TopSiteInfoColumnLabelStyle">
                <asp:Label ID="Label10" runat="server" Text="Zip Code:"></asp:Label>
            </td>
            <td class="style11">
                <asp:TextBox ID="ZipCodeTextBox" runat="server" TabIndex="9"></asp:TextBox>
            </td>
            <td class="TopSiteInfoColumnLabelStyle">
                &nbsp;
            </td>
            <td class="TopSiteInfoSmallColumnTextStyle">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" class="style17" colspan="4">
                <hr />
            </td>
        </tr>
        <tr>
            <td class="TopSiteInfoColumnLabelStyle">
                <asp:Label ID="Label54" runat="server" Text="Delivery Contact Name:"></asp:Label>
            </td>
            <td class="TopSiteInfoLargeColumnTextStyle">
                <asp:TextBox ID="DeliveryContactNameTextBox" runat="server" CssClass="TopSiteInfoLargeTextBoxStyle"
                    TabIndex="10"></asp:TextBox>
            </td>
            <td class="TopSiteInfoColumnLabelStyle">
                <asp:Label ID="Label55" runat="server" Text="Delivery Contact Phone:"></asp:Label>
            </td>
            <td class="TopSiteInfoSmallColumnTextStyle">
                <asp:TextBox ID="DeliveryContactPhoneTextBox" runat="server" TabIndex="11" CssClass="PhoneTextBoxStyle"></asp:TextBox><cc1:MaskedEditExtender
                    ID="MaskedEditExtender1" runat="server" TargetControlID="DeliveryContactPhoneTextBox"
                    Mask="(999)999-9999" MaskType="Number" InputDirection="LeftToRight" ClearMaskOnLostFocus="false" />
            </td>
        </tr>
        <tr>
            <td class="TopSiteInfoColumnLabelStyle">
                <asp:Label ID="Label2" runat="server" Text="Site Route:"></asp:Label>
            </td>
            <td class="TopSiteInfoLargeColumnTextStyle">
                <asp:DropDownList ID="RouteDropDownList" runat="server" CssClass="TopSiteInfoLargeTextBoxStyle"
                    TabIndex="12">
                    <asp:ListItem>North</asp:ListItem>
                    <asp:ListItem>South</asp:ListItem>
                    <asp:ListItem Value="Food Rescue C2">Food Rescue C2</asp:ListItem>
                    <asp:ListItem Value="Cold-CCK">Cold-CCK</asp:ListItem>
                    <asp:ListItem Value="Cold-CCK North">Cold-CCK North</asp:ListItem>
                    <asp:ListItem Value="Cold-CCK South">Cold-CCK South</asp:ListItem>
                    <asp:ListItem Value="N/A">N/A</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="TopSiteInfoColumnLabelStyle">
                <asp:Label ID="Label60" runat="server" Text="Site Type:"></asp:Label>
            </td>
            <td class="TopSiteInfoSmallColumnTextStyle">
                <asp:DropDownList ID="SiteTypeDropDownListBox" runat="server" Width="117px" TabIndex="13">
                    <asp:ListItem>Hot</asp:ListItem>
                    <asp:ListItem>Cold</asp:ListItem>
                    <asp:ListItem Value="Hot/Cold">Hot/Cold</asp:ListItem>
                    <asp:ListItem Value="Pantry">Pantry</asp:ListItem>
                    <asp:ListItem>Lunch Bus</asp:ListItem>
                    <asp:ListItem>Cold-CCK</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center" style="height: 15px;" colspan="4">
                <cc1:Accordion ID="Accordion1" runat="server" FadeTransitions="True" SelectedIndex="0"
                    TransitionDuration="300" HeaderCssClass="accordionHeader" ContentCssClass="accordionContent"
                    Width="100%" Height="1354px">
                    <Panes>
                        <cc1:AccordionPane ID="CACFPContactInformationAccordianPanel" runat="server">
                            <Header>
                                SITE CONTACT INFORMATION (CACFP)</Header>
                            <Content>
                                <table width="100%">
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label11" runat="server" Text="Primary Contact Name:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPPrimaryContactNameTextBox" runat="server" CssClass="TopSiteInfoLargeAccordianTextBoxStyle"
                                                TabIndex="14"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            <asp:Label ID="Label12" runat="server" Text="Primary Contact Phone:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPPrimaryContactPhoneTextBox" runat="server" CssClass="PhoneTextBoxStyle"
                                                TabIndex="15"></asp:TextBox><cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                                    TargetControlID="CACFPPrimaryContactPhoneTextBox" Mask="(999)999-9999" MaskType="Number"
                                                    InputDirection="LeftToRight" ClearMaskOnLostFocus="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label9" runat="server" Text="Primary Contact Email:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPPrimaryContactEmailTextBox" runat="server" Width="225px" TabIndex="16"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            &nbsp;
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <td align="center" colspan="4">
                                        <hr />
                                    </td>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            Alt. Contact Name:
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPAltContactNameTextBox" runat="server" TabIndex="17" CssClass="TopSiteInfoLargeAccordianTextBoxStyle"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            Alt. Contact Phone:
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPAltContactPhoneTextBox" runat="server" TabIndex="18" CssClass="PhoneTextBoxStyle"></asp:TextBox><cc1:MaskedEditExtender
                                                ID="MaskedEditExtender3" runat="server" TargetControlID="CACFPAltContactPhoneTextBox"
                                                Mask="(999)999-9999" MaskType="Number" InputDirection="LeftToRight" ClearMaskOnLostFocus="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            Alt. Contact Email:
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPAltContactEmailTextBox" runat="server" TabIndex="19" CssClass="TopSiteInfoLargeAccordianTextBoxStyle"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            Site Fax:
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPFaxTextBox" runat="server" TabIndex="20" CssClass="PhoneTextBoxStyle"></asp:TextBox><cc1:MaskedEditExtender
                                                ID="MaskedEditExtender4" runat="server" TargetControlID="CACFPFaxTextBox" Mask="(999)999-9999"
                                                MaskType="Number" InputDirection="LeftToRight" ClearMaskOnLostFocus="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label50" runat="server" Text="Emergency Contact Name:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPEmergencyContactNameTextBox" runat="server" TabIndex="21" CssClass="TopSiteInfoLargeAccordianTextBoxStyle"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            <asp:Label ID="Label48" runat="server" Text="Emergency Contact Phone:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPEmergencyContactPhoneTextBox" runat="server" TabIndex="22"
                                                CssClass="PhoneTextBoxStyle"></asp:TextBox><cc1:MaskedEditExtender ID="MaskedEditExtender5"
                                                    runat="server" TargetControlID="CACFPEmergencyContactPhoneTextBox" Mask="(999)999-9999"
                                                    MaskType="Number" InputDirection="LeftToRight" ClearMaskOnLostFocus="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label49" runat="server" Text="Emergency Contact Notes:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPEmergencyContactNotesTextBox" runat="server" TextMode="MultiLine"
                                                CssClass="TopSiteInfoLargeAccordianTextBoxStyle" TabIndex="23"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            &nbsp;
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="SFSPContactInformationAccordianPanel" runat="server">
                            <Header>
                                SITE CONTACT INFORMATION (SFSP)</Header>
                            <Content>
                                <table width="100%">
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label61" runat="server" Text="Primary Contact Name:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPPrimaryContactNameTextBox" runat="server" CssClass="TopSiteInfoLargeAccordianTextBoxStyle"
                                                TabIndex="24"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            <asp:Label ID="Label62" runat="server" Text="Primary Contact Phone:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPPrimaryContactPhoneTextBox" runat="server" CssClass="PhoneTextBoxStyle"
                                                TabIndex="25"></asp:TextBox><cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                                                    TargetControlID="SFSPPrimaryContactPhoneTextBox" Mask="(999)999-9999" MaskType="Number"
                                                    InputDirection="LeftToRight" ClearMaskOnLostFocus="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label63" runat="server" Text="Primary Contact Email:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPPrimaryContactEmailTextBox" runat="server" CssClass="TopSiteInfoLargeAccordianTextBoxStyle"
                                                TabIndex="26"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            &nbsp;
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <td align="center" class="style17" colspan="4">
                                        <hr />
                                    </td>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            Alt. Contact Name:
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPAltContactNameTextBox" runat="server" TabIndex="27" CssClass="TopSiteInfoLargeAccordianTextBoxStyle"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            Alt. Contact Phone:
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPAltContactPhoneTextBox" runat="server" TabIndex="28" CssClass="PhoneTextBoxStyle"></asp:TextBox><cc1:MaskedEditExtender
                                                ID="MaskedEditExtender7" runat="server" TargetControlID="SFSPAltContactPhoneTextBox"
                                                Mask="(999)999-9999" MaskType="Number" InputDirection="LeftToRight" ClearMaskOnLostFocus="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            Alt. Contact Email:
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPAltContactEmailTextBox" runat="server" TabIndex="29" CssClass="TopSiteInfoLargeAccordianTextBoxStyle"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            Site Fax:
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPFaxTextBox" runat="server" TabIndex="30" CssClass="PhoneTextBoxStyle"></asp:TextBox><cc1:MaskedEditExtender
                                                ID="MaskedEditExtender8" runat="server" TargetControlID="SFSPFaxTextBox" Mask="(999)999-9999"
                                                MaskType="Number" InputDirection="LeftToRight" ClearMaskOnLostFocus="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label64" runat="server" Text="Emergency Contact Name:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPEmergencyContactNameTextBox" runat="server" TabIndex="31" CssClass="TopSiteInfoLargeAccordianTextBoxStyle"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            <asp:Label ID="Label65" runat="server" Text="Emergency Contact Phone:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPEmergencyContactPhoneTextBox" runat="server" TabIndex="32" CssClass="PhoneTextBoxStyle"></asp:TextBox><cc1:MaskedEditExtender
                                                ID="MaskedEditExtender9" runat="server" TargetControlID="SFSPEmergencyContactPhoneTextBox"
                                                Mask="(999)999-9999" MaskType="Number" InputDirection="LeftToRight" ClearMaskOnLostFocus="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label66" runat="server" Text="Emergency Contact Notes:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPEmergencyContactNotesTextBox" runat="server" TextMode="MultiLine"
                                                CssClass="TopSiteInfoLargeAccordianTextBoxStyle" TabIndex="33"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane2" runat="server">
                            <Header>
                                SITE CODES AND COMMUNITY INFORMATION
                            </Header>
                            <Content>
                                <table width="100%">
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label56" runat="server" Text="Agency ID:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="AgencyIDTextBox" runat="server" TabIndex="35" Width="225px"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            <asp:Label ID="Label5" runat="server" Text="Program ID: "></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="ProgramIDTextBox" runat="server" TabIndex="36" Width="100px"></asp:TextBox>
                                        </td>
                                        <tr>
                                            <td class="TopSiteInfoColumnAccordianLabelStyle">
                                                <asp:Label ID="Label41" runat="server" Text="ISBE #:"></asp:Label>
                                            </td>
                                            <td class="TopSiteInfoAccordianColumnTextStyle">
                                                <asp:TextBox ID="ISBETextBox" runat="server" TabIndex="37" Width="225px"></asp:TextBox>
                                            </td>
                                            <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                                <asp:Label ID="FEINLabel" runat="server" Text="FEIN #: "></asp:Label>
                                            </td>
                                            <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                                <asp:TextBox ID="FEINTextBox" runat="server" TabIndex="38" Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label14" runat="server" Text="LAH Code:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="LAHCodeTextBox" runat="server" TabIndex="39" Width="225px"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            <asp:Label ID="Label42" runat="server" Text="Nearest School:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="NearestSchoolTextBox" runat="server" Width="150px" TabIndex="40"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label51" runat="server" Text="Community Area:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="CommunityAreaTextBox" runat="server" CssClass="TopSiteInfoLargeAccordianTextBoxStyle"
                                                TabIndex="41"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            <asp:Label ID="Label43" runat="server" Text="Unmet need?"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="UnmetNeedTextBox" runat="server" TabIndex="42" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label67" runat="server" Text="Notes:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="NotesTextBox" runat="server" TextMode="MultiLine" CssClass="TopSiteInfoLargeAccordianTextBoxStyle"
                                                TabIndex="43"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle" valign="bottom">
                                            <asp:Label ID="Label4" runat="server" Text="Check If Active Site:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle" valign="top">
                                            <asp:CheckBox ID="ActiveCheckBox" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="CACFPComplianceInformationAccordionPane" runat="server">
                            <Header>
                                COMPLIANCE INFORMATION (CACFP)
                            </Header>
                            <Content>
                                <table width="100%">
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            Pre Op Visit:
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPPreOpVisitTextBox" runat="server" TabIndex="44"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            First Week Visit:
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPFirstWeekVisitTextBox" runat="server" TabIndex="45"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            First Month Visit:
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPFirstMonthVisitTextBox" runat="server" TabIndex="46"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            Visit 1:
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="Visit1TextBox" runat="server" TabIndex="47"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            Visit 2:
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="Visit2TextBox" runat="server" TabIndex="48"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            Additional Visits:
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPAdditionalVisitsTextBox" runat="server" TabIndex="49" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label46" runat="server" Text="Start Date:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPStartDateTextBox" runat="server" TabIndex="50" Width="150px"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            <asp:Label ID="Label47" runat="server" Text="End Date:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPEndDateTextBox" runat="server" TabIndex="51" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label69" runat="server" Text="Serving Days:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPServingDaysTextBox" runat="server" TabIndex="52" Width="150px"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            <asp:Label ID="Label70" runat="server" Text="Serving Time:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPServingTimeTextBox" runat="server" TabIndex="53" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            Dates of No Service:
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPDatesofNoServiceTextBox" runat="server" TextMode="MultiLine"
                                                TabIndex="54" CssClass="TopSiteInfoLargeAccordianTextBoxStyle"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            <asp:Label ID="Label73" runat="server" Text="Training Date:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPTrainingDateTextBox" runat="server" TabIndex="55" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label74" runat="server" Text="Trainee(s):"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPTraineesTextBox" runat="server" TextMode="MultiLine" TabIndex="56"
                                                CssClass="TopSiteInfoLargeAccordianTextBoxStyle"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            <asp:Label ID="Label75" runat="server" Text="Notes:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPNotesTextBox" runat="server" TextMode="MultiLine" TabIndex="57"
                                                Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label71" runat="server" Text="Monitor:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="CACFPMonitorTextBox" runat="server" TabIndex="52" Width="150px"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            &nbsp;
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <cc1:CalendarExtender ID="CACFPPreOpVisitTextBoxCalendarExtender" runat="server"
                                                TargetControlID="CACFPPreOpVisitTextBox">
                                            </cc1:CalendarExtender>
                                            <cc1:CalendarExtender ID="CACFPFirstWeekVisitTextBoxCalendarExtender" runat="server"
                                                TargetControlID="CACFPFirstWeekVisitTextBox">
                                            </cc1:CalendarExtender>
                                            <cc1:CalendarExtender ID="CACFPFirstMonthVisitTextBoxCalendarExtender" runat="server"
                                                TargetControlID="CACFPFirstMonthVisitTextBox">
                                            </cc1:CalendarExtender>
                                            <cc1:CalendarExtender ID="Visit1TextBoxCalendarExtender" runat="server" TargetControlID="Visit1TextBox">
                                            </cc1:CalendarExtender>
                                            <cc1:CalendarExtender ID="Visit2TextBoxCalendarExtender" runat="server" TargetControlID="Visit2TextBox">
                                            </cc1:CalendarExtender>
                                            <cc1:CalendarExtender ID="CACFPStartDateTextBoxCalendarExtender" runat="server" TargetControlID="CACFPStartDateTextBox">
                                            </cc1:CalendarExtender>
                                            <cc1:CalendarExtender ID="CACFPEndDateTextBoxCalendarExtender" runat="server" TargetControlID="CACFPEndDateTextBox">
                                            </cc1:CalendarExtender>
                                            <cc1:CalendarExtender ID="CACFPTrainingDateTextBoxCalendarExtender" runat="server"
                                                TargetControlID="CACFPTrainingDateTextBox">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                </table>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="SFSPComplianceInformationAccordionPane" runat="server">
                            <Header>
                                COMPLIANCE INFORMATION (SFSP)
                            </Header>
                            <Content>
                                <table width="100%">
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            Pre Op Visit:
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPPreOpVisitTextBox" runat="server" TabIndex="58"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            First Week Visit:
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPFirstWeekVisitTextBox" runat="server" TabIndex="59"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            First Month Visit:
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPFirstMonthVisitTextBox" runat="server" TabIndex="60"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            Additional Visits:
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPAdditionalVisitsTextBox" runat="server" TabIndex="61"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label52" runat="server" Text="Start Date:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPStartDateTextBox" runat="server" TabIndex="62"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            <asp:Label ID="Label53" runat="server" Text="End Date:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPEndDateTextBox" runat="server" TabIndex="63"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label57" runat="server" Text="Lunch Serving Days:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPLunchServingDaysTextBox" runat="server" TabIndex="64" CssClass="TopSiteInfoLargeAccordianTextBoxStyle"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            <asp:Label ID="Label58" runat="server" Text="Lunch Serving Time:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPLunchServingTimeTextBox" runat="server" TabIndex="65"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label16" runat="server" Text="Breakfast Serving Days:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPBreakfastServingDaysTextBox" runat="server" TabIndex="66" CssClass="TopSiteInfoLargeAccordianTextBoxStyle"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            <asp:Label ID="Label35" runat="server" Text="Breakfast Serving Time:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPBreakfastServingTimeTextBox" runat="server" TabIndex="67"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            Dates of No Service:
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPDatesOfNoServiceTextBox" runat="server" TextMode="MultiLine"
                                                CssClass="TopSiteInfoLargeAccordianTextBoxStyle" TabIndex="68"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            <asp:Label ID="Label44" runat="server" Text="Training Date:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPTrainingDateTextBox" runat="server" TabIndex="69"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label45" runat="server" Text="Trainee(s):"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPTraineesTextBox" runat="server" TextMode="MultiLine" TabIndex="70"
                                                CssClass="TopSiteInfoLargeAccordianTextBoxStyle"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            <asp:Label ID="Label68" runat="server" Text="Notes:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPNotesTextBox" runat="server" TextMode="MultiLine" TabIndex="71"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TopSiteInfoColumnAccordianLabelStyle">
                                            <asp:Label ID="Label72" runat="server" Text="Monitor:"></asp:Label>
                                        </td>
                                        <td class="TopSiteInfoAccordianColumnTextStyle">
                                            <asp:TextBox ID="SFSPMonitorTextBox" runat="server" TabIndex="52" Width="150px"></asp:TextBox>
                                        </td>
                                        <td class="TopSiteInfoColumnSmallAccordianLabelStyle">
                                            &nbsp;
                                        </td>
                                        <td class="TopSiteInfoSmallAccordianColumnTextStyle">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="SFSPPreOpVisitTextBox">
                                            </cc1:CalendarExtender>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="SFSPFirstWeekVisitTextBox">
                                            </cc1:CalendarExtender>
                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="SFSPFirstMonthVisitTextBox">
                                            </cc1:CalendarExtender>
                                            <cc1:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="SFSPStartDateTextBox">
                                            </cc1:CalendarExtender>
                                            <cc1:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="SFSPEndDateTextBox">
                                            </cc1:CalendarExtender>
                                            <cc1:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="SFSPTrainingDateTextBox">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                </table>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane69" runat="server">
                            <Header>
                                COMMENTS
                            </Header>
                            <Content>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <div id="CommentsAddDiv" visible="true" style="display: inline; width: 100%;" runat="server">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton ID="CreateNewCommentButton" runat="server" Font-Bold="True" OnClick="CreateNewCommentButton_Click"
                                                                OnClientClick="onInvoke()" Text="Click Here To Add New Comment" Width="297px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div id="CommentsDiv" style="display: none; width: 100%; height: 1px;" runat="server">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="height: 5px;" align="center">
                                                            <asp:Panel ID="Panel2" runat="server">
                                                                <table width="100%" style="text-align: center">
                                                                    <tr align="center">
                                                                        <td style="font-weight: bold; width: 20%" align="center">
                                                                            Comment Date
                                                                        </td>
                                                                        <td style="font-weight: bold; width: 60%" align="center">
                                                                            Comment
                                                                        </td>
                                                                        <td style="font-weight: bold; width: 20%" align="center">
                                                                            User Name
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="4">
                                                            <asp:GridView ID="CommentsGridView" runat="server" AutoGenerateColumns="False" OnRowEditing="CommentsGridView_RowEditing"
                                                                OnRowCancelingEdit="CommentsGridView_RowCancelingEdit" OnRowUpdating="CommentsGridView_RowUpdating"
                                                                EmptyDataText="You have deleted all comment records" OnRowDeleting="CommentsGridView_RowDeleting"
                                                                OnRowCommand="CommentsGridView_RowCommand" OnSelectedIndexChanged="CommentsGridView_SelectedIndexChanged"
                                                                Width="100%" ShowHeader="False">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Panel ID="Panel1" runat="server">
                                                                                <table width="100%" style="text-align: center">
                                                                                    <tr align="center">
                                                                                        <td align="center" style="width: 20%">
                                                                                            <asp:Label ID="CommentIDLabel1" runat="server" Text='<%# Eval("CommentID") %>' Visible="False"></asp:Label><asp:Label
                                                                                                ID="Label6" runat="server" Text='<%# Eval("CommentDate") %>' Width="150px"></asp:Label>
                                                                                        </td>
                                                                                        <td align="center" style="width: 60%">
                                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Comment") %>' Width="400px"></asp:Label>
                                                                                        </td>
                                                                                        <td align="center" style="width: 20%">
                                                                                            <asp:Label ID="Label13" runat="server" Text='<%# Eval("UserName") %>' Width="150px"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                            <cc1:HoverMenuExtender ID="HoverMenuExtender1" runat="server" HoverCssClass="popupHover"
                                                                                PopupControlID="PopupMenu" TargetControlID="Panel1" PopupPosition="Left">
                                                                            </cc1:HoverMenuExtender>
                                                                            <asp:Panel ID="PopupMenu" runat="server" CssClass="popupMenu" BackColor="White" BorderColor="Black"
                                                                                BorderWidth="1px">
                                                                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="AddNew" Text="Add New"
                                                                                    ForeColor="Blue"></asp:LinkButton>
                                                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" Text="Edit" ForeColor="Blue"></asp:LinkButton>
                                                                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Delete" Text="Delete"
                                                                                    ForeColor="Blue"></asp:LinkButton>
                                                                            </asp:Panel>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:Panel ID="Panel1" runat="server">
                                                                                <table width="100%" style="text-align: center">
                                                                                    <tr align="center">
                                                                                        <td align="center" style="width: 20%">
                                                                                            <asp:Label ID="CommentIDLabel" runat="server" Text='<%# Eval("CommentID") %>' Visible="False"></asp:Label>
                                                                                            <asp:Label ID="CommentDateLabel" runat="server" Text='<%# Eval("CommentDate") %>'
                                                                                                Width="150px"></asp:Label>
                                                                                        </td>
                                                                                        <td align="center" style="width: 60%">
                                                                                            <asp:TextBox ID="CommentTextBox" runat="server" TextMode="MultiLine" Text='<%# Eval("Comment") %>'
                                                                                                Width="400px"></asp:TextBox>
                                                                                        </td>
                                                                                        <td align="center" style="width: 20%">
                                                                                            <asp:Label ID="CommentUserNameLabel" runat="server" Text='<%# Eval("UserName") %>'
                                                                                                Width="150px"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                            <cc1:HoverMenuExtender ID="HoverMenuExtender2" runat="server" HoverCssClass="popupHover"
                                                                                PopupControlID="PopupMenu" PopupPosition="Right" TargetControlID="Panel1">
                                                                            </cc1:HoverMenuExtender>
                                                                            <asp:Panel ID="PopupMenu" runat="server" CssClass="popupMenu" BackColor="White" BorderColor="Black"
                                                                                BorderWidth="1px">
                                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                                                                                    Text="Update" ForeColor="Blue"></asp:LinkButton>
                                                                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    Text="Cancel" ForeColor="Blue"></asp:LinkButton>
                                                                            </asp:Panel>
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                        <tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </Content>
                        </cc1:AccordionPane>
                    </Panes>
                </cc1:Accordion>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4" style="width: 100%; height: 9px" valign="top">
                <asp:Button ID="CreateNewRecurrenceButton" runat="server" Font-Bold="True" OnClick="CreateNewRecurrenceButton_Click"
                    Text="Click Here To Create New Delivery Recurrence" TabIndex="72" Width="322px"
                    OnClientClick="onInvoke()" />
                <asp:Button ID="CreateRangeCancellationButton" runat="server" OnClick="CreateRangeCancellationButton_Click"
                    Text="Cancel/Reschedule Deliveries For A Date Range" Enabled="True" Font-Bold="True"
                    OnClientClick="onInvoke()" Width="329px" />
                <asp:Button ID="CancelRescheduleDeliveriesButton" runat="server" OnClick="CancelRescheduleDeliveriesButton_Click"
                    Text="Cancel/Reschedule Individual Deliveries" Enabled="True" Font-Bold="True"
                    OnClientClick="onInvoke()" Width="329px" />
                <br />
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Click Links Below To Delete/Create Cancelled Delivery Or Edit Existing Delivery Recurrence"
                    Font-Size="Smaller" Width="580px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4" style="border-style: solid solid none solid; border-width: thin;
                border-color: #FFFFFF; width: 100%;" valign="top">
                <table id="CalendarNavigationTable" width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 25%" align="left" bgcolor="Silver" class="style25">
                            &nbsp<asp:LinkButton ID="PreviousMonthLinkButton" runat="server" ForeColor="#004D45"
                                Font-Bold="true" OnClick="PreviousMonthLinkButton_Click">LinkButton</asp:LinkButton>
                            <asp:HiddenField ID="PreviousMonthHiddenField" runat="server" />
                        </td>
                        <td style="width: 25%" align="right" bgcolor="Silver" class="style24">
                            <asp:DropDownList ID="drpCalMonth" runat="Server" OnSelectedIndexChanged="drpCalMonth_SelectedIndexChanged"
                                AutoPostBack="true" ForeColor="#004D45" Font-Bold="True" Width="105px" BackColor="Silver"
                                Font-Names="Arial Black">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 25%" align="left" bgcolor="Silver" class="style24">
                            <asp:DropDownList ID="drpCalYear" runat="Server" OnSelectedIndexChanged="drpCalYear_SelectedIndexChanged"
                                ForeColor="#004D45" Font-Bold="True" AutoPostBack="true" BackColor="Silver" Font-Names="Arial Black">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 25%" align="right" bgcolor="Silver" class="style24">
                            <asp:HiddenField ID="NextMonthHiddenField" runat="server" />
                            <asp:LinkButton ID="NextMonthLinkButton" runat="server" ForeColor="#004D45" Font-Bold="true"
                                OnClick="NextMonthLinkButton_Click">LinkButton</asp:LinkButton>&nbsp
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="width: 100%; border-style: none solid solid solid; border-width: thin;
            border-color: #FFFFFF;">
            <td colspan="4" valign="top" style="border-style: none solid solid solid; border-width: thin;
                border-color: #FFFFFF;">
                <asp:Calendar ID="SiteDeliveryCalendar" runat="server" Width="100%" OnDayRender="SiteDeliveryCalendar_DayRender"
                    OnVisibleMonthChanged="SiteDeliveryCalendar_VisibleMonthChanged" FirstDayOfWeek="Monday"
                    DayNameFormat="Full" NextMonthText=">" PrevMonthText="<" SelectMonthText="" UseAccessibleHeader="False"
                    NextPrevFormat="ShortMonth" ShowTitle="False" CellPadding="0">
                    <DayStyle BackColor="Transparent" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                        ForeColor="White" />
                    <NextPrevStyle ForeColor="#004D45" />
                    <OtherMonthDayStyle BackColor="#E0E0E0" ForeColor="#004D45" />
                    <TitleStyle Font-Bold="True" ForeColor="#004D45" />
                </asp:Calendar>
            </td>
        </tr>
        <tr style="width: 100%; border-style: none solid solid solid; border-width: thin;
            border-color: #FFFFFF;">
            <td align="center" colspan="4">
                <asp:Panel ID="CalendarExceptionPanel" runat="server" Width="64%" BackColor="#00C7B3"
                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black">
                    <table style="width: 100%">
                        <tr>
                            <td align="right" style="width: 50%">
                                <asp:Label ID="DateOfExceptionLabel" runat="server" Font-Bold="true" Text="Date of Delivery Event:"></asp:Label>
                            </td>
                            <td align="left" style="width: 50%">
                                <asp:Label ID="DeliveryDateLabel" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="ExceptionTypeLabel" runat="server" Font-Bold="true" Text="Event Type:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="DeliveryExceptionTypeLabel" Font-Bold="true" runat="server" Text="Label"
                                    Width="298px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="DeliveryEventDetailsLabelLabel" runat="server" Text="Delivery Rescheduled Date:"
                                    Font-Bold="true"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="DeliveryCancellationTextBox" runat="server" Width="187px"></asp:TextBox>
                            </td>
                            <cc1:CalendarExtender ID="CalendarExtender11" runat="server" BehaviorID="DeliveryCancellationBehaviorID"
                                TargetControlID="DeliveryCancellationTextBox" OnClientShowing="setCancellationDate">
                            </cc1:CalendarExtender>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <asp:Label ID="NotesLabel" runat="server" Text="Notes:" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="DeliveryExceptionNotes" runat="server" TextMode="MultiLine" Width="308px"
                                    Height="77px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:HiddenField ID="ParentDeliveryIDHiddenField" runat="server" />
                                <asp:HiddenField ID="GroupCancellationIDHiddenField" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="DeleteExceptionButton" runat="server" Font-Bold="True" OnClick="DeleteExceptionButton_Click"
                                    Text="Delete Event" OnClientClick="return confirm('Do you really want to delete this cancellation?');" />
                                <asp:Button ID="AddExceptionButton" runat="server" Font-Bold="True" Text="Add/Modify Event"
                                    OnClick="AddExceptionButton_Click" />
                                <asp:Button ID="CancelExceptionButton" runat="server" Font-Bold="True" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4" style="width: 100%; height: 12px">
                <asp:Panel ID="DeliveryQuestionPanel" runat="server" BackColor="#00C7B3" BorderColor="Black"
                    BorderStyle="Solid" BorderWidth="1px" Width="350px" ForeColor="Black">
                    <table width="100%">
                        <tr>
                            <td align="right" colspan="1" style="width: 30%; height: 8px" valign="bottom">
                            </td>
                            <td align="left" style="width: 70%; height: 8px" valign="bottom">
                                <asp:RadioButton ID="EditRecurRadioButton" runat="server" Width="226px" Font-Bold="true"
                                    Checked="True" Text="View/Edit Delivery Recurrence" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="1" style="width: 30%; height: 22px" valign="bottom">
                            </td>
                            <td align="left" style="width: 70%; height: 22px" valign="bottom">
                                <asp:RadioButton ID="ExceptionRadioButton" Font-Bold="true" runat="server" Text="Create Delivery Exception"
                                    Width="225px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2" style="height: 1px" valign="bottom">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="height: 8px" valign="bottom">
                                <asp:Button ID="DeliveryQuestionOKButton" runat="server" Font-Bold="True" OnClick="QuestionAnswer"
                                    Text="OK" Width="72px" OnClientClick="onInvoke()" />
                                <asp:Button ID="CancelDeliveryQuestionButton" runat="server" Font-Bold="True" Width="72px"
                                    Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4" class="style6">
                <asp:Panel ID="DeliveryRecurrencePanel" runat="server" BackColor="#00C7B3" BorderColor="Black"
                    BorderStyle="Solid" BorderWidth="1px" Width="500px" ForeColor="Black">
                    <table style="width: 100%;">
                        <tr style="width: 100%;">
                            <td align="right" valign="top" style="padding: 0px; border-spacing: 0px; width: 50%">
                                <asp:Label ID="Label83" runat="server" Text="Meal Type:" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            </td>
                            <td align="left" valign="top" style="padding: 0px; border-spacing: 0px; width: 50%">
                                <asp:Label ID="RecurrenceMealTypeLabel" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                <asp:DropDownList ID="MealTypeDropDownList" runat="server" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="width: 100%;">
                             <td align="right" valign="top" style="padding: 0px; border-spacing: 0px; width: 50%">
                                <asp:Label ID="Label18" runat="server" Text="Schedule:" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            </td>
                            <td align="left" valign="top" style="padding: 0px; border-spacing: 0px; width: 50%">
                                <asp:Label ID="ScheduleTypeLabel" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label><asp:RadioButton
                                    ID="CACFPRadioButton" Text="CACFP" runat="server" Font-Bold = "true" Style="padding-right: 5px" />
                                <asp:RadioButton ID="SFSPRadioButton" Text="SFSP" runat="server" Font-Bold = "true" />
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%;">                        
                        <tr>
                            <td align="right" class="style21">
                                <asp:Label ID="Label85" runat="server" Font-Bold="true" Text="Start Date:"></asp:Label>
                            </td>
                            <td align="left" class="style21">
                                <asp:TextBox ID="RecurrenceStartDateTextBox" runat="server" Width="80px"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender5" runat="server" BehaviorID="RecurrenceStartDateCalendarExtenderBehaviorID"
                                    TargetControlID="RecurrenceStartDateTextBox">
                                </cc1:CalendarExtender>
                            </td>
                            <td align="right" class="style21">
                                <asp:Label ID="Label84" runat="server" Font-Bold="true" Text="End Date:"></asp:Label>
                            </td>
                            <td align="left" class="style21">
                                <asp:TextBox ID="RecurrenceEndDateTextBox" runat="server" Width="80px"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender10" runat="server" BehaviorID="RecurrenceEndDateCalendarExtenderBehaviorID"
                                    OnClientShowing="setRecurrenceEndDateMonth" TargetControlID="RecurrenceEndDateTextBox">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <asp:Button ID="SaveDeliveryRecurrenceButton" runat="server" Font-Bold="True" OnClick="SaveDeliveryRecurrenceButton_Click"
                                    OnClientClick="onInvoke()" Text="Save Start/End Date Changes" Width="297px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <hr />
                            </td>
                        </tr>
                    </table>
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div id="DeliveryRecurrenceDetailPanel" runat="server">
                                <table style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px;
                                    border-top-style: none; padding-top: 0px; border-right-style: none; border-left-style: none;
                                    border-bottom-style: none; width: 100%;" align="center">
                                    <tr>
                                        <td style="height: 5px;" align="center">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <table width="100%" style="text-align: center">
                                                    <tr>
                                                        <td align="center" colspan="3" style="font-weight: bold; text-decoration: underline;">
                                                            Delivery Recurrence Detail
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td style="font-weight: bold;">
                                                            Meal Count
                                                        </td>
                                                        <td style="font-weight: bold;">
                                                            Delivery Day
                                                        </td>
                                                        <td style="font-weight: bold;">
                                                            Serving Day
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 5px;" align="center">
                                            <asp:GridView ID="DeliveryRecurrenceDetailGridView" runat="server" AutoGenerateColumns="False"
                                                OnRowEditing="DeliveryRecurrenceDetailGridView_RowEditing" OnRowCancelingEdit="DeliveryRecurrenceDetailGridView_RowCancelingEdit"
                                                OnRowUpdating="DeliveryRecurrenceDetailGridView_RowUpdating" EmptyDataText="You have deleted all records in the Delivery Recurrence"
                                                OnRowDeleting="DeliveryRecurrenceDetailGridView_RowDeleting" OnRowCommand="DeliveryRecurrenceDetailGridView_RowCommand"
                                                OnSelectedIndexChanged="DeliveryRecurrenceDetailGridView_SelectedIndexChanged"
                                                Width="100%" ShowHeader="False">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <table width="100%" style="text-align: center">
                                                                    <tr align="center">
                                                                        <td align="center" style="width: 30%">
                                                                            <asp:Label ID="DeliveryRecurrenceDetailLastModifiedLabel1" runat="server" Text='<%# Eval("LastModified") %>'
                                                                                Visible="False"></asp:Label>
                                                                            <asp:Label ID="DeliveryRecurrenceDetailIDLabel1" runat="server" Text='<%# Eval("DeliveryRecurrenceDetailID") %>'
                                                                                Visible="False"></asp:Label>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("MealCount") %>'></asp:Label>
                                                                        </td>
                                                                        <td align="center" style="width: 35%">
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("DeliveryDay") %>'></asp:Label>
                                                                        </td>
                                                                        <td align="center" style="width: 35%">
                                                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("ServingDay") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                            <cc1:HoverMenuExtender ID="HoverMenuExtender1" runat="server" HoverCssClass="popupHover"
                                                                PopupControlID="PopupMenu" TargetControlID="Panel1" PopupPosition="Left">
                                                            </cc1:HoverMenuExtender>
                                                            <asp:Panel ID="PopupMenu" runat="server" CssClass="popupMenu" BackColor="White" BorderColor="Black"
                                                                BorderWidth="1px">
                                                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="AddNew" Text="Add New"
                                                                    ForeColor="Blue"></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" Text="Edit" ForeColor="Blue"></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Delete" Text="Delete"
                                                                    ForeColor="Blue"></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <table width="100%" style="text-align: center">
                                                                    <tr align="center">
                                                                        <td align="center" style="width: 30%">
                                                                            <asp:Label ID="DeliveryRecurrenceDetailLastModifiedLabel" runat="server" Text='<%# Eval("LastModified") %>'
                                                                                Visible="false"></asp:Label>
                                                                            <asp:Label ID="DeliveryRecurrenceDetailIDLabel" runat="server" Text='<%# Eval("DeliveryRecurrenceDetailID") %>'
                                                                                Visible="false"></asp:Label>
                                                                            <asp:TextBox ID="MealCountTextBox" runat="server" Width="40px" Text='<%# Eval("MealCount") %>'></asp:TextBox>
                                                                        </td>
                                                                        <td align="center" style="width: 35%">
                                                                            <asp:DropDownList ID="DeliveryDayDropDownList" runat="server" SelectedValue='<%# Eval("DeliveryDay") %>'>
                                                                                <asp:ListItem Value="Monday">Monday</asp:ListItem>
                                                                                <asp:ListItem Value="Tuesday">Tuesday</asp:ListItem>
                                                                                <asp:ListItem Value="Wednesday">Wednesday</asp:ListItem>
                                                                                <asp:ListItem Value="Thursday">Thursday</asp:ListItem>
                                                                                <asp:ListItem Value="Friday">Friday</asp:ListItem>
                                                                                <asp:ListItem Value="Saturday">Saturday</asp:ListItem>
                                                                                <asp:ListItem Value="Sunday">Sunday</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td align="center" style="width: 35%">
                                                                            <asp:DropDownList ID="ServingDayDropDownList" runat="server" SelectedValue='<%# Eval("ServingDay") %>'>
                                                                                <asp:ListItem Value="Monday">Monday</asp:ListItem>
                                                                                <asp:ListItem Value="Tuesday">Tuesday</asp:ListItem>
                                                                                <asp:ListItem Value="Wednesday">Wednesday</asp:ListItem>
                                                                                <asp:ListItem Value="Thursday">Thursday</asp:ListItem>
                                                                                <asp:ListItem Value="Friday">Friday</asp:ListItem>
                                                                                <asp:ListItem Value="Saturday">Saturday</asp:ListItem>
                                                                                <asp:ListItem Value="Sunday">Sunday</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                            <cc1:HoverMenuExtender ID="HoverMenuExtender2" runat="server" HoverCssClass="popupHover"
                                                                PopupControlID="PopupMenu" PopupPosition="Right" TargetControlID="Panel1">
                                                            </cc1:HoverMenuExtender>
                                                            <asp:Panel ID="PopupMenu" runat="server" CssClass="popupMenu" BackColor="White" BorderColor="Black"
                                                                BorderWidth="1px">
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                                                                    Text="Update" ForeColor="Blue"></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                    Text="Cancel" ForeColor="Blue"></asp:LinkButton>
                                                            </asp:Panel>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <table width="100%">
                                <tr>
                                    <td align="right" style="text-align: center">
                                        <asp:HiddenField ID="DeliveryRecurrenceIDHiddenField" runat="server" />
                                        <asp:HiddenField ID="RecurrenceModeHiddenField" runat="server" />
                                        <asp:HiddenField ID="MealTypeIDHiddenField" runat="server" />
                                        <asp:HiddenField ID="RecurrenceDetailModeHiddenField" runat="server" />
                                        <asp:HiddenField ID="RecurrenceLastModifiedHiddenField" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="style23">
                                        <asp:Button ID="DeleteRecurrenceButton" runat="server" Font-Bold="True" OnClick="DeleteRecurrenceButton_Click"
                                            OnClientClick="return confirm('Do you really want to delete this recurrence?');onInvoke()"
                                            Text="Delete Recurrence" Width="151px" />
                                        <asp:Button ID="CloseRecurrenceButton" runat="server" Font-Bold="True" OnClick="CloseRecurrenceButton_Click"
                                            OnClientClick="onInvoke()" Text="Close/Cancel" Width="151px" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="4" style="height: 22px;">
                <asp:Panel ID="GroupExceptionPanel" runat="server" BackColor="#00C7B3" Width="716px"
                    ForeColor="Black">
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <table style="width: 98%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right" class="style39">
                                        <asp:Label ID="Label76" runat="server" Font-Bold="true" Text="Start of Cancellation Date Range:"></asp:Label>
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="CancellationRangeStartDateLabel" runat="server" Font-Bold="true" Text="Label"
                                            Width="156px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="style39">
                                        <asp:Label ID="Label77" runat="server" Font-Bold="true" Text="End of Cancellation Date Range:"></asp:Label>
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="CancellationRangeEndDateLabel" runat="server" Font-Bold="true" Text="Label"
                                            Width="156px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="style39">
                                        <asp:Label ID="Label78" runat="server" Font-Bold="true" Text="Meal Type:"></asp:Label>
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="CancellationMealTypeLabel" runat="server" Font-Bold="true" Text="Label"
                                            Width="156px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="style39">
                                        <asp:Label ID="Label15" runat="server" Font-Bold="true" Text="Schedule:"></asp:Label>
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="CancellationRangeScheduleTypeLabel" runat="server" Font-Bold="true"
                                            Text="Label" Width="156px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" rowspan="4" class="style39">
                                        <div id="RemovedDiv" style="overflow: auto; width: 325px; height: 125px">
                                            <asp:ListBox ID="RemovedDeliverySiteList" runat="server" SelectionMode="Multiple">
                                            </asp:ListBox>
                                        </div>
                                    </td>
                                    <td align="left" class="style37">
                                    </td>
                                    <td align="left" rowspan="4" class="style38">
                                        <div id="AddedDiv" runat="server" style="overflow-x: auto; width: 325px; height: 125px;">
                                            <asp:ListBox ID="AddedDeliverySiteList" runat="server" CssClass="cssLstQueries" Height="125px"
                                                SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="style37">
                                        <asp:Button ID="RemoveExceptionButton0" runat="server" Height="26px" OnClick="Button2_Click"
                                            Text="&lt;-" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="style41">
                                        <asp:Button ID="AddExceptionButton0" runat="server" OnClick="Button1_Click" Text="-&gt;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="style37">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="style39">
                                        Site(s) Above <b>Will Not</b> Have Their Meals Cancelled
                                    </td>
                                    <td align="center" class="style37">
                                        &nbsp;
                                    </td>
                                    <td align="center" class="style38">
                                        Site(s) Above <b>Will</b> Have Their Meals Cancelled
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="style40">
                                        <asp:Label ID="Label79" runat="server" Text="Reschedule Date:" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="left" class="style32" colspan="2">
                                        <asp:TextBox ID="RescheduleDateTextBox" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="3" style="height: 10px" valign="top">
                                        <asp:Label ID="Label80" runat="server" Text="Notes:"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="3" style="height: 10px" valign="top">
                                        <asp:TextBox ID="NotesTextBox0" runat="server" TextMode="MultiLine" Width="704px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <cc1:CalendarExtender ID="RescheduleDateCalendarExtender" runat="server" BehaviorID="RescheduleDateCalendarExtender"
                                TargetControlID="RescheduleDateTextBox" OnClientShowing="setCancellationRangeRescheduleDate">
                            </cc1:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:Button ID="SaveGroupExceptionButton" runat="server" Font-Bold="True" OnClick="SaveGroupCancellations_Click"
                                    OnClientClick="onInvoke()" Text="Save/Edit Group Cancellations" Width="220px" />
                                <asp:Button ID="CloseGroupCancellationButton" runat="server" Font-Bold="True" Text="Close" />
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="DeliveryEventIDHiddenTextBox" runat="server" />
                    <asp:HiddenField ID="MealDeliveryTypeHiddenTextBox" runat="server" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="4" style="height: 22px;">
                <asp:Panel ID="CancellationRangeDatePanel" runat="server" BackColor="#00C7B3" BorderColor="Black"
                    BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" HorizontalAlign="Center"
                    Width="50%">
                    <table>
                        <tr>
                            <td align="center" colspan="2" style="font-weight: bold; text-decoration: underline">
                                Enter Cancellation Range Parameter(s) Below
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 50%;">
                                <asp:Label ID="ReportStartDateLabel" Font-Bold="true" runat="server" Text="Cancellation Range Start Date:"
                                    Width="195px"></asp:Label>
                            </td>
                            <td align="left" class="style26">
                                <asp:TextBox ID="CancellationRangeStartDateTextBox" Font-Bold="true" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 50%;">
                                <asp:Label ID="Label81" runat="server" Font-Bold="true" Text="Cancellation Range End Date:"
                                    Width="182px"></asp:Label>
                            </td>
                            <td align="left" class="style26">
                                <asp:TextBox ID="CancellationRangeEndDateTextBox" Font-Bold="true" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style1" valign="top">
                                <asp:Label ID="MealTypeLabel" Font-Bold="true" runat="server" Text="Meal Delivery Type:"></asp:Label>
                            </td>
                            <td align="left" class="style26" valign="top">
                                <asp:DropDownList ID="MealDeliveryTypeDropDownList" runat="server" Width="205px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style1" valign="middle" rowspan="3">
                                <asp:Label ID="Label17" Font-Bold="true" runat="server" Text="Schedule:"></asp:Label>
                            </td>
                            <td align="left" class="style26" valign="top">
                                <asp:CheckBox ID="CACFPCheckbox" Text="CACFP" runat="server" 
                                    style="font-weight: 700">
                                </asp:CheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style26" valign="top">
                                <asp:CheckBox ID="SFSPCheckbox" Text="SFSP" runat="server" 
                                    style="font-weight: 700" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style26" valign="top">
                                <asp:CheckBox ID="NACheckbox" Text = "N/A" runat="server" 
                                    style="font-weight: 700" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 299px; height: 20px">
                                <asp:Button ID="CancelButton" runat="server" Font-Bold="True" Text="Cancel" />
                            </td>
                            <td align="left" class="style27">
                                <asp:Button ID="ContinueCancellationButton" runat="server" Font-Bold="True" OnClick="ContinueButton_Click"
                                    OnClientClick="onInvoke()" Text="Continue" />
                            </td>
                        </tr>
                    </table>
                    <cc1:ModalPopupExtender ID="mpe" runat="server" BehaviorID="mpe" TargetControlID="ClientButton"
                        PopupControlID="CalendarExceptionPanel" CancelControlID="CancelExceptionButton">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="DeliveryDayDetailsModalPopupButton" runat="server" Text="Button"
                        Style="display: none;" />
                    <asp:Button ID="DeliveryRecurrenceModalPopupButton" runat="server" Text="Button"
                        Style="display: none;" />
                    <asp:Button ID="DeliveryQuestionPopupButton" runat="server" Text="Button" Style="display: none;" />
                    <cc1:ModalPopupExtender ID="RecurrencePopupExtender" runat="server" BehaviorID="RecurrencePopupExtender"
                        TargetControlID="DeliveryRecurrenceModalPopupButton" PopupControlID="DeliveryRecurrencePanel">
                    </cc1:ModalPopupExtender>
                    <cc1:ModalPopupExtender ID="DeliveryQuestionPopupExtender" BehaviorID="DeliveryQuestionPopupExtender"
                        runat="server" TargetControlID="DeliveryQuestionPopupButton" PopupControlID="DeliveryQuestionPanel"
                        OkControlID="CancelDeliveryQuestionButton">
                    </cc1:ModalPopupExtender>
                    <cc1:CalendarExtender ID="CancellationRangeStartCalendarExtender" runat="server"
                        TargetControlID="CancellationRangeStartDateTextBox">
                    </cc1:CalendarExtender>
                    <cc1:CalendarExtender ID="CancellationRangeEndCalendarExtender" runat="server" TargetControlID="CancellationRangeEndDateTextBox"
                        BehaviorID="CancellationRangeEndCalendar" OnClientShowing="setCancellationRangeDate">
                    </cc1:CalendarExtender>
                    <cc1:ModalPopupExtender ID="ContinueModalPopupExtender" runat="server" BehaviorID="ContinueModalPopupExtender"
                        OkControlID="CancelButton" PopupControlID="CancellationRangeDatePanel" TargetControlID="ContinueTriggerButton">
                    </cc1:ModalPopupExtender>
                    <cc1:ModalPopupExtender ID="CancellationModalPopupExtender" runat="server" BehaviorID="CancellationModalPopupExtender"
                        OkControlID="CloseGroupCancellationButton" PopupControlID="GroupExceptionPanel"
                        TargetControlID="CancellationTriggerButton">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="ContinueTriggerButton" runat="server" Style="display: none;" Text="ContinueTriggerButton" />
                    <asp:Button ID="CancellationTriggerButton" runat="server" Style="display: none;"
                        Text="CancellationTriggerButton" />
                    <asp:Button ID="ClientButton" runat="server" Style="display: none" Text="Launch Modal Popup (Client)" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="4" style="height: 22px;">
                <asp:Panel ID="CancellationReschedulPanel" runat="server" BackColor="#00C7B3" BorderColor="Black"
                    BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" HorizontalAlign="Center"
                    Width="850px">
                    <table style="width: 100%">
                        <tr>
                            <td align="center" colspan="4" style="font-weight: bold; height: 21px; text-decoration: underline">
                                Add Cancelled/Rescheduled Deliveries
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="font-weight: bold; height: 21px;">
                                Date Of Delivery To Be Cancelled:
                            </td>
                            <td align="left" style="font-weight: bold; height: 21px; text-decoration: underline">
                                <asp:TextBox ID="CancellationDateTextBox" runat="server" OnTextChanged="CancellationDateTextBox_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td align="right" style="font-weight: bold; height: 21px;">
                                Date Delivery Is To Be Rescheduled:
                            </td>
                            <td align="left" style="font-weight: bold; height: 21px; text-decoration: underline">
                                <asp:TextBox ID="CancellationRescheduleDateTextBox" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2" style="font-weight: bold;" class="style21" valign="top">
                                <asp:Label ID="Label82" runat="server" Text="Meal Type:"></asp:Label>
                            </td>
                            <td align="left" colspan="2" style="font-weight: bold;" class="style21" valign="top">
                                <asp:DropDownList ID="CancellationMealTypeDropDownList" runat="server" Width="346px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style21" colspan="2" 
                                style="font-weight: bold;" valign="middle">
                                <asp:Label ID="Label86" runat="server" Font-Bold="true" Text="Schedule:"></asp:Label>
                            </td>
                            <td align="left" class="style21" colspan="2" style="font-weight: bold;" 
                                valign="top">
                                <asp:RadioButtonList id="IndividualCancellationScheduleRadioButtonList" runat="server"><asp:ListItem>CACFP</asp:ListItem><asp:ListItem>SFSP</asp:ListItem><asp:ListItem>N/A</asp:ListItem>   
                                </asp:RadioButtonList>                                 
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2" style="font-weight: bold;" valign="top">
                                Notes:
                            </td>
                            <td align="left" style="font-weight: bold;" valign="top" colspan="2">
                                <asp:TextBox ID="CancellationRescheduleNotesTextBox" runat="server" TextMode="MultiLine"
                                    Width="351px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4" style="font-weight: bold; height: 21px;">
                                <asp:Button ID="SaveCancellationRescheduleButton" runat="server" Font-Bold="True"
                                    Text="Save Cancelled/Rescheduled Delivery" Width="293px" OnClick="SaveCancellationRescheduleButton_Click"
                                    OnClientClick="onInvoke()" />
                                <asp:Button ID="CancellationRescheduleCloseButton" runat="server" Font-Bold="True"
                                    Text="Close" Width="123px" />
                            </td>
                        </tr>
                    </table>
                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="CancellationDateTextBox">
                    </cc1:CalendarExtender>
                    <cc1:CalendarExtender ID="CalendarExtender9" runat="server" BehaviorID="CalendarExtender9"
                        TargetControlID="CancellationRescheduleDateTextBox" OnClientShowing="setCancellationIndividualDate">
                    </cc1:CalendarExtender>
                    <cc1:ModalPopupExtender ID="CancellationRescheduleModalPopupExtender" runat="server"
                        BehaviorID="CancellationRescheduleModalPopupExtender" OkControlID="CancellationRescheduleCloseButton"
                        PopupControlID="CancellationReschedulPanel" TargetControlID="CancellationTriggerButton">
                    </cc1:ModalPopupExtender>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        Sys.Net.WebRequestManager.add_invokingRequest(onInvoke);
        Sys.Net.WebRequestManager.add_completedRequest(onComplete);

        function onInvoke(sender, args) {
            $find('<%= mpeProgress.ClientID %>').show();
        }

        function onComplete(sender, args) {
            $find('<%= mpeProgress.ClientID %>').hide();
        }

        function pageUnload() {
            Sys.Net.WebRequestManager.remove_invokingRequest(onInvoke);
            Sys.Net.WebRequestManager.remove_completedRequest(onComplete);
        }

    </script>
</asp:Content>
