using UnityEngine;

public class Rotatable2D : MonoBehaviour
{
    public bool RotateX, RotateY;

    private int _counter;
    private int _maxCounter;

    // Use this for initialization
    private void Start()
    {
        _counter = 0;
        _maxCounter = 3;
    }

    // TODO Remove repeated code
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

    private void RotateXAxis()
    {
        gameObject.transform.Rotate(180f, 0f, 0f);
    }

    private void RotateYAxis()
    {
        gameObject.transform.Rotate(0f, 180f, 0f);
    }
}