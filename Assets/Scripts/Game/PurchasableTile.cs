/*
 *       Class: PurchasableTile
 *      Author: Harish Bhagat
 *        Year: 2017
 */

/// <summary>
/// Used to define a tile that can be purchased in-game and destroyed when needed.
/// </summary>
public class PurchasableTile : Tile
{
    /// <summary>
    /// Used to load and store data from XML. 
    /// </summary>
    public new TileData Data = new TileData();

    /// <summary>
    /// The price.
    /// </summary>
    public float Price;
    /// <summary>
    /// The upkeep cost.
    /// </summary>
    public float Upkeep;
}