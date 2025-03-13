using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour, IGradable
{
    [SerializeField] private Sprite _icon;

    protected Coroutine _cooldownCoroutine;
    protected Coroutine _castCoroutine;

    private Player _player;
    private int _currentLVL = 0;
    private int _maxLVL;
    private int _upgradePrice;
    private int _defaultUpgradePrice = 1000;
    private float _cooldownTime;
    private float _duration;
    private bool _isReady = true;

    public Sprite Icon => _icon;
    public bool IsReady { get => _isReady; protected set => _isReady = value; }
    public float Duration { get => _duration; protected set => _duration = value; }
    public int CurrentLVL => _currentLVL;
    public int UpgradePrice { get => _upgradePrice; protected set => _upgradePrice = value; }
    public int MaxLVL { get => _maxLVL; protected set { _maxLVL = value; MaxLVLChanged?.Invoke(_maxLVL); } }
    public float CooldownTime { get => _cooldownTime; protected set => _cooldownTime = value; }
    public Player Player { get => _player; }

    public Action<float> CurrentLVLChanged { get; set; }
    public Action<float> MaxLVLChanged { get; set; }
    public Action CooldownEnded;
    public Action<float> CooldownStarted;

    protected abstract IEnumerator CastLogic();
    protected abstract void UpdateSkillAttributes();

    public virtual void Init(Player player)
    {
        _player = player;
        UpdateSkillAttributes();
    }

    public virtual void Cast()
    {
        if (IsReady == false || _player.IsDead)
            return;

        StartCooldown();
        _castCoroutine = StartCoroutine(CastLogic());
    }

    public void SetLVL(int lvl)
    {
        if (lvl < 0 || lvl > _maxLVL)
            return;

        _currentLVL = lvl;
        CurrentLVLChanged?.Invoke(_currentLVL);

        UpdateSkillAttributes();
    }

    public void Upgrade()
    {
        if (_currentLVL >= _maxLVL)
            return;

        _currentLVL++;
        CalculateUpgradePrice();
        UpdateSkillAttributes();
        CurrentLVLChanged?.Invoke(_currentLVL);
    }

    public void ConnectButton(Button button)
    {
        button.onClick.AddListener(Cast);
    }

    protected virtual void CalculateUpgradePrice()
    {
        _upgradePrice = _defaultUpgradePrice * (2 * (_currentLVL + 1));
    }

    protected void StartCooldown()
    {
        _cooldownCoroutine = StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        _isReady = false;

        CooldownStarted?.Invoke(_cooldownTime);

        yield return new WaitForSeconds(_cooldownTime);
        _isReady = true;

        CooldownEnded?.Invoke();
    }
}
