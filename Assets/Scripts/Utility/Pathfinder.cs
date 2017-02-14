// Video used: https://www.youtube.com/watch?v=mZfyt03LDH4&t=806s

using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private Grid _grid;

    // Use this for initialization
    void Start()
    {
        _grid = GetComponent<Grid>(); // must be on same GameObject
    }

    private void FindPath(Node startNode, Node targetNode)
    {
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        // Add start node to open
        openSet.Add(startNode);

        // Loop nodes
        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
                // Check for lowest fCost, if they're both the same compare hCost
                if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost)
                    currentNode = openSet[i];

            // Remove current node from open set
            openSet.Remove(currentNode);
            // Add current node to closed set
            closedSet.Add(currentNode);

            // Check if path has been found
            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            // Loop through neighbours
            foreach (Node neighbour in _grid.GetNeighbours(currentNode))
            {
                // Check if neighbour is not traversable or closed
                if (!neighbour.Traversable || closedSet.Contains(neighbour)) continue;

                // Check if new path to neighbour is shorter or neighbour is not in open
                int newMoveCost = currentNode.GCost + GetDistance(currentNode, neighbour);

                if (newMoveCost < neighbour.GCost || !openSet.Contains(neighbour))
                {
                    // Set fCost by setting gCost and hCost
                    neighbour.GCost = newMoveCost;
                    neighbour.HCost = GetDistance(neighbour, targetNode);
                    // Set parent
                    neighbour.parent = currentNode;

                    // Add neighbour to open set
                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
               }
            }
        }
    }

    // Used to constantly retrace the path as it updates
    private void RetracePath(Node startNode, Node targetNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = targetNode;

        // Work backwards
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
    }

    private int GetDistance(Node startNode, Node targetNode)
    {
        int distX = Mathf.Abs(startNode.GridX - targetNode.GridX);
        int distY = Mathf.Abs(startNode.GridY - targetNode.GridY);

        if (distX > distY)
            return 14 * distY + 10 * (distX - distY);

        return 14 * distX + 10 * (distY - distX);
    }
}