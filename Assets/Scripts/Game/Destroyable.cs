using UnityEngine;

public class Destroyable : MonoBehaviour
{
    private Map2D _map;

    // Use this for initialization
    private void Start()
    {
        _map = GameObject.Find("Game").GetComponent<Map2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Check destroy and hover state
        Tile currentTile = _map.CurrentTile;

        if (currentTile == null || !_map.DestroyMode) return;

        // Check for clicks
        if (Input.GetMouseButtonUp(0))
        {
            // Check position
            Debug.Log(currentTile.transform.position);
            if (gameObject.transform.position != currentTile.transform.position) return;

                // Remove object from lists
                PurchasableTile tile = gameObject.GetComponent<PurchasableTile>();

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
