<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebOrders.aspx.cs" Inherits="Kitchen_Default" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="asm" runat="server" />
     <cc1:CalendarExtender ID="StartDateCalendarExtender" runat="server" Format="MM/dd/yyyy"
        TargetControlID="ReportStartDateTextBox">
    </cc1:CalendarExtender>
         
    
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
    <div style="height: 320px">
    
<%--        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="True" EnableDatabaseLogonPrompt="False" 
            EnableParameterPrompt="False" GroupTreeImagesFolderUrl="" Height="962px" 
            oninit="CrystalReportViewer1_Init" ReportSourceID="CrystalReportSource1" 
            ToolbarImagesFolderUrl="" ToolPanelWidth="200px" Width="2362px" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="C:\onlineordering.rpt">
            </Report>
        </CR:CrystalReportSource>--%>
    
    </div>
    </form>
</body>
</html>
