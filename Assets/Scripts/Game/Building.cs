/*
 *       Class: Building
 *      Author: Harish Bhagat
 *        Year: 2017
 */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Used to define a building class.
/// </summary>
public class Building : Tile
{
    /// <summary>
    /// Used to store and load data from XML.
    /// </summary>
    public new BuildingData Data;

    // Variables

    /// <summary>
    /// The direction.
    /// </summary>
    public BuildingDirection Direction;

    /// <summary>
    /// The minimum and maximum number of occupants the building can have.
    /// </summary>
    public Vector2 OccupantMinMax = new Vector2(0, 100);
    /// <summary>
    /// The minimum and maximum level the building can be.
    /// </summary>
    public Vector2 LevelMinMax = new Vector2(1, 3);
    /// <summary>
    /// The minimum and maximum level of happiness the building can be at.
    /// </summary>
    public Vector2 HappinessMinMax = new Vector2(0, 100);

    private int _happiness;
    private int _occupants;
    private int _level = 1;

    /// <summary>
    /// Stores all HappinessBoosters which affect this building.
    /// </summary>
    public HashSet<HappinessBooster> Boosters;

    /// <summary>
    /// Used to define how often a building should be checked for upgrades.
    /// </summary>
    private CountdownTimer _upgradeTimer;

    // Game instance

    /// <summary>
    /// Store's a reference to the instance of the game's manager.
    /// </summary>
    private Game _game;
    /// <summary>
    /// Store's a reference to the instance of the game's map.
    /// </summary>
    private Map2D _map;

    /// <summary>
    /// Used for initialisation, called before the Start() function.
    /// </summary>
    private void Awake()
    {
        // Intialise variables
        _game = GameObject.Find("Game").GetComponent<Game>();
        _map = _game.gameObject.GetComponent<Map2D>();
        Boosters = new HashSet<HappinessBooster>();

        _upgradeTimer = new CountdownTimer {Seconds = 5f};
        _upgradeTimer.Begin();

        Data = new BuildingData();

        switch (TileType)
        {
            case TileType.Residential:
                OccupantMinMax = new Vector2(0, 100);
                break;
            case TileType.Commercial:
                OccupantMinMax = new Vector2(0, 225);
                break;
            case TileType.Office:
                OccupantMinMax = new Vector2(0, 500);
                break;
        }
    }

    // Properties

    /// <summary>
    /// The building's tax percentage, calculated using the building's type.
    /// </summary>
    public float TaxPercentage
    {
        get
        {
            switch (TileType)
            {
                case TileType.Residential:
                    return _game.ResidentialTax;
                case TileType.Commercial:
                    return _game.CommercialTax;
                case TileType.Office:
                    return _game.OfficeTax;
                default:
                    return 1;
            }
        }
    }

    /// <summary>
    /// The building's level of happiness.
    /// </summary>
    public int Happiness
    {
        get
        {
            // Get clamped boosters sum
            _happiness = Mathf.Clamp(Boosters.Sum(booster => booster.Boost), 0, 100);
            // Return happiness
            return _happiness;
        }
    }

    /// <summary>
    /// The number of occupants living in the building.
    /// </summary>
    public int Occupants
    {
        get
        {
            // Calculate number of occupants
            _occupants =  Mathf.RoundToInt(Happiness / 100f * (OccupantMinMax.y * (Level / LevelMinMax.y)));
            // Return occupants
            return _occupants;
        }
    }

    /// <summary>
    /// The level of the building.
    /// </summary>
    public int Level
    {
        get { return _level; }
        set { _level = (int)Mathf.Clamp(value, LevelMinMax.x, LevelMinMax.y); }
    }

    /// <summary>
    /// Calculates the amount of tax generated.
    /// </summary>
    /// <returns>The amount of tax generated.</returns>
    public virtual float CollectTax()
    {
        // Calculate the amount of tax generated
        float happy = _happiness == 0 ? 1 : _happiness;
        float occ = _occupants == 0 ? 1 : _occupants;

        float tax = happy * (TaxPercentage / 100) * (occ * (Level / LevelMinMax.y)); // Check occupantCount and happiness to prevent dividing by 0 error
        return tax < 0f ? 0f : tax;
    }

    /// <summary>
    /// Stores data to a tileData object, used for saving to XML.
    /// </summary>
    public override void StoreData()
    {
        base.StoreData();

        // Store data
        TileData tileData = base.Data;

        Data.TileType = tileData.TileType;
        Data.PosX = tileData.PosX;
        Data.PosY = tileData.PosY;

        // Store rotation
        Data.RotY = transform.eulerAngles.y;

        // Store level
        Data.Level = Level;
    }

    /// <summary>
    /// Loads data from a tileData object.
    /// </summary>
    public override void LoadData()
    {
        base.LoadData();

        // Load data
        TileData tileData = base.Data;
        TileType = tileData.TileType;

        transform.Rotate(Vector3.up, Data.RotY);

        // Load level
        Level = Data.Level;
    }

    /// <summary>
    /// Called once per frame, used to process building upgrades.
    /// </summary>
    private void Update()
    {
        // Check timer and stats to see if this building needs upgrading, don't bother checking if the building is already at it's max level
        if (_upgradeTimer.IsDone() && Level != (int)LevelMinMax.y)
        {
            // Check if building is eligible for an upgrade
            if (Level == 1 && Happiness >= 50 || Level == 2 && Happiness >= 80)
            {
                // Upgrade
                _map.SpawnBuilding(TileType, Level + 1, transform.position, transform.rotation);
                // Destroy current object
                _map.Buildings.Remove(this);
                Destroy(gameObject);
            }

            // Reset timer
            _upgradeTimer.ResetClock();
            _upgradeTimer.Begin();
        }
        else
            _upgradeTimer.Update();
    }
}