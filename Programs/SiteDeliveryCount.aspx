<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"  Title = "CCK Site Delivery Count" MasterPageFile="~/Site.master"
    CodeFile="SiteDeliveryCount.aspx.cs" Inherits="SiteDeliveryCount" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="GCFDGlobalsNamespace" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" language="JavaScript">
        function updatetotalused(totalUsedTextBox) {
            var sent;
            sent = document.getElementById('ctl00_MainContent_MealsSentLabel' + totalUsedTextBox).innerText;
            var firstMeal = document.getElementById('ctl00_MainContent_FirstMealTextBox' + totalUsedTextBox).value;
            var secondMeal = document.getElementById('ctl00_MainContent_SecondsTextBox' + totalUsedTextBox).value;
            var progAdults = document.getElementById('ctl00_MainContent_ProgAdultsTextBox' + totalUsedTextBox).value;
            var disallowed = document.getElementById('ctl00_MainContent_DisallowedTextBox' + totalUsedTextBox).value;
            var totalUsed;

            if (firstMeal == '') {
                firstMeal = '0';
            }

            if (secondMeal == '') {
                secondMeal = '0';
            }

            if (disallowed == '') {
                disallowed = '0';
            }

            if (progAdults == '') {
                progAdults = '0';
            }

            totalUsed = parseInt(firstMeal) + parseInt(secondMeal) + parseInt(progAdults) + parseInt(disallowed);

            document.getElementById('ctl00_MainContent_TotalUsedTextBox' + totalUsedTextBox).value = totalUsed;

            document.getElementById('ctl00_MainContent_TotalUnusedTextBox' + totalUsedTextBox).value = parseInt(sent) - totalUsed;
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="asm" runat="server" EnablePartialRendering="true" />
    <div align="center">
        <asp:Panel ID="DailyCountPanel" runat="server" BorderColor="Black" BorderStyle="Solid"
            BorderWidth="1px" Width="950px">
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <table width="100%" id="DailyCountTable">
                        <tr>
                            <td align="center" colspan="11">
                                <br />
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="4">
                                <asp:Label ID="Label1" runat="server" Text="Delivery Week:"></asp:Label>
                            </td>
                            <td align="left" colspan="7">
                                <asp:TextBox ID="DeliveryWeekTextBox" runat="server" AutoPostBack="true" Height="18px"
                                    OnTextChanged="DeliveryWeekTextBox_TextChanged" Width="184px"></asp:TextBox>
                                <cc1:CalendarExtender ID="DeliveryWeekCalendarExtender" runat="server" Format="MM/dd/yyyy"
                                    TargetControlID="DeliveryWeekTextBox">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="4">
                                <asp:Label ID="Label2" runat="server" Text="Delivery Type:"></asp:Label>
                            </td>
                            <td align="left" colspan="7">
                                <asp:DropDownList ID="DeliveryTypeDropDownList" runat="server" Width="276px" OnSelectedIndexChanged="DeliveryTypeDropDownList_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="11" 
                                style="padding: 10px; text-align: left; height: 15px; margin-top: inherit;
                                margin-right: inherit; margin-bottom: auto; margin-left: 2px; color: #FFFFFF;">
                                <asp:DataList ID="DeliverySiteListView" runat="server" RepeatColumns="3" RepeatDirection="horizontal"
                                    CellPadding="3" HorizontalAlign="Left" ForeColor="White" Width="100%">
                                    <ItemTemplate>
                                        <span style="cursor: pointer;">
                                            <asp:LinkButton runat="server" OnClick="SiteName_OnClick" ForeColor="White" Text="<%# Container.DataItem %>"></asp:LinkButton></span></ItemTemplate>
                                </asp:DataList><br />
                            </td>
                        </tr>
                        <div runat="server" id="test">
                            <tr>
                                <td style="font-size: small; height: 20px; text-align: center;" align="right" 
                                    colspan="11">
                                    <hr />
                                    <asp:Label ID="SiteNameLabel" runat="server"></asp:Label><br />
                                    <asp:Label ID="DeliveryDateRangeLabel" runat="server"></asp:Label><br />
                                    <asp:Label ID="MealTypeLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    Serving Date
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    # Sent
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    Attendance
                                </td>
                                <td colspan="2" style="font-size: small; width: 10%; height: 20px">
                                    1st Meal
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    2nds
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    Prog Adults
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    Disallowed
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    Nut. Educ.</td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    Total Used
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    Total Unused
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:Label ID="MealDateLabel1" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:Label ID="MealsSentLabel1" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="DailyCountHiddenField1" runat="server" />
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="AttendanceTextBox1" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="2" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="FirstMealTextBox1" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="SecondsTextBox1" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="ProgAdultsTextBox1" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="DisallowedTextBox1" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:DropDownList ID="NutritionEducationDropDownListBox1" runat="server" Width="45px">
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="TotalUsedTextBox1" runat="server" BorderStyle="None" 
                                        Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="TotalUnusedTextBox1" runat="server" BorderStyle="None" 
                                        Width="57px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: small; height: 20px; width: 10%;">
                                    <asp:Label ID="MealDateLabel2" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:Label ID="MealsSentLabel2" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="DailyCountHiddenField2" runat="server" />
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="AttendanceTextBox2" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="2" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="FirstMealTextBox2" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="SecondsTextBox2" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="ProgAdultsTextBox2" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="DisallowedTextBox2" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:DropDownList ID="NutritionEducationDropDownListBox2" runat="server" Width="45px">
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="TotalUsedTextBox2" runat="server" BorderStyle="None" 
                                        Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="TotalUnusedTextBox2" runat="server" BorderStyle="None" 
                                        Width="57px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:Label ID="MealDateLabel3" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:Label ID="MealsSentLabel3" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="DailyCountHiddenField3" runat="server" />
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="AttendanceTextBox3" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="2" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="FirstMealTextBox3" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="SecondsTextBox3" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="ProgAdultsTextBox3" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="DisallowedTextBox3" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:DropDownList ID="NutritionEducationDropDownListBox3" runat="server" Width="45px">
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="TotalUsedTextBox3" runat="server" Width="57px" 
                                        BorderStyle="None"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="TotalUnusedTextBox3" runat="server" Width="57px" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:Label ID="MealDateLabel4" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:Label ID="MealsSentLabel4" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="DailyCountHiddenField4" runat="server" />
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="AttendanceTextBox4" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="2" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="FirstMealTextBox4" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="SecondsTextBox4" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="ProgAdultsTextBox4" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="DisallowedTextBox4" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:DropDownList ID="NutritionEducationDropDownListBox4" runat="server" Width="45px">
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="TotalUsedTextBox4" runat="server" Width="57px" 
                                        BorderStyle="None"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="TotalUnusedTextBox4" runat="server" BorderStyle="None" Width="57px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: small; width: 10%; height: 21px">
                                    <asp:Label ID="MealDateLabel5" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="font-size: small; width: 10%; height: 21px">
                                    <asp:Label ID="MealsSentLabel5" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="DailyCountHiddenField5" runat="server" />
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 21px">
                                    <asp:TextBox ID="AttendanceTextBox5" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="2" style="font-size: small; width: 10%; height: 21px">
                                    <asp:TextBox ID="FirstMealTextBox5" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 21px">
                                    <asp:TextBox ID="SecondsTextBox5" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td style="font-size: small; width: 10%; height: 21px">
                                    <asp:TextBox ID="ProgAdultsTextBox5" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 21px">
                                    <asp:TextBox ID="DisallowedTextBox5" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td style="font-size: small; width: 10%; height: 21px">
                                    <asp:DropDownList ID="NutritionEducationDropDownListBox5" runat="server" Width="45px">
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="font-size: small; width: 10%; height: 21px">
                                    <asp:TextBox ID="TotalUsedTextBox5" runat="server" Width="57px" BorderStyle="None"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 21px">
                                    <asp:TextBox ID="TotalUnusedTextBox5" runat="server" BorderStyle="None" Width="57px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:Label ID="MealDateLabel6" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:Label ID="MealsSentLabel6" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="DailyCountHiddenField6" runat="server" />
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="AttendanceTextBox6" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="2" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="FirstMealTextBox6" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="SecondsTextBox6" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="ProgAdultsTextBox6" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="DisallowedTextBox6" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:DropDownList ID="NutritionEducationDropDownListBox6" runat="server" Width="45px">
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="TotalUsedTextBox6" runat="server" Width="57px" 
                                        BorderStyle="None"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="TotalUnusedTextBox6" runat="server" BorderStyle="None" Width="57px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:Label ID="MealDateLabel7" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:Label ID="MealsSentLabel7" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="DailyCountHiddenField7" runat="server" />
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="AttendanceTextBox7" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="2" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="FirstMealTextBox7" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="SecondsTextBox7" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="ProgAdultsTextBox7" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="DisallowedTextBox7" runat="server" Width="57px"></asp:TextBox>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:DropDownList ID="NutritionEducationDropDownListBox17" runat="server" Width="45px">
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="TotalUsedTextBox7" runat="server" Width="57px" 
                                        BorderStyle="None"></asp:TextBox>
                                </td>
                                <td colspan="1" style="font-size: small; width: 10%; height: 20px">
                                    <asp:TextBox ID="TotalUnusedTextBox7" runat="server" BorderStyle="None" Width="57px"></asp:TextBox>
                                </td>
                            </tr>
                        </div>
                        <tr>
                            <td colspan="11" style="font-size: small; height: 20px">
                                <asp:Button ID="SaveDailyCountButton" runat="server" OnClick="SaveDailyCountButton_Click"
                                    Text="Save Daily Counts" />
                                <asp:Button ID="CancelDailyCountsButton" runat="server" 
                                    Text="Clear Count Values" onclick="CancelDailyCountsButton_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>
