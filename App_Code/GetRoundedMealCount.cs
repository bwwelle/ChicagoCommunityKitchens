using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class GetRoundedMealCount
{
    private string productionCount;
    private string mealCount;

    public string MealCount
    {
        get
        {
            return mealCount;
        }
        set
        {
            mealCount = value;
        }
    }

    public string ProductionCount
    {
        get
        {
            return productionCount;
        }
        set
        {
            productionCount = value;
        }
    }

    public string RoundedMealCountForCurrentMeal()
    {
        if (Convert.ToInt32(mealCount) > 99)
        {
            if (Convert.ToString(mealCount).Length == 4)
            {
                if (Convert.ToInt32(Convert.ToString(mealCount).Substring(2, 2)) >= 0 &&
                    Convert.ToInt32(Convert.ToString(mealCount).Substring(2, 2)) <= 25)
                {
                    productionCount = Convert.ToString(mealCount).Substring(0, 2) + "75";
                }
                else if (Convert.ToInt32(Convert.ToString(mealCount).Substring(2, 2)) >= 26 &&
                         Convert.ToInt32(Convert.ToString(mealCount).Substring(2, 2)) <= 50)
                {
                    productionCount = Convert.ToString(Convert.ToInt32(Convert.ToString(mealCount).Substring(0, 2)) + 1) +
                                      "00";
                }
                else if (Convert.ToInt32(Convert.ToString(mealCount).Substring(2, 2)) >= 51 &&
                         Convert.ToInt32(Convert.ToString(mealCount).Substring(2, 2)) <= 75)
                {
                    productionCount = Convert.ToString(Convert.ToInt32(Convert.ToString(mealCount).Substring(0, 2)) + 1) +
                                      "25";
                }
                else if (Convert.ToInt32(Convert.ToString(mealCount).Substring(2, 2)) >= 76 &&
                         Convert.ToInt32(Convert.ToString(mealCount).Substring(2, 2)) <= 99)
                {
                    productionCount =
                        Convert.ToString(Convert.ToInt32(Convert.ToString(mealCount).Substring(0, 2)) + 1) + "50";
                }
            }
            else
            {
                if (Convert.ToInt32(Convert.ToString(mealCount).Substring(1, 2)) >= 0 &&
                    Convert.ToInt32(Convert.ToString(mealCount).Substring(1, 2)) <= 25)
                {
                    productionCount = Convert.ToString(mealCount).Substring(0, 1) + "75";
                }
                else if (Convert.ToInt32(Convert.ToString(mealCount).Substring(1, 2)) >= 26 &&
                         Convert.ToInt32(Convert.ToString(mealCount).Substring(1, 2)) <= 50)
                {
                    productionCount = Convert.ToString(Convert.ToInt32(Convert.ToString(mealCount).Substring(0, 1)) + 1) +
                                      "00";
                }
                else if (Convert.ToInt32(Convert.ToString(mealCount).Substring(1, 2)) >= 51 &&
                         Convert.ToInt32(Convert.ToString(mealCount).Substring(1, 2)) <= 75)
                {
                    productionCount = Convert.ToString(Convert.ToInt32(Convert.ToString(mealCount).Substring(0, 1)) + 1) +
                                      "25";
                }
                else if (Convert.ToInt32(Convert.ToString(mealCount).Substring(1, 2)) >= 76 &&
                         Convert.ToInt32(Convert.ToString(mealCount).Substring(1, 2)) <= 99)
                {
                    productionCount =
                        Convert.ToString(Convert.ToInt32(Convert.ToString(mealCount).Substring(0, 1)) + 1) + "50";
                }
            }
        }
        else
        {
            productionCount = Convert.ToString(mealCount);
        }

        return productionCount;
    }
}