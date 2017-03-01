using UnityEngine;

public class Destroyable : MonoBehaviour
{
    private Map2D _map;
    private Tile _tile;
    private Vector3 _tilePos;

    // Use this for initialization
    private void Start()
    {
        _map = GameObject.Find("Game").GetComponent<Map2D>();
        _tile = gameObject.GetComponent<Tile>();
        _tilePos = _tile.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        // Update tile position (Prevents Error) // TODO Fix this
        _tilePos = _tile.transform.position;

        // Check destroy and hover state
        Tile currentTile = _map.CurrentTile;
        if (currentTile == null || !(currentTile.transform.position == _tilePos) || !_map.DestroyMode) return;

        // Check for clicks
        if (Input.GetMouseButtonUp(0))
        {
            // Remove object from lists
            Tile tile = gameObject.GetComponent<Tile>();

            _map.Buildings.Remove(gameObject.GetComponent<Building>());
            _map.Decorations.Remove(tile);
            _map.Roads.Remove(tile);

            // Destroy object
            Destroy(gameObject);
            // Set current tile buildable property
            currentTile.Buildable = true;
            // Toggle mode
            _map.ToggleDestroyMode();
        }
    }
}
