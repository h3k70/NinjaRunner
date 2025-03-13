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

    public override void Init(Player player)
    {
        MaxLVL = 5;
        Duration = 0.2f;
        base.Init(player);
    }

    public void AnimEventEnableSwordCollider()
    {
        Attack();
    }

    public override void Cast()
    {
        if (IsReady == false)
            return;

        _animator.SetTrigger(PlayerAnimHash.Slash);

        StartCooldown();
    }

    protected override IEnumerator CastLogic()
    {
        _trailEffect.duration = Duration;
        _trailEffect.enabled = true;
        yield return new WaitForSeconds(Duration);
        _swordCollider.enabled = false;
        _trailEffect.enabled = false;
    }

    protected override void UpdateSkillAttributes()
    {
        switch (CurrentLVL)
        {
            case 0:
                CooldownTime = 2f;
                break;

            case 1:
                CooldownTime = 1.8f;
                break;

            case 2:
                CooldownTime = 1.6f;
                break;

            case 3:
                CooldownTime = 1.4f;
                break;
                
            case 4:
                CooldownTime = 1.2f;
                break;
                
            case 5:
                CooldownTime = 0.8f;
                break;

            default:
                break;
        }
    }

    private void Attack()
    {
        _swordCollider.enabled = true;
        _audioSlash.pitch = Random.Range(0.7f, 1.25f);
        _audioSlash.Play();
        StartCoroutine(CastLogic());
    }
}
