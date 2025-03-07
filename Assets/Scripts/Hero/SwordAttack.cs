using System;
using System.Collections;
using UnityEngine;

public class SwordAttack : MonoBehaviour, ISkillble
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _attackDelay = 1.4f;
    [SerializeField] private Sword _sword;

    private bool _isReady = true;
    private Coroutine _cooldownCoroutine;

    public Action CooldownEnded { get; set; }
    public Action<float> CooldownStarted { get; set; }

    public Sprite Icon => _icon;

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
        CooldownStarted?.Invoke(_attackDelay);

        yield return new WaitForSecondsRealtime(_attackDelay);
        _isReady = true;

        CooldownEnded?.Invoke();
    }
}
