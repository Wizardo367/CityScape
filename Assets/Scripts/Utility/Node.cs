// Video used: https://www.youtube.com/watch?v=nhiFx28e7JY&t=530s

using UnityEngine;

public class Node : MonoBehaviour
{
    public bool Traversable;
    public int GridX, GridY;

    public int GCost;
    public int HCost;

    public Node parent;

    public Node(bool traversable, int gridX, int gridY)
    {
        Traversable = traversable;
        GridX = gridX;
        GridY = gridY;
    }

    public int FCost
    {
        get { return GCost + HCost; }
    }
}