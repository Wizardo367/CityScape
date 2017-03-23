/*
 *       Class: TileDataContainer
 *      Author: Harish Bhagat
 *        Year: 2017
 */

// Video used: https://www.youtube.com/watch?v=Y8Di-Q6qpU4

using System.Collections.Generic;
using System.Xml.Serialization;

/// <summary>Used to serialise game data.</summary>
[XmlRoot("TileCollection")]
public class GameDataContainer
{
    [XmlElement("GameStateData")]
    public GameStateData GameStateData = new GameStateData();
    [XmlArray("Tiles"), XmlArrayItem("Tile")]
    public List<TileData> GroundDataList = new List<TileData>();
    [XmlArray("Buildings"), XmlArrayItem("Building")]
    public List<BuildingData> BuildingDataList = new List<BuildingData>();
    [XmlArray("Roads"), XmlArrayItem("Road")]
    public List<TileData> RoadDataList = new List<TileData>();
    [XmlArray("Decorations"), XmlArrayItem("Decoration")]
    public List<TileData> DecorationDataList = new List<TileData>();
}