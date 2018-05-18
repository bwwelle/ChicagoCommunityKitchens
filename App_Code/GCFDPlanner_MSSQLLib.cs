using System;
using System.Data;
using System.Web;
using GCFDDatabaseLibrary;

namespace GCFDPlannerDatabaseLibrary
{
    public interface IGCFDDatabase : I_GCFD_Database
    {
        bool dbWrite2WebServiceLog(string strSource, string strTransType, string strFeedback);
        bool dbWrite2WebServiceLog(string strSource, string strFeedback);
        bool dbUpdateCurrentState(string strParameter, string strValue, bool bLiteral);
        bool dbUpdateRequestState(string strParameter, string strValue, bool bLiteral);
        void dbTrashRemoval();
        DataSet dbGetLogLevels();
        DataSet dbGetTransLog();
        DataSet dbGetCDD(string strCDDType);
        DataSet GetReceipes(int selectedMealComponent);
        DataSet GetMealComponents();
        DataSet GetReceipesByID(int selecedReceipeID);
        DataSet GetMenuEvents(string eventDate, int receipeID, int targetMenu);
        DataSet GetMenuEventsByDate(DateTime eventDate);
        DataSet GetDeliverySites(string deliveryStatus);
    }

    public class GCFDPlanner_MSSQLLib : GCFD_MSSQL_Database
    {
        public void ClearSessionVariables()
        {
            HttpContext.Current.Session["NumberOfWeeksToDisplay"] = "-1";

            HttpContext.Current.Session["ReportType"] = "None";
            HttpContext.Current.Session["CommunityArea"] = "None";
            HttpContext.Current.Session["RecipeDisplayMode"] = "New";
            HttpContext.Current.Session["RecipeTypeName"] = "None";
            HttpContext.Current.Session["RecipeID"] = "-1";
            HttpContext.Current.Session["PageDenied"] = "";
            HttpContext.Current.Session["RecipeMode"] = "None";
            HttpContext.Current.Session["IngredientMode"] = "None";
            HttpContext.Current.Session["NonFoodItemMode"] = "None";
            HttpContext.Current.Session["LabelReportText"] = "None";

            HttpContext.Current.Session["SiteID"] = "-1";
            HttpContext.Current.Session["SiteName"] = "None";
            HttpContext.Current.Session["SiteMode"] = "None";

            HttpContext.Current.Session["IngredientID"] = "-1";
            HttpContext.Current.Session["NonFoodItemID"] = "-1";
            HttpContext.Current.Session["SiteViewMode"] = "None";

            HttpContext.Current.Session["DirectionID"] = "-1";
            HttpContext.Current.Session["OriginalNonFoodItemName"] = "None";
            HttpContext.Current.Session["OriginalMealDeliveryTypeName"] = "None";
            HttpContext.Current.Session["RecipeIngredientMode"] = "None";
            HttpContext.Current.Session["MealCalendarID"] = "None";
            HttpContext.Current.Session["MealCalendarDayCount"] = "0";
            HttpContext.Current.Session["RecipeIngredientID"] = "-1";
            HttpContext.Current.Session["NewIngredientName"] = "None";
            HttpContext.Current.Session["NewNonFoodItemName"] = "None";
            HttpContext.Current.Session["RecipeDirectionID"] = "-1";
            HttpContext.Current.Session["NewMealDeliveryTypeName"] = "None";
            HttpContext.Current.Session["RecipeDirectionMode"] = "None";
            HttpContext.Current.Session["DeliveryCalendarDayCount"] = "None";

            HttpContext.Current.Session["OriginalRecipeName"] = "None";

            HttpContext.Current.Session["RecipeIngredientName"] = "None";

            HttpContext.Current.Session["RecipeDirectionNumber"] = "-1";

            HttpContext.Current.Session["RecipeCondimentName"] = "-1";

            HttpContext.Current.Session["RecipeCondimentMode"] = "None";

            HttpContext.Current.Session["DeliveryWeekdays"] = "None";

            HttpContext.Current.Session["ServingWeekdays"] = "None";

            HttpContext.Current.Session["DeliveryEventMode"] = "None";

            HttpContext.Current.Session["DeliveryWeekdayCount"] = "0";

            HttpContext.Current.Session["PopupMode"] = "";

            HttpContext.Current.Session["ServingWeekdayMealCount"] = "";
            HttpContext.Current.Session["DeliveryCalendarMode"] = "";
            HttpContext.Current.Session["MealMode"] = "";
            HttpContext.Current.Session["DeliveryDate"] = "";
        	HttpContext.Current.Session["RecipeCondimentID"] = "-1";
        	HttpContext.Current.Session["RecipeCondimentMode"] = "None";

			HttpContext.Current.Session["CookChillMode"] = "None";

        	HttpContext.Current.Session["CookChillID"] = "-1";
        	HttpContext.Current.Session["MealID"] = "-1";
        	HttpContext.Current.Session["MealDate"] = "";

        	HttpContext.Current.Session["YieldEditMode"] = "None";
        	HttpContext.Current.Session["YieldEditObject"] = "None";
			HttpContext.Current.Session["RegularYieldType"] = "None";
			HttpContext.Current.Session["MealComponentType"] = "None";
        	HttpContext.Current.Session["NewRecipeName"] = "None";
			HttpContext.Current.Session["NewSiteName"] = "None";
			HttpContext.Current.Session["NewSiteAddress"] = "None";
			HttpContext.Current.Session["NewSiteCity"] = "None";
			HttpContext.Current.Session["NewSiteState"] = "None";
			HttpContext.Current.Session["NewSiteZip"] = "None";
			HttpContext.Current.Session["NewSiteContactName"] = "None";
			HttpContext.Current.Session["NewSiteContactPhone"] = "None";

            HttpContext.Current.Session["NewNotes"] = "None";
            HttpContext.Current.Session["NewNoService"] = "None";
            HttpContext.Current.Session["NewFEINNo"] = "None";
            HttpContext.Current.Session["NewDeliveryContactName"] = "None";
            HttpContext.Current.Session["NewDeliveryContactPhone"] = "None";

        	HttpContext.Current.Session["MealCount"] = "0";
			HttpContext.Current.Session["CurrentMonth"] = "None";
        	HttpContext.Current.Session["DeliveryCurrentMonth"] = "None";
        	HttpContext.Current.Session["MenuCurrentMonth"] = "None";
			HttpContext.Current.Session["BreakfastServingWeekdays"] = "None";
			HttpContext.Current.Session["ServingWeekdayBreakfastCount"] = "None";
        	HttpContext.Current.Session["ServingMode"] = "None";
        	HttpContext.Current.Session["ReportDate"] = "None";
            HttpContext.Current.Session["DisplayedRelatedEvents"] = "False";
            HttpContext.Current.Session["MealType"] = "None";
            HttpContext.Current.Session["NewLAHSiteCode"] = "None";
            HttpContext.Current.Session["NewEmergencyPhone"] = "None";
            HttpContext.Current.Session["NewEmergencyNotes"] = "None";
            HttpContext.Current.Session["NewEmail"] = "None";
            HttpContext.Current.Session["NewAgencyID"] = "None";
            HttpContext.Current.Session["NewIsColdMealSite"] = "None";

            HttpContext.Current.Session["NewISBECode"] = "None";
            HttpContext.Current.Session["NewNearestSchool"] = "None";
            HttpContext.Current.Session["NewUnmetNeed"] = "None";
            HttpContext.Current.Session["NewTrainingDate"] = "None";
            HttpContext.Current.Session["NewTrainee"] = "None";
            HttpContext.Current.Session["NewMealStartTime"] = "None";
            HttpContext.Current.Session["NewMealEndTime"] = "None";
            HttpContext.Current.Session["NewCommunityArea"] = "None";
            HttpContext.Current.Session["NewStartDate"] = "None";
            HttpContext.Current.Session["NewEndDate"] = "None";
            HttpContext.Current.Session["NewFax"] = "None";
            HttpContext.Current.Session["NewPhoneExtension"] = "None";
            HttpContext.Current.Session["NewAltContactPhone"] = "None";
            HttpContext.Current.Session["NewAltContactName"] = "None";
            HttpContext.Current.Session["NewFirstDelivery"] = "None";
            HttpContext.Current.Session["NewLastDelivery"] = "None";
            HttpContext.Current.Session["MealDeliveryTypeMode"] = "None";

            return;
        }

