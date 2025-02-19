using DG.Tweening;
using System;
using System.Collections;
using TrailsFX;
using UnityEngine;
using UnityEngine.Splines;

public class Move : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _heightOfBuildJump = 2f;
    [SerializeField] private SplineAnimate _splineAnimate;
    [SerializeField] private Animator _animator;
    [SerializeField] private TrailEffect[] _trailEffects;

    private float _jumpSpeed;
    [SerializeField] private Build _currentBuild;
    private int _currentSplineIndex;
    private bool _isCanJumpOnSpline = true;

    private float _maxDistanceForJumpFlipAnim = 7;
    private float _minDistanceForJumpFlipAnim = 5;
    private float _multipleForAnimSpeedRun = 0.2f;
    private float _multipleForAnimSpeedJump = 0.1f;
    private float _multipleForJumpSpeed = 2f;

    public float Speed
    { 
        get => _speed; 

        set 
        { 
            _speed = value; 
            _splineAnimate.MaxSpeed = _speed;
            _animator.SetFloat(PlayerAnimHash.RunSpeedAnim, _multipleForAnimSpeedRun * _speed);
            JumpSpeed = _speed * _multipleForJumpSpeed;
        } 
    }

    public float JumpSpeed
    {
        get => _jumpSpeed;

        protected set
        {
            _jumpSpeed = value;
            _animator.SetFloat(PlayerAnimHash.JumpSpeedAnim, _multipleForAnimSpeedJump * _jumpSpeed);
        }
    }

    public void Init()
    {
        Speed = _speed;
        _splineAnimate.MaxSpeed = Speed;
    }

    private void Awake()
    {
        Init();

        _currentSplineIndex = 0;
        _splineAnimate.Container = _currentBuild.SplineContainers[0];
        _splineAnimate.Completed += OnSplineCompleted;
        _splineAnimate.Play();
    }

    public void JumpToSpline(float dir)
    {
        if (_isCanJumpOnSpline == false)
            return;

        int tempIndex = _currentSplineIndex + -(int)dir;

        if (_currentBuild.SplineContainers.Length > tempIndex && tempIndex >= 0)
        {
            FindNearestPointOnSpline(_currentBuild.SplineContainers[tempIndex], out Vector3 nearestPoint, out float timeOnSpline);

            if(timeOnSpline == 1 || timeOnSpline == 0)
                return;

            StartCoroutine(TrailRenderJob());
            StartRunOnSpline(_currentBuild.SplineContainers[tempIndex], timeOnSpline);

            _currentSplineIndex = tempIndex;
        }
    }

    private void OnSplineCompleted()
    {
        StartCoroutine(JumpToNextBuildJob());
    }

    private void StartRunOnSpline(SplineContainer splineContainer, float timeOnSpline)
    {
        _animator.SetTrigger(PlayerAnimHash.JumpStan);
        _splineAnimate.Completed -= OnSplineCompleted;
        _splineAnimate.Container = splineContainer;
        _splineAnimate.Completed += OnSplineCompleted;
        _splineAnimate.NormalizedTime = timeOnSpline;
        _splineAnimate.Play();
    }

    private void FindNearestPointOnSpline(SplineContainer splineContainer, out Vector3 nearestPoint, out float timeOnSpline, int step = 50)
    {
        Spline spline = splineContainer.Spline;
        Vector3 targetPosition = splineContainer.transform.InverseTransformPoint(transform.position);
        nearestPoint = Vector3.zero;
        timeOnSpline = 0f;
        float closestDistance = float.MaxValue;

        for (int i = 0; i <= step; i++)
        {
            float t = i / (float)step;
            Vector3 pointOnSpline = spline.EvaluatePosition(t);
            float distance = (targetPosition - pointOnSpline).sqrMagnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestPoint = pointOnSpline;
                timeOnSpline = t;
            }
        }
        nearestPoint = splineContainer.transform.TransformPoint(nearestPoint);
    }

    private SplineContainer FindNearestSpline(SplineContainer[] splines)
    {
        float closestDistance = float.MaxValue;
        SplineContainer closestSline = null;

        foreach (var item in splines)
        {
            Vector3 position = item.transform.TransformPoint(item.Spline[0].Position);

            float distance = (transform.position - position).sqrMagnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestSline = item;
            }
        }
        return closestSline;
    }

    private void StartAnimJump(float distance)
    {
        if (_minDistanceForJumpFlipAnim < distance && distance < _maxDistanceForJumpFlipAnim)
            _animator.SetTrigger(PlayerAnimHash.JumpFlip);
        else
            _animator.SetTrigger(PlayerAnimHash.LongJump);
    }

    private IEnumerator JumpToNextBuildJob()
    {
        _isCanJumpOnSpline = false;

        SplineContainer _targetSpline = FindNearestSpline(_currentBuild.NextBuild.SplineContainers);

        Vector3 startPointOnSpline = _targetSpline.transform.TransformPoint(_targetSpline.Spline[0].Position);
        float distanceX = Math.Abs(startPointOnSpline.x - transform.position.x);
        float distance = Vector3.Distance(transform.position, startPointOnSpline);
        float time = distanceX / (Speed * _multipleForJumpSpeed);
        JumpSpeed = distance / time;
        Vector3 halfPath = (transform.position + startPointOnSpline) / 2f;
        float heighDistance;

        if (Math.Abs(transform.position.y - startPointOnSpline.y) < _heightOfBuildJump)
            heighDistance = startPointOnSpline.y + _heightOfBuildJump;
        else
            heighDistance = halfPath.y;

        StartAnimJump(distance);

        transform.LookAt(new Vector3(startPointOnSpline.x, transform.position.y, startPointOnSpline.z));

        transform.DOMove(new Vector3(halfPath.x, heighDistance, halfPath.z), time / 2).SetEase(Ease.Linear);
        yield return new WaitForSeconds(time / 2);

        transform.DOMove(startPointOnSpline, time / 2).SetEase(Ease.Linear);
        yield return new WaitForSeconds(time / 2);

        _currentBuild = _currentBuild.NextBuild;
        _currentSplineIndex = Array.IndexOf(_currentBuild.SplineContainers, _targetSpline);

        StartRunOnSpline(_currentBuild.SplineContainers[_currentSplineIndex], 0);

        _isCanJumpOnSpline = true;

        yield return null;
    }

    private IEnumerator TrailRenderJob()
    {
        foreach (var item in _trailEffects)
            item.color = Color.white;

        yield return new WaitForSecondsRealtime(_trailEffects[0].duration);

        foreach (var item in _trailEffects)
            item.color = Color.clear;
    }
}
