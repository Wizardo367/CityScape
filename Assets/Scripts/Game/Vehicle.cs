using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public List<Node> Path;
    public float Speed = 0.01f;
    public bool Stationary = true;

    private List<Node> _traversedNodes;
    private Vector3 _targetPos;

    private void Start()
    {
        // Initialise variables
        _traversedNodes = new List<Node>();
        _targetPos = gameObject.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        // Check for a path or if stationary or if the last move is done
        if (Path.Count == 0 || Stationary || !MoveDone(_targetPos)) return;

        // Get the next node
        Node node = Path[0];
        // Get the position of the node
        Vector3 nodePos = node.gameObject.transform.position;
        // Set targetPos
        _targetPos = nodePos;
        // Get current position
        Vector3 currentPos = gameObject.transform.position;
        // Travel from your current position to the node
        gameObject.transform.position = Vector3.MoveTowards(currentPos, _targetPos, Speed * Time.deltaTime);

        // Remove the node from the list and add it to traversedNodes
        Path.RemoveAt(0);
        _traversedNodes.Add(node);
    }

    private bool MoveDone(Vector3 targetPos)
    {
        return gameObject.transform.position == targetPos;
    }
}
