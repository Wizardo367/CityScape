/*
 *       Class: TileData
 *      Author: Harish Bhagat
 *        Year: 2017
 */

using System.Xml.Serialization;

// Video used: https://www.youtube.com/watch?v=Y8Di-Q6qpU4

/// <summary>
/// Class used to hold Tile data for serialisation.
/// </summary>
public class TileData
{
    [XmlAttribute("Buildable")]
    public bool buildable;

    [XmlAttribute("Destructable")]
    public bool destructable;

    [XmlElement("TileType")]
    public TileType tileType;

    [XmlElement("PosX")]
    public float posX;

    [XmlElement("PosY")]
    public float posY;

    [XmlElement("PosZ")]
    public float posZ;
}