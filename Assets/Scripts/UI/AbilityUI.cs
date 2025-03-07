using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private Image _frame;
    [SerializeField] private Color _frameReadyColor;
    [SerializeField] private Color _frameNotReadyColor;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;

    private Skill _skill;

    public void Init(Skill skill)
    {
        _skill = skill;

        _icon.sprite = _skill.Icon;

        _skill.CooldownStarted += OnCooldownStarted;
        _skill.CooldownEnded += OnCooldownEnded;

        _skill.ConnectButton(_button);

        OnCooldownEnded();
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
