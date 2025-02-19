using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Move _move;
    [SerializeField] private float _attackDelay = 0.7f;

    private bool _isCanAttack = true;

    private PlayerInput _inputs;

    private void Awake()
    {
        _inputs = new PlayerInput();

        _inputs.Player.Attack.performed += context => OnAttack();
        _inputs.Player.Move.performed += context => _move.JumpToSpline(context.ReadValue<float>());
    }

    private void OnEnable()
    {
        _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }

    private void OnAttack()
    {
        if (_isCanAttack == false)
            return;

        _isCanAttack = false;
        _animator.SetTrigger(PlayerAnimHash.Slash);
        StartCoroutine(CooldownAttackJob());
    }

    private IEnumerator CooldownAttackJob()
    {
        yield return new WaitForSecondsRealtime(_attackDelay);
        _isCanAttack = true;
    }
}