using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public List<Node> Path;
    public float Speed = 5;
    public bool Stationary;

    private List<Node> _traversedNodes;

    private void Start()
    {
        // Initialise variables
        _traversedNodes = new List<Node>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Check for a path or if stationary
        if (Path.Count == 0 || Stationary) return;

        // Get the next node
        Node node = Path[0];
        // Get the position of the node
        Vector3 nodePos = node.transform.position;
        // Get current position
        Vector3 currentPos = gameObject.transform.position;

        // Travel from your current position to the node
        gameObject.transform.position = Vector3.MoveTowards(currentPos, nodePos, Speed * Time.deltaTime);

        // Remove the node from the list and add it to traversedNodes
        Path.RemoveAt(0);
        _traversedNodes.Add(node);
    }
}
