using System.Collections;
using TrailsFX;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private TrailEffect _trailEffect;
    [SerializeField] private AudioSource _audioSlash;
    [SerializeField] private AudioSource _audioBlood;
    [SerializeField] private Collider _swordCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage();
            _audioBlood.pitch = Random.Range(0.8f, 1.25f);
            _audioBlood.Play();
        }
    }

    public void Activate()
    {
        _swordCollider.enabled = true;
        _audioSlash.pitch = Random.Range(0.7f, 1.25f);
        _audioSlash.Play();
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
