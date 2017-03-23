using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows for a path to be found between two roads.
/// </summary>
public class RoadPathFinder : MonoBehaviour
{
    /// <summary>
    /// The size of the map in tiles.
    /// </summary>
    public int MapSizeX, MapSizeY;
    /// <summary>
    /// The node group to use for pathfinding.
    /// </summary>
    public GameObject NodeGroup;
    /// <summary>
    /// Determines whether or not the path should be highlighted on the map.
    /// </summary>
    public bool ShowPath;

    private int[,] _map;
    private Grid _grid;
    private Pathfinder _pathfinder;

    private int noOfTiles;

    /// <summary>
    /// Initialises this instance.
    /// </summary>
    private void Start()
    {
        _map = new int[MapSizeX, MapSizeY];
    }

    /// <summary>
    /// Returns a tile using it's label.
    /// </summary>
    /// <param name="label">The label.</param>
    /// <returns>A tile.</returns>
    private Tile GetTile(string label)
    {
        int id = int.Parse(label);
        GameObject go = NodeGroup.transform.GetChild(id).gameObject;

        return go.GetComponent<Tile>();
    }

    /// <summary>
    /// Finds a path between two roads.
    /// </summary>
    /// <param name="start">The starting point.</param>
    /// <param name="goal">The destination.</param>
    public void FindPath(Node start, Node goal)
    {
        // Check if the map needs updating
        Node[] nodes = NodeGroup.GetComponentsInChildren<Node>();
        int childCount = nodes.Length;

        if (childCount != noOfTiles)
        {
            UpdateMap();
            noOfTiles = childCount;
        }

        // Clear previous colour path
        if (ShowPath)
            foreach (Node node in nodes)
            {
                SpriteRenderer sp = node.gameObject.GetComponent<SpriteRenderer>();
                sp.color = Color.white;
            }

        _grid = gameObject.GetComponent<Grid>();
        _grid.Layout = _map;
        _pathfinder = new Pathfinder(_grid);
        _pathfinder.Start(start, goal);

        // Prevent infinite loop
        CountdownTimer timeoutTimer = new CountdownTimer {Seconds = 1f};
        timeoutTimer.Begin();

        while (!_pathfinder.PathFound)
        {
            _pathfinder.Step();

            if (timeoutTimer.IsDone())
                return;
            
            timeoutTimer.Update();
        }

        // Debug.Log("Search done, path length: " + _pathfinder.Path.Count + ", iterations: " + _pathfinder.Iterations);

        // Show path
        if (ShowPath)
            foreach (Node node in _pathfinder.Path)
            {
                SpriteRenderer sp = GetTile(node.Label).gameObject.GetComponent<SpriteRenderer>();
                sp.color = Color.red;
            }
    }

    /// <summary>
    /// Updates the map.
    /// </summary>
    public void UpdateMap()
    {
        // Loop through nodes
        Node[] searchTiles = NodeGroup.GetComponentsInChildren<Node>();

        // Mark all tiles as occupied
        for (int x = 0; x < MapSizeX; x++)
            for (int y = 0; y < MapSizeY; y++)
                _map[x, y] = 1;

        // Mark road tiles as traversable
        Node[] roadNodes = GameObject.Find("Game/Tiles/Roads").GetComponentsInChildren<Node>();

        foreach (Node searchNode in searchTiles)
            foreach (Node roadNode in roadNodes)
                if ((Vector2)searchNode.gameObject.transform.position == (Vector2)roadNode.gameObject.transform.position)
                    _map[searchNode.GridX, searchNode.GridY] = searchNode.Traversable ? 0 : 1;
    }

    /// <summary>
    /// Used to determine if a path has been found.
    /// </summary>
    /// <returns>A bool.</returns>
    public bool PathFound()
    {
        return _pathfinder.PathFound;
    }

    /// <summary>
    /// Gets the path.
    /// </summary>
    /// <returns></returns>
    public List<Node> GetPath()
    {
        return _pathfinder.Path;
    }
}