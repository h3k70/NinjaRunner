using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Move _move;
    [SerializeField] private SwordAttack _baseAttack;

    private PlayerInput _inputs;
    [SerializeField] private Build _currentBuild;
    private void Awake()
    {
        _move.Init(_currentBuild);

        _inputs = new PlayerInput();

        _inputs.Player.Attack.performed += context => _baseAttack.OnAttack();
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
}