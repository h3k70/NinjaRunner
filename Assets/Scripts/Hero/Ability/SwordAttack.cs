using System.Collections;
using TrailsFX;
using UnityEngine;

public class SwordAttack : Skill
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TrailEffect _trailEffect;
    [SerializeField] private AudioSource _audioSlash;
    [SerializeField] private AudioSource _audioBlood;
    [SerializeField] private Collider _swordCollider;

    public void AnimEventEnableSwordCollider()
    {
        Attack();
    }

    public override void Activate()
    {
        if (IsReady == false)
            return;

        _animator.SetTrigger(PlayerAnimHash.Slash);

        StartCooldown();
    }
    private void Attack()
    {
        _swordCollider.enabled = true;
        _audioSlash.pitch = Random.Range(0.7f, 1.25f);
        _audioSlash.Play();
        StartCoroutine(AttackJob());
    }

    private IEnumerator AttackJob()
    {
        _trailEffect.duration = Duration;
        _trailEffect.enabled = true;
        yield return new WaitForSecondsRealtime(Duration);
        _swordCollider.enabled = false;
        _trailEffect.enabled = false;
    }
}
