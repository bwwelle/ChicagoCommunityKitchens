using System;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

/// <summary>
/// Summary description for ProcessReport
/// </summary>
public class ProcessReport
{
    public string name = "";
    public string startDate = "";
    public string endDate = "";
    public string reportID = "";
    public string mealType = "";
    public string communityArea = "";
    public string siteName = "";
    public string labelText = "";
    public string sliceCount = "";
    public string bunCount = "";
    public string loafCount = "";
    public string bagCount = "";
    public string scheduleType = "";
    public HttpResponse response;

    public string Name
    {
        get
        {
            return (name);
        }
        set
        {
            name = value;
        }
    }

    public string LabelText
    {
        get
        {
            return (labelText);
        }
        set
        {
            labelText = value;
        }
    }

    public string ReportID
    {
        get
        {
            return (reportID);
        }
        set
        {
            reportID = value;
        }
    }

    public string StartDate
    {
        get
        {
            return (startDate);
        }
        set
        {
            startDate = value;
        }
    }

    public string EndDate
    {
        get
        {
            return (endDate);
        }
        set
        {
            endDate = value;
        }
    }

    public string SiteName
    {
        get
        {
            return (siteName);
        }
        set
        {
            siteName = value;
        }
    }

    public string MealType
    {
        get
        {
            return (mealType);
        }
        set
        {
            mealType = value;
        }
    }

    public string ScheduleType
    {
        get
        {
            return (scheduleType);
        }
        set
        {
            scheduleType = value;
        }
    }

    public string CommunityArea
    {
        get
        {
            return (communityArea);
        }
        set
        {
            communityArea = value;
        }
    }

    public string SliceCount
    {
        get
        {
            return (sliceCount);
        }
        set
        {
            sliceCount = value;
        }
    }

    public string BunCount
    {
        get
        {
            return (bunCount);
        }
        set
        {
            bunCount = value;
        }
    }


    public string LoafCount
    {
        get
        {
            return (loafCount);
        }
        set
        {
            loafCount = value;
        }
    }

    public string BagCount
    {
        get
        {
            return (bagCount);
        }
        set
        {
            bagCount = value;
        }
    }

    public HttpResponse Response
    {
        get
        {
            return (response);
        }
        set
        {
            response = value;
        }
    }

    public void RenderReport()
    {
        string mimeType, encoding, extension;
        string[] streamids;
        Warning[] warnings;
        ReportViewer rview = new ReportViewer();
        ReportParameter startDateParameter = new ReportParameter();
        ReportParameter endDateParameter = new ReportParameter();
        ReportParameter mealTypeParameter = new ReportParameter();
        ReportParameter scheduleTypeParameter = new ReportParameter();
        ReportParameter reportIDParameter = new ReportParameter();
        ReportParameter communityAreaParameter = new ReportParameter();
        ReportParameter siteNameParameter = new ReportParameter();
        ReportParameter labelTextParameter = new ReportParameter();
        ReportParameter bunCountParameter = new ReportParameter();
        ReportParameter loafCountParameter = new ReportParameter();
        ReportParameter sliceCountParameter = new ReportParameter();
        ReportParameter bagCountParameter = new ReportParameter();
        const string deviceInfo = "<DeviceInfo>" + "<SimplePageHeaders>True</SimplePageHeaders>" + "</DeviceInfo>";

        rview.ServerReport.ReportServerUrl = new Uri("http://gcfd-websrvr/reportserver");
        rview.ServerReport.ReportPath = "http://gcfd-websrvr/gcfdinternalresources/Reports/" + Name + ".rdl";
        rview.ServerReport.Refresh();


        if (!string.IsNullOrEmpty(startDate))
        {
            startDateParameter.Name = "MealStartDate";
            startDateParameter.Values.Add(StartDate);
            rview.ServerReport.SetParameters(new ReportParameter[] { startDateParameter });
        }

        if (!string.IsNullOrEmpty(endDate))
        {
            endDateParameter.Name = "MealEndDate";
            endDateParameter.Values.Add(EndDate);
            rview.ServerReport.SetParameters(new ReportParameter[] { endDateParameter });
        }

        if (!string.IsNullOrEmpty(mealType))
        {
            mealTypeParameter.Name = "MealType";
            mealTypeParameter.Values.Add(MealType);
            rview.ServerReport.SetParameters(new ReportParameter[] { mealTypeParameter });
        }

        if (!string.IsNullOrEmpty(reportID))
        {
            reportIDParameter.Name = "ReportID";
            reportIDParameter.Values.Add(ReportID);
            rview.ServerReport.SetParameters(new ReportParameter[] { reportIDParameter });
        }

        if (!string.IsNullOrEmpty(communityArea))
        {
            communityAreaParameter.Name = "CommunityArea";
            communityAreaParameter.Values.Add(CommunityArea);
            rview.ServerReport.SetParameters(new ReportParameter[] { communityAreaParameter });
        }

        if (!string.IsNullOrEmpty(siteName))
        {
            siteNameParameter.Name = "SiteName";
            siteNameParameter.Values.Add(SiteName);
            rview.ServerReport.SetParameters(new ReportParameter[] { siteNameParameter });
        }

        if (!string.IsNullOrEmpty(labelText))
        {
            labelTextParameter.Name = "ReportText";
            labelTextParameter.Values.Add(LabelText);
            rview.ServerReport.SetParameters(new ReportParameter[] { labelTextParameter });
        }

        if (!string.IsNullOrEmpty(sliceCount))
        {
            sliceCountParameter.Name = "SliceCount";
            sliceCountParameter.Values.Add(SliceCount);
            rview.ServerReport.SetParameters(new ReportParameter[] { sliceCountParameter });
        }

        if (!string.IsNullOrEmpty(bunCount))
        {
            bunCountParameter.Name = "BunCount";
            bunCountParameter.Values.Add(BunCount);
            rview.ServerReport.SetParameters(new ReportParameter[] { bunCountParameter });
        }

        if (!string.IsNullOrEmpty(loafCount))
        {
            loafCountParameter.Name = "LoafCount";
            loafCountParameter.Values.Add(LoafCount);
            rview.ServerReport.SetParameters(new ReportParameter[] { loafCountParameter });
        }

        if (!string.IsNullOrEmpty(bagCount))
        {
            bagCountParameter.Name = "BagCount";
            bagCountParameter.Values.Add(BagCount);
            rview.ServerReport.SetParameters(new ReportParameter[] { bagCountParameter });
        }

        rview.Width = Unit.Percentage(100);

        Response.Clear();

        byte[] bytes = rview.ServerReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension,
            out streamids, out warnings);

        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-disposition", "filename=" + name + ".pdf");
        Response.OutputStream.Write(bytes, 0, bytes.Length);
        Response.OutputStream.Flush();
        Response.OutputStream.Close();
        Response.Flush();
        Response.Close();
    }
}
