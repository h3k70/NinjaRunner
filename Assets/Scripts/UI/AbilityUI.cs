using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private Image _frame;
    [SerializeField] private Color _frameReadyColor;
    [SerializeField] private Color _frameNotReadyColor;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;

    private ISkillble _skill;

    public void Init(ISkillble skill)
    {
        _skill = skill;

        _icon.sprite = _skill.Icon;

        _skill.CooldownStarted += OnCooldownStarted;
        _skill.CooldownEnded += OnCooldownEnded;
    }

    private void OnCooldownEnded()
    {
        _frame.color = _frameReadyColor;
        _frame.fillAmount = 1;
        _frame.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        _frame.transform.DOScale(1, 0.5f);
    }

    private void OnCooldownStarted(float time)
    {
        _frame.color = _frameNotReadyColor;
        _frame.fillAmount = 0;
        _frame.DOFillAmount(1, time);
    }
}
