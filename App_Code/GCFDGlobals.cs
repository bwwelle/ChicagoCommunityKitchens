using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System.Reflection;
using GCFDPlannerDatabaseLibrary;

namespace GCFDGlobalsNamespace
{
    public class GCFDGlobals
    {
        private string m_strConnectString;
        public static HttpResponse Response;
        public static HttpRequest Request;
        public static string strCookieValue;
        public const string C_VERSION = "  Version 1.0.0.0";
        private bool m_bGoGeneric = false;
        public static string strAppID = "1";
        public const string C_SESSION_VALID_LOGIN = "VALID_LOGIN";
        public static GCFDPlanner_MSSQLLib m_GCFDPlannerDatabaseLibrary;
        public const string C_MY_AMALGAMATE_NAME = "Amalgamate";        
        public static string strCookieName;
        public const string C_MY_NAME = "Main";
        private string m_strMyServerName = "";        
        private static bool m_bTracking;        
        private static bool m_bVerbosity;        
        private static bool m_bTransLogging;

        public GCFDGlobals(string strConnnectString)
        {
            MethodBase oMethod = MethodBase.GetCurrentMethod();

            if (!Directory.Exists(ConfigurationManager.AppSettings.Get("LogDirectory")))
            {
                Directory.CreateDirectory(ConfigurationManager.AppSettings.Get("LogDirectory"));
            }

            m_strConnectString = strConnnectString;

            m_bTracking = false;

            if (!OpenTheDatabase()) return;

            m_GCFDPlannerDatabaseLibrary.dbOpenConnection(m_strConnectString);
                 
            return;
        }

        public static bool TransLogging
        {
            get { return m_bTransLogging; }
            
            set { m_bTransLogging = value; }
        }

        public static bool Verbosity
        {
            get { return m_bVerbosity; }
            
            set { m_bVerbosity = value; }
        }

        public string MyServerName
        {
            get { return m_strMyServerName; }
            
            set { m_strMyServerName = value.ToUpper(); }
        }

        public string ConnectString
        {
            get { return m_strConnectString; }

            set { m_strConnectString = value; }
        }

        public bool GoGeneric
        {
            get { return m_bGoGeneric; }

            set { m_bGoGeneric = value; }
        }

       public static string dbGetValue(DataSet oData, int iRow, string strColumnName)
        {
            string strData = oData.Tables[0].Rows[iRow].ItemArray[
                oData.Tables[0].Columns.IndexOf(strColumnName)].ToString();

            if (strData == null) return "";

            return strData;
        }

        public static string dbGetValue(DataRow oData, string strColumnName)
                 {
            string strData = oData.ItemArray[
                oData.Table.Columns.IndexOf(strColumnName)].ToString();

            if (strData == null) return "";

            return strData;
        }

        public static string SetCookieValue(string strVariableName, string strValue)
        {
            Response = HttpContext.Current.Response;

            Request = HttpContext.Current.Request;

            strCookieName = "C_" + strVariableName;

            if (Request.Cookies[strCookieName] != null)
            {
                strCookieValue = Request.Cookies[strCookieName].Value;

                if (strCookieValue != strValue)
                {
                    Request.Cookies.Remove(strCookieName);
                }
            }

            if (Request.Cookies[strCookieName] == null)
            {
                HttpCookie appCookie = new HttpCookie(strCookieName);

                appCookie.Value = strValue;

                appCookie.Expires = DateTime.Now.AddDays(365);

                Response.Cookies.Add(appCookie);
            }

            return "true";
        }

        public static void Tracker(string strItem)
        {
            if (m_bTracking == true)
            {
            }
        }

        public static string FormErrorLog(System.Reflection.MethodBase oMethod, string strCallingFunction, string strErrorMsg)
        {
            return "An unexpected Error has occurred  within Class.Routine = [" + oMethod.DeclaringType.Name + "." + oMethod.Name + "]" +
                        " when Calling [" + strCallingFunction + "]" + strErrorMsg;
        }

        public static string FormErrorLog(Exception ex, System.Reflection.MethodBase oMethod)
        {
            return "An unexpected Error has occurred  within Class.Routine = [" + oMethod.DeclaringType.Name + "." + oMethod.Name + "] " +
                ex.Message;
        }

        private bool OpenTheDatabase()
        {
            if (m_GCFDPlannerDatabaseLibrary == null)
                    m_GCFDPlannerDatabaseLibrary = new GCFDPlanner_MSSQLLib();

            return true;
        }

        public static void ErrorHandler(string strWebService, MethodBase oMethod, string strCallingFunction, string strErrorMsg)
        {
            string strFullMsg = "An unexpected Error has occurred  within Class.Routine = [" + oMethod.DeclaringType.Name + "." + oMethod.Name + "]" +
                " when Calling [" + strCallingFunction + "]" + strErrorMsg;
        }

        public static void ErrorHandler(string strWebService, Exception Ex, System.Reflection.MethodBase oMethod)
        {
        }

        public static string GetConnectString()
        {
            string strConnectString = ConfigurationManager.AppSettings.Get("ConnectionString");

            return strConnectString;
        }

        public static bool ReconnectDB(string strConnnectString)
        {

            if (m_GCFDPlannerDatabaseLibrary == null)
                m_GCFDPlannerDatabaseLibrary = new GCFDPlanner_MSSQLLib();

             m_GCFDPlannerDatabaseLibrary.dbOpenConnection(strConnnectString);

            return true;
        }
    }
}