using UnityEngine;

public class RoadPathFinder : MonoBehaviour
{
    public int MapSizeX, MapSizeY;
    public GameObject NodeGroup;
    public bool ShowPath;

    private int[,] _map;
    private Grid _grid;
    private Pathfinder _pathfinder;

    private int noOfTiles;

    // Use this for initialization
    private void Start()
    {
        _map = new int[MapSizeX, MapSizeY];
    }

    private Tile GetTile(string label)
    {
        int id = int.Parse(label);
        GameObject go = NodeGroup.transform.GetChild(id).gameObject;

        return go.GetComponent<Tile>();
    }

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

        while (!_pathfinder.PathFound)
            _pathfinder.Step();

        Debug.Log("Search done, path length: " + _pathfinder.Path.Count + ", iterations: " + _pathfinder.Iterations);

        // Show path
        if (ShowPath)
            foreach (Node node in _pathfinder.Path)
            {
                SpriteRenderer sp = GetTile(node.Label).gameObject.GetComponent<SpriteRenderer>();
                sp.color = Color.red;
            }
    }

    public void UpdateMap()
    {
        // Loop through nodes
        Node[] searchTiles = NodeGroup.GetComponentsInChildren<Node>();

        // Mark all tiles as occupied
        for (int x = 0; x < MapSizeX; x++)
            for (int y = 0; y < MapSizeY; y++)
                _map[x, y] = 1;

        // Mark road tiles as traversable
        foreach (Node node in searchTiles)
            _map[node.GridX, node.GridY] = node.Traversable ? 0 : 1;
    }
}
