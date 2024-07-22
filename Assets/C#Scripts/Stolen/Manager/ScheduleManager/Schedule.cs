using System;
using UnityEngine;

public class Schedule
{
    public float duration { get; private set; }
    public float startTime { get; private set; }
    public float endTime { get; private set; }
    
    private event Action onStartCallback;
    private event Action onEndCallback;

    public Schedule(float duration) : this(duration, null) { }
    public Schedule(float duration, Action onEndCallback) : this(duration,onEndCallback, null) { }
    public Schedule(float duration, Action onEndCallback, Action onStartCallback) : this(duration, onEndCallback, onStartCallback, 0) { }
    public Schedule(float duration, Action onEndCallback, Action onStartCallback,float delay)
    {
        this.duration = duration;
        startTime = Time.realtimeSinceStartup + delay;
        endTime = Time.realtimeSinceStartup + duration + delay;

        this.onStartCallback = onStartCallback;
        this.onEndCallback = onEndCallback;
    }

    public void InvokeStartCallback()
    {
        onStartCallback?.Invoke();
    }

    public void InvokeEndCallback()
    {
        onEndCallback?.Invoke();
    }
    
}