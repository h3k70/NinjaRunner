using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private float _duration;

    protected Coroutine _cooldownCoroutine;
    private bool _isReady = true;

    public Sprite Icon => _icon;
    public bool IsReady { get => _isReady; protected set => _isReady = value; }
    public float Duration { get => _duration; protected set => _duration = value; }

    public Action CooldownEnded;
    public Action<float> CooldownStarted;

    public abstract void Activate();

    public void ConnectButton(Button button)
    {
        button.onClick.AddListener(Activate);
    }

    protected void StartCooldown()
    {
        _cooldownCoroutine = StartCoroutine(CooldownAttackJob());
    }

    private IEnumerator CooldownAttackJob()
    {
        _isReady = false;

        CooldownStarted?.Invoke(_cooldownTime);

        yield return new WaitForSeconds(_cooldownTime);
        _isReady = true;

        CooldownEnded?.Invoke();
    }
}
