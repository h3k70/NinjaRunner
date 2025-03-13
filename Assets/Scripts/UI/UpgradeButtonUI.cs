using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TMP_Text _prise;
    [SerializeField] private TMP_Text _maxText;
    [SerializeField] private Image[] _gradeFrame;
    [SerializeField] private Image[] _gradeCheckMark;

    private IGradable _gradable;
    private Player _player;

    public void Init(IGradable gradable, Player player)
    {
        gameObject.SetActive(true);

        _player = player;
        _gradable = gradable;
        _icon.sprite = _gradable.Icon;

        for (int i = 0; i < _gradable.MaxLVL; i++)
        {
            _gradeFrame[i].gameObject.SetActive(true);
        }
        UpdateInfo();

        _gradable.CurrentLVLChanged += OnCurrentLVLChanged;
        _buyButton.onClick.AddListener(TryBuy);
    }

    private void OnCurrentLVLChanged(int lvl)
    {
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        for (int i = 0; i < _gradable.CurrentLVL; i++)
        {
            _gradeCheckMark[i].DOFillAmount(1, 0.5f);
        }

        _prise.text = _gradable.UpgradePrice.ToString();

        if (_gradable.CurrentLVL >= _gradable.MaxLVL)
        {
            _maxText.gameObject.SetActive(true);
            _prise.gameObject.SetActive(false);
            _buyButton.gameObject.SetActive(false);
        }
    }

    private void TryBuy()
    {
        if (_player.TrySpendCoin(_gradable.UpgradePrice))
        {
            _gradable.Upgrade();
            UpdateInfo();
        }
    }
}
