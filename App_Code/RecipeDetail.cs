using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using GCFDGlobalsNamespace;

/// <summary>
/// 
/// </summary>
public class RecipeDetail
{
    private string sql;
    private string recipeID;
    private string recipeDetailID;
    private string isDefault;
    private string servingSize;
    private string servingSizeTypeID;
    private string servingSizeTypeName;
    private string volumeWeight;
    private string conversion;
    private string numberOfServings;
    private string yieldInPounds;
    private string packageTypeID;
    private string packageTypeName;
    private string packagesPerBatch;
    private string servingsPerPackage;
    private string servingsPerBatch;
    private string notes;

    public string RecipeID
    {
        get
        {
            return recipeID;
        }
        set
        {
            recipeID = value;
        }
    }
    public string RecipeDetailID
    {
        get
        {
            return recipeDetailID;
        }
        set
        {
            recipeDetailID = value;
        }
    }
    public string IsDefault
    {
        get
        {
            return isDefault;
        }
        set
        {
            isDefault = value;
        }
    }
    public string ServingSize
    {
        get
        {
            return servingSize;
        }
        set
        {
            servingSize = value;
        }
    }
    public string ServingSizeTypeID
    {
        get
        {
            return servingSizeTypeID;
        }
        set
        {
            servingSizeTypeID = value;
        }
    }
    public string ServingSizeTypeName
    {
        get
        {
            return servingSizeTypeName;
        }
        set
        {
            servingSizeTypeName = value;
        }
    }    
    public string VolumeWeight
    {
        get
        {
            return volumeWeight;
        }
        set
        {
            volumeWeight = value;
        }
    }
    public string Conversion
    {
        get
        {
            return conversion;
        }
        set
        {
            conversion = value;
        }
    }
    public string NumberOfServings
    {
        get
        {
            return numberOfServings;
        }
        set
        {
            numberOfServings = value;
        }
    }    
    public string YieldInPounds
    {
        get
        {
            return yieldInPounds;
        }
        set
        {
            yieldInPounds = value;
        }
    }
    public string PackageTypeID
    {
        get
        {
            return packageTypeID;
        }
        set
        {
            packageTypeID = value;
        }
    }
    public string PackageTypeName
    {
        get
        {
            return packageTypeName;
        }
        set
        {
            packageTypeName = value;
        }
    }
    public string PackagesPerBatch
    {
        get
        {
            return packagesPerBatch;
        }
        set
        {
            packagesPerBatch = value;
        }
    }
    public string ServingsPerPackage
    {
        get
        {
            return servingsPerPackage;
        }
        set
        {
            servingsPerPackage = value;
        }
    }
    public string ServingsPerBatch
    {
        get
        {
            return servingsPerBatch;
        }
        set
        {
            servingsPerBatch = value;
        }
    }
    public string Notes
    {
        get
        {
            return notes;
        }
        set
        {
            notes = value;
        }
    }

    public void GetMealRecipeDetail()
    {
        sql = "SELECT DISTINCT RecipeID, IsDefault, ServingSize, ServingSizeTypeID, ServingSizeTypeName, VolumeWeight, Conversion, NumberOfServings, YieldInPounds, PackageTypeID, PackageTypeName, PackagesPerBatch, ServingsPerPackage, ServingsPerBatch, Notes FROM vwRecipeDetail WHERE RecipeDetailID = " + recipeDetailID;
        DataSet recipeYieldDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(sql);

        foreach (DataRow recipeYieldDataRow in recipeYieldDataSet.Tables[0].Rows)
        {
            recipeID = GCFDGlobals.dbGetValue(recipeYieldDataRow, "RecipeID");
            isDefault = GCFDGlobals.dbGetValue(recipeYieldDataRow, "IsDefault");
            servingSize = GCFDGlobals.dbGetValue(recipeYieldDataRow, "ServingSize");
            servingSizeTypeID = GCFDGlobals.dbGetValue(recipeYieldDataRow, "ServingSizeTypeID");
            servingSizeTypeName = GCFDGlobals.dbGetValue(recipeYieldDataRow, "ServingSizeTypeName");
            volumeWeight = GCFDGlobals.dbGetValue(recipeYieldDataRow, "VolumeWeight");
            conversion = GCFDGlobals.dbGetValue(recipeYieldDataRow, "Conversion");
            numberOfServings = GCFDGlobals.dbGetValue(recipeYieldDataRow, "NumberOfServings");
            yieldInPounds = GCFDGlobals.dbGetValue(recipeYieldDataRow, "YieldInPounds");
            packageTypeID = GCFDGlobals.dbGetValue(recipeYieldDataRow, "PackageTypeID");
            packageTypeName = GCFDGlobals.dbGetValue(recipeYieldDataRow, "PackageTypeName");
            packagesPerBatch = GCFDGlobals.dbGetValue(recipeYieldDataRow, "PackagesPerBatch");
            servingsPerPackage = GCFDGlobals.dbGetValue(recipeYieldDataRow, "ServingsPerPackage");
            servingsPerBatch = GCFDGlobals.dbGetValue(recipeYieldDataRow, "ServingsPerBatch");
            notes = GCFDGlobals.dbGetValue(recipeYieldDataRow, "Notes");
        }
    }

    public void GetDefaultRecipeDetail()
    {
        sql = "SELECT DISTINCT RecipeID, IsDefault, ServingSize, ServingSizeTypeID, ServingSizeTypeName, VolumeWeight, Conversion, NumberOfServings, YieldInPounds, PackageTypeID, PackageTypeName, PackagesPerBatch, ServingsPerPackage, ServingsPerBatch, Notes FROM vwRecipeDetail WHERE RecipeID = " + recipeID + " AND IsDefault = 'true'";
        DataSet recipeYieldDataSet = GCFDGlobals.m_GCFDPlannerDatabaseLibrary.dbSelectDataSet(sql);

        foreach (DataRow recipeYieldDataRow in recipeYieldDataSet.Tables[0].Rows)
        {
            recipeID = GCFDGlobals.dbGetValue(recipeYieldDataRow, "RecipeID");
            isDefault = GCFDGlobals.dbGetValue(recipeYieldDataRow, "IsDefault");
            servingSize = GCFDGlobals.dbGetValue(recipeYieldDataRow, "ServingSize");
            servingSizeTypeID = GCFDGlobals.dbGetValue(recipeYieldDataRow, "ServingSizeTypeID");
            servingSizeTypeName = GCFDGlobals.dbGetValue(recipeYieldDataRow, "ServingSizeTypeName");
            volumeWeight = GCFDGlobals.dbGetValue(recipeYieldDataRow, "VolumeWeight");
            conversion = GCFDGlobals.dbGetValue(recipeYieldDataRow, "Conversion");
            numberOfServings = GCFDGlobals.dbGetValue(recipeYieldDataRow, "NumberOfServings");
            yieldInPounds = GCFDGlobals.dbGetValue(recipeYieldDataRow, "YieldInPounds");
            packageTypeID = GCFDGlobals.dbGetValue(recipeYieldDataRow, "PackageTypeID");
            packageTypeName = GCFDGlobals.dbGetValue(recipeYieldDataRow, "PackageTypeName");
            packagesPerBatch = GCFDGlobals.dbGetValue(recipeYieldDataRow, "PackagesPerBatch");
            servingsPerPackage = GCFDGlobals.dbGetValue(recipeYieldDataRow, "ServingsPerPackage");
            servingsPerBatch = GCFDGlobals.dbGetValue(recipeYieldDataRow, "ServingsPerBatch");
            notes = GCFDGlobals.dbGetValue(recipeYieldDataRow, "Notes");
        }
    }
}