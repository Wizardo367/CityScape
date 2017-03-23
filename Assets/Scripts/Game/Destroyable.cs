/*
 *       Class: Destroyable
 *      Author: Harish Bhagat
 *        Year: 2017
 */

using UnityEngine;

/// <summary>
/// Attached to an object to make is destructable.
/// </summary>
public class Destroyable : MonoBehaviour
{
    /// <summary>
    /// A reference to the game's current map.
    /// </summary>
    private Map2D _map;

    /// <summary>
    /// Used to initialise variables.
    /// </summary>
    private void Start()
    {
        _map = GameObject.Find("Game").GetComponent<Map2D>();
    }

    /// <summary>
    /// Called once per frame; it's used to check if the object is to be destroyed.
    /// </summary>
    private void Update()
    {
        // Check destroy and hover state
        Tile currentTile = _map.CurrentTile;

        if (currentTile == null || !_map.DestroyMode) return;

        // Check for clicks
        if (Input.GetMouseButtonUp(0))
        {
            // Check position
            if ((Vector2)gameObject.transform.position != (Vector2)currentTile.transform.position) return;

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
