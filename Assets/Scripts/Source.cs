using System;
using UnityEngine;

public class Source : IValueble
{
    protected float _maxValue;
    protected float _value;

    public float MaxValue { get => _maxValue; }
    public float Value { get => _value; }

    public Action<float> ValueChanged { get; set; }
    public Action<float> MaxValueChanged { get; set; }

    private int _currentLVL = 0;
    private int _maxLVL = 10;
    private float _totalSource;

    public Action<int> LVLChanged;
    public Action<float> TotalChanged;

    public int CurrentLVL { get => _currentLVL; }
    public float Total { get => _totalSource; }

    public void Init(int startLVL = 0)
    {
        _currentLVL = startLVL;
        _maxValue = GetValueForNextLVL();
    }
    
    public void ResetMe()
    {
        _currentLVL = 0;
        _maxValue = GetValueForNextLVL();
        _totalSource = 0;
    }

    public void Add(float value)
    {
        _totalSource += value * (_currentLVL * 0.1f + 1);
        TotalChanged?.Invoke(_totalSource);

        if (_maxLVL <= _currentLVL)
            return;

        _value += value;

        if (_value >= _maxValue)
        {
            float tempValue = _value - _maxValue;
            _value = 0;

            _maxValue = GetValueForNextLVL();
            MaxValueChanged?.Invoke(_maxValue);

            if(_currentLVL + 1 <= _maxLVL)
            {
                _currentLVL++;
                LVLChanged?.Invoke(_currentLVL);
            }
            Add(tempValue);
            return;
        }
        ValueChanged?.Invoke(_value);
    }

    private float GetValueForNextLVL()
    {
        //_maxValue *= (-0.1f * _currentLVL + 2);

        switch (_currentLVL)
        {
            case 0:
                return 100;
                
            case 1:
                return 300;
                
            case 2:
                return 500;
                
            case 3:
                return 700; 
                            
            case 4:
                return 1000;
                
            case 5:
                return 1500;
                
            case 6:
                return 3000;
                
            case 7:
                return 4000;
                
            case 8:
                return 5500;
                
            case 9:
                return 8000;

            default:
                return 7000;
        }
    }
}
