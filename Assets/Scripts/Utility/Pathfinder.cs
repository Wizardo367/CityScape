using System;
using System.Collections.Generic;
using UnityEngine;

// Video used: https://www.youtube.com/playlist?list=PLhPNOL0P0EY1ksFFhhoN5SsNYHaw8U2AP
// Video used: https://www.youtube.com/watch?v=mZfyt03LDH4

/// <summary>
/// Used to find a path between two Nodes within a Grid.
/// </summary>
public class Pathfinder
{
	/// <summary>
	/// The grid to search.
	/// </summary>
	public Grid Grid;
	/// <summary>
	/// The open set of nodes.
	/// </summary>
	public List<Node> OpenSet;
	/// <summary>
	/// The closed set of nodes.
	/// </summary>
	public List<Node> ClosedSet;
	/// <summary>
	/// The path found.
	/// </summary>
	public List<Node> Path;
	/// <summary>
	/// The goal node.
	/// </summary>
	public Node GoalNode;
	/// <summary>
	/// The number of iterations processed to find the path.
	/// </summary>
	public int Iterations;
	/// <summary>
	/// Used to determine if the path has been found.
	/// </summary>
	public bool PathFound;

	/// <summary>
	/// Initialises a new instance of the <see cref="Pathfinder"/> class.
	/// </summary>
	/// <param name="grid">The grid.</param>
	public Pathfinder(Grid grid)
    {
        Grid = grid;
    }

	/// <summary>
	/// Starts the pathfinder.
	/// </summary>
	/// <param name="start">The start node.</param>
	/// <param name="goal">The goal node.</param>
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

	/// <summary>
	/// Used to step between nodes during the pathfinding process.
	/// </summary>
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

	/// <summary>
	/// Adds a neighbour to the current node.
	/// </summary>
	/// <param name="node">The node.</param>
	/// <param name="neighbour">The neighbour.</param>
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

	/// <summary>
	/// Gets the grid position.
	/// </summary>
	/// <param name="node">The node.</param>
	/// <returns></returns>
	public Vector2 GetGridPosition(Node node)
    {
        int nodeSize = node.Size;
        Vector2 worldPosition = node.gameObject.transform.position;

        return GetGridPosition(worldPosition, nodeSize);
    }

	/// <summary>
	/// Gets the grid position.
	/// </summary>
	/// <param name="worldPosition">The world position.</param>
	/// <param name="nodeSize">Size of the node.</param>
	/// <returns></returns>
	public Vector2 GetGridPosition(Vector2 worldPosition, int nodeSize)
    {
        int gridX = (int)Math.Round(worldPosition.x / nodeSize + 0.5f);
        int gridY = (int)Math.Round(worldPosition.y / nodeSize + 0.5f);

        return new Vector2(gridX, gridY);
    }

	/// <summary>
	/// Gets the index of the node.
	/// </summary>
	/// <param name="node">The node.</param>
	/// <param name="nodes">The nodes to search through.</param>
	/// <returns></returns>
	public int GetNodeIndex(Node node, List<Node> nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
            if (node == nodes[i])
                return i;

        return -1;
    }

	/// <summary>
	/// Gets the distance between nodes.
	/// </summary>
	/// <param name="nodeA">Node a.</param>
	/// <param name="nodeB">Node b.</param>
	/// <returns></returns>
	public int GetDistance(Node nodeA, Node nodeB)
    {
        // Gets the distance between two nodes
        int distX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        int distY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

        return distX + distY;
    }

	/// <summary>
	/// Chooses the next node.
	/// </summary>
	/// <returns>The next node to process.</returns>
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