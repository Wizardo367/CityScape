/*
 *       Class: TileDataContainer
 *      Author: Harish Bhagat
 *        Year: 2016
 */

// Video used: https://www.youtube.com/watch?v=Y8Di-Q6qpU4

using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("TileCollection")]
public class TileDataContainer
{
    [XmlArray("Tiles"), XmlArrayItem("Tile")]
    public List<TileData> tileDataList = new List<TileData>();
}