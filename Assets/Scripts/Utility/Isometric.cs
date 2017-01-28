/*
 *       Class: Isometric
 *      Author: Harish Bhagat
 *        Year: 2017
 */

// Website used: https://gamedevelopment.tutsplus.com/tutorials/creating-isometric-worlds-a-primer-for-game-developers--gamedev-6511
// Website used: https://breadcrumbsinteractive.com/two-unity-tricks-isometric-games/

using UnityEngine;

/// <summary>
/// A static class used for converting between 2D cartesian and isometric coordinates.
/// </summary>
public static class Isometric
{
    /// <summary>
    /// Used to convert cartesian coordinates to isometric coordinates.
    /// </summary>
    /// <param name="x">X coordinate to be converted.</param>
    /// <param name="y">Y coordinate to be converted.</param>
    /// <returns>Returns Vector2.</returns>
    public static Vector2 CartToIso(float x, float y)
    {
        // Convert the coordinates from cartesian to isometric
        Vector2 newCoordinates = new Vector2
        {
            x = x - y,
            y = (x + y) * 0.5f
        };

        return newCoordinates;
    }

    /// <summary>
    /// Used to convert cartesian coordinates to isometric coordinates.
    /// </summary>
    /// <param name="coordinates">The coordinates to be converted.</param>
    /// <returns>Returns Vector2.</returns>
    public static Vector2 CartToIso(Vector2 coordinates)
    {
        // Convert the coordinates from cartesian to isometric
        return CartToIso(coordinates.x, coordinates.y);
    }

    /// <summary>
    /// Used to convert isometric coordinates to cartesian coordinates.
    /// </summary>
    /// <param name="x">X coordinate to be converted.</param>
    /// <param name="y">Y coordinate to be converted.</param>
    /// <returns>Returns Vector2.</returns>
    public static Vector2 IsoToCart(float x, float y)
    {
        // Convert the coordinates from cartesian to isometric
        float yTwo = 2 * y;

        Vector2 newCoordinates = new Vector2()
        {
            x = (yTwo + x) * 0.5f,
            y = (yTwo - x) * 0.5f
        };

        return newCoordinates;
    }

    /// <summary>
    /// Used to convert isometric coordinates to cartesian coordinates.
    /// </summary>
    /// <param name="coordinates">The coordinates to be converted.</param>
    /// <returns>Returns Vector2.</returns>
    public static Vector2 IsoToCart(Vector2 coordinates)
    {
        // Convert the coordinates from cartesian to isometric
        return IsoToCart(coordinates.x, coordinates.y);
    }

    public static void DepthSort(GameObject go)
    {
        // Get all children
        Transform[] spriteTransforms = go.GetComponentsInChildren<Transform>();
        SpriteRenderer[] spriteRenderers = go.GetComponentsInChildren<SpriteRenderer>();

        int noOfChildren = spriteTransforms.Length;
        const int isometricRangePerYUnit = 100;

        // Depth sort
        for (int i = 0; i < noOfChildren; i++)
        {
            spriteRenderers[i].sortingOrder = -(int)(go.transform.position.y * isometricRangePerYUnit) + i;
        }
    }
}