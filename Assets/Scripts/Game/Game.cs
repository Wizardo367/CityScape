/*
 *       Class: Game
 *      Author: Harish Bhagat
 *        Year: 2017
 */

using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

/// <summary>
/// Stores and manages an instance of the game.
/// </summary>
public class Game : MonoBehaviour
{
    /// <summary>
    /// The game state.
    /// </summary>
    public GameState GameState;

    // Game properties

	/// <summary>
	/// Residential tax percentage.
	/// </summary>
	public float ResidentialTax = 5f;
	/// <summary>
	/// Commercial tax percentage.
	/// </summary>
	public float CommercialTax = 5f;
	/// <summary>
	/// Office tax percentage.
	/// </summary>
	public float OfficeTax = 5f;
    /// <summary>
    /// The money.
    /// </summary>
    public int Money;

    /// <summary>
    /// Gets the happiness.
    /// </summary>
    /// <value>
    /// The happiness.
    /// </value>
    public int Happiness
    {
        get
        {
            // Calculate happiness
            List<Building> buildings = _map.Buildings;
            return buildings.Sum(building => building.Happiness) / (buildings.Count == 0 ? 1 : buildings.Count); // Check for division by 0
        }
    }

    /// <summary>
    /// Gets the population.
    /// </summary>
    /// <value>
    /// The population.
    /// </value>
    public int Population
    {
        get
        {
            // Get a sum of each building's population
            return _map.Buildings.Sum(building => building.Occupants);
        }
    }

    /// <summary>
    /// The world path.
    /// </summary>
    public static string Path;

    private Map2D _map;
    private bool _paused;

    /// <summary>
    /// The music volume
    /// </summary>
    public float MusicVolume = 1f;
    /// <summary>
    /// The SFX volume
    /// </summary>
    public float SFXVolume = 1f;

    /// <summary>
    /// The game over screen
    /// </summary>
    public GameObject GameOverScreen;

    private AudioSource _musicSource, _sfxSource;
    private CountdownTimer _gameTimer;

    /// <summary>
    /// Initialises variables, called before Start().
    /// </summary>
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

    /// <summary>
    /// Initialises the game.
    /// </summary>
    private void InitGame()
    {
        // Load the game
        Load();

        // Centre the camera
        _map.CentreCameraView(Camera.main);

        // Autosave every 30 seconds
        // InvokeRepeating("Save", 30f, 30f);
    }

    /// <summary>
    /// Saves this instance.
    /// </summary>
    public void Save()
    {
        // Save the game

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

        // Save game state data
        
        // Money
        gameDataContainer.GameStateData.Money = Money;

        // Serialize data
        XMLSerializer.Serialize(gameDataContainer, Path);
    }

    /// <summary>
    /// Loads an instance.
    /// </summary>
    /// <returns></returns>
    public bool Load()
    {
        // Load the game

        // Get game state data

        // Deserialize data
        GameDataContainer tileDataContainer = XMLSerializer.Deserialize<GameDataContainer>(Path);
        Money = tileDataContainer.GameStateData.Money;

        // Load the world
        _map.Load(Path);

        return true;
    }

    /// <summary>
    /// Loads the main menu.
    /// </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }

    /// <summary>
    /// Used to mark the end of a game.
    /// </summary>
    public void GameOver()
    {
        // Delete the save
        System.IO.File.Delete(Path);
        // Return to the main menu
        LoadMainMenu();
    }

    /// <summary>
    /// Sets the pause.
    /// </summary>
    /// <param name="state">if set to <c>true</c> [state].</param>
    public void SetPause(bool state)
    {
        // Set gamestate
        GameState = state ? GameState.Paused : GameState.Active;
        // Set timescale
        Time.timeScale = state ? 0f : 1f;
    }

    /// <summary>
    /// Toggles the pause.
    /// </summary>
    public void TogglePause()
    {
        // Toggle Gamestate
        GameState = GameState == GameState.Active ? GameState.Paused : GameState.Active;
        // Toggle timescale for pausing and playing
        Time.timeScale = !_paused ? 0 : 1;
        _paused = !_paused;
    }

    /// <summary>
    /// Toggles the music.
    /// </summary>
    public void ToggleMusic()
    {
        // Mute or unmute music
        _musicSource.mute = !_musicSource.mute;
    }

    /// <summary>
    /// Toggles the SFX.
    /// </summary>
    public void ToggleSFX()
    {
        // Mute or unmute sfx
        _sfxSource.mute = !_sfxSource.mute;
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    private void Update()
    {
        // Check for pause
        if (GameState == GameState.Paused) return;

        // Collect taxes
        if (_gameTimer != null)
        {
            if (_gameTimer.IsDone())
            {
                // Collect tax
                Money += Mathf.RoundToInt(_map.Buildings.Sum(building => building.CollectTax()));

                // Calculate upkeep costs
                float upkeep =
                    _map.Decorations.Select(tile => tile.gameObject.GetComponent<PurchasableTile>())
                        .Where(purchasable => purchasable != null)
                        .Sum(purchasable => purchasable.Upkeep);
                upkeep +=
                    _map.Roads.Select(tile => tile.gameObject.GetComponent<PurchasableTile>())
                        .Where(purchasable => purchasable != null)
                        .Sum(purchasable => purchasable.Upkeep);

                // Subtract upkeep costs
                Money -= Mathf.RoundToInt(upkeep);

                _gameTimer.ResetClock();
                _gameTimer.Begin();
            }
            else
                _gameTimer.Update();
        }

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