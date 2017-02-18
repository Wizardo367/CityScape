using UnityEngine;

public class RoadPathFinder : MonoBehaviour
{
    public int MapSizeX, MapSizeY;
    public GameObject MapGroup;

    private int[,] _map;
    private Grid _grid;
    private Pathfinder _pathfinder;

    // Use this for initialization
    private void Start()
    {
        _map = new int[MapSizeX, MapSizeY];
    }

    private Tile GetTile(string label)
    {
        int id = int.Parse(label);
        GameObject go = MapGroup.transform.GetChild(id).gameObject;

        return go.GetComponent<Tile>();
    }

    public void FindPath(Node start, Node goal)
    {
        // Loop through nodes
        Node[] searchTiles = MapGroup.GetComponentsInChildren<Node>();

        // Mark occupied grid positions
        for (int x = 0; x < MapSizeX; x++)
            for (int y = 0; y < MapSizeY; y++)
            {
                Node node = searchTiles[x * MapSizeY + y];
                _map[x, y] = (x == node.GridX && y == node.GridY) ? 0 : 1;
            }

        _grid = new Grid(_map);
        _pathfinder = new Pathfinder(_grid);
        _pathfinder.Start(start, goal);

        while (!_pathfinder.PathFound)
            _pathfinder.Step();

        Debug.Log("Search done, path length: " + _pathfinder.Path.Count + ", iterations: " + _pathfinder.Iterations);

        // Show path
        foreach (Node node in _pathfinder.Path)
        {
            SpriteRenderer sp = GetTile(node.Label).gameObject.GetComponent<SpriteRenderer>();
            sp.color = Color.red;
        }
    }
}
