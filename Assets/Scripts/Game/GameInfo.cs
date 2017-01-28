/*
 *       Class: GameInfo
 *      Author: Harish Bhagat
 *        Year: 2017
 */

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to store information on the current state of the game.
/// </summary>
class GameInfo : MonoBehaviour
{
    // Variables
    private static float money;
    private static Dictionary<BuildingType, int> taxSettings;

    private void Awake()
    {
        // Initalise variables
        money = 0;

        // Set default tax amounts
        foreach (BuildingType buildingType in Enum.GetValues(typeof(BuildingType)))
            taxSettings.Add(buildingType, 7);
    }
}