using System.Collections;
using UnityEngine;

public class Healing : Skill
{
    [SerializeField] private ParticleSystem _particle;

    private float _count;

    public override void Init(Player player)
    {
        MaxLVL = 5;
        Duration = 0;
        base.Init(player);
    }

    protected override IEnumerator CastLogic()
    {
        Player.Health.Add(_count);
        _particle.Play();

        yield break;
    }

    protected override void UpdateSkillAttributes()
    {
        switch (CurrentLVL)
        {
            case 0:
                CooldownTime = 120f;
                _count = 15;
                break;

            case 1:
                CooldownTime = 90f;
                _count = 20;
                break;

            case 2:
                CooldownTime = 80f;
                _count = 30;
                break;

            case 3:
                CooldownTime = 80f;
                _count = 35;
                break;
                
            case 4:
                CooldownTime = 80f;
                _count = 40;
                break;
                
            case 5:
                CooldownTime = 70f;
                _count = 40;
                break;

            default:
                break;
        }
    }
}
