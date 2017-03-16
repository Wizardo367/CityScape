/*
 *       Class: GameStateData
 *      Author: Harish Bhagat
 *        Year: 2017
 */

using System.Xml.Serialization;

// Video used: https://www.youtube.com/watch?v=Y8Di-Q6qpU4

/// <summary>
/// Class used to hold game state data for serialisation. 
/// </summary>
public class GameStateData
{
    [XmlElement("Money")]
    public int Money;
}