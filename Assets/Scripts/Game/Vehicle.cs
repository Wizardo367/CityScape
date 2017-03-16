using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public List<Node> Path;
    public float Speed = 1f;
    public bool Stationary = true;

    private Vector3 _currentPos;
    private Vector3 _targetPos;
    private Direction2D _direction;
    private RotatableSprite2D _rotatableSprite2D;

    private void Start()
    {
        // Initialise variables
        _targetPos = gameObject.transform.position;
        _rotatableSprite2D = gameObject.GetComponent<RotatableSprite2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Update current position
        _currentPos = transform.position;

        // Check for a path or if stationary or if the last move is done
        if (!MoveDone() || Stationary) return;

        // Check if the vehicle needs to despawn
        if (Path.Count == 0)
        {
            Despawn();
            return;
        }

        // Get the next node
        Node node = Path[0];
        // Set targetPos
        _targetPos = node.transform.position;
        // Check if the sprite needs to be changed/rotated
        ProcessRotation();

        // Remove the node from the list
        Path.RemoveAt(0);
    }

    private bool MoveDone()
    {
        // Check if the move to the target is done
        if (_currentPos == _targetPos)
            return true;

        // Travel from your current position to the node
        gameObject.transform.position = Vector2.MoveTowards(_currentPos, _targetPos, Time.deltaTime * (Speed * 0.1f));

        return false;
    }

    private void Despawn()
    {
        Destroy(gameObject, 2f);
    }

    private void ProcessRotation()
    {
        // Get the X and Y difference between the current position and the target position

        // Get the direction of travel
        bool posX = _targetPos.x > _currentPos.x;
        bool posY = _targetPos.y > _currentPos.y;

        if (posX && posY)
            _direction = Direction2D.Up;
        else if (!posX && !posY)
            _direction = Direction2D.Down;
        else if (!posX && posY)
            _direction = Direction2D.Left;
        else
            _direction = Direction2D.Right;

        // Change the sprite
        _rotatableSprite2D.SetRotation(_direction);
    }
}
