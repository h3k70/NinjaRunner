using System.Collections;
using UnityEngine;

public class Healing : Skill
{
    [SerializeField] private ParticleSystem _particle;

    private float _count;

    public override void Init(Player player)
    {
        MaxLVL = 3;
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
            case 1:
                CooldownTime = 100f;
                _count = 20;
                break;

            case 2:
                CooldownTime = 90f;
                _count = 25;
                break;

            case 3:
                CooldownTime = 80f;
                _count = 30;
                break;

            default:
                break;
        }
    }
}
