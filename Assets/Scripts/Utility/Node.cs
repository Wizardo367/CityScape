using System.Collections.Generic;
using UnityEngine;

// Video used: https://www.youtube.com/playlist?list=PLhPNOL0P0EY1ksFFhhoN5SsNYHaw8U2AP

/// <summary>
/// Defines a node; used within pathfinding classes.
/// </summary>
public class Node : MonoBehaviour
{
	/// <summary>
	/// Neighbouring nodes.
	/// </summary>
	public List<Node> Neighbours = new List<Node>();
	/// <summary>
	/// The previous node, connected to this node.
	/// </summary>
	public Node Previous;
	/// <summary>
	/// The label, used to find nodes.
	/// </summary>
	public string Label = "";

	/// <summary>
	/// Gets a value indicating whether this <see cref="Node"/> is traversable.
	/// </summary>
	/// <value>
	///   <c>true</c> if traversable; otherwise, <c>false</c>.
	/// </value>
	public bool Traversable
    {
        get { return TraversableUp || TraversableDown || TraversableLeft || TraversableRight; }
    }

	/// <summary>
	/// The traversable directions.
	/// </summary>
	public bool TraversableUp, TraversableDown, TraversableLeft, TraversableRight;

	/// <summary>
	/// The grid size.
	/// </summary>
	public int GridX, GridY;
	/// <summary>
	/// The size of the node in pixels.
	/// </summary>
	public int Size;

	/// <summary>
	/// The g cost.
	/// </summary>
	public int GCost;
	/// <summary>
	/// The h cost.
	/// </summary>
	public int HCost;

	/// <summary>
	/// Clears the node.
	/// </summary>
	public void Clear()
    {
        Previous = null;
    }

	/// <summary>
	/// Gets the f cost.
	/// </summary>
	/// <value>
	/// The f cost.
	/// </value>
	public int FCost
    {
        get { return GCost + HCost; }
    }
}