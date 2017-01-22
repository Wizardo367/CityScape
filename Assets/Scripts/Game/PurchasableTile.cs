/*
 *       Class: PurchasableTile
 *      Author: Harish Bhagat
 *        Year: 2016
 */

/// <summary>
/// Used to define a tile that can be purchased in-game and destroyed when needed.
/// </summary>
public class PurchasableTile : Tile
{
    public new PurchasableTileData data = new PurchasableTileData();

    public float price;

    // Initialisation
    void Awake()
    {
        // Make objects desructable - inherited member
        destructable = true;
    }

    /// <summary>
    /// Stores data to a PurchaseableTileData object.
    /// </summary>
    public override void StoreData()
    {
        base.StoreData();
        data = (PurchasableTileData)base.data;
        data.price = price;
    }

    /// <summary>
    /// Loads data from a PurchaseableTileData object.
    /// </summary>
    public override void LoadData()
    {
        // Load local data before base data to prevent overwriting
        price = data.price;

        base.LoadData();
        data = (PurchasableTileData)base.data;

        // TODO Check that local data doesn't need to be set again
    }
}