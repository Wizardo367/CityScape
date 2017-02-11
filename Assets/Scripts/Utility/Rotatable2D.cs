using UnityEngine;

public class Rotatable2D : MonoBehaviour
{
    public bool RotateX, RotateY;
    public bool UseXAsPrimarySprite;

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
                gameObject.transform.Rotate(0f, 180f, 0f);
            else if (_counter == 1 || _counter == 3)
                gameObject.transform.Rotate(180f, 0f, 0f);
        }
        else if (RotateX)
        {
            gameObject.transform.Rotate(0f, 180f, 0f);
        }
        else if (RotateY)
        {
            gameObject.transform.Rotate(180f, 0f, 0f);
        }
    }
}