using System;
using UnityEngine;

public class Resource
{
    private float _maxValue;
    private float _value;

    public float MaxValue { get => _maxValue; }
    public float Value { get => _value; }

    public Action<float> ValueChanged;
    public Action<float> MaxValueChanged;
    public Action Ended;

    public void Init(float maxValue)
    {
        _maxValue = maxValue;
        _value = maxValue;

        MaxValueChanged?.Invoke(maxValue);
        ValueChanged?.Invoke(maxValue);
    }

    public void Take(float value)
    {
        _value -= value;

        if(_value <= 0)
        {
            _value = 0;
            Ended?.Invoke();
        }
        ValueChanged?.Invoke(_value);
    }

    public void Add(float value)
    {
        _value += value;

        if(_value > _maxValue)
            _value = _maxValue;

        ValueChanged?.Invoke(_value);
    }
}