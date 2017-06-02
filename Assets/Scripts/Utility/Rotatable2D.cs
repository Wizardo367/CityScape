using UnityEngine;

/// <summary>
/// Defines a 2D object that can be rotated using it's transform.
/// </summary>
public class Rotatable2D : MonoBehaviour
{
	/// <summary>
	/// Used to determine if rotation is the X axis is valid.
	/// </summary>
	public bool RotateX;
	/// <summary>
	/// Used to determine if rotation is the Y axis is valid.
	/// </summary>
	public bool RotateY;

    private int _counter;
    private int _maxCounter;

	/// <summary>
	/// Initialises this instance.
	/// </summary>
	private void Start()
    {
        _counter = 0;
        _maxCounter = 3;
    }

	/// <summary>
	/// Rotates the object.
	/// </summary>
	public void Rotate()
    {
        // Check counter
        _counter++;

        if (_counter > _maxCounter)
            _counter = 0;

        // Rotate sprite
        if (RotateX && RotateY)
        {
            if (_counter == 0 || _counter == 2)
                RotateXAxis();
            else if (_counter == 1 || _counter == 3)
                RotateYAxis();
        }
        else if (RotateX)
            RotateXAxis();
        else if (RotateY)
            RotateYAxis();
    }

	/// <summary>
	/// Rotates along the x axis.
	/// </summary>
	private void RotateXAxis()
    {
        gameObject.transform.Rotate(180f, 0f, 0f);
    }

	/// <summary>
	/// Rotates along the y axis.
	/// </summary>
	private void RotateYAxis()
    {
        gameObject.transform.Rotate(0f, 180f, 0f);
    }
}