using UnityEngine;
using System.Collections.Generic;

public class IsoObject : MonoBehaviour
{
    // Properties
    public int tileSize;
    public Vector2 objectDimensions;
    public List<GameObject> tiles;

    void Start()
    {
        // Sort coordinates

        // Convert cartesian coordinates to isometric
        foreach (GameObject go in tiles)
        {
            Vector3 position = go.transform.position;
            go.transform.position = Isometric.CartToIso(position);
        }
    }

    public static void Position(Vector3 currentPosition)
    {
        // Position object using the center of the bottom-left tile
    }
}
