<%@ Page Language="C#" Title = "CCK Application Development Changes" AutoEventWireup="true" CodeFile="CCKDevelopmentChanges.aspx.cs" MasterPageFile="~/Site.master" Inherits="CCKDevelopmentChanges" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <table style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
            border-bottom: black thin solid; table-layout: auto; padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; vertical-align: middle; direction: ltr; line-height: normal; padding-top: 0px; letter-spacing: normal; border-collapse: collapse; text-align: center;" width="100%">
            <tr>
                <td align="center" colspan="5" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 10%; border-bottom: black thin solid; height: 43px; font-weight: normal;">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="CCK Application Changes Log" Font-Size="X-Large"></asp:Label></td>
            </tr>
            <tr>
                <td align="center" colspan="5" 
                    style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 10%; border-bottom: black thin solid; height: 43px; font-weight: normal;" 
                    class="style1">
                    <strong>Current Version: Version 3.0</strong></td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    font-weight: normal; border-left: black thin solid; width: 10%; border-bottom: black thin solid;
                    height: 47px">
                    <asp:Label ID="Label2" runat="server" Text="Date" Font-Bold="True"></asp:Label></td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid; width: 10%; border-bottom: black thin solid;
                    height: 47px">
                    <asp:Label ID="Label3" runat="server" Text="Version Number" Font-Bold="True"></asp:Label></td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    font-weight: bold; border-left: black thin solid; width: 5%; border-bottom: black thin solid;
                    height: 47px">
                    #</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    font-weight: bold; overflow: auto; border-left: black thin solid; width: 20%;
                    border-bottom: black thin solid; height: 47px; text-align: center;">
                    Page(s)</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 47px;">
                    <asp:Label ID="Label4" runat="server" Text="Change Description" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 10%; border-bottom: black thin solid;
                    height: 13px; font-weight: normal;">
                    N/A</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 10%; border-bottom: black thin solid;
                    height: 13px">
                    Beta 1.0</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid;
                    height: 13px">
                    1</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 13px; text-align: center;">
                    All</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid;
                    height: 13px">
                    Initial Coding.</td>
            </tr>
            <tr>
                <td align="center" rowspan="7" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 10%; border-bottom: black thin solid; font-weight: normal; height: 43px;">
                    05/14/2009</td>
                <td align="center" rowspan="7" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 10%; border-bottom: black thin solid;">
                    Beta 1.1</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid;">
                    1</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid; text-align: center;">
                    recipedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid;">
                    Disabled flash display of initial popup windows when opened in.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid;">
                    2</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid; text-align: center;">
                    welcome.aspx;changelog.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid;">
                    Created change log and began incrementing version numbers.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid;">
                    3</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid; text-align: center;">
                    recipedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid;">
                    Set focus on correct controls when popups are opened.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid;">
                    4</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid; text-align: center;">
                    recipedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid;">
                    Changed text on 
                    &nbsp; dummy data in gridviews.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid;">
                    5</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid; text-align: center;">
                    recipedetails.apsx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid;">
                    Provided functionality to enable the inserting of new ingredients if one was missed.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 20px;">
                    6</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid; text-align: center; height: 20px;">
                    recipedetails.apsx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 20px;">
                    Update logic when user chooses the gridview row value to not look at the cells text
                    value but at the row index value.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid;">
                    7</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid; text-align: center;">
                    recipedetails.apsx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid;">
                    Added logic to have values of recipename and recipecondiment when saving any ingredient,
                    direction, and./or condiment.</td>
            </tr>
            <tr>
                <td align="center" rowspan="5" style="border-right: black thin solid; border-top: black thin solid;
                    font-weight: normal; border-left: black thin solid; width: 10%; border-bottom: black thin solid">
                    05/19/2009</td>
                <td align="center" rowspan="5" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 10%; border-bottom: black thin solid">
                    Beta 1.2</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid">
                    1</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    text-align: center">
                    recipe.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid">
                    Added code to round number within yield popupbox javascipt code.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid">
                    2</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    text-align: center">
                    Recipe Table</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid">
                    Modified Recipe.VolumeWeightEquivalent field to save as decimeal value.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid">
                    3</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    text-align: center">
                    recipedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid">
                    Saving recipe returns to recipe.aspx page.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid">
                    4</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    text-align: center">
                    recipedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid">
                    Fixed delete ingredient functionality to renumber all ingredients to their new correct
                    number.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 19px;">
                    5</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    text-align: center; height: 19px;">
                    changelog.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 19px;">
                    Reformated page display and added "Back To Main Menu" button.</td>
            </tr>
            <tr>
                <td align="center" rowspan="1" style="border-right: black thin solid; border-top: black thin solid;
                    font-weight: normal; border-left: black thin solid; width: 10%; border-bottom: black thin solid;
                    height: 19px">
                    05/20/2009</td>
                <td align="center" rowspan="1" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 10%; border-bottom: black thin solid; height: 19px">
                    Beta 1.3</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 19px">
                    1</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 19px; text-align: center">
                    recipedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 19px">
                    Changed the values to read as fractions instead of decimals.</td>
            </tr>
            <tr>
                <td align="center" rowspan="5" style="border-right: black thin solid; border-top: black thin solid;
                    font-weight: normal; border-left: black thin solid; width: 10%; border-bottom: black thin solid">
                    06/10/2009</td>
                <td align="center" rowspan="5" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 10%; border-bottom: black thin solid">
                    Beta 1.4</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 5px">
                    1</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 5px; text-align: center">
                    recipereport.aspx; recipedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 5px">
                    Created recipe report demo.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 13px">
                    2</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 13px; text-align: center">
                    mealcalendar.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 13px">
                    Fixed logic behind rounding meal counts.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 13px">
                    3</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 13px; text-align: center">
                    mealcalendar.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 13px">
                    Added javascript logic for yield updates.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 13px">
                    4</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 13px; text-align: center">
                    recipedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 13px">
                    Updated values in dropdowns for yields to reflect database entry with leading and
                    trailing zeros.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 13px">
                    5</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 13px; text-align: center">
                    sitedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 13px">
                    Made "Illinois" as default for the state field.</td>
            </tr>
            <tr>
                <td align="center" rowspan="3" style="border-right: black thin solid; border-top: black thin solid;
                    font-weight: normal; border-left: black thin solid; width: 10%; border-bottom: black thin solid">
                    6/16/2009</td>
                <td align="center" rowspan="3" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 10%; border-bottom: black thin solid">
                    Beta 1.5</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 13px">
                    1</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 13px; text-align: center">
                    sitedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 13px">
                    Fixed "Illinois" as default functionality.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 13px">
                    2</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 13px; text-align: center">
                    sitedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 13px">
                    Created a button to allow for the user to save the site and create a new one.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 13px">
                    3</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 13px; text-align: center">
                    sitedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 13px">
                    Made the default serving day equal to "Monday" when the delivery day of "Friday"
                    is chosen.</td>
            </tr>
            <tr>
                <td align="center" rowspan="3" style="border-right: black thin solid; border-top: black thin solid;
                    font-weight: normal; border-left: black thin solid; width: 10%; border-bottom: black thin solid">
                    6/16/2009</td>
                <td align="center" rowspan="3" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 10%; border-bottom: black thin solid">
                    Beta 1.6</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 13px">
                    1</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 13px; text-align: center">
                    sites.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 13px">
                    Fixed choose by letter functionality.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 15px">
                    2</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 15px; text-align: center">
                    sitedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 15px">
                    Delete sites with sitename = 'Enter Site Name Here' on cancel of site update.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 13px">
                    3</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 13px; text-align: center">
                    sitedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 13px">
                    On save returns to site.aspx with message of save successfull.</td>
            </tr>
            <tr>
                <td align="center" rowspan="5" style="border-right: black thin solid; border-top: black thin solid;
                    font-weight: normal; border-left: black thin solid; width: 10%; border-bottom: black thin solid">
                    6/252009</td>
                <td align="center" rowspan="5" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 10%; border-bottom: black thin solid">
                    Beta 1.7</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 13px">
                    1</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 13px; text-align: center">
                    sitedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 13px">
                    On month change followed by refresh, returns to month previously viewing.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 13px">
                    2</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 13px; text-align: center">
                    sitedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 13px">
                    On save returns to the list of sites that was previously being viewed.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 13px">
                    3</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 13px; text-align: center">
                    mealcalendar.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 13px">
                    Fixed updating of cookchill yield details.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 13px">
                    4</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 13px; text-align: center">
                    deliveryreceipt.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 13px">
                    Fixed the delivery reciept due to mass amounts of data creating errors due to report
                    viewer size restraints.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 19px">
                    5</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 19px; text-align: center">
                    mealcalendar.aspx;deliverycalendar.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 19px">
                    On refresh will return to month that was being viewed previously.</td>
            </tr>
            <tr>
                <td align="center" rowspan="2" style="border-right: black thin solid; border-top: black thin solid;
                    font-weight: normal; border-left: black thin solid; width: 10%; border-bottom: black thin solid">
                    6/26/2009</td>
                <td align="center" rowspan="2" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 10%; border-bottom: black thin solid">
                    Beta 1.8</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 19px">
                    1</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 19px; text-align: center">
                    sitedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 19px">
                    Breakfast functionality working.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 19px">
                    2</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 19px; text-align: center">
                    deliveryreceipt.aspx;deliverycalendar.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 19px">
                    Report working on report computers.</td>
            </tr>
            <tr>
                <td align="center" rowspan="1" style="border-right: black thin solid; border-top: black thin solid;
                    font-weight: normal; border-left: black thin solid; width: 10%; border-bottom: black thin solid;
                    height: 21px">
                    6/29/2009</td>
                <td align="center" rowspan="1" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 10%; border-bottom: black thin solid; height: 21px">
                    Beta 1.9</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 21px">
                    1</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 21px; text-align: center">
                    sitedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 21px">
                    Added breakfast exception functionality.</td>
            </tr>
            <tr>
                <td align="center" rowspan="1" style="border-right: black thin solid; border-top: black thin solid;
                    font-weight: normal; border-left: black thin solid; width: 10%; border-bottom: black thin solid;
                    height: 21px">
                    6/29/2009</td>
                <td align="center" rowspan="1" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 10%; border-bottom: black thin solid; height: 21px">
                    Beta 2.0</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 21px">
                    1</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 21px; text-align: center">
                    sitedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 21px">
                    Added functionality for the selection of the breakfast delivery day seperate from
                    CCK Meals.</td>
            </tr>
            <tr>
                <td align="center" rowspan="8" style="border-right: black thin solid; border-top: black thin solid;
                    font-weight: normal; border-left: black thin solid; width: 10%; border-bottom: black thin solid">
                    7/6/2009</td>
                <td align="center" rowspan="8" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 10%; border-bottom: black thin solid">
                    Beta 2.1</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 20px">
                    1</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 20px; text-align: center">
                    recipedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 20px">
                    ChangeIndex value of selection rows of condiments, ingredients, directions to look
                    at text instead of index number for successfull updating.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 20px">
                    2</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 20px; text-align: center">
                    recipedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 20px">
                    Fixed add condiments functionality.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 20px">
                    3</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 20px; text-align: center">
                    recipedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 20px">
                    Fixed update/delete ingredients and directions functionality.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 20px">
                    4</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 20px; text-align: center">
                    mealcalendar.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 20px">
                    Added value of "No *** For This Meal" to all dropdown boxes of meal componets, displayed
                    on calendar with "---".</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 23px">
                    5</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 23px; text-align: center">
                    recipedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 23px">
                    Added condiment delivery units.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 23px">
                    6</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 23px; text-align: center">
                    recipedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 23px">
                    Fixed label on yield to not read ...gallon super bag for regular recipes.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 23px">
                    7</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 23px; text-align: center">
                    mealcalendar.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 23px">
                    Changed colors for "other" meal items.</td>
            </tr>
            <tr>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 23px">
                    8</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 23px; text-align: center">
                    mealcalendar.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 23px">
                    Changed value of "No *** For This Meal" to read "None".</td>
            </tr>
            <tr>
                <td align="center" rowspan="1" style="border-right: black thin solid; border-top: black thin solid;
                    font-weight: normal; border-left: black thin solid; width: 10%; border-bottom: black thin solid;
                    height: 24px">
                    7/7/2009</td>
                <td align="center" rowspan="1" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 10%; border-bottom: black thin solid; height: 24px">
                    Beta 2.2</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 24px">
                    1</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 24px; text-align: center">
                    recipedetails.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 24px">
                    Fixed Ingredient/Direction add/edit functionality for correct placement.</td>
            </tr>
            <tr>
                <td align="center" rowspan="1" style="border-right: black thin solid; border-top: black thin solid;
                    font-weight: normal; border-left: black thin solid; width: 10%; border-bottom: black thin solid;
                    height: 24px">
                    7/15/2009</td>
                <td align="center" rowspan="1" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 10%; border-bottom: black thin solid; height: 24px">
                    Beta 2.3</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 5%; border-bottom: black thin solid; height: 24px">
                    1</td>
                <td align="center" style="border-right: black thin solid; border-top: black thin solid;
                    overflow: auto; border-left: black thin solid; width: 20%; border-bottom: black thin solid;
                    height: 24px; text-align: center">
                    mealcalendar.aspx</td>
                <td align="left" style="border-right: black thin solid; border-top: black thin solid;
                    border-left: black thin solid; width: 55%; border-bottom: black thin solid; height: 24px">
                    Fixed counts and listing of sites.</td>
            </tr>
            </table>
    
    </div>
</asp:Content>
