/*
 *       Class: Game
 *      Author: Harish Bhagat
 *        Year: 2017
 */

using UnityEngine;
using System.IO;

public class Game : MonoBehaviour
{
    private static Game _instance;
    private GameState _gameState;

    private Map2D _map;

    Camera _mainCamera;

    private bool _paused;
    private string _baseFilePath, _worldName;

    public static Game Instance { get { return _instance; } }

    // Initialisation
    void Awake()
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

        // Grab camera reference
        _mainCamera = Camera.main;

        // File paths
        _baseFilePath = Application.persistentDataPath;
        _worldName = "world_beach";

        // Initialise the game
        InitGame();
    }

    void InitGame()
    {
        // Load the game
        Load();

        // Centre the camera
        _map.centreCameraView(_mainCamera);

        // Autosave every 30 seconds
        //InvokeRepeating("Save", 30f, 30f);
    }

    void RestoreWorldFiles()
    {
        // Copy world files to persistantDataPath
        TextAsset[] worlds = Resources.LoadAll<TextAsset>("Worlds/");

        foreach (TextAsset world in worlds)
            File.WriteAllText(Path.Combine(_baseFilePath, world.name + ".xml"), world.text);
    }

    public void Save()
    {
        // Save the game

        // TODO Only save buildings, seperate files?
        // Save the world map
        TileDataContainer tileDataContainer = new TileDataContainer();

        foreach (Tile tile in _map.Tiles)
            tileDataContainer.tileDataList.Add(tile.data);

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
}