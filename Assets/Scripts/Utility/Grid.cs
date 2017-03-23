// Video used: https://www.youtube.com/playlist?list=PLhPNOL0P0EY1ksFFhhoN5SsNYHaw8U2AP

using UnityEngine;

/// <summary>
/// Defines a grid for pathfinding.
/// </summary>
public class Grid : MonoBehaviour
{
	/// <summary>
	/// The number of rows.
	/// </summary>
	public int Rows;
	/// <summary>
	/// The numbers of columns.
	/// </summary>
	public int Columns;
	/// <summary>
	/// The nodes of the grid.
	/// </summary>
	public Node[] Nodes;
	/// <summary>
	/// An instance of Map2D.
	/// </summary>
	public GameObject Map;
	/// <summary>
	/// The grid layout; 0 represents a traversable node, 1 a blocked node.
	/// </summary>
	public int[,] Layout;

	/// <summary>
	/// Initialises this instance.
	/// </summary>
	private void Start()
    {
        // Initialise layout
        if (Layout == null)
            Layout = new int[Rows, Columns];

        // Create array
        Nodes = Map.GetComponentsInChildren<Node>();

        // Populate node array
        for (int i = 0; i < Nodes.Length; i++)
            Nodes[i].Label = i.ToString();

        // Index the nodes
        for (int r = 0; r < Rows; r++)
            for (int c = 0; c < Columns; c++)
            {
                Node node = Nodes[Columns * r + c];
                node.GridX = r;
                node.GridY = c;

                // Check for unwalkable nodes
                if (Layout[r, c] == 1) continue;

                // Get neighbours

                // Up
                if (r > 0)
                    node.Neighbours.Add(Nodes[Columns * (r - 1) + c]);

                // Right
                if (c < Columns - 1)
                    node.Neighbours.Add(Nodes[Columns * r + c + 1]);

                // Down
                if (r < Rows - 1)
                    node.Neighbours.Add(Nodes[Columns * (r + 1) + c]);

                // Left
                if (c > 0)
                    node.Neighbours.Add(Nodes[Columns * r + c - 1]);
            }
    }
}