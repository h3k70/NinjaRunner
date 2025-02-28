using System;
using System.Collections;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _attackDelay = 1.4f;
    [SerializeField] private Sword _sword;

    private bool _isReady = true;
    private Coroutine _cooldownCoroutine;

    public Action CooldownEnded;
    public Action CooldownStarted;

    public void AnimEventEnableSwordCollider()
    {
        _sword.Activate();
    }

    public void OnAttack()
    {
        if (_isReady == false)
            return;

        _isReady = false;
        _animator.SetTrigger(PlayerAnimHash.Slash);

        _cooldownCoroutine = StartCoroutine(CooldownAttackJob());
    }

    private IEnumerator CooldownAttackJob()
    {
        CooldownStarted?.Invoke();

        yield return new WaitForSecondsRealtime(_attackDelay);
        _isReady = true;

        CooldownEnded?.Invoke();
    }
}
