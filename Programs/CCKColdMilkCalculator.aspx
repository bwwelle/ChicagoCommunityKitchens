<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Title="CCK Milk Ordering Calculator"
    CodeFile="CCKColdMilkCalculator.aspx.cs" Inherits="MilkOrderingCalculator" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" language="JavaScript">
       function updateChocolateMilkQuantities() {            
            if (document.all('ctl00_MainContent_ChocolateInventoryTextBox').value != "" && document.all('ctl00_MainContent_ChocolateExpirationDateTextBox').value != "" && (document.all('ctl00_MainContent_DateTextBox').value != "")) 
            {
                var inInventory = parseInt(document.all('ctl00_MainContent_ChocolateInventoryTextBox').value);

                var expDate = new Date(document.all('ctl00_MainContent_ChocolateExpirationDateTextBox').value);

                var needed = parseInt(document.all('ctl00_MainContent_ChocolateNeededForWeekdaysBeforeLabel').innerHTML);

                var date = new Date(document.all('ctl00_MainContent_DateTextBox').value);

//                var newDate = new Date(date.setDate(date.getDate() + 2));

                if (expDate < date)
                {
                    document.all('ctl00_MainContent_QuantityOfChocolateAvailableLabel').innerHTML = 0;
                }
                else
                {
                    document.all('ctl00_MainContent_QuantityOfChocolateAvailableLabel').innerHTML = inInventory - needed;
                }

                document.all('ctl00_MainContent_ChocolateAmountNeededLabel').innerHTML = (parseInt(document.all('ctl00_MainContent_ChocolateMilkCrateCountTotalNeededLabel').innerHTML) - parseInt(document.all('ctl00_MainContent_QuantityOfChocolateAvailableLabel').innerHTML)) + 3;
            }        
            else
            {
                document.all('ctl00_MainContent_QuantityOfChocolateAvailableLabel').innerHTML ='N/A';
                document.all('ctl00_MainContent_ChocolateAmountNeededLabel').innerHTML = 'N/A'

            }
        }

        function updateWhiteMilkQuantities() {
            if (document.all('ctl00_MainContent_WhiteInventoryTextBox').value != "" && document.all('ctl00_MainContent_WhiteExpirationDateTextBox').value != "" && (document.all('ctl00_MainContent_DateTextBox').value != "")) {
                var inInventory = parseInt(document.all('ctl00_MainContent_WhiteInventoryTextBox').value);

                var expDate = new Date(document.all('ctl00_MainContent_WhiteExpirationDateTextBox').value);

                var needed = parseInt(document.all('ctl00_MainContent_WhiteNeededForWeekdaysBeforeLabel').innerHTML);

                var date = new Date(document.all('ctl00_MainContent_DateTextBox').value);

//                var newDate = new Date(date.setDate(date.getDate() + 2));

                if (expDate < date) {
                    document.all('ctl00_MainContent_QuantityOfWhiteAvailableLabel').innerHTML = 0;
                }
                else {
                    document.all('ctl00_MainContent_QuantityOfWhiteAvailableLabel').innerHTML = inInventory - needed;
                }

                document.all('ctl00_MainContent_WhiteAmountNeededLabel').innerHTML = (parseInt(document.all('ctl00_MainContent_WhiteMilkCrateCountTotalNeededLabel').innerHTML) - parseInt(document.all('ctl00_MainContent_QuantityOfWhiteAvailableLabel').innerHTML)) + 3;
            }
            else {
                document.all('ctl00_MainContent_QuantityOfWhiteAvailableLabel').innerHTML = 'N/A';
                document.all('ctl00_MainContent_WhiteAmountNeededLabel').innerHTML = 'N/A'

            }
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style3
        {
            height: 19px;
        }
        .style5
        {
            height: 23px;
        }
        .style9
        {
            width: 110px;
            text-align: center;
        }
        .style10
        {
            width: 110px;
            height: 19px;
            text-align: center;
        }
        .style11
        {
            width: 110px;
            height: 23px;
            text-align: center;
        }
        .style13
        {
            width: 99px;
        }
        .style14
        {
            width: 99px;
            height: 19px;
        }
        .style15
        {
            width: 99px;
            height: 23px;
        }
        .style16
        {
        }
        .style17
        {
            height: 20px;
        }
        .style18
        {
            width: 110px;
            height: 20px;
        }
        .style26
        {
            width: 85px;
            text-align: center;
        }
        .style31
        {
            width: 140px;
            text-align: center;
        }
        .style32
        {
            width: 140px;
            height: 19px;
            text-align: center;
        }
        .style33
        {
            width: 140px;
            height: 23px;
            text-align: center;
        }
        .style34
        {
            width: 140px;
            height: 37px;
            text-align: center;
        }
        .style37
        {
            height: 19px;
            width: 96px;
            text-align: right;
        }
        .style38
        {
            height: 23px;
            text-align: right;
        }
        .style40
        {
            height: 20px;
            width: 96px;
        }
        .style41
        {
            height: 12px;
            width: 96px;
            text-align: center;
        }
        .style42
        {
            height: 12px;
        }
        .style43
        {
            width: 110px;
            height: 12px;
        }
        .style44
        {
        }
        .style48
        {
            height: 20px;
            text-align: center;
        }
        .style49
        {
            height: 23px;
            text-align: center;
        }
        .style50
        {
            width: 122px;
            height: 23px;
            text-align: center;
        }
        .style51
        {
            text-align: center;
        }
        .style52
        {
            height: 19px;
            text-align: center;
        }
        .style53
        {
            text-align: right;
        }
        .style55
        {
            width: 110px;
            }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="asm" runat="server" />
    <div>
        <table class="style1" style="border-style: none; border-color: #FFFFFF;">
            <tr>
                <td colspan="7" style="border-style: none; border-color: #FFFFFF;" 
                    class="style51">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="border: thin solid #000000" class="style51">
                    &nbsp;
                </td>
                <td style="border: thin solid #000000; font-weight: bold; font-size: small;" class="style51">
                    <asp:Label ID="WeekDay1Label" runat="server" Text=""></asp:Label>
                    </td>
                <td class="style9" style="border: thin solid #000000; font-weight: bold; font-size: small;">
                    <asp:Label ID="WeekDay2Label" runat="server" Text=""></asp:Label>
                    </td>
                <td class="style31" style="border: thin solid #000000; font-weight: bold; font-size: small;">
                    <asp:Label ID="WeekDay3Label" runat="server" Text=""></asp:Label>
                    </td>
                <td style="border: thin solid #000000; font-weight: bold; font-size: small;" class="style26">
                    <asp:Label ID="WeekDay4Label" runat="server" Text=""></asp:Label>
                    </td>
                <td align="center" class="style13" style="border: thin solid #000000; font-weight: bold;
                    font-size: small;">
                    <asp:Label ID="WeekDay5Label" runat="server" Text=""></asp:Label>
                   </td>
                <td align="center" style="border: thin solid #000000">
                   
                </td>
            </tr>
            <tr>
                <td style="border: thin solid #000000; font-weight: bold; font-size: small;" 
                    class="style53">
                    Date
                </td>
                <td style="border: thin solid #000000" class="style51">
                    <asp:TextBox ID="DateTextBox" runat="server" Width="69px" 
                        ontextchanged="DateTextBox_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <cc1:CalendarExtender ID="DateTextBox_CalendarExtender" runat="server" TargetControlID="DateTextBox">
                    </cc1:CalendarExtender>
                </td>
                <td class="style9" style="border: thin solid #000000">
                    <asp:Label ID="Date2Label" runat="server" Width="69px"></asp:Label>
                </td>
                <td class="style31" style="border: thin solid #000000">
                    <asp:Label ID="Date3Label" runat="server"></asp:Label>
                </td>
                <td style="border: thin solid #000000; text-align: center;">
                    <asp:Label ID="Date4Label" runat="server" Width="69px"></asp:Label>
                </td>
                <td align="center" class="style13" style="border: thin solid #000000">
                    <asp:Label ID="Date5Label" runat="server" Width="69px"></asp:Label>
                </td>
                <td align="center" style="border: thin solid #000000; font-weight: bold; font-size: small;">
                    Total Needed
                </td>
            </tr>
            <tr>
                <td class="style37" style="border: thin solid #000000; font-weight: bold; font-size: small;">
                    Breakfast Milk
                </td>
                <td class="style52" style="border: thin solid #000000">
                    <asp:Label ID="WhiteMilkCrateCountDate1Label" runat="server" Width="69px" Text="N/A"></asp:Label>
                </td>
                <td class="style10" style="border: thin solid #000000">
                    <asp:Label ID="WhiteMilkCrateCountDate2Label" runat="server" Text="N/A" Width="69px"></asp:Label></td>
                <td class="style32" style="border: thin solid #000000">
                    <asp:Label ID="WhiteMilkCrateCountDate3Label" runat="server" Text="N/A" Width="69px"></asp:Label></td>
                <td style="border: thin solid #000000; text-align: center;">
                    <asp:Label ID="WhiteMilkCrateCountDate4Label" runat="server" Text="N/A" Width="69px"></asp:Label></td>
                <td align="center" class="style14" style="border: thin solid #000000">
                    <asp:Label ID="WhiteMilkCrateCountDate5Label" runat="server" Text="N/A" Width="69px"></asp:Label></td>
                <td align="center" class="style3" style="border: thin solid #000000">
                    <asp:Label ID="WhiteMilkCrateCountTotalNeededLabel" runat="server" Text="N/A" Width="69px"></asp:Label></td>
            </tr>
            <tr>
                <td class="style38" style="border: thin solid #000000; font-weight: bold; font-size: small;">
                    Lunch Milk 
                </td>
                <td class="style49" style="border: thin solid #000000">
                    <asp:Label ID="ChocolateMilkCrateCountDate1Label" runat="server" Text="N/A" Width="69px"></asp:Label></td>
                <td class="style11" style="border: thin solid #000000">
                     <asp:Label ID="ChocolateMilkCrateCountDate2Label" runat="server" Text="N/A" Width="69px"></asp:Label></td>
                <td class="style33" style="border: thin solid #000000">
                     <asp:Label ID="ChocolateMilkCrateCountDate3Label" runat="server" Text="N/A" Width="69px"></asp:Label></td>
                <td style="border: thin solid #000000; text-align: center;">
                     <asp:Label ID="ChocolateMilkCrateCountDate4Label" runat="server" Text="N/A" Width="69px"></asp:Label></td>
                <td align="center" class="style15" style="border: thin solid #000000">
                     <asp:Label ID="ChocolateMilkCrateCountDate5Label" runat="server" Text="N/A" Width="69px"></asp:Label></td>
                <td align="center" class="style5" style="border: thin solid #000000">
                     <asp:Label ID="ChocolateMilkCrateCountTotalNeededLabel" runat="server" Text="N/A" Width="69px"></asp:Label></td>
            </tr>
            <tr>
                <td class="style38" style="border: thin solid #000000" colspan="7">
                    <asp:Label ID="Date1Label" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="border: thin solid #000000" class="style51">
                    &nbsp;
                </td>
                <td style="border: thin solid #000000" valign="bottom" class="style51">
                    &nbsp;<strong>
                    In Inventory</strong></td>
                <td style="border: thin solid #000000; font-weight: bold; font-size: small; text-align: center;"
                    valign="bottom">
                     <asp:Label ID="WeekdaysNameBeforeLabel" runat="server" Text="N/A" 
                        Width="105px" Height="17px" style="text-align: center"></asp:Label></td>
                <td class="style34" style="border: thin solid #000000; font-weight: bold; font-size: small;"
                    valign="bottom">
                    Exp Date
                </td>
                <td class="style50" style="border: thin solid #000000;" valign="bottom">
                    <strong>Quantity Available For Use Until Exp Date</strong>
                </td>
                <td align="center" class="style16" style="border: thin solid #000000; font-weight: bold;
                    font-size: small;" valign="bottom" colspan="2" rowspan="3">
                   <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="ChocolateExpirationDateTextBox">
                    </cc1:CalendarExtender><cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="WhiteExpirationDateTextBox">
                    </cc1:CalendarExtender></td>
            </tr>
            <tr>
                <td style="border: thin solid #000000; font-weight: bold; font-size: small; text-align: right;">
                    Breakfast Milk
                </td>
                <td style="border: thin solid #000000; text-align: center;">
                    <asp:TextBox ID="WhiteInventoryTextBox" runat="server" Width="69px"></asp:TextBox>
                </td>
                <td class="style11" style="border: thin solid #000000">
                     <asp:Label ID="WhiteNeededForWeekdaysBeforeLabel" runat="server" Text="N/A" 
                        Width="69px"></asp:Label>
                </td>
                <td class="style33" style="border: thin solid #000000">
                    <asp:TextBox ID="WhiteExpirationDateTextBox" runat="server" Width="69px"></asp:TextBox>
                </td>
                <td style="border: thin solid #000000; text-align: center;">
                     <asp:Label ID="QuantityOfWhiteAvailableLabel" runat="server" Text="N/A" 
                        Width="69px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style38" style="border: thin solid #000000; font-weight: bold; font-size: small;">
                    Lunch Milk 
                </td>
                <td class="style49" style="border: thin solid #000000">
                    <asp:TextBox ID="ChocolateInventoryTextBox" runat="server" Width="69px"></asp:TextBox>
                </td>
                <td class="style11" style="border: thin solid #000000">
                     <asp:Label ID="ChocolateNeededForWeekdaysBeforeLabel" runat="server" Text="N/A" 
                        Width="69px"></asp:Label>
                </td>
                <td class="style33" style="border: thin solid #000000">
                    <asp:TextBox ID="ChocolateExpirationDateTextBox" runat="server" Width="69px"></asp:TextBox>
                </td>
                <td style="border: thin solid #000000; text-align: center;">
                     <asp:Label ID="QuantityOfChocolateAvailableLabel" runat="server" Text="N/A" 
                        Width="69px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style48" style="border-style: none; border-color: #FFFFFF;" 
                    colspan="7">
                </td>
            </tr>
            <tr>
                <td class="style41" style="border: thin solid #000000">
                </td>
                <td align="center" class="style42" style="border: thin solid #000000">
                    <strong>Amount Needed</strong></td>
                <td align="center" class="style43" style="border: thin solid #000000; font-weight: bold;
                    font-size: small;">
                    &nbsp;</td>
                <td align="center" class="style44" 
                    style="border-style: none; border-color: #FFFFFF" colspan="4" rowspan="3">
                   &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="border: thin solid #000000; font-weight: bold; font-size: small; text-align: right;">
                    Breakfast Milk
                </td>
                <td align="center" style="border: thin solid #000000">
                     <asp:Label ID="WhiteAmountNeededLabel" runat="server" Text="N/A" 
                        Width="69px"></asp:Label>
                </td>
                <td align="center" class="style55" 
                    style="border: thin solid #000000; text-align: left;">
                    <strong>(3 extra included)</strong></td>
            </tr>
            <tr>
                <td class="style40" 
                    style="border: thin solid #000000; font-weight: bold; font-size: small; text-align: right;">
                    Lunch Milk
                </td>
                <td align="center" class="style17" style="border: thin solid #000000">
                     <asp:Label ID="ChocolateAmountNeededLabel" runat="server" Text="N/A" 
                        Width="69px"></asp:Label>
                </td>
                <td align="center" class="style18" 
                    style="border: thin solid #000000; text-align: left;">
                    <strong>(3 extra included)</strong>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
