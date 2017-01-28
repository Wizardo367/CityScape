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
    public TileData data = new TileData();

    public bool buildable;
    public bool destructable;
    public TileType tileType;

    /// <summary>
    /// Stores data to a TileData object.
    /// </summary>
    public virtual void StoreData()
    {
        data.buildable = buildable;
        data.destructable = destructable;
        data.tileType = tileType;

        // Store position
        Vector3 position = gameObject.transform.position;
        data.posX = position.x;
        data.posY = position.y;
        data.posZ = position.z;
    }

    /// <summary>
    /// Loads data from a TileData object.
    /// </summary>
    public virtual void LoadData()
    {
        // Load data
        buildable = data.buildable;
        destructable = data.destructable;
        tileType = data.tileType;
        transform.position = new Vector3(data.posX, data.posY, data.posZ);
    }
}