using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeadMenuUI : MonoBehaviour
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _reviveButton;
    [SerializeField] private TMP_Text _bestSource;

    private Game _game;

    public Button HomeButton { get => _homeButton; set => _homeButton = value; }
    public Button ReviveButton { get => _reviveButton; set => _reviveButton = value; }

    public void Init(Game game)
    {
        _game = game;

        _game.BestSourceChanged += BestSourceChanged;
    }

    private void BestSourceChanged(float source)
    {
        _bestSource.text = Mathf.RoundToInt(source).ToString();
    }
}
