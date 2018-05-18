<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Wait.aspx.cs" Inherits="Wait" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title>PageLoad</title>
<script type="text/javascript" language="javascript">

var iLoopCounter = 1;
var iMaxLoop = 6;
var iIntervalId;

function BeginPageLoad() {
location.href = "<%=Request.QueryString["Page"] %>";
iIntervalId = window.setInterval("iLoopCounter=UpdateProgressMeter(iLoopCounter, iMaxLoop)", 500);
}

function EndPageLoad() {
window.clearInterval(iIntervalId);
ProgressMeter.innerText = "Page Loaded -- Not Transferring";
}

function UpdateProgressMeter(iCurrentLoopCounter, iMaximumLoops) {

iCurrentLoopCounter += 1;

if (iCurrentLoopCounter <= iMaximumLoops) {
ProgressMeter.innerText += ".";
return iCurrentLoopCounter;
}
else {
ProgressMeter.innerText = "";
return 1;
}
}

</script>
</head>
<body onload="BeginPageLoad()" onunload="EndPageLoad()">
<form id="Form1" method="post" runat="server">
<table border="0" cellpadding="0" cellspacing="0" width="99%" height="99%" align="center" valign="middle">
<tr>
<td align="center" valign="middle">
<font color="navy" size="7">
<span id="MessageText">Page Is Loading&nbsp;-- Please Wait</span>
<span id="ProgressMeter"
style="WIDTH:25px;TEXT-ALIGN:left"></span>
</font>
</td>
</tr>
</table>
</form>
</body>
</html>

