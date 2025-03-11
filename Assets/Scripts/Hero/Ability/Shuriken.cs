using System;
using TrailsFX;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Shuriken : Skill
{
    [SerializeField] private AudioSource _slash;
    [SerializeField] private Animator _animator;
    [SerializeField] private Move _move;
    [SerializeField] private GameObject _shuriken;

    private float _shurikenScale = 15;

    public override void Init(Player player)
    {
        MaxLVL = 5;
        base.Init(player);
    }

    protected override IEnumerator CastLogic()
    {
        _animator.SetTrigger(PlayerAnimHash.ShurikenJump);

        _shuriken.SetActive(true);
        _shuriken.transform.localScale = Vector3.zero;
        _shuriken.transform.DOScale(_shurikenScale, 0.5f);

        _move.IsCanPlayJumpAnim = false;
        _slash.Play();

        yield return new WaitForSeconds(Duration);

        _move.IsCanPlayJumpAnim = true;
        _slash.Stop();

        _shuriken.SetActive(false);
        _animator.SetTrigger(PlayerAnimHash.ShurikenEnd);
    }

    protected override void UpdateSkillAttributes()
    {
        switch (CurrentLVL)
        {
            case 1:
                CooldownTime = 100f;
                Duration = 4;
                break;

            case 2:
                CooldownTime = 90f;
                Duration = 5;
                break;

            case 3:
                CooldownTime = 80f;
                Duration = 6;
                break;

            case 4:
                CooldownTime = 70f;
                Duration = 7;
                break;

            case 5:
                CooldownTime = 60f;
                Duration = 8;
                break;

            default:
                break;
        }
    }
}
