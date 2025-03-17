using DG.Tweening;
using UnityEngine;

public class HandTutorialUI : MonoBehaviour
{
    [SerializeField] private float _swapDistance = 100;
    [SerializeField] private float _swapDuration = 1;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    public void Stop()
    {
        transform.DOKill();
        transform.position = _startPosition;
        transform.localScale = Vector3.one;
    }

    public void SwapUp()
    {
        Vector3 vector3 = new Vector3(transform.position.x, transform.position.y + _swapDistance, transform.position.z);
        transform.DOMove(vector3, _swapDuration).SetLoops(-1).SetUpdate(true); ;
    }
    
    public void SwapDown()
    {
        Vector3 vector3 = new Vector3(transform.position.x, transform.position.y - _swapDistance, transform.position.z);
        transform.DOMove(vector3, _swapDuration).SetLoops(-1).SetUpdate(true); ;
    }
        
    public void Tap()
    {
        transform.DOScale(0.8f, _swapDuration).SetLoops(-1).SetUpdate(true); ;
    }

    public void SetPosition(Vector3 vector3)
    {
        transform.position = vector3;
    }
}
