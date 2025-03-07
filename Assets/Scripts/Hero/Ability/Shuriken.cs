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

    public override void Activate()
    {
        if (IsReady == false)
            return;

        _animator.SetTrigger(PlayerAnimHash.ShurikenJump);

        _shuriken.SetActive(true);
        _shuriken.transform.localScale = Vector3.zero;
        _shuriken.transform.DOScale(_shurikenScale, 0.5f);

        StartCooldown();
        StartCoroutine(AttackJob());
    }

    private IEnumerator AttackJob()
    {
        _move.IsCanPlayJumpAnim = false;
        _slash.Play();

        yield return new WaitForSeconds(Duration);

        _move.IsCanPlayJumpAnim = true;
        _slash.Stop();

        _shuriken.SetActive(false);
        _animator.SetTrigger(PlayerAnimHash.ShurikenEnd);
    }
}
