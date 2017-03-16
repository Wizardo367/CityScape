using System;
using System.Collections.Generic;
using UnityEngine;

// Video used: https://www.youtube.com/playlist?list=PLhPNOL0P0EY1ksFFhhoN5SsNYHaw8U2AP
// Video used: https://www.youtube.com/watch?v=mZfyt03LDH4

public class Pathfinder
{
    public Grid Grid;
    public List<Node> OpenSet;
    public List<Node> ClosedSet;
    public List<Node> Path;
    public Node GoalNode;
    public int Iterations;
    public bool PathFound;

    public Pathfinder(Grid grid)
    {
        Grid = grid;
    }

    public void Start(Node start, Node goal)
    {
        // Add start to open set
        OpenSet = new List<Node> {start};

        // Add goal node
        GoalNode = goal;

        // Initialise variables
        ClosedSet = new List<Node>();
        Path = new List<Node>();
        Iterations = 0;

        // Clear previous nodes to prevent error
        foreach (Node node in Grid.Nodes)
            node.Clear();
    }

    public void Step()
    {
        // Search next node and neighbours
        
        // Check if path has been found
        if (Path.Count > 0)
            return;

        if (OpenSet.Count == 0)
        {
            PathFound = true;
            return;
        }

        // Keeping track helps prevent system from stalling
        Iterations++;

        Node node;

        while (true)
        {
            // Get next node
            node = ChooseNode();

            // Check if node is null
            if (node == null)
            {
                PathFound = true;
                return;
            }

            // Check if the node is traversable
            if (!node.Traversable)
            {
                OpenSet.Remove(node);
                ClosedSet.Add(node);
                continue;
            }

            break;
        }

        // Remove node from open set and add to closed set
        OpenSet.Remove(node);
        ClosedSet.Add(node);

        // Check if goal has been reached
        if (node == GoalNode)
        {
            while (node != null)
            {
                // Reverse path
                Path.Insert(0, node);
                node = node.Previous;
            }

            PathFound = true;
            return;
        }

        // Iterate through neighbours
        foreach (Node neighbour in node.Neighbours)
            AddNeighbour(node, neighbour);
    }

    public void AddNeighbour(Node node, Node neighbour)
    {
        if (ClosedSet.Contains(neighbour)) return;
        
        int moveCost = node.GCost + GetDistance(node, neighbour);

        // New path found
        if (moveCost < node.GCost || !OpenSet.Contains(neighbour))
        {
            // Check for traversability
            if (node.TraversableUp && !neighbour.TraversableDown ||
                node.TraversableDown && !neighbour.TraversableUp ||
                node.TraversableLeft && !neighbour.TraversableRight ||
                node.TraversableRight && !neighbour.TraversableLeft)
                return;

            // Add neighbour
            neighbour.GCost = moveCost;
            neighbour.HCost = GetDistance(neighbour, GoalNode);
            neighbour.Previous = node;

            if (!OpenSet.Contains(neighbour))
                OpenSet.Add(neighbour);
        }
    }

    public Vector2 GetGridPosition(Node node)
    {
        int nodeSize = node.Size;
        Vector2 worldPosition = node.gameObject.transform.position;

        return GetGridPosition(worldPosition, nodeSize);
    }

    public Vector2 GetGridPosition(Vector2 worldPosition, int nodeSize)
    {
        int gridX = (int)Math.Round(worldPosition.x / nodeSize + 0.5f);
        int gridY = (int)Math.Round(worldPosition.y / nodeSize + 0.5f);

        return new Vector2(gridX, gridY);
    }

    public int GetNodeIndex(Node node, List<Node> nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
            if (node == nodes[i])
                return i;

        return -1;
    }

    public int GetDistance(Node nodeA, Node nodeB)
    {
        // Gets the distance between two nodes
        int distX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        int distY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

        return distX + distY;
    }

    public Node ChooseNode()
    {
        // Get current node
        Node node;

        // Check if any nodes are left to explore
        if (OpenSet.Count > 0)
            node = OpenSet[0];
        else
            return null;

        foreach (Node selectedNode in OpenSet)
            if (selectedNode.FCost < node.FCost || selectedNode.FCost == node.FCost && selectedNode.HCost < node.HCost)
                node = selectedNode;

        return node;
    }
}
