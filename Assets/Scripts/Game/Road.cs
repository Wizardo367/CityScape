using UnityEngine;

/// <summary>
/// Used to define a road.
/// </summary>
public class Road : MonoBehaviour
{
    /// <summary>
    /// The traversability directions.
    /// </summary>
    public bool TraversableUp, TraversableDown, TraversableLeft, TraversableRight;

    /// <summary>
    /// Rotates the object 180 degrees on it's y axis.
    /// </summary>
    public void RotateY()
    {
        // Rotates traversable directions by 180 degrees in the Y axis

        // Check for four way road
        if (TraversableUp && TraversableDown && TraversableLeft && TraversableRight)
            return;

        if (TraversableUp && TraversableDown && !TraversableLeft && !TraversableRight ||
            !TraversableUp && !TraversableDown && TraversableLeft && TraversableRight)
        {
            // Rotate two directional road
            TraversableUp = !TraversableUp;
            TraversableDown = !TraversableDown;
            TraversableLeft = !TraversableLeft;
            TraversableRight = !TraversableRight;
        }
        else if (TraversableUp && TraversableDown && (!TraversableLeft || !TraversableRight))
        {
            // Swap left and right directions for three way road
            TraversableLeft = !TraversableLeft;
            TraversableRight = !TraversableRight;
        }
        else if (TraversableLeft && TraversableRight && (!TraversableUp || !TraversableDown))
        {
            // Swap up and down directions for three way road
            TraversableUp = !TraversableUp;
            TraversableDown = !TraversableDown;
        }
    }

    /// <summary>
    /// Determines whether this road [can travel to] another.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <param name="direction">The direction.</param>
    /// <returns>
    ///   <c>true</c> if this instance [can travel to] the specified other; otherwise, <c>false</c>.
    /// </returns>
    public bool CanTravelTo(Road other, Direction2D direction)
    {
        // Checks if travel between two roads is possible
        return direction == Direction2D.Up && TraversableUp && other.TraversableDown ||
               direction == Direction2D.Down && TraversableDown && other.TraversableUp ||
               direction == Direction2D.Left && TraversableLeft && other.TraversableRight ||
               direction == Direction2D.Right && TraversableRight && other.TraversableLeft;
    }
}