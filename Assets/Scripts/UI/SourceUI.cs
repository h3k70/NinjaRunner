using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SourceUI : MonoBehaviour
{
    [SerializeField] private BarUI _expBar;
    [SerializeField] private TMP_Text _lvl;
    [SerializeField] private TMP_Text _Coin;

    private Source _source;
    private Player _player;

    public void Init(Source source, Player player)
    {
        _source = source;
        _player = player;

        _expBar.Init(_source);
        OnCoinCountChanged(_player.Coins);
        OnLVLChanged(0);

        _source.LVLChanged += OnLVLChanged;
        _player.CoinCountChanged += OnCoinCountChanged;
    }

    private void OnCoinCountChanged(int value)
    {
        _Coin.text = value.ToString();
    }

    private void OnLVLChanged(int lvl)
    {
        _lvl.text = $"{1 + (lvl * 0.1)}X";
    }
}
