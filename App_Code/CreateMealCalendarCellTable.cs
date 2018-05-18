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
public class CreateMealCalendarCellTable
{
    private DateTime mealDate;
    private string mealTypeID;
    private string m_SQL;
    private DataSet calendarScheduledDataSet;
    private DataSet calendarRescheduledDataSet;
    private DataSet scheduledTotalMealCountDataSet;
    private DataSet rescheduledTotalMealCountDataSet;

    public DataSet ScheduledTotalMealCountDataSet
    {
        get
        {
            return scheduledTotalMealCountDataSet;
        }
        set
        {
            scheduledTotalMealCountDataSet = value;
        }
    }

    public DataSet RescheduledTotalMealCountDataSet
    {
        get
        {
            return rescheduledTotalMealCountDataSet;
        }
        set
        {
            rescheduledTotalMealCountDataSet = value;
        }
    }

    public DataSet CalendarRescheduledDataSet
    {
        get
        {
            return calendarRescheduledDataSet;
        }
        set
        {
            calendarRescheduledDataSet = value;
        }
    }

    public DataSet CalendarScheduledDataSet
    {
        get
        {
            return calendarScheduledDataSet;
        }
        set
        {
            calendarScheduledDataSet = value;
        }
    }
    
    public DateTime MealDate
    {
        get
        {
            return (mealDate);
        }
        set
        {
            mealDate = value;
        }
    }

    public string MealTypeID
    {
        get
        {
            return (mealTypeID);
        }
        set
        {
            mealTypeID = value;
        }
    }

    public Table CalendarCellTable()
    {
        Table m_MealTable = new Table();
        TableRow m_MealRow = new TableRow();
        TableCell m_RegularMealTableCell = new TableCell();
        TableCell m_ExtraMealTableCell = new TableCell();

        m_MealTable.CellPadding = 0;
        m_MealTable.CellSpacing = 0;

        m_MealTable.Width = Unit.Percentage(100);
        m_MealTable.HorizontalAlign = HorizontalAlign.Center;

        m_RegularMealTableCell.Width = Unit.Percentage(50);
        m_RegularMealTableCell.VerticalAlign = VerticalAlign.Top;
        m_RegularMealTableCell.HorizontalAlign = HorizontalAlign.Left;

        m_ExtraMealTableCell.Width = Unit.Percentage(50);
        m_ExtraMealTableCell.VerticalAlign = VerticalAlign.Top;
        m_ExtraMealTableCell.HorizontalAlign = HorizontalAlign.Right;

        m_MealRow.Width = Unit.Percentage(100);
        m_MealRow.VerticalAlign = VerticalAlign.Top;

        m_RegularMealTableCell.Controls.Add(RegularMealTable());
        m_MealRow.Controls.Add(m_RegularMealTableCell);

        m_ExtraMealTableCell.Controls.Add(ExtraMealTable());
        m_MealRow.Controls.Add(m_ExtraMealTableCell);

        m_MealTable.Controls.Add(m_MealRow);

        return m_MealTable;
    }

