using System;
using UnityEngine;

public interface IGradable
{
    public Sprite Icon { get; }
    public int CurrentLVL { get; }
    public int MaxLVL { get; }
    public float UpgradePrice { get; }

    public Action<int> CurrentLVLChanged { get; set; }
    public Action<int> MaxLVLChanged { get; set; }

    public void SetLVL(int lvl);
    public void Upgrade();
}