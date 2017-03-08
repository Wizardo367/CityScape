using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSlot : MonoBehaviour
{
    [Tooltip("When true, the script is used to load data from the slot, when false it's used to create a new game.")]
    public bool LoadData;
    [Tooltip("The name of the slot to be loaded from or to create a new world in.")]
    public string SlotName;
    [Tooltip("The name of the world to use, when creating a new save.")]
    public string WorldName;

    private string _path;

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

    public void SetLoadData(bool value)
    {
        // Used for button clicks
        LoadData = value;
    }

    public void SetWorldName(string world)
    {
        // Used for button clicks
        WorldName = world;
    }
    
    private void SetSavePath()
    {
        _path = Application.persistentDataPath + "/Saves/" + SlotName + ".xml";
        Game.Path = _path;
    }

    private void LoadGame()
    {
        // Load Scene
        SceneManager.LoadScene("Game");
    }
}
