/*
 *       Class: Game
 *      Author: Harish Bhagat
 *        Year: 2017
 */

using System.Collections.Generic;
using UnityEngine;
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

    public static string Path;

    private Map2D _map;
    private bool _paused;

    public float MusicVolume = 1f;
    public float SFXVolume = 1f;

    public GameObject GameOverScreen;

    private AudioSource _musicSource, _sfxSource;
    private CountdownTimer _gameTimer;

    // Initialisation
    private void Awake()
    {
        // Initialise tax timer
        _gameTimer = new CountdownTimer {Seconds = 10f};
        _gameTimer.Begin();

        // Initialise map
        _map = gameObject.GetComponent<Map2D>();

        // Get audio sources
        _musicSource = transform.FindChild("Music").GetComponent<AudioSource>();
        _sfxSource = transform.FindChild("SFX").GetComponent<AudioSource>();

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
        // InvokeRepeating("Save", 30f, 30f);
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
        XMLSerializer.Serialize(gameDataContainer, Path);
    }

    public bool Load()
    {
        // Load the game

        // Load the world
        _map.Load(Path);

        return true;
    }

    public void LoadMainMenu()
    {
        SceneManager.UnloadSceneAsync("Game");
        SceneManager.LoadScene("Main Menu");
    }

    public void GameOver()
    {
        // Delete the save
        System.IO.File.Delete(Path);
        // Return to the main menu
        LoadMainMenu();
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

    private void Update()
    {
        // Check for pause
        if (GameState == GameState.Paused) return;

        // Collect taxes
        if (_gameTimer.IsDone())
        {
            Money += Mathf.RoundToInt(_map.Buildings.Sum(building => building.CollectTax()));

            // Calculate upkeep costs
            float upkeep = _map.Decorations.Select(tile => tile.gameObject.GetComponent<PurchasableTile>()).Where(purchasable => purchasable != null).Sum(purchasable => purchasable.Upkeep);
            upkeep += _map.Roads.Select(tile => tile.gameObject.GetComponent<PurchasableTile>()).Where(purchasable => purchasable != null).Sum(purchasable => purchasable.Upkeep);

            // Subtract upkeep costs
            Money -= Mathf.RoundToInt(upkeep);

            _gameTimer.ResetClock();
            _gameTimer.Begin();
        }
        else
            _gameTimer.Update();

        // Check for gameover
        if (Money < -1000)
        {
            // Pause game
            SetPause(true);
            // Display game over screen
            GameOverScreen.SetActive(true);
        }
    }
}