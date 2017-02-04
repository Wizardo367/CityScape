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
    public new PurchasableTileData Data = new PurchasableTileData();

    public float Price;

    // Initialisation
    private void Awake()
    {
        // Make objects desructable - inherited member
        Destructable = true;
    }

    /// <summary>
    /// Stores data to a PurchaseableTileData object.
    /// </summary>
    public override void StoreData()
    {
        base.StoreData();
        Data = (PurchasableTileData)base.Data;
        Data.price = Price;
    }

    /// <summary>
    /// Loads data from a PurchaseableTileData object.
    /// </summary>
    public override void LoadData()
    {
        // Load local data before base data to prevent overwriting
        Price = Data.price;

        base.LoadData();
        Data = (PurchasableTileData)base.Data;

        // TODO Check that local data doesn't need to be set again
    }
}