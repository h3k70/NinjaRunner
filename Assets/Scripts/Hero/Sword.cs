using System.Collections;
using TrailsFX;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private TrailEffect _trailEffect;
    [SerializeField] private Collider _swordCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.Die();
        }
    }

    public void Activate()
    {
        _swordCollider.enabled = true;
        StartCoroutine(AttackJob());
    }

    private IEnumerator AttackJob()
    {
        _trailEffect.enabled = true; 
        yield return new WaitForSecondsRealtime(_trailEffect.duration);
        _swordCollider.enabled = false;
        _trailEffect.enabled = false;
    }
}
