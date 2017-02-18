/*
 *       Class: Map2D
 *      Author: Harish Bhagat
 *        Year: 2017
 */

// Website used: https://gamedevelopment.tutsplus.com/tutorials/creating-isometric-worlds-a-primer-for-game-developers--gamedev-6511

using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// A class to be used for generating and loading 2D cartesian and isometric maps.
/// </summary>
public class Map2D : MonoBehaviour
{
    public bool Isometric;
    public int TileSizeX;
    public int XSize, YSize;
    public GameObject Prefab;
    public Tile[,] Tiles;
    public List<Building> Buildings;

    public bool EnableTileHighlighting;
    public Color TileHighlightColour;
    private Color _normalTileColour;

    private bool _destroyMode;
    private Tile _currentTile;
    private Tile _lastTileClicked;
    private TileType _tileToBePlaced;

    // Pathfinding variables
    private int[,] _layout;
    private Grid _grid;
    private Pathfinder _pathfinder;

    private void Awake()
    {
        // Variable initialisation
        Tiles = new Tile[XSize, YSize];
        Buildings = new List<Building>();
        _normalTileColour = new Color(1f, 1f, 1f, 1f);

        _layout = new int[XSize, YSize];
        // Identify non-walkable blocks within layout

    }

    /// <summary>
    /// Changes the position of the camera to the centre of the map.
    /// </summary>
    /// <param name="currentCamera">The camera to be positioned.</param>
    public void CentreCameraView(Camera currentCamera)
    {
        Vector3 mapCentre = Tiles[Tiles.GetLength(0) / 2, Tiles.GetLength(1) / 2].transform.position;
        mapCentre.z = -10f;
        currentCamera.transform.position = mapCentre;
    }

    /// <summary>
    /// Generates and returns a list of tiles.
    /// </summary>
    public void Generate()
    {
        // Get pixels per unit
        float ppu = Prefab.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        float tileSizeInUnits = TileSizeX / ppu;

        // Create and place each tile
        for (int y = 0; y < YSize; y++)
            for (int x = 0; x < XSize; x++)
            {
                // Calculate position
                Vector3 position;
                float halfUnitTileSize = tileSizeInUnits * 0.5f;

                if (Isometric)
                    position = transform.position +
                               (Vector3)
                               global::Isometric.CartToIso(transform.right * (x * halfUnitTileSize) +
                                                           transform.up * (y * halfUnitTileSize));
                else
                    position = transform.position + transform.right * (x * tileSizeInUnits) +
                               transform.up * (y * tileSizeInUnits);

                // Create tile
                Tile tile = CreateTile(TileType.Grass, position);
                // Make GameObject a child object
                tile.gameObject.transform.parent = GameObject.Find("Tiles/Ground").transform;
                // Add tile to list
                Tiles[x, y] = tile;
            }
    }

    /// <summary>
    /// Loads an existing map.
    /// </summary>
    /// <param name="path">The XML file path which contains the world data.</param>
    /// <returns></returns>
    public void Load(string path)
    {
        // Clear existing tiles
        Tiles = new Tile[XSize, YSize];

        // Check if the file exists
        if (!File.Exists(path))
            return;

        // Deserialize data
        TileDataContainer tileDataContainer = XMLSerializer.Deserialize<TileDataContainer>(path);

        // Clear parent GameObject
        GameObject parentObj = GameObject.Find("Tiles/Ground");
        foreach (Transform child in parentObj.transform)
            Destroy(child.gameObject);

        // Create tile
        List<TileData> tileDataList = tileDataContainer.tileDataList;
        int arrayCounter = 0;

        for (int y = 0; y < YSize; y++)
            for (int x = 0; x < XSize; x++)
            {
                // Create tile
                TileData tileData = tileDataList[arrayCounter];
                Tile tile = CreateTile(tileData);
                // Set properties
                tile.Buildable = tileData.buildable;
                tile.Destructable = tileData.destructable;
                // Make tile a child object
            tile.gameObject.transform.parent = parentObj.transform;
                // Add tile to array
                Tiles[x, y] = tile;
                // Update array counter
                arrayCounter++;
            }
    }

    /// <summary>
    /// Creates and returns a Tile.
    /// </summary>
    /// <param name="tileType">The type of tile to be created.</param>
    /// <param name="position">The position of the newly created tile.</param>
    /// <returns></returns>
    private static Tile CreateTile(TileType tileType, Vector3 position)
    {
        // Get prefab of tileType
        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + tileType);

        // Instantiate
        GameObject go = Instantiate(prefab, position, Quaternion.identity);
        // Get tile script
        Tile tile = go.GetComponent<Tile>();

        // Store tile data
        tile.StoreData();

        // Return tile
        return tile;
    }

    /// <summary>
    /// Creates and return a Tile.
    /// </summary>
    /// <param name="data">A reference to an existing TileData object, that is to be used for the created Tile object.</param>
    /// <returns></returns>
    private static Tile CreateTile(TileData data)
    {
        // Create tile
        return CreateTile(data.tileType, new Vector3(data.posX, data.posY, data.posZ));
    }

    private void Update()
    {
        // Check for keyboard input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Disable destroy action
            if (_destroyMode)
                ToggleDestroyMode();
        }

        foreach (Tile tile in Tiles)
        {
            // Set highlight colour
            if (EnableTileHighlighting && tile.GetHighlightColour() != TileHighlightColour)
                tile.SetHighlighting(true, TileHighlightColour);
            else
                // Remove highlight
                tile.SetHighlighting(false, _normalTileColour);

            // Check for clicks
            if (tile.WasClicked())
                _lastTileClicked = tile;
        }
    }

    public void SpawnRandomBuilding()
    {
        // Pick a random tile
        Tile randTile;

        while (true)
        {
            // Get a random tile
            randTile = Tiles[Random.Range(0, Tiles.GetLength(0)), Random.Range(0, Tiles.GetLength(1))];

            // Check if the tile is buildable
            Vector3 randTilePosition = randTile.transform.position;

            // Check if the tile is buildable, occupied and whether the map is full
            if (!randTile.Buildable ||
                Buildings.Count != Tiles.Length &&
                Buildings.Any(building => building.transform.position == randTilePosition)) continue;

            break;
        }

        // Get tile position
        Vector3 tilePosition = randTile.transform.position;

        // Pick random building
        TileType randTileType = (TileType)Random.Range(0, 3);
        const int variation = 1;
        const int level = 1;

        // Spawn building
        SpawnBuilding(randTileType, variation, level, tilePosition, Quaternion.identity);
    }

    public void SpawnBuilding(TileType type, int variation, int level, Vector3 position, Quaternion rotation)
    {
        // Place building
        GameObject go = Resources.Load<GameObject>("Prefabs/" + type.ToString() + "_" + variation + "_" + level);
        GameObject newBuilding = Instantiate(go, position, rotation, GetTileParent(go).transform);

        // Add to list
        Buildings.Add(newBuilding.GetComponent<Building>());
    }

    public void SpawnTile(TileType type, Vector3 position, Quaternion rotation)
    {
        GameObject go = Resources.Load<GameObject>("Prefabs/" + type.ToString());
        GameObject tile = Instantiate(go, position, rotation, GetTileParent(go).transform);
    }

    public Tile GetLastTileClicked()
    {
        // Return tile
        Tile lastTile = _lastTileClicked;
        _lastTileClicked = null;
        return lastTile;
    }

    public TileType GetTileToBePlaced()
    {
        // Return tile type and reset variable
        TileType tileType = _tileToBePlaced;
        _tileToBePlaced = TileType.None;
        return tileType;
    }

    public void SetHighlightColour(float r, float g, float b, float a)
    {
        TileHighlightColour = new Color(r, g, b, a);
    }

    public void SetHighlightColour(string colour)
    {
        // Prevents case sensitivity
        colour = colour.ToLower();

        switch (colour)
        {
            case "cyan":
                TileHighlightColour = Color.cyan;
                break;
            case "green":
                TileHighlightColour = Color.green;
                break;
            case "orange":
                TileHighlightColour = new Color(1f, 0.5f, 0f, 1f);
                break;
            case "red":
                TileHighlightColour = Color.red;
                break;
            default:
                TileHighlightColour = Color.white;
                break;
        }
    }

    public void SetTileTemplate(string tileType)
    {
        _tileToBePlaced = (TileType)System.Enum.Parse(typeof(TileType), tileType);
    }

    public void SetCurrentTile(Tile tile)
    {
        _currentTile = tile;
    }

    public Tile GetCurrentTile()
    {
        return _currentTile;
    }

    public void ToggleDestroyMode()
    {
        _destroyMode = !_destroyMode;
        // Set highlight colour
        EnableTileHighlighting = _destroyMode;
        SetHighlightColour(_destroyMode ? "red" : "");
    }

    public bool GetDestroyState()
    {
        return _destroyMode;
    }

    public GameObject GetTileParent(GameObject go)
    {
        // Get the tiles parent and return it, this is done so that gameobjects are organised
        Tile tile = go.GetComponent<Tile>();
        TileType type = tile.TileType;

        switch (type)
        {
            case TileType.None:
                break;
            case TileType.Commercial:
            case TileType.CommercialMarker:
            case TileType.Office:
            case TileType.OfficeMarker:
            case TileType.Residential:
            case TileType.ResidentialMarker:
                return GameObject.Find("Game/Tiles/Buildings");
            case TileType.CrossRoad:
            case TileType.StraightRoad:
            case TileType.StraightTurnRoadX:
            case TileType.StraightTurnRoadY:
                return GameObject.Find("Game/Tiles/Roads");
            case TileType.Grass:
            case TileType.Sand:
            case TileType.SandWater:
            case TileType.Water:
                return GameObject.Find("Game/Tiles/Ground");
        }

        return GameObject.Find("Game/Tiles");
    }


}