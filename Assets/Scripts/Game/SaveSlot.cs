using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Responsible for creating, updating and loading save files.
/// </summary>
public class SaveSlot : MonoBehaviour
{
    [Tooltip("When true, the script is used to load data from the slot, when false it's used to create a new game.")]
    public bool LoadData; /// <summary>Used to determine whether data should be loaded from the slot or a new game save should be created.</summary>
    [Tooltip("The name of the slot to be loaded from or to create a new world in.")]
    public string SlotName; /// <summary>The name of the slot to be loaded from or to create a new world in.</summary>
    [Tooltip("The name of the world to use when creating a new save.")]
    public string WorldName; /// <summary>The name of the world to use when creating a new save.</summary>

    private string _path;

    /// <summary>
    /// Called when an associated object is clicked.
    /// </summary>
    public void OnClick()
    {
        if (LoadData)
        {
            // Load Path
            SetSavePath();
            // Check if the file exists
            if (!File.Exists(_path)) return;

            // Load game
            LoadGame();
        }
        else
        {
            // New Game

            // Load world
            TextAsset world = Resources.Load<TextAsset>("Worlds/" + WorldName);
            // Check if the file exists
            if (world == null) return;

            // Copy file to saves and rename to the given slot
            SetSavePath();
            File.WriteAllText(_path, world.text);

            // Load game
            LoadGame();
        }
    }

    /// <summary>
    /// Sets the data to load.
    /// </summary>
    /// <param name="value">if set to <c>true</c> [value].</param>
    public void SetLoadData(bool value)
    {
        // Used for button clicks
        LoadData = value;
    }

    /// <summary>
    /// Sets the name of the world.
    /// </summary>
    /// <param name="world">The world.</param>
    public void SetWorldName(string world)
    {
        // Used for button clicks
        WorldName = world;
    }

    /// <summary>
    /// Sets the save path.
    /// </summary>
    private void SetSavePath()
    {
		// Check if directory exits
	    string dir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\CityScape\\Saves\\";

	    if (!Directory.Exists(dir))
		    Directory.CreateDirectory(dir);

		// Set path
        _path = dir + SlotName + ".xml";
		Game.Path = _path;
    }

    /// <summary>
    /// Loads the game scene.
    /// </summary>
    private void LoadGame()
    {
        // Load Scene
        SceneManager.LoadScene("Game");
    }
}
