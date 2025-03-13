using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class FoldingUI : MonoBehaviour
{
    [SerializeField] private RectTransform _collapsedPosition;
    [SerializeField] private Vector2 _collapsedSize;
    [SerializeField] private Vector2 _expandedSize;

    private RectTransform _object;
    private float _duration;

    public Action CollapseEnded;
    public Action CollapseYEnded;
    public Action ExpandEnded;

    public void Init(RectTransform rectTransform, float duration)
    {
        _duration = duration / 2;
        _object = rectTransform;
    }

    public void Collapse()
    {
        StartCoroutine(CollapseJob());
    }

    public void Expand()
    {
        StartCoroutine(ExpandJob());
    }

    public void Spread(Vector2 size)
    {
        _object.DOSizeDelta(size, _duration).SetEase(Ease.Linear).SetUpdate(true);
    }

    public void FoldY()
    {
        _object.DOSizeDelta(new Vector2(_object.sizeDelta.x, _collapsedSize.y), _duration).SetEase(Ease.Linear).SetUpdate(true);
        CollapseYEnded?.Invoke();
    }

    private void FoldX()
    {
        _object.DOSizeDelta(new Vector2(_collapsedSize.x, _object.sizeDelta.y), _duration).SetEase(Ease.Linear).SetUpdate(true);
    }

    private void Fold()
    {
        _object.DOSizeDelta(_collapsedSize, _duration).SetEase(Ease.Linear).SetUpdate(true);
    }

    private void SpreadY()
    {
        _object.DOSizeDelta(new Vector2(_object.sizeDelta.x, _expandedSize.y), _duration).SetEase(Ease.Linear).SetUpdate(true);
    }
    
    private void SpreadX()
    {
        _object.DOSizeDelta(new Vector2(_expandedSize.x, _object.sizeDelta.y), _duration).SetEase(Ease.Linear).SetUpdate(true);
    }    

    private void Spread()
    {
        _object.DOSizeDelta(_expandedSize, _duration).SetEase(Ease.Linear).SetUpdate(true);
    }

    private void MoveInCollapsePosition()
    {
        _object.DOMove(_collapsedPosition.position, _duration).SetUpdate(true);
    }

    private void MoveInExpandPosition()
    {
        _object.DOAnchorPos(new Vector2(_expandedSize.x / 2, 0), _duration).SetUpdate(true);
    }

    private IEnumerator CollapseJob()
    {
        FoldY();
        yield return new WaitForSecondsRealtime(_duration);
        FoldX();
        //Fold();
        MoveInCollapsePosition();
        yield return new WaitForSecondsRealtime(_duration);
        CollapseEnded?.Invoke();
    }

    private IEnumerator ExpandJob()
    {
        SpreadX();
        MoveInExpandPosition();
        yield return new WaitForSecondsRealtime(_duration);
        SpreadY();
        //Spread();
        yield return new WaitForSecondsRealtime(_duration);
        ExpandEnded?.Invoke();
    }
}