        public DataSet GetReceipes(int selectedMealComponent)
        {
            string strSQL = "SELECT * FROM vwRecipeDisplay WHERE iMealComponent = " + selectedMealComponent + " AND bActive = 1";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetAllReceipes()
        {
            string strSQL = "SELECT * FROM tblRecipe";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetActiveRecipes()
        {
            string strSQL = "SELECT * FROM tblRecipes WHERE bActive = 1";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetUserActions()
        {
            string strSQL = "SELECT * FROM UserAction ORDER BY DateCreated";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetActiveRecipesForGrid(string whereVariable)
        {
            string strSQL = "SELECT szRecipeName AS 'Recipe Name' FROM tblRecipes WHERE bActive = 1" + whereVariable;

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetMenuEventsByDate(DateTime eventDate)
        {
            string strSQL = "SELECT * FROM vwMenuEvents WHERE dtDate = '" + eventDate.ToShortDateString() + "'";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetRecipeType()
        {
            string strSQL = "SELECT * FROM RecipeTypeDict WHERE RecipeTypeID <> 0 ORDER BY RecipeTypeName";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetReceipesByID(int selectedReceipeID)
        {
            string strSQL = "SELECT * FROM vwRecipeDisplay WHERE iRecipeID = " + selectedReceipeID + " and bActive = 1";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetMenuEvents(DateTime eventDate, int receipeID, int targetMenu)
        {
            string strSQL = "SELECT * FROM tblMenuEvents WHERE dtDate = '" + eventDate.ToShortDateString() + "' AND iRecipeID = " + receipeID + " AND iTargetMenu = " + targetMenu;

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetScheduledDeliverySites(DateTime deliveryDate)
        {
            string strSQL = "SELECT * FROM tblDeliverySites LEFT OUTER JOIN tblDelivery ON tblDelivery.iSiteID = tblDeliverySites.iSiteID AND Right(tblDelivery.DeliveryID, 8) = '" + deliveryDate.ToString("yyyyMMdd") + "' WHERE tblDeliverySites.iSiteID Not IN(SELECT iSiteID FROM tblDelivery WHERE DeliveryStatus <> 'Scheduled') ORDER BY tblDeliverySites.szSiteName";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetDeliverySitesByStatus(string deliveryStatus, DateTime deliveryDate)
        {
            string strSQL = "SELECT * FROM tblDeliverySites INNER JOIN tblDelivery ON tblDelivery.iSiteID = tblDeliverySites.iSiteID AND tblDelivery.sDeliveryDate = '" + deliveryDate.ToShortDateString() + "' AND tblDelivery.DeliveryStatus = '" + deliveryStatus + "' ORDER BY tblDeliverySites.szSiteName";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetDeliveryInformationByID(int deliverySiteID, DateTime deliveryDate)
        {
            string strSQL = "SELECT * FROM tblDelivery WHERE tblDelivery.iSiteID = '" + deliverySiteID + "' AND tblDelivery.sDeliveryDate = '" + deliveryDate.ToShortDateString() + "'";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetCalendarMenus(DateTime eventStartDate, DateTime eventEndDate)
        {
            string strSQL = "SELECT * FROM vwMenuEvents WHERE dtDate >= '" + eventStartDate.ToShortDateString() + "' AND dtDate <= '" + eventEndDate.ToShortDateString() + "' ORDER BY dtDate, iTargetMenu, iPriority";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetCalendarDeliveries(DateTime eventStartDate, DateTime eventEndDate)
        {
            string strSQL = "SELECT tblDelivery.dtDeliveryDate, tblDelivery.DeliveryStatus AS 'DeliveryStatus', tblDeliverySites.szSiteName AS 'SiteName' FROM tblDelivery, tblDeliverySites WHERE tblDeliverySites.iSiteID = tblDelivery.iSiteID AND tblDelivery.sDeliveryDate >= CONVERT(DATETIME, '" + eventStartDate.ToShortDateString() + "') AND tblDelivery.sDeliveryDate <= CONVERT(DATETIME, '" + eventEndDate.ToShortDateString() + "') AND tblDelivery.DeliveryStatus <> 'Scheduled' ORDER BY tblDelivery.DeliveryStatus";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetIngredientsByName(string ingredientName)
        {
            string strSQL = "SELECT * FROM tblIngredients WHERE szIngredientName = '" + ingredientName + "'";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetReceipeById(string receipeID)
        {
            string strSQL = "SELECT * FROM vwRecipeDisplay WHERE iRecipeID = '" + receipeID + "' and bActive = 1";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetRecipeIngredientsById(string receipeID)
        {
			string strSQL = "SELECT RecipeIngredientID, IngredientName AS 'Ingredient Name', IngredientNumber as 'Number', Measure, RecipeUnit AS 'Recipe Unit', PrepInfo As 'Prep Info', Notes AS 'Notes' FROM vwRecipeIngredientDisplay WHERE RecipeID = '" + receipeID + "' AND Active = 1 ORDER BY 'Number'";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetRecipeDirectionsById(string receipeID)
        {
            string strSQL = "SELECT RecipeDirectionID, DirectionNumber AS 'Number', RecipeDirection AS 'Direction' FROM RecipeDirection WHERE RecipeID = '" + receipeID + "' AND Active = 1 ORDER BY DirectionNumber";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetRecipeCondimentsById(string receipeID)
        {
            string strSQL = "SELECT RecipeCondimentID, CondimentName AS 'Condiment', CondimentAmount AS 'Amount', CondimentTypeName AS 'Units', PackageType AS 'Delivery Package' FROM vwRecipeCondimentDisplay WHERE RecipeID = '" + receipeID + "'";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetFoodTypes()
        {
            string strSQL = "SELECT * FROM tblFoodType";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetMeasurements()
        {
            string strSQL = "SELECT * FROM Measurement";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetGenericIngredients()
        {
            string strSQL = "SELECT * FROM tblIngredientsGeneric";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetRecipes()
        {
            string strSQL = "SELECT * FROM tblRecipes";

            return dbSelectDataSet(strSQL);
        }
        
        public DataSet GetRecipesByLetter(string letter)
        {
            string strSQL = "SELECT * FROM tblRecipes WHERE LEFT(szRecipeName, 1) = '" + letter + "' ORDER BY RecipeName";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetActiveRecipesByLetter(string letter)
        {
			string strSQL = "SELECT RecipeID, RecipeName as 'Recipe Name' FROM Recipe WHERE LEFT(RecipeName, 1) = '" + letter + "' ORDER BY RecipeName";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetActiveIngredientsByLetter(string letter)
        {
            string strSQL = "SELECT IngredientID AS IngredientID, IngredientName as 'Ingredient Name' FROM Ingredient WHERE LEFT(IngredientName, 1) = '" + letter + "' and Active = 1 ORDER BY IngredientName";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetActiveNonFoodItemByLetter(string letter)
        {
            string strSQL = "SELECT NonFoodItemID AS NonFoodItemID, ItemName as 'Item Name' FROM NonFoodItem WHERE LEFT(ItemName, 1) = '" + letter + "' and Active = 1 ORDER BY ItemName";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetActiveMealDeliveryTypeByLetter(string letter)
        {
            string strSQL = "SELECT MealTypeID, MealTypeName as 'Meal Type Name' FROM MealTypeDict WHERE LEFT(MealTypeName, 1) = '" + letter + "' ORDER BY MealTypeName";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetDistinctRecipeIDs()
        {
            string strSQL = "SELECT DISTINCT(iRecipeID) FROM tblRecipes ORDER BY iRecipeID";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetMeasurementsByName(string measurementName)
        {
            string strSQL = "SELECT * FROM tblMeasurements WHERE szName = '" + measurementName + "'";

            return dbSelectDataSet(strSQL);
        }

        public DataSet GetSitesByLetter(string letter)
        {
            string strSQL = "SELECT SiteID, SiteName AS 'Site Name' FROM Site WHERE LEFT(SiteName, 1) = '" + letter + "' ORDER BY SiteName";

            return dbSelectDataSet(strSQL);
        }
    }
}

