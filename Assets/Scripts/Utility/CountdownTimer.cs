using UnityEngine;

public class CountdownTimer
{
    public bool Paused = true;
    public float Seconds;

    private float _setSeconds;

    public void Begin()
    {
        Paused = false;
        _setSeconds = Seconds;
    }

    public bool IsDone()
    {
        return Seconds <= 0;
    }

    public void ResetClock()
    {
        Seconds = _setSeconds;
        Paused = true;
    }

    public void Update()
    {
        if (!Paused || IsDone())
            Seconds -= Time.deltaTime;
    }
}