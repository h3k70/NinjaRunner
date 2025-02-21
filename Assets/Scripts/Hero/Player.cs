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
    private bool _isSwipeEnded = true;
    private Vector2 _swipeStartPosition;
    private float _minSwipeDistance = 70;

    private void Awake()
    {
        _move.Init(_currentBuild);

        _inputs = new PlayerInput();

        _inputs.Player.Attack.performed += context => _baseAttack.OnAttack();
        _inputs.Player.Move.started += context => OnSwipeStarted(context.ReadValue<Vector2>());
        _inputs.Player.Move.performed += context => OnSwipe(context.ReadValue<Vector2>());
        _inputs.Player.Move.canceled += context => OnSwipeEnded(context.ReadValue<Vector2>());
    }

    private void OnSwipeStarted(Vector2 dir)
    {
        _swipeStartPosition = dir;

        if (dir.y == 1 || dir.y == -1)
        {
            _move.JumpToSpline(dir.y);
        }
    }

    private void OnSwipe(Vector2 position)
    {
        Vector2 swipeDelta = position - _swipeStartPosition;

        if (_isSwipeEnded && (swipeDelta).magnitude > _minSwipeDistance)
        {
            if (swipeDelta.y > 0)
            {
                _move.JumpToSpline(1);
            }
            else
            {
                _move.JumpToSpline(-1);
            }
            _isSwipeEnded = false;
        }
    }

    private void OnSwipeEnded(Vector2 dir)
    {
        _swipeStartPosition = Vector2.zero;
        _isSwipeEnded = true;
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