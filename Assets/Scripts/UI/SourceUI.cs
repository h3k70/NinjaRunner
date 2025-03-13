using TMPro;
using UnityEngine;
using DG.Tweening;

public class SourceUI : MonoBehaviour
{
    [SerializeField] private BarUI _expBar;
    [SerializeField] private TMP_Text _lvl;
    [SerializeField] private TMP_Text _totalSource;
    [SerializeField] private TMP_Text _Coin;

    private Source _source;
    private Player _player;
    private Vector3 _textLVLScale = new Vector3(4f, 4f, 1);
    private float _textLVLScaleDuration = 0.5f;

    public void Init(Source source, Player player)
    {
        _source = source;
        _player = player;

        _expBar.Init(_source);
        OnCoinCountChanged(_player.Coins);
        OnLVLChanged(0);

        _source.LVLChanged += OnLVLChanged;
        _source.TotalChanged += OnTotalChanged;
        _player.CoinCountChanged += OnCoinCountChanged;
    }

    private void OnTotalChanged(float value)
    {
        _totalSource.text = Mathf.RoundToInt(value).ToString();
    }

    private void OnCoinCountChanged(float value)
    {
        _Coin.text = value.ToString();
    }

    private void OnLVLChanged(int lvl)
    {
        _lvl.transform.localScale = _textLVLScale;
        _lvl.transform.DOScale(1, _textLVLScaleDuration);
        _lvl.text = $"{1 + (lvl * 0.1)}X";
    }
}
