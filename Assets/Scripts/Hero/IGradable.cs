using System;

public interface IGradable
{
    public int CurrentLVL { get; }
    public int MaxLVL { get; }
    public int UpgradePrice { get; }

    public Action<float> CurrentLVLChanged { get; set; }
    public Action<float> MaxLVLChanged { get; set; }

    public void SetLVL(int lvl);
    public void Upgrade();
}