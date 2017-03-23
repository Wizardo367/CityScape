using UnityEngine;

/// <summary>
/// Allows for the placement of objects.
/// </summary>
public class PlaceableObjectSpawner : MonoBehaviour
{
    [Range(0.0f, 1.0f), Tooltip("The alpha value of the object before it's placed.")]
    public float HoverAlpha = 0.5f; /// <summary> The alpha value of the object before it's placed. </summary>
    /// <summary>
    /// The object to be placed.
    /// </summary>
    public GameObject Target;

    private GameObject _go;
    private SpriteRenderer[] _spriteRenderers;
    private Quaternion _currentRotation;
    private Color _defaultColour;
    private Map2D _map;
    private Game _game;

    /// <summary>
    /// Initialises this instance.
    /// </summary>
    public void Init()
    {
        // Reset map destroy state
        if (_map.DestroyMode)
            _map.ToggleDestroyMode();

        // Initialise object
        _go = Instantiate(Target, _map.GetTileParent(Target).transform);
        _defaultColour = Color.white;
        _currentRotation = Quaternion.identity;

        // Set alpha
        _spriteRenderers = _go.GetComponentsInChildren<SpriteRenderer>();
        ToggleAlpha();
    }

    /// <summary>
    /// Initialises variables.
    /// </summary>
    private void Start()
    {
        _game = GameObject.Find("Game").GetComponent<Game>();
        _map = _game.gameObject.GetComponent<Map2D>();
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    private void Update()
    {
        // Check if the object is null
        if (_go == null) return;

        // Follow mouse/current tile till placed
        Tile currentTile = _map.CurrentTile;
        if (currentTile != null)
        {
            Vector3 tilePos = currentTile.transform.position;
            tilePos.z = -1f; // Fixes rendering error
            _go.transform.position = tilePos;
        }

        // Check for keyboard input
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Rotate
            Rotatable2D rotatable2D = _go.GetComponent<Rotatable2D>();

            if (rotatable2D != null)
            {
                rotatable2D.Rotate();
                _currentRotation = rotatable2D.gameObject.transform.rotation;
                _go.transform.rotation = _currentRotation;
            }

            // Check for roads
            Road road = _go.GetComponent<Road>();

            if (road != null)
                road.RotateY();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Cancel placement
            Destroy(_go);
        }

        // Check for clicks
        if (Input.GetMouseButtonDown(0))
        {
            // Object placed

            // Check if the tile is buildable
            if (currentTile != null && currentTile.Buildable)
            {
                // Process tile types
                ProcessNode(currentTile);
                ProcessMarker();
                ProcessTile(_go);

                // Check for purchase price
                PurchasableTile purchasable = _go.GetComponent<PurchasableTile>();
                if (purchasable != null)
                    _game.Money -= (int) purchasable.Price;

                // Set alpha
                ToggleAlpha();

                // Disable builadable property of the tile
                currentTile.Buildable = false;

                // Set go to null
                _go = null;
            }
        }
    }

    /// <summary>
    /// Toggles the alpha.
    /// </summary>
    private void ToggleAlpha()
    {
        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
        {
            Color currentColor = spriteRenderer.color;
            Color newColour = _defaultColour;
            newColour.a = Mathf.Approximately(currentColor.a, HoverAlpha) ? 1f : HoverAlpha;
            spriteRenderer.color = newColour;
        }
    }

    /// <summary>
    /// Processes the marker.
    /// </summary>
    private void ProcessMarker()
    {
        // Activate marker script if found
        MonoBehaviour markerScript = _go.GetComponent<Marker>();
        if (markerScript != null)
        {
            // Enable script
            markerScript.enabled = true;
            // Rotation
            ((Marker)markerScript).SetFinalRotation(_currentRotation);
        }
        else
        {
            // Rotation
            _go.transform.rotation = _currentRotation;
        }
    }

    /// <summary>
    /// Processes the node.
    /// </summary>
    /// <param name="currentTile">The current tile.</param>
    private void ProcessNode(Tile currentTile)
    {
        // Check if placed object is a road
        Node node = currentTile.gameObject.GetComponent<Node>();
        Road road = _go.GetComponent<Road>();
        if (node == null || road == null) return;

        // Find the map tile Node underneath the current Node and set it's traversability
        node.TraversableUp = road.TraversableUp;
        node.TraversableDown = road.TraversableDown;
        node.TraversableLeft = road.TraversableLeft;
        node.TraversableRight = road.TraversableRight;

        // Update map
        GameObject.Find("Game").GetComponent<RoadPathFinder>().UpdateMap();
    }

    /// <summary>
    /// Processes the tile.
    /// </summary>
    /// <param name="gameObj">The game object.</param>
    private void ProcessTile(GameObject gameObj)
    {
        // Buildings are handled seperately in the SpawnBuilding() method in the Map2D class. (Because of marker spawning)

        Tile tile = gameObj.GetComponent<Tile>();
        if (tile == null) return;

        // Adds the tile to the correct list in game
        switch (tile.TileType)
        {
            case TileType.CrossRoad:
            case TileType.StraightRoad:
            case TileType.StraightTurnRoadX:
            case TileType.StraightTurnRoadY:
                _map.Roads.Add(tile);
                break;
            case TileType.Tree:
            case TileType.Pavement:
                _map.Decorations.Add(tile);
                break;
        }
    }
}