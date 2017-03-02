/*
 *       Class: Game
 *      Author: Harish Bhagat
 *        Year: 2017
 */

using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameState GameState;

    // Game properties
    public float ResidentialTax = 5f, CommercialTax = 5f, OfficeTax = 5f;
    public int Money;

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
            return _map.Buildings.Sum(building => building.Occupants);
        }
    }

    private Map2D _map;

    private bool _paused;
    private string _baseFilePath, _worldName;

    public float MusicVolume = 1f;
    public float SFXVolume = 1f;

    private AudioSource _musicSource, _sfxSource;

    private CountdownTimer _taxTimer;

    // Initialisation
    private void Awake()
    {
        // Initialise tax timer
        _taxTimer = new CountdownTimer {Seconds = 10f};
        _taxTimer.Begin();

        // Initialise map
        _map = gameObject.GetComponent<Map2D>();

        // Get audio sources
        _musicSource = transform.FindChild("Music").GetComponent<AudioSource>();
        _sfxSource = transform.FindChild("SFX").GetComponent<AudioSource>();

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
        InvokeRepeating("Save", 30f, 30f);
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
        Debug.Log("SAVE");

        // Save the world map
        GameDataContainer gameDataContainer = new GameDataContainer();

        // Ground tiles
        foreach (Tile tile in _map.GroundTiles)
        {
            tile.StoreData();
            gameDataContainer.GroundDataList.Add(tile.Data);
        }

        // Buildings
        foreach (Building building in _map.Buildings)
        {
            building.StoreData();
            gameDataContainer.BuildingDataList.Add(building.Data);
        }

        // Roads
        foreach (Tile tile in _map.Roads)
        {
            tile.StoreData();
            gameDataContainer.RoadDataList.Add(tile.Data);
        }

        // Decorations
        foreach (Tile tile in _map.Decorations)
        {
            tile.StoreData();
            gameDataContainer.DecorationDataList.Add(tile.Data);
        }

        // Serialize data
        XMLSerializer.Serialize(gameDataContainer, Path.Combine(_baseFilePath, "Save/" + _worldName + ".xml"));
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
        //RestoreWorldFiles();
        // World map
        _map.Load(Path.Combine(_baseFilePath, worldName + ".xml"));
    }

    public void SetPause(bool state)
    {
        // Set gamestate
        GameState = state ? GameState.Paused : GameState.Active;
        // Set timescale
        Time.timeScale = state ? 0f : 1f;
    }

    public void TogglePause()
    {
        // Toggle Gamestate
        GameState = GameState == GameState.Active ? GameState.Paused : GameState.Active;
        // Toggle timescale for pausing and playing
        Time.timeScale = !_paused ? 0 : 1;
        _paused = !_paused;
    }

    public void ToggleMusic()
    {
        // Mute or unmute music
        _musicSource.mute = !_musicSource.mute;
    }

    public void ToggleSFX()
    {
        // Mute or unmute sfx
        _sfxSource.mute = !_sfxSource.mute;
    }

    public void LoadMainMenu()
    {
        SceneManager.UnloadSceneAsync("Game");
        SceneManager.LoadScene("Main Menu");
    }

    private void Update()
    {
        // Check for pause
        if (GameState == GameState.Paused) return;

        // Collect taxes
        if (_taxTimer.IsDone())
        {
            Money += Mathf.RoundToInt(_map.Buildings.Sum(building => building.CollectTax()));

            _taxTimer.ResetClock();
            _taxTimer.Begin();
        }
        else
            _taxTimer.Update();
    }
}