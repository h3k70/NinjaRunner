using UnityEngine;

public class Healing : Skill
{
    [SerializeField] private Player _player;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private float _count = 20;

    public override void Activate()
    {
        if (IsReady == false)
            return;

        _player.Health.Add(_count);
        _particle.Play();

        StartCooldown();
    }
}
