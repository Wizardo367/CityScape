/*
 *       Class: PurchasableTileData
 *      Author: Harish Bhagat
 *        Year: 2016
 */

using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// Class used to hold PurchasableTile data for serialisation.
/// </summary>
public class PurchasableTileData : TileData
{
    [XmlElement("Price")]
    public float price;
}