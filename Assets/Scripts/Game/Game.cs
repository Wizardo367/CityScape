/*
 *       Class: Game
 *      Author: Harish Bhagat
 *        Year: 2017
 */

using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Game : MonoBehaviour
{
    private static Game _instance;
    private GameState _gameState;

    // Game properties
    public float ResidentialTax = 5f, CommercialTax = 5f, OfficeTax = 5f;
    public int Money = 1000;

    public int Happiness
    {
        get
        {
            // Calculate happiness
            List<Building> buildings = _map.Buildings;
            return buildings.Sum(building => building.Happiness) / (buildings.Count == 0 ? 1 : buildings.Count); // Check for division by 0
        }
    }

    public int Population
    {
        get
        {
            // Get a sum of each building's population
            return _map.Buildings.Sum(building => building.OccupantCount);
        }
    }

    private Map2D _map;

    private bool _paused;
    private string _baseFilePath, _worldName;

    public float MusicVolume = 1f;
    public float SFXVolume = 1f;

    public static Game Instance { get { return _instance; } }

    // Initialisation
    private void Awake()
    {
        // Check for existing instance
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);

        // Initialise map
        _map = gameObject.GetComponent<Map2D>();

        // Set gameState
        _gameState = GameState.Active;

        // Don't destroy instance when loading a new scene
        DontDestroyOnLoad(gameObject);

        // File paths
        _baseFilePath = Application.persistentDataPath;
        _worldName = "world_beach";

        // Initialise the game
        InitGame();
    }

    private void InitGame()
    {
        // Load the game
        Load();

        // Centre the camera
        _map.CentreCameraView(Camera.main);

        // Autosave every 30 seconds
        //InvokeRepeating("Save", 30f, 30f);
    }

    private void RestoreWorldFiles()
    {
        // Copy world files to persistantDataPath
        TextAsset[] worlds = Resources.LoadAll<TextAsset>("Worlds/");

        foreach (TextAsset world in worlds)
            File.WriteAllText(Path.Combine(_baseFilePath, world.name + ".xml"), world.text);
    }

    public void Save()
    {
        // Save the game

        // Save the world map
        TileDataContainer tileDataContainer = new TileDataContainer();

        foreach (Tile tile in _map.Tiles)
            tileDataContainer.tileDataList.Add(tile.Data);

        // Serialize data
        XMLSerializer.Serialize(tileDataContainer, Path.Combine(_baseFilePath, _worldName + ".xml"));
    }

    public bool Load()
    {
        // Load the game

        // Load the world
        LoadWorld(_worldName);

        return true;
    }

    public void LoadWorld(string worldName)
    {
        // Restore world files in case of update
        RestoreWorldFiles();
        // World map
        _map.Load(Path.Combine(_baseFilePath, worldName + ".xml"));
    }

    public void TogglePause()
    {
        // Toggle timescale for pausing and playing
        Time.timeScale = !_paused ? 0 : 1;
        _paused = !_paused;
    }

    public void MuteMusic()
    {
        // Set using music volume for simplicity
        MusicVolume = 0f;
    }

    public void MuteSFX()
    {
        // Set using sfx volume for simplicity
        SFXVolume = 0f;
    }

    private void Update()
    {

    }
}