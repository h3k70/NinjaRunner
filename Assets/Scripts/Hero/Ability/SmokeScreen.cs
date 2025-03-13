using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmokeScreen : Skill
{
    [SerializeField] private AudioSource _slash;
    [SerializeField] private AudioSource _smokeAudio;
    [SerializeField] private Move _move;
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private Image _hitImage;
    [SerializeField] private CameraFollow _camera;
    [SerializeField] private LayerMask _layer;

    private float _radius = 15;
    private Color _tempColor;
    private float _tempTrailDuration;
    private float _trailDuration = 3f;
    private float _deley = 0.3f;
    private Collider[] _enemiesColliders;

    private void Start()
    {
        _tempColor = _hitImage.color;
        _tempTrailDuration = _move.TrailEffects[0].duration;
    }

    public override void Init(Player player)
    {
        MaxLVL = 5;
        Duration = 1;
        base.Init(player);
    }

    protected override IEnumerator CastLogic()
    {
        _move.StopRun();

        _enemiesColliders = Physics.OverlapSphere(transform.position, _radius, _layer);

        foreach (var item in _move.TrailEffects)
        {
            item.Clear();
            item.color = Color.black;
            item.duration = _trailDuration;
        }
        yield return null;

        _hitImage.color = Color.black;
        Player.IsCanTakeDamage = false;
        _move.IsCanPlayTrailEffects = false;


        _smoke.transform.parent = null;
        _camera.TargetMoveTransform = _camera.transform;
        _smoke.Play();
        _smokeAudio.Play();

        foreach (var item in _enemiesColliders)
        {
            Player.transform.position = item.transform.position;

            Enemy enemy = item.GetComponent<Enemy>();

            if (enemy.IsDead)
                continue;

            enemy.TakeDamage();
            _slash.Play();

            yield return new WaitForSeconds(_deley);
        }
        _smoke.transform.parent = Player.transform;
        _smoke.transform.localPosition = Vector3.zero;

        _camera.TargetMoveTransform = null;
        _hitImage.DOFade(0, Duration).SetEase(Ease.Linear);

        _hitImage.color = _tempColor;
        Player.IsCanTakeDamage = true;
        _move.IsCanPlayTrailEffects = true;

        foreach (var item in _move.TrailEffects)
        {
            item.duration = _tempTrailDuration;
            item.color = Color.clear;
        }
        _move.StartRun();
    }

    protected override void UpdateSkillAttributes()
    {
        switch (CurrentLVL)
        {
            case 1:
                CooldownTime = 60f;
                _radius = 9;
                break;

            case 2:
                CooldownTime = 55f;
                _radius = 11;
                break;

            case 3:
                CooldownTime = 50f;
                _radius = 13;
                break;

            case 4:
                CooldownTime = 45f;
                _radius = 15;
                break;

            case 5:
                CooldownTime = 40f;
                _radius = 17;
                break;

            default:
                break;
        }
    }
}
