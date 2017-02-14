// Video used: https://www.youtube.com/watch?v=nhiFx28e7JY&t=530s

using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [Tooltip("The size of the grid in nodes; (e.g. 100x100)")]
    public Vector2 GridWorldSize;
    public int NodeSize;

    private Node[,] grid;

    private void Start()
    {
        // Initialise variables 
        // TODO Make this better
        grid = new Node[int.Parse(GridWorldSize.x.ToString()), int.Parse(GridWorldSize.y.ToString())];

        // Populate grid array
        Transform[] childrenTransforms = gameObject.GetComponentsInChildren<Transform>();
        int counter = 0;

        for (int y = 0; y < GridWorldSize.y; y++)
            for (int x = 0; x < GridWorldSize.x; x++)
            {
                // Get node
                Node node = childrenTransforms[counter].gameObject.GetComponent<Node>();
                // Check if node is null
                if (node == null) continue;
                // Add node
                node.GridX = x;
                node.GridY = y;
                grid[x, y] = node;
                // Update counter
                counter++;
            }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        // Check in a 3x3 pattern (-1 start helps identify the starting node)
        for (int x = -1; x <= -1; x++)
            for (int y = -1; y <= -1; y++)
            {
                // Check if node is the node being checked
                if (x == 0 && y == 0) continue;

                int checkX = node.GridX + x;
                int checkY = node.GridY + y;

                if (checkX >= 0 && checkX < GridWorldSize.x && checkY >= 0 && checkY < GridWorldSize.y)
                    neighbours.Add(grid[checkX, checkY]);
            }

        return neighbours;
    }

    public Node GetNode(Vector3 worldPosition)
    {
        for (int y = 0; y < GridWorldSize.y; y++)
            for (int x = 0; x < GridWorldSize.x; x++)
            {
                // Get node
                Node node = grid[x, y];
                // Compare positions
                if (node.transform.position == worldPosition)
                    return node;
            }

        return null;
    }
}