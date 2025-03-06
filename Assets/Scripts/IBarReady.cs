using System;

public interface IBarReady
{
    public float MaxValue { get; set; }
    public float Value { get; set; }

    public Action<float> ValueChanged { get; set; }
    public Action<float> MaxValueChanged { get; set; }
}
