using System.Collections;
using TrailsFX;
using UnityEngine;

public class Slash : MonoBehaviour
{
    [SerializeField] private AudioSource _audioBlood;
    [SerializeField] private float _minPitch = 0.8f;
    [SerializeField] private float _maxPitch = 1.25f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage();
            _audioBlood.pitch = Random.Range(_minPitch, _maxPitch);
            _audioBlood.Play();
        }
    }
}
