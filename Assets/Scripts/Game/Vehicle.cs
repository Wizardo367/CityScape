using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public List<Node> Path;
    public float Speed = 1f;
    public bool Stationary = true;

    private Vector3 _targetPos;

    private void Start()
    {
        // Initialise variables
        _targetPos = gameObject.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        // Check for a path or if stationary or if the last move is done
        if (!MoveDone() || Path.Count == 0 || Stationary) return;

        // Get the next node
        Node node = Path[0];
        // Get the position of the node
        Vector3 nodePos = node.gameObject.transform.position;
        // Set targetPos
        _targetPos = nodePos;

        // Remove the node from the list
        Path.RemoveAt(0);
    }

    private bool MoveDone()
    {
        // Get current position
        Vector3 currentPos = gameObject.transform.position;

        // Check if the move to the target is done
        if (currentPos == _targetPos)
            return true;

        // Travel from your current position to the node
        gameObject.transform.position = Vector2.MoveTowards(currentPos, _targetPos, Time.deltaTime * (Speed * 0.1f));

        return false;
    }
}
