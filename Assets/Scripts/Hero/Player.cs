using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _maxHP = 100;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _takeDamageAudio;
    [SerializeField] private Move _move;
    [SerializeField] private SwordAttack _baseAttack;
    [SerializeField] private Transform _dieCameraPoint;

    private Health _health = new();
    private PlayerInput _inputs;
    private bool _isSwipeEnded = true;
    private Vector2 _swipeStartPosition;
    private float _minSwipeDistance = 70;
    private int _currentCoins;

    public Health Health { get => _health; }
    public Move Move { get => _move; }
    public int Coins { get => _currentCoins; }
    public Transform DieCameraPoint { get => _dieCameraPoint; }
    public ISkillble BaseAttack { get => _baseAttack; }
    public ISkillble FirstSkill { get => _baseAttack; }
    public ISkillble SecondSkill { get => _baseAttack; }
    public ISkillble ThirdSkill { get => _baseAttack; }

    public Action Died;
    public Action DamageTaked;
    public Action<int> CoinCountChanged;

    private void Awake()
    {
        _inputs = new PlayerInput();

        _inputs.Player.Attack.performed += context => _baseAttack.OnAttack();
        _inputs.Player.Move.started += context => OnSwipeStarted(context.ReadValue<Vector2>());
        _inputs.Player.Move.performed += context => OnSwipe(context.ReadValue<Vector2>());
        _inputs.Player.Move.canceled += context => OnSwipeEnded(context.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Build>(out Build build))
        {
            _move.CloserBuild = build;
        }
    }

    public void Init(Transform runPoint, Build build = null)
    {
        _health.Init(_maxHP, _maxHP);
        _move.Init(runPoint, build);

        _health.Ended += Die;
    }

    public void TakeDamage(float value)
    {
        _animator.SetTrigger(PlayerAnimHash.JumpStan);
        _takeDamageAudio.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
        _takeDamageAudio.Play();

        _health.Take(value);

        DamageTaked?.Invoke();
    }

    public void AddCoin(int value)
    {
        _currentCoins += value;

        CoinCountChanged?.Invoke(_currentCoins);
    }

    public void TrySpendCoin(int value)
    {
        CoinCountChanged?.Invoke(_currentCoins);
    }

    private void Die()
    {
        _health.Ended -= Die;

        _inputs.Player.Disable();
        _move.enabled = false;
        _animator.SetTrigger(PlayerAnimHash.Die);

        Died?.Invoke();
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
}