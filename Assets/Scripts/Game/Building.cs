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
    public TileType type;

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
        switch (type)
        {
            case TileType.Residential:
                occupantMinMax = new Vector2(0, 100);
                break;
            case TileType.Commercial:
                occupantMinMax = new Vector2(0, 225);
                break;
            case TileType.Office:
                occupantMinMax = new Vector2(0, 500);
                break;
        }
    }

    // Properties
    public int TaxPercentage
    {
        get { return taxPercentage; }
        set { taxPercentage = (int)Mathf.Clamp(value, taxMinMax.x, taxMinMax.y); }
    }

    public int OccupantCount
    {
        get { return occupantCount; }
        set { occupantCount = (int)Mathf.Clamp(value, occupantMinMax.x, occupantMinMax.y); }
    }

    public int Level
    {
        get { return level; }
        set { level = (int)Mathf.Clamp(value, levelMinMax.x, levelMinMax.y); }
    }

    public int Happiness
    {
        get { return happiness; }
        set { happiness = (int)Mathf.Clamp(value, happinessMinMax.x, happinessMinMax.y); }
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