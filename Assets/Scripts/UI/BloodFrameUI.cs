using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BloodFrameUI : MonoBehaviour
{
    [SerializeField] private Image _bloodFrame;
    [SerializeField] private float _duration = 0.5f;

    private Color _clearColor;
    private Color _defaultColor;

    private void Start()
    {
        _defaultColor = new Color(_bloodFrame.color.r, _bloodFrame.color.g, _bloodFrame.color.b, 1);
        _clearColor = new Color(_bloodFrame.color.r, _bloodFrame.color.g, _bloodFrame.color.b, 0);
    }

    public void StartHitAnim()
    {
        _bloodFrame.color = _defaultColor;
        _bloodFrame.DOColor(_clearColor, _duration);
    }
}
