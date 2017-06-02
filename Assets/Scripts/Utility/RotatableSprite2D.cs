using System;
using UnityEngine;

/// <summary>
/// Defines a 2D object that can appear to be rotated using different sprites.
/// </summary>
public class RotatableSprite2D : MonoBehaviour
{
	/// <summary>
	/// Used to determine if rotation is the X axis is valid.
	/// </summary>
	public bool RotateX;
	/// <summary>
	/// Used to determine if rotation is the Y axis is valid.
	/// </summary>
	public bool RotateY;

	/// <summary>
	/// Rotation sprite Up.
	/// </summary>
	public Sprite Up;
	/// <summary>
	/// Rotation sprite Down.
	/// </summary>
	public Sprite Down;
	/// <summary>
	/// Rotation sprite Left.
	/// </summary>
	public Sprite Left;
	/// <summary>
	/// Rotation sprite Right.
	/// </summary>
	public Sprite Right;

    private SpriteRenderer _spriteRenderer;
    private Sprite _currentSprite;

	/// <summary>
	/// Initialises this instance.
	/// </summary>
	private void Start()
    {
        // Initialise variables
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _currentSprite = _spriteRenderer.sprite;

        SetStartingSprite();
    }

	/// <summary>
	/// Sets the starting sprite.
	/// </summary>
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

	/// <summary>
	/// Rotates the object.
	/// </summary>
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
            _currentSprite = _currentSprite == Left ? Right : Left;
        else if (RotateY)
            _currentSprite = _currentSprite == Up ? Down : Up;

        _spriteRenderer.sprite = _currentSprite;
    }

	/// <summary>
	/// Sets the rotation.
	/// </summary>
	/// <param name="direction">The direction.</param>
	/// <exception cref="ArgumentOutOfRangeException">direction - null</exception>
	public void SetRotation(Direction2D direction)
    {
        // Set sprite
        switch (direction)
        {
            case Direction2D.Up:
                _currentSprite = Up;
                break;
            case Direction2D.Right:
                _currentSprite = Right;
                break;
            case Direction2D.Down:
                _currentSprite = Down;
                break;
            case Direction2D.Left:
                _currentSprite = Left;
                break;
            default:
                throw new ArgumentOutOfRangeException("direction", direction, null);
        }

        _spriteRenderer.sprite = _currentSprite;
    }
}
