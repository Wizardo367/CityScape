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
        // TODO Optimise this

        // Check destroy and hover state
        Tile currentTile = _map.GetCurrentTile();
        if (!(currentTile.transform.position == _tilePos) || !_map.GetDestroyState()) return;

        // Check for clicks
        if (Input.GetMouseButtonUp(0))
        {
            // Destroy object
            Destroy(gameObject);
            // Set current tile buildable property
            currentTile.Buildable = true;
            // Toggle mode
            _map.ToggleDestroyMode();
        }
    }
}
