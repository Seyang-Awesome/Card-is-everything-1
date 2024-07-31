using System;
using System.Collections.Generic;
using UnityEngine;

public class ClampInt
{
    private int value;
    private int min;
    private int max;
    public event Action<int> onSetValue;
    public event Action<int> onValueChanged;

    public int Value
    {
        get => value;
        set
        {
            int temp = this.value;
            this.value = Mathf.Clamp(value, min, max);
            onSetValue?.Invoke(this.value);
            if(temp != this.value) onValueChanged?.Invoke(this.value);
        }
    }
    
    public ClampInt(int initValue, int min, int max)
    {
        value = initValue;
        this.min = min;
        this.max = max;
    }
}

public class ClampFloat
{
    private float value;
    private float min;
    private float max;
    public event Action<float> onSetValue;
    public event Action<float> onValueChanged;

    public float Value
    {
        get => value;
        set
        {
            float temp = this.value;
            this.value = Mathf.Clamp(value, min, max);
            onSetValue?.Invoke(this.value);
            if(Mathf.Approximately(temp, this.value)) onValueChanged?.Invoke(this.value);
        }
    }
    
    public ClampFloat(float initValue, float min, float max)
    {
        value = initValue;
        this.min = min;
        this.max = max;
    }
}
