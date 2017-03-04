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
    public new TileData Data = new TileData();

    public float Price;
    public float Upkeep;
}