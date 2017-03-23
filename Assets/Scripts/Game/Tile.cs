/*
 *       Class: Tile
 *      Author: Harish Bhagat
 *        Year: 2017
 */

using UnityEngine;

/// <summary>
/// Used to define a tile object.
/// </summary>
public class Tile : MonoBehaviour
{
    /// <summary>
    /// Used to load and save data from XML.
    /// </summary>
    public TileData Data = new TileData();

    /// <summary>
    /// Determines whether or not this tile can be built-upon.
    /// </summary>
    public bool Buildable;
    /// <summary>
    /// The tile type.
    /// </summary>
    public TileType TileType;

    private bool _mouseOnTile;

    private SpriteRenderer _spriteRenderer;
    private Map2D _map;

    /// <summary>
    /// Initialises this instance.
    /// </summary>
    private void Start()
    {
        // Initialise
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _map = GameObject.Find("Game").GetComponent<Map2D>();
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    private void Update()
    {
        // Check for escape key
        if (Input.GetKeyDown(KeyCode.Escape))
            ProcessHighlighting(false);

        // Check for left mouse click
        if (_mouseOnTile && Input.GetMouseButtonDown(0))
        {
            // Set last tile clicked
            _map.LastTileClicked = this;
        }
    }

    /// <summary>
    /// Called when the pointer enters the object.
    /// </summary>
    private void OnMouseEnter()
    {
        ProcessHighlighting(_map.EnableTileHighlighting);
        _mouseOnTile = true;

        // Set current tile
        TileType type = TileType;

        if (type == TileType.Grass || type == TileType.Sand || type == TileType.SandWater || type == TileType.Water)
            _map.CurrentTile = this;
    }

    /// <summary>
    /// Called when the pointer exits the object.
    /// </summary>
    private void OnMouseExit()
    {
        ProcessHighlighting(false);
        _mouseOnTile = false;
    }

    /// <summary>
    /// Processes highlighting.
    /// </summary>
    /// <param name="enable">if set to <c>true</c> [enable].</param>
    private void ProcessHighlighting(bool enable)
    {
        // Set highlight colour
        _spriteRenderer.color = enable ? _map.TileHighlightColour : Color.white;
    }

    /// <summary>
    /// Stores data to a TileData object.
    /// </summary>
    public virtual void StoreData()
    {
        Data.Buildable = Buildable;
        Data.TileType = TileType;

        // Store position
        Vector3 position = transform.position;
        Data.PosX = position.x;
        Data.PosY = position.y;

        // Store rotation
        Data.RotY = transform.eulerAngles.y;
    }

    /// <summary>
    /// Loads data from a TileData object.
    /// </summary>
    public virtual void LoadData()
    {
        // Load data
        Buildable = Data.Buildable;
        TileType = Data.TileType;
        transform.position = new Vector2(Data.PosX, Data.PosY);

        transform.Rotate(0f, Data.RotY, 0f);
    }

    /// <summary>
    /// Gets the highlight colour.
    /// </summary>
    /// <returns>Current highlight colour.</returns>
    public Color GetHighlightColour()
    {
        return _map.TileHighlightColour;
    }
}