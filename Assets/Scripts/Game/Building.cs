/*
 *       Class: Building
 *      Author: Harish Bhagat
 *        Year: 2016
 */

using UnityEngine;

/// <summary>
/// Used to define a building class.
/// </summary>
public abstract class Building : MonoBehaviour
{
    // RTTI Type of building
    public BuildingType buildingType;

    // Variables
    public Vector2 taxMinMax;
    public Vector2 occupantMinMax;
    public Vector2 levelMinMax;
    public Vector2 happinessMinMax;

    void Awake()
    {
        // Set variables
        taxMinMax = new Vector2(0, 20);
        levelMinMax = new Vector2(1, 3);
        happinessMinMax = new Vector2(0, 100);

        switch(buildingType)
        {
            case BuildingType.Residential:
                occupantMinMax = new Vector2(0, 100);
                break;
            case BuildingType.Commercial:
                occupantMinMax = new Vector2(0, 225);
                break;
            case BuildingType.Office:
                occupantMinMax = new Vector2(0, 500);
                break;
            default:
                break;
        }
    }

    // Properties
    public int taxPercentage
    {
        get
        {
            return taxPercentage;
        }
        set
        {
            if (taxPercentage < taxMinMax.x)
                taxPercentage = (int)taxMinMax.x;
            else if (taxPercentage > taxMinMax.y)
                taxPercentage = (int)taxMinMax.y;
        }
    }

    public int occupantCount
    {
        get
        {
            return occupantCount;
        }
        set
        {
            if (occupantCount < occupantMinMax.x)
                occupantCount = (int)occupantMinMax.x;
            else if (occupantCount > occupantMinMax.y)
                occupantCount = (int)occupantMinMax.y;
        }
    }

    public int level
    {
        get
        {
            return level;
        }
        set
        {
            if (level < levelMinMax.x)
                level = (int)levelMinMax.x;
            else if (level > levelMinMax.y)
                level = (int)levelMinMax.y;
        }
    }

    public int happiness
    {
        get
        {
            return happiness;
        }
        set
        {
            if (happiness < happinessMinMax.x)
                happiness = (int)happinessMinMax.x;
            else if (happiness > happinessMinMax.y)
                happiness = (int)happinessMinMax.y;
        }
    }

    /// <summary>
    /// Calculates the amount of tax generated within a given time.
    /// </summary>
    /// <param name="collectionInterval">How often tax should be collected.</param>
    /// <param name="lastCollection">The amount of time that has passed since the last tax collection.</param>
    /// <returns></returns>
    public virtual float CollectTax(float collectionInterval, float lastCollection)
    {
        float tax;
        float timePassed = collectionInterval - lastCollection;

        // Calculate the amount of time that has passed since the last collection
        if (timePassed < 0)
            timePassed = collectionInterval + (timePassed * -1);

        // Calculate the amount of tax generated
        tax = ((taxPercentage * occupantCount) / (level * happiness)) * 100;

        // Correct the amount of tax for the amount of time that has passed since the last collection
        tax *= (timePassed / collectionInterval);

        return tax;
    }
}