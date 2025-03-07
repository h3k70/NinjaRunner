using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SmokeScreen : Skill
{
    [SerializeField] private Player _player;
    [SerializeField] private Move _move;
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private Image _hitImage;

    private Color _tempColor;
    private float _tempTrailDuration;
    private float _trailDuration = 3f;

    public override void Activate()
    {
        if (IsReady == false)
            return;

        _smoke.Play();
        _tempColor = _hitImage.color;
        _tempTrailDuration = _move.TrailEffects[0].duration;

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

        yield return new WaitForSecondsRealtime(Duration);

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
