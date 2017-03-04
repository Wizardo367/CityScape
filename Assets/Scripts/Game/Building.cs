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
    // RTTI Type of building
    public new BuildingData Data;

    // Variables
    public BuildingDirection Direction;

    public Vector2 OccupantMinMax = new Vector2(0, 100);
    public Vector2 LevelMinMax = new Vector2(1, 3);
    public Vector2 HappinessMinMax = new Vector2(0, 100);

    private int _happiness;
    private int _occupants;
    private int _level = 1;

    public HashSet<HappinessBooster> Boosters;

    private CountdownTimer _upgradeTimer;

    // Game instance
    private Game _game;
    private Map2D _map;

    private void Awake()
    {
        // Intialise variables
        _game = GameObject.Find("Game").GetComponent<Game>();
        _map = _game.GetComponent<Map2D>();
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

    public int Level
    {
        get { return _level; }
        set { _level = (int)Mathf.Clamp(value, LevelMinMax.x, LevelMinMax.y); }
    }

    /// <summary>
    /// Calculates the amount of tax generated.
    /// </summary>
    /// <returns></returns>
    public virtual float CollectTax()
    {
        // Calculate the amount of tax generated
        float happy = _happiness == 0 ? 1 : _happiness;
        float occ = _occupants == 0 ? 1 : _occupants;

        float tax = happy * _level / (TaxPercentage / occ); // Check occupantCount and happiness to prevent dividing by 0 error
        return Mathf.Approximately(tax, 1f) ? 0f : tax; // If tax is 1 collect nothing
    }

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

    private void Update()
    {
        // Check timer and stats to see if this building needs upgrading, don't bother checking if the building is already at it's max level
        if (_upgradeTimer.IsDone() && Level != (int)LevelMinMax.y)
        {
            // Check if building is eligible for upgrade
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