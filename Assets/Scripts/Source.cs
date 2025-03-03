using System;
using UnityEngine;

public class Source : Resource
{
    private int _currentLVL = 0;
    private int _maxLVL = 10;
    private float _multiplierOfMaxValue = 2f;

    public Action<int> LVLChanged;

    public int CurrentLVL { get => _currentLVL; }

    public override void Add(float value)
    {
        _value += value;

        if (_value >= _maxValue)
        {
            float tempValue = _value - _maxValue;
            _value = 0;

            _maxValue *= _multiplierOfMaxValue;
            MaxValueChanged(_maxValue);

            if(_currentLVL + 1 <= _maxLVL)
            {
                _currentLVL++;
                LVLChanged?.Invoke(_currentLVL);
            }
            Add(tempValue);
            return;
        }
        ValueChanged(_value);
    }
}
