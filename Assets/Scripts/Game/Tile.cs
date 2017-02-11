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
    public bool Destructable;
    public TileType TileType; // RTTI

    private bool _clicked;
    private bool _mouseOnTile;

    private bool _enableHighlighting;
    private Color _highlightColour;
    private Color _normalColour;

    private SpriteRenderer _spriteRenderer;
    private Map2D _map;

    private void Start()
    {
        // Initialise
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _normalColour = _spriteRenderer.color;
        _map = GameObject.Find("Game").GetComponent<Map2D>();
    }

    private void Update()
    {
        // Check for left mouse click
        if (_mouseOnTile && Input.GetMouseButtonDown(0))
            _clicked = true;
    }

    private void OnMouseEnter()
    {
        if (_enableHighlighting)
            _spriteRenderer.color = _highlightColour;
        _mouseOnTile = true;

        // Set current tile
        _map.SetCurrentTile(this);
    }

    private void OnMouseExit()
    {
        _spriteRenderer.color = _normalColour;
        _mouseOnTile = false;
    }

    /// <summary>
    /// Stores data to a TileData object.
    /// </summary>
    public virtual void StoreData()
    {
        Data.buildable = Buildable;
        Data.destructable = Destructable;
        Data.tileType = TileType;

        // Store position
        Vector3 position = gameObject.transform.position;
        Data.posX = position.x;
        Data.posY = position.y;
        Data.posZ = position.z;
    }

    /// <summary>
    /// Loads data from a TileData object.
    /// </summary>
    public virtual void LoadData()
    {
        // Load data
        Buildable = Data.buildable;
        Destructable = Data.destructable;
        TileType = Data.tileType;
        transform.position = new Vector3(Data.posX, Data.posY, Data.posZ);
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

    public void SetHighlighting(bool enable, Color highlightColour)
    {
        _enableHighlighting = enable;
        _highlightColour = highlightColour;
        _highlightColour.a = 1f;
    }

    public Color GetHighlightColour()
    {
        return _highlightColour;
    }
}