    public Table RegularMealTable()
    {
        Table m_RegularMealTable = new Table();
        TableCell regularTotalTableCell = new TableCell();
        TableRow totalTableRow = new TableRow();
        TableCell regularTableCell = new TableCell();
        TableRow regularTableRow = new TableRow();
        TableRow spaceTableRow = new TableRow();
        TableCell spaceTableCell = new TableCell();
        TableCell regularMealTotalTableCell = new TableCell();
        TableRow regularMealTotalTableRow = new TableRow();

        string totalRegularMealCount = "0";        
        int buttonCount = 1;
        string mealID = "";

        m_RegularMealTable.CellPadding = 0;
        m_RegularMealTable.CellSpacing = 0;

        DataRow[] drFiltered = ScheduledTotalMealCountDataSet.Tables[0].Select("DeliveryDate = #" + mealDate.ToString("MM/dd/yyyy") + "#");

        if (drFiltered.Length != 0)
        {
            totalRegularMealCount = GCFDGlobals.dbGetValue(drFiltered[0], "MealCount");
        }

        Label totalMealCountLabel = new Label();
        totalMealCountLabel.Text = totalRegularMealCount;
        totalMealCountLabel.Font.Name = "Arial";
        totalMealCountLabel.Font.Bold = true;
        totalMealCountLabel.Font.Underline = true;
        totalMealCountLabel.Font.Size = FontUnit.Parse("8");
        totalMealCountLabel.ID = "TotalMealCountButton";
        regularMealTotalTableCell.Width = Unit.Percentage(100);
        regularMealTotalTableCell.HorizontalAlign = HorizontalAlign.Left;
        regularMealTotalTableCell.Controls.Add(totalMealCountLabel);
        regularMealTotalTableRow.Controls.Add(regularMealTotalTableCell);
        regularMealTotalTableRow.Width = Unit.Percentage(100);
        m_RegularMealTable.Controls.Add(regularMealTotalTableRow);

        DataRow[] drMealDataFiltered = CalendarScheduledDataSet.Tables[0].Select("DeliveryDate = #" + mealDate.ToString("MM/dd/yyyy") + "#");

        for (int i = 0; i < drMealDataFiltered.Length; i++)
        {
            regularTotalTableCell = new TableCell();
            totalTableRow = new TableRow();
            regularTableCell = new TableCell();
            regularTableRow = new TableRow();
            spaceTableRow = new TableRow();
            spaceTableCell = new TableCell();

            if (GCFDGlobals.dbGetValue(drMealDataFiltered[i], "MealID") != mealID)
            {
                mealID = GCFDGlobals.dbGetValue(drMealDataFiltered[i], "MealID");

                LinkButton mealCountLabel = new LinkButton();
                mealCountLabel.Style.Add("text-decoration", "none");
                mealCountLabel.Text = GCFDGlobals.dbGetValue(drMealDataFiltered[i], "MealCount");
                mealCountLabel.Font.Name = "Arial";
                mealCountLabel.Font.Bold = false;
                mealCountLabel.Font.Size = FontUnit.Parse("8");
                mealCountLabel.ForeColor = System.Drawing.Color.Black;
                mealCountLabel.ID = "MealCountButton" + mealID;
                mealCountLabel.Attributes.Add("href", "javascript:__doPostBack('CalendarLinkButton','" + mealID + "')");
                regularTotalTableCell.Width = Unit.Percentage(100);
                regularTotalTableCell.HorizontalAlign = HorizontalAlign.Left;
                regularTotalTableCell.Controls.Add(mealCountLabel);
                totalTableRow.Controls.Add(regularTotalTableCell);

                m_RegularMealTable.Controls.Add(totalTableRow);
            }

            LinkButton mealDetailLabel = new LinkButton();
            mealDetailLabel.Text = GCFDGlobals.dbGetValue(drMealDataFiltered[i], "RecipeName");
            mealDetailLabel.Font.Name = "Arial";
            mealDetailLabel.Font.Bold = false;
            mealDetailLabel.Font.Size = FontUnit.Parse("8");

            switch (GCFDGlobals.dbGetValue(drMealDataFiltered[i], "RecipeTypeID"))
            {
                case ("1"):
                    mealDetailLabel.ForeColor = System.Drawing.Color.Black ;

                    break;
                case ("2"):
                    mealDetailLabel.ForeColor = System.Drawing.Color.DarkGreen  ;

                    break;
                case ("3"):
                    mealDetailLabel.ForeColor = System.Drawing.Color.DarkRed;

                    break;
                case ("4"):
                    mealDetailLabel.ForeColor = System.Drawing.Color.DarkCyan;

                    break;
                case ("5"):
                    mealDetailLabel.ForeColor = System.Drawing.Color.SteelBlue;

                    break;
                case ("6"):
                    mealDetailLabel.ForeColor = System.Drawing.Color.DarkSeaGreen;

                    break;
            }

            mealDetailLabel.ID = buttonCount + "LinkButton" + mealID;
            mealDetailLabel.Style.Add("text-decoration", "none");
            mealDetailLabel.Attributes.Add("href", "javascript:__doPostBack('CalendarLinkButton','" + mealID + "')");
            regularTableCell.Width = Unit.Percentage(100);
            regularTableCell.HorizontalAlign = HorizontalAlign.Left;

            regularTableCell.Controls.Add(mealDetailLabel);
            regularTableRow.Controls.Add(regularTableCell);
            regularTableRow.Width = Unit.Percentage(100);

            m_RegularMealTable.Controls.Add(regularTableRow);

            buttonCount = buttonCount + 1;

            m_RegularMealTable.Controls.Add(spaceTableRow);
        }

        return m_RegularMealTable;
    }

