/*
 *       Class: Map2D
 *      Author: Harish Bhagat
 *        Year: 2016
 */

// Website used: https://gamedevelopment.tutsplus.com/tutorials/creating-isometric-worlds-a-primer-for-game-developers--gamedev-6511

using UnityEngine;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// A class to be used for generating and loading 2D cartesian and isometric maps.
/// </summary>
public class Map2D : MonoBehaviour
{
    public bool isometric;
    public int tileSizeX;
    public int xSize, ySize;
    public GameObject prefab;
    public List<Tile> tiles = new List<Tile>();

    /// <summary>
    /// Generates and returns a list of tiles.
    /// </summary>
    public List<Tile> Generate()
    {
        // Get pixels per unit
        float ppu = prefab.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        float tileSizeInUnits = tileSizeX / ppu;

        // Create and place each tile
        for (int y = 0; y < ySize; y++)
            for (int x = 0; x < xSize; x++)
            {
                // Calculate position
                Vector3 position;

                if (isometric)
                    position = transform.position + (Vector3)Isometric.CartToIso((transform.right * (x * tileSizeInUnits / 2)) + (transform.up * (y * tileSizeInUnits / 2)));
                else
                    position = transform.position + (transform.right * (x * tileSizeInUnits)) + (transform.up * (y * tileSizeInUnits));

                // Create tile
                Tile tile = CreateTile(TileType.Grass, position);

                // Make GameObject a child object
                tile.gameObject.transform.parent = GameObject.Find("Tiles/Ground").transform;

                // Add tile to list
                tiles.Add(tile);
            }

        // Return tiles
        return tiles;
    }

    /// <summary>
    /// Loads an existing map.
    /// </summary>
    /// <param name="path">The XML file path which contains the world data.</param>
    /// <returns></returns>
    public static List<Tile> Load(string path)
    {
        // Create new List
        List<Tile> tiles = new List<Tile>();

        // Check if the file exists
        if (!File.Exists(path))
            return null;

        // Deserialize data
        TileDataContainer tileDataContainer = XMLSerializer.Deserialize<TileDataContainer>(path);

        // Clear parent GameObject
        GameObject parentGO = GameObject.Find("Tiles/Ground");
        foreach (Transform child in parentGO.transform)
            Destroy(child.gameObject);

        // Create tile
        foreach (TileData tileData in tileDataContainer.tileDataList)
        {
            // Create tile
            Tile tile = CreateTile(tileData);
            // Make tile a child object
            tile.gameObject.transform.parent = parentGO.transform;
            // Add tile to list
            tiles.Add(tile);
        }

        return tiles;
    }

    /// <summary>
    /// Creates and returns a Tile.
    /// </summary>
    /// <param name="tileType">The type of tile to be created.</param>
    /// <param name="position">The position of the newly created tile.</param>
    /// <returns></returns>
    public static Tile CreateTile(TileType tileType, Vector3 position)
    {
        // Get prefab of tileType
        GameObject gameObject = Resources.Load<GameObject>("Prefabs/" + tileType);

        // Instantiate
        GameObject go = Instantiate(gameObject, position, Quaternion.identity);
        // Get tile script
        Tile tile = go.GetComponent<Tile>();
        
        // Store tile data
        tile.StoreData();

        // Return tile
        return tile;
    }

    /// <summary>
    /// Creates and return a Tile.
    /// </summary>
    /// <param name="data">A reference to an existing TileData object, that is to be used for the created Tile object.</param>
    /// <returns></returns>
    public static Tile CreateTile(TileData data)
    {
        // Create tile
        return CreateTile(data.tileType, new Vector3(data.posX, data.posY, data.posZ));
    }
}