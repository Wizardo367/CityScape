using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableSprite2D : MonoBehaviour
{
    public bool RotateX, RotateY;
    public Sprite Up, Down, Left, Right;

    private SpriteRenderer _spriteRenderer;
    private Sprite _currentSprite;

    // Use this for initialization
    private void Start()
    {
        // Initialise variables
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _currentSprite = _spriteRenderer.sprite;

        SetStartingSprite();
    }

    private void SetStartingSprite()
    {
        if (_currentSprite == Up)
            _currentSprite = Up;
        else if (_currentSprite == Down)
            _currentSprite = Down;
        else if (_currentSprite == Left)
            _currentSprite = Left;
        else if (_currentSprite == Right)
            _currentSprite = Right;
    }

    public void Rotate()
    {
        // Rotate sprite
        if (RotateX && RotateY)
        {
            // Rotate clockwise
            if (_currentSprite == Up)
                _currentSprite = Right;
            else if (_currentSprite == Right)
                _currentSprite = Down;
            else if (_currentSprite == Down)
                _currentSprite = Left;
            else if (_currentSprite == Left)
                _currentSprite = Up;
        }
        else if (RotateX)
        {
            _currentSprite = (_currentSprite == Left ? Right : Left);
        }
        else if (RotateY)
        {
            _currentSprite = (_currentSprite == Up ? Down : Up);
        }

        _spriteRenderer.sprite = _currentSprite;
    }

    public void SetRotation(Direction2D direction)
    {
        // Set sprite
        switch (direction)
        {
            case Direction2D.Up:
                _currentSprite = Right;
                break;
            case Direction2D.Right:
                _currentSprite = Down;
                break;
            case Direction2D.Down:
                _currentSprite = Left;
                break;
            case Direction2D.Left:
                _currentSprite = Up;
                break;
            default:
                throw new ArgumentOutOfRangeException("direction", direction, null);
        }

        _spriteRenderer.sprite = _currentSprite;
    }
}