    public Table ExtraMealTable()
    {
        Table m_ExtraMealTable = new Table();
        TableCell extraTotalTableCell = new TableCell();
        TableRow totalTableRow = new TableRow();
        TableCell extraTableCell = new TableCell();
        TableRow extraTableRow = new TableRow();
        TableRow spaceTableRow = new TableRow();
        TableCell spaceTableCell = new TableCell();
        TableCell extraMealTotalTableCell = new TableCell();
        TableRow extraMealTotalTableRow = new TableRow();

        string totalExtraMealCount = "0";
        string mealID = "";
        int buttonCount = 1;

        m_ExtraMealTable.CellPadding = 0;
        m_ExtraMealTable.CellSpacing = 0;
        m_ExtraMealTable.Width = Unit.Percentage(100);
        m_ExtraMealTable.HorizontalAlign = HorizontalAlign.Left;

        DataRow[] drFiltered = RescheduledTotalMealCountDataSet.Tables[0].Select("DeliveryDate = #" + mealDate.ToString("MM/dd/yyyy") + "#");

        if (drFiltered.Length != 0)
        {
            totalExtraMealCount = GCFDGlobals.dbGetValue(drFiltered[0], "MealCount");
        }

        LinkButton totalMealCountLabel = new LinkButton();
        totalMealCountLabel.Text = totalExtraMealCount;
        totalMealCountLabel.Font.Name = "Arial";
        totalMealCountLabel.Font.Bold = true;
        totalMealCountLabel.Font.Underline = true;
        totalMealCountLabel.Font.Size = FontUnit.Parse("8");
        totalMealCountLabel.ID = "TotalExtaMealCountButton" + mealDate;
        totalMealCountLabel.ForeColor = System.Drawing.Color.Black;
        totalMealCountLabel.Attributes.Add("href", "javascript:__doPostBack('CalendarLinkButton','ExtraMealCount" + mealDate.ToString("MM/dd/yyyy") + "')");
        extraMealTotalTableCell.Width = Unit.Percentage(100);

        if (totalMealCountLabel.Text != "0")
        {
            extraMealTotalTableCell.BackColor = System.Drawing.Color.Yellow;
        }

        extraMealTotalTableCell.HorizontalAlign = HorizontalAlign.Right;
        extraMealTotalTableCell.Controls.Add(totalMealCountLabel);
        extraMealTotalTableRow.Controls.Add(extraMealTotalTableCell);
        extraMealTotalTableRow.Width = Unit.Percentage(100);

        m_ExtraMealTable.Controls.Add(extraMealTotalTableRow);

        DataRow[] drMealDataFiltered = CalendarRescheduledDataSet.Tables[0].Select("DeliveryDate = #" + mealDate.ToString("MM/dd/yyyy") + "#");

        for (int i = 0; i < drMealDataFiltered.Length; i++)
        {
            extraTotalTableCell = new TableCell();
            totalTableRow = new TableRow();
            extraTableCell = new TableCell();
            extraTableRow = new TableRow();
            spaceTableRow = new TableRow();
            spaceTableCell = new TableCell();

            if (GCFDGlobals.dbGetValue(drMealDataFiltered[i], "MealID") != mealID)
            {
                mealID = GCFDGlobals.dbGetValue(drMealDataFiltered[i], "MealID");

                LinkButton mealCountLabel = new LinkButton();
                mealCountLabel.Style.Add("text-decoration", "none");
                mealCountLabel.Text = GCFDGlobals.dbGetValue(drMealDataFiltered[i], "MealCount");
                mealCountLabel.Font.Name = "Arial";
                mealCountLabel.Font.Bold = false;
                mealCountLabel.Font.Size = FontUnit.Parse("8");
                mealCountLabel.ForeColor = System.Drawing.Color.Black;
                mealCountLabel.ID = "MealCountButton" + mealID;
                mealCountLabel.Attributes.Add("href", "javascript:__doPostBack('CalendarLinkButton','" + mealID + "')");
                extraTotalTableCell.Width = Unit.Percentage(100);
                extraTotalTableCell.HorizontalAlign = HorizontalAlign.Right;
                extraTotalTableCell.Controls.Add(mealCountLabel);
                totalTableRow.Controls.Add(extraTotalTableCell);

                m_ExtraMealTable.Controls.Add(totalTableRow);
            }

            LinkButton mealDetailLabel = new LinkButton();
            mealDetailLabel.Text = GCFDGlobals.dbGetValue(drMealDataFiltered[i], "RecipeName");
            mealDetailLabel.Font.Name = "Arial";
            mealDetailLabel.Font.Bold = false;
            mealDetailLabel.Font.Size = FontUnit.Parse("8");

            switch (GCFDGlobals.dbGetValue(drMealDataFiltered[i], "RecipeTypeID"))
            {
                case ("1"):
                    mealDetailLabel.ForeColor = System.Drawing.Color.Black;

                    break;
                case ("2"):
                    mealDetailLabel.ForeColor = System.Drawing.Color.DarkGreen;

                    break;
                case ("3"):
                    mealDetailLabel.ForeColor = System.Drawing.Color.DarkRed;

                    break;
                case ("4"):
                    mealDetailLabel.ForeColor = System.Drawing.Color.DarkCyan;

                    break;
                case ("5"):
                    mealDetailLabel.ForeColor = System.Drawing.Color.SteelBlue;

                    break;
                case ("6"):
                    mealDetailLabel.ForeColor = System.Drawing.Color.DarkSeaGreen;
                   
                    break;
            }

            mealDetailLabel.ID = buttonCount + "LinkButton" + mealID;
            mealDetailLabel.Style.Add("text-decoration", "none");
            mealDetailLabel.Attributes.Add("href", "javascript:__doPostBack('CalendarLinkButton','" + mealID + "')");
            extraTableCell.Width = Unit.Percentage(100);
            extraTableCell.HorizontalAlign = HorizontalAlign.Right;

            extraTableCell.Controls.Add(mealDetailLabel);
            extraTableRow.Controls.Add(extraTableCell);
            extraTableRow.Width = Unit.Percentage(100);

            m_ExtraMealTable.Controls.Add(extraTableRow);

            buttonCount = buttonCount + 1;

            m_ExtraMealTable.Controls.Add(spaceTableRow);
        }

        return m_ExtraMealTable;
    }
}