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
    public TileData Data = new TileData();

    public bool Buildable;
    public TileType TileType; // RTTI

    private bool _clicked;
    private bool _mouseOnTile;

    private SpriteRenderer _spriteRenderer;
    private Map2D _map;

    private void Start()
    {
        // Initialise
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _map = GameObject.Find("Game").GetComponent<Map2D>();
    }

    private void Update()
    {
        // Check for escape key
        if (Input.GetKeyDown(KeyCode.Escape))
            ProcessHighlighting(false);

        // Check for left mouse click
        if (_mouseOnTile && Input.GetMouseButtonDown(0))
            _clicked = true;
    }

    private void OnMouseEnter()
    {
        ProcessHighlighting(_map.EnableTileHighlighting);
        _mouseOnTile = true;

        // Set current tile
        TileType type = TileType;

        if (type == TileType.Grass || type == TileType.Sand || type == TileType.SandWater || type == TileType.Water)
            _map.CurrentTile = this;
    }

    private void OnMouseExit()
    {
        ProcessHighlighting(false);
        _mouseOnTile = false;
    }

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
    /// Returns true if the tile was clicked, the value is reset after the check.
    /// </summary>
    /// <returns>Whether or not the tile was clicked.</returns>
    public bool WasClicked()
    {
        // Store value
        bool clicked = _clicked;
        // Reset click
        _clicked = false;
        // Return original value
        return clicked;
    }

    public Color GetHighlightColour()
    {
        return _map.TileHighlightColour;
    }
}