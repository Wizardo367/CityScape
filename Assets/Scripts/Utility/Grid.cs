// Video used: https://www.youtube.com/playlist?list=PLhPNOL0P0EY1ksFFhhoN5SsNYHaw8U2AP

using UnityEngine;

public class Grid : MonoBehaviour
{
    public int Rows;
    public int Columns;
    public Node[] Nodes;
    public GameObject Map;
    public int[,] Layout;

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