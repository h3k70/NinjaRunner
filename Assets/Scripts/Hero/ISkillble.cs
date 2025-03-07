using System;
using UnityEngine;

public interface ISkillble
{
    public Sprite Icon { get; }
    public Action CooldownEnded { get; set; }
    public Action<float> CooldownStarted { get; set; }
}
