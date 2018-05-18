using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GCFDGlobalsNamespace;
using System.Data.SqlClient;
using System.Globalization;
using System.Data;

/// <summary>
/// 
/// </summary>
public class DeliveryRecurrenceDetail
{    
    public string m_SQL;

    public DataTable Fetch(string DeliveryRecurrenceID) 
    {
        m_SQL = "SELECT DeliveryRecurrenceDetailID, MealCount, DeliveryDay, ServingDay, CONVERT(varchar, LastModified, 121) AS LastModified FROM DeliveryRecurrenceDetail WHERE DeliveryRecurrenceID = " + DeliveryRecurrenceID + " ORDER BY (CASE WHEN DeliveryDay = 'Sunday' THEN '1' WHEN DeliveryDay = 'Monday' THEN '2' WHEN DeliveryDay = 'Tuesday' THEN '3' WHEN DeliveryDay = 'Wednesday' THEN '4' WHEN DeliveryDay = 'Thursday' THEN '5' WHEN DeliveryDay = 'Friday' THEN '6' WHEN DeliveryDay = 'Saturday' THEN '7' END), (CASE WHEN ServingDay = 'Sunday' THEN '1' WHEN ServingDay = 'Monday' THEN '2' WHEN ServingDay = 'Tuesday' THEN '3' WHEN ServingDay = 'Wednesday' THEN '4' WHEN ServingDay = 'Thursday' THEN '5' WHEN ServingDay = 'Friday' THEN '6' WHEN ServingDay = 'Saturday' THEN '7' END)";
        DataSet m_DeliveryRecurrenceDetailsDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        return m_DeliveryRecurrenceDetailsDataSet.Tables[0];        
    }

    public string Update(string UserName, string DeliveryRecurrenceDetailID, string MealCount, string DeliveryDay, string ServingDay, string DeliveryRecurrenceDetailLastModifiedDate) 
    {
        m_SQL = "DECLARE @NewDeliveryRecurrenceID int EXECUTE spDeliveryRecurrenceDetail '" + UserName + "', " + DeliveryRecurrenceDetailID + ", " + MealCount + ", '" + DeliveryDay + "', '" + ServingDay + "', '" + DeliveryRecurrenceDetailLastModifiedDate + "', @NewDeliveryRecurrenceID = @NewDeliveryRecurrenceID OUTPUT SELECT @NewDeliveryRecurrenceID as 'DeliveryRecurrenceID'";
        DataSet m_DeliveryRecurrenceDetailsDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        return GCFDGlobals.dbGetValue(m_DeliveryRecurrenceDetailsDataSet.Tables[0].Rows[0], "DeliveryRecurrenceID");
    }

    public string Delete(string UserName, string DeliveryRecurrenceDetailID, string MealCount, string DeliveryDay, string ServingDay, string DeliveryRecurrenceDetailLastModifiedDate) 
    {
        m_SQL = "DECLARE @NewDeliveryRecurrenceID int EXECUTE spDeliveryRecurrenceDetail '" + UserName + "', " + DeliveryRecurrenceDetailID + ", " + MealCount + ", '" + DeliveryDay + "', '" + ServingDay + "', '" + DeliveryRecurrenceDetailLastModifiedDate + "', @NewDeliveryRecurrenceID = @NewDeliveryRecurrenceID OUTPUT SELECT @NewDeliveryRecurrenceID as 'DeliveryRecurrenceID'";
        DataSet m_DeliveryRecurrenceDetailsDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(m_SQL);

        return GCFDGlobals.dbGetValue(m_DeliveryRecurrenceDetailsDataSet.Tables[0].Rows[0], "DeliveryRecurrenceID");
    }
}