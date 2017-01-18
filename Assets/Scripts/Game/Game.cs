/*
 *       Class: Game
 *      Author: Harish Bhagat
 *        Year: 2016
 */

using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class Game : MonoBehaviour
{
    private static Game instance;

    List<GameObject> buildings;
    List<Tile> mapTiles = new List<Tile>();
    Vector3 mapCentre;

    Camera mainCamera;

    string baseFilePath, worldName;

    public static Game Instance { get { return instance; } }

    // Initialisation
    void Awake()
    {
        // Check for existing instance
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // Don't destroy instance when loading a new scene
        DontDestroyOnLoad(gameObject);

        // Grab camera reference
        mainCamera = Camera.main;

        // File paths
        baseFilePath = Application.persistentDataPath;
        worldName = "world_beach";

        // Initialise the game
        InitGame();
    }

    void InitGame()
    {
        // Load the game
        Load();

        // Centre the camera
        mapCentre = mapTiles[(mapTiles.Count - 1) / 2].transform.position;
        mapCentre.z = -10f;
        mainCamera.transform.position = mapCentre;

        // Autosave every 30 seconds
        //InvokeRepeating("Save", 30f, 30f);
    }

    void RestoreWorldFiles()
    {
        // Copy world files to persistantDataPath
        TextAsset[] worlds = Resources.LoadAll<TextAsset>("Worlds/");

        foreach (TextAsset world in worlds)
            File.WriteAllText(Path.Combine(baseFilePath, world.name + ".xml"), world.text);
    }

    public void Save()
    {
        // Save the game

        // TODO Only save buildings, seperate files?
        // Save the world map
        TileDataContainer tileDataContainer = new TileDataContainer();

        foreach (Tile tile in mapTiles)
            tileDataContainer.tileDataList.Add(tile.data);

        // Serialize data
        XMLSerializer.Serialize(tileDataContainer, Path.Combine(baseFilePath, worldName + ".xml"));
    }

    public bool Load()
    {
        // Load the game

        // Load the world
        LoadWorld(worldName);

        return true;
    }

    public void LoadWorld(string worldName)
    {
        // World map
        mapTiles = Map2D.Load(Path.Combine(baseFilePath, worldName + ".xml"));

        // Check if mapTiles is null, if so try again
        if (mapTiles == null)
        {
            RestoreWorldFiles();
            mapTiles = Map2D.Load(Path.Combine(baseFilePath, worldName + ".xml"));
        }
    }

    public void SpawnBuidling()
    {
        // Pick random tile position
        int randTileIndex = Random.Range(0, mapTiles.Count);
        Vector3 tilePosition = mapTiles[randTileIndex].transform.position;

        // Pick random building
        string randBuildingType = ((BuildingType)Random.Range(0, 3)).ToString();
        string randBuilding = randBuildingType + "_1_1";

        // Place building
        GameObject go = Resources.Load<GameObject>("Prefabs/" + randBuilding);
        Instantiate(go, tilePosition, Quaternion.identity, GameObject.Find("Buildings").transform);
    }
}