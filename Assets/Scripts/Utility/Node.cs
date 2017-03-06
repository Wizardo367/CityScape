using System.Collections.Generic;
using UnityEngine;

// Video used: https://www.youtube.com/playlist?list=PLhPNOL0P0EY1ksFFhhoN5SsNYHaw8U2AP

public class Node : MonoBehaviour
{
    public List<Node> Neighbours = new List<Node>();
    public Node Previous;
    public string Label = "";

    public bool Traversable
    {
        get { return TraversableUp || TraversableDown || TraversableLeft || TraversableRight; }
    }

    public bool TraversableUp, TraversableDown, TraversableLeft, TraversableRight;

    public int GridX, GridY;
    public int Size;

    public int GCost;
    public int HCost;

    public void Clear()
    {
        Previous = null;
    }

    public int FCost
    {
        get { return GCost + HCost; }
    }
}