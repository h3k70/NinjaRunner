using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Button _swithButton;
    [SerializeField] private Button _resume;
    [SerializeField] private Button _homeButton;

    private FoldingUI _foldingUI;
    private bool _isShowed = false;

    public Button HomeButton { get => _homeButton; }

    private void OnEnable()
    {
        _swithButton.gameObject.SetActive(true);

        _foldingUI.CollapseEnded += OnCollapseEnded;
        _foldingUI.ExpandEnded += OnExpandEnded;
    }

    private void OnDisable()
    {
        _swithButton.gameObject.SetActive(false);

        _foldingUI.CollapseEnded -= OnCollapseEnded;
        _foldingUI.ExpandEnded -= OnExpandEnded;
    }

    public void Init(FoldingUI foldingUI)
    {
        _foldingUI = foldingUI;
        _swithButton.onClick.AddListener(SwitchMenu);
        _resume.onClick.AddListener(Hide);
    }

    public void Show()
    {
        _isShowed = true;
        Time.timeScale = 0;
        _swithButton.interactable = false;

        _foldingUI.Expand();
    }

    public void Hide()
    {
        _isShowed = false;
        _swithButton.interactable = false;

        _foldingUI.Collapse();
    }

    private void SwitchMenu()
    {
        if (_isShowed)
            Hide();
        else
            Show();
    }

    private void OnExpandEnded()
    {
        _swithButton.interactable = true;
    }

    private void OnCollapseEnded()
    {
        _swithButton.interactable = true;
        Time.timeScale = 1;
    }
}
