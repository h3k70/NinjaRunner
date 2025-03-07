using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmokeScreen : Skill
{
    [SerializeField] private Player _player;
    [SerializeField] private Move _move;
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private Image _hitImage;
    [SerializeField] private CameraFollow _camera;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _radius = 15;

    private Color _tempColor;
    private float _tempTrailDuration;
    private Vector3 _tempPosition;
    private float _trailDuration = 3f;
    private float _deley = 0.3f;
    private Collider[] _enemiesColliders;

    private void Start()
    {
        _tempColor = _hitImage.color;
        _tempTrailDuration = _move.TrailEffects[0].duration;
    }

    public override void Activate()
    {
        if (IsReady == false)
            return;

        _enemiesColliders = Physics.OverlapSphere(transform.position, _radius, _layer);

        StartCooldown();
        StartCoroutine(AttackJob());
    }

    private IEnumerator AttackJob()
    {
        foreach (var item in _move.TrailEffects)
        {
            item.duration = _trailDuration;
            item.color = Color.black;
        }

        _hitImage.color = Color.black;
        _player.IsCanTakeDamage = false;
        _move.IsCanPlayTrailEffects = false;


        _smoke.transform.parent = null;
        _move.JumpToGround();
        _tempPosition = _player.transform.position;
        _camera.TargetMoveTransform = _camera.transform;
        _smoke.Play();

        foreach (var item in _enemiesColliders)
        {
            _player.transform.position = item.transform.position;
            _player.transform.LookAt(item.transform.position);
            item.GetComponent<Enemy>().TakeDamage();
            yield return new WaitForSeconds(_deley);
        }
        _player.transform.position = _tempPosition;

        _smoke.transform.parent = _player.transform;
        _smoke.transform.localPosition = Vector3.zero;

        _camera.TargetMoveTransform = null;

        _hitImage.DOFade(0, Duration).SetEase(Ease.Linear);

        _hitImage.color = _tempColor;
        _player.IsCanTakeDamage = true;
        _move.IsCanPlayTrailEffects = true;

        foreach (var item in _move.TrailEffects)
        {
            item.duration = _tempTrailDuration;
            item.color = Color.clear;
        }
    }
}
