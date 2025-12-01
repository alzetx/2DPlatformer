using System;
using UnityEngine;

[Serializable]
public class Countdown
{
    [field: SerializeField]
    public float Duration { get; private set; }
    [field: SerializeField]
    public float CurrentTime { get; private set; }
    public event Action OnTimeEnded;
    public Countdown()
    {
    }

    public Countdown(float duration, Action onTimeEnded = null)
    {
        this.Duration = duration;
        CurrentTime = duration;
        OnTimeEnded = onTimeEnded;
    }

    public bool IsPlaying()
    {
        return CurrentTime > 0;
    }

    public bool IsEnded()
    {
        return CurrentTime <= 0;
    }

    public void Tick(float deltaTime)
    {
        CurrentTime = Mathf.Max(CurrentTime - deltaTime, 0);
        if (CurrentTime <= 0)
        {
            OnTimeEnded?.Invoke();
        }
    }

    public void Reset()
    {
        CurrentTime = Duration;
    }
}
