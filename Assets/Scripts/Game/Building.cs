/*
 *       Class: Building
 *      Author: Harish Bhagat
 *        Year: 2017
 */

using UnityEngine;

/// <summary>
/// Used to define a building class.
/// </summary>
public class Building : MonoBehaviour
{
    // RTTI Type of building
    public BuildingType type;

    // Variables
    public BuildingDirection Direction;

    public Vector2 taxMinMax = new Vector2(0, 20);
    public Vector2 occupantMinMax = new Vector2(0, 100);
    public Vector2 levelMinMax = new Vector2(1, 3);
    public Vector2 happinessMinMax = new Vector2(0, 100);

    private int taxPercentage;
    private int occupantCount;
    private int level;
    private int happiness;

    private void Awake()
    {
        switch(type)
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
        }
    }

    // Properties
    public int TaxPercentage
    {
        get
        {
            return taxPercentage;
        }
        set
        {
            if (value < taxMinMax.x)
                taxPercentage = (int) taxMinMax.x;
            else if (value > taxMinMax.y)
                taxPercentage = (int) taxMinMax.y;
            else
                taxPercentage = value;
        }
    }

    public int OccupantCount
    {
        get
        {
            return occupantCount;
        }
        set
        {
            if (value < occupantMinMax.x)
                occupantCount = (int) occupantMinMax.x;
            else if (value > occupantMinMax.y)
                occupantCount = (int) occupantMinMax.y;
            else
                occupantCount = value;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            if (value < levelMinMax.x)
                level = (int) levelMinMax.x;
            else if (value > levelMinMax.y)
                level = (int) levelMinMax.y;
            else
                level = value;
        }
    }

    public int Happiness
    {
        get
        {
            return happiness;
        }
        set
        {
            if (value < happinessMinMax.x)
                happiness = (int) happinessMinMax.x;
            else if (value > happinessMinMax.y)
                happiness = (int) happinessMinMax.y;
            else
                happiness = value;
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
        float timePassed = collectionInterval - lastCollection;

        // Calculate the amount of time that has passed since the last collection
        if (timePassed < 0)
            timePassed = collectionInterval + (timePassed * -1);

        // Calculate the amount of tax generated
        float tax = ((taxPercentage * occupantCount) / (level * happiness)) * 100;

        // Correct the amount of tax for the amount of time that has passed since the last collection
        tax *= (timePassed / collectionInterval);

        return tax;
    }
}