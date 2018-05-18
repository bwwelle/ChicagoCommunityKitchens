<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SFSPComplianceSiteInformationReport.aspx.cs" Inherits="SFSPComplianceSiteInformationReport" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SFSP Compliance Site Information Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%">
            <tr>
                <td colspan="3" style="height: 486px">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
            Height="469px" Width="1000px" SizeToReportContent="True"> </rsweb:ReportViewer>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3" style="height: 26px">
                    </td>
            </tr>
        </table>
        &nbsp;
    
    </div>
    </form>
</body>
</html>
