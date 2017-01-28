/*
 *       Class: PurchasableTileData
 *      Author: Harish Bhagat
 *        Year: 2017
 */

using System.Xml.Serialization;

/// <summary>
/// Class used to hold PurchasableTile data for serialisation.
/// </summary>
public class PurchasableTileData : TileData
{
    [XmlElement("Price")]
    public float price;
}