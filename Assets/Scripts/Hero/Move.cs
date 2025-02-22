using DG.Tweening;
using System;
using System.Collections;
using TrailsFX;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class Move : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _heightOfBuildJump = 2f;
    [SerializeField] private SplineAnimate _splineAnimate;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _runPoint;
    [SerializeField] private TrailEffect[] _trailEffects;

    private float _jumpSpeed;
    private Build _currentBuild;
    private Build _closerBuild;
    private int _currentSplineIndex;
    private bool _isGround = true;
    private Coroutine _runCorounine;

    private float _minDistanceForJumpAnim = 0.5f;
    private float _minDistanceForJumpFlipAnim = 5;

    private float _multipleForAnimSpeedRun = 0.2f;
    private float _multipleForAnimSpeedJump = 1f;
    private float _multipleForJumpSpeed = 1.5f;

    public float Speed
    { 
        get
        {
            return _speed / _currentBuild.transform.lossyScale.x;
        }

        set 
        { 
            _speed = value; 
            _splineAnimate.MaxSpeed = Speed;
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
        }
    }
    public Transform RunPoint { get => _runPoint; set => _runPoint = value; }
    public Build CloserBuild { get => _closerBuild; set => _closerBuild = value; }

    public void Init(Build build)
    {
        _currentBuild = build;
        Speed = _speed;
        _splineAnimate.MaxSpeed = Speed;

        _currentSplineIndex = 0;
        _splineAnimate.Container = _currentBuild.SplineContainers[0];
        _splineAnimate.Completed += OnSplineCompleted;
        _splineAnimate.Play();
    }

    public void JumpToSpline(float dir)
    {
        int tempIndex = _currentSplineIndex + (int)dir;

        if (_isGround == false || tempIndex == -2)
            return;

        if (tempIndex == -1)
        {
            JumpToGround();
            return;
        }

        if (_currentBuild == null)
        {
            if (_closerBuild.SplineContainers.Length > tempIndex && tempIndex >= 0)
            {
                FindNearestPointOnSpline(_closerBuild.SplineContainers[tempIndex], out Vector3 nearestPoint, out float timeOnSpline);

                if (timeOnSpline == 1 || timeOnSpline == 0)
                    return;

                _currentBuild = _closerBuild;

                StartCoroutine(TrailRenderJob());
                StartRunOnSpline(_currentBuild.SplineContainers[tempIndex], timeOnSpline);

                _currentSplineIndex = tempIndex;

                return;
            }
        }
            

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

    private void JumpToGround()
    {
        _currentSplineIndex = -1;
        _currentBuild = null;
        _splineAnimate.Pause();
        StartCoroutine(TrailRenderJob());
        transform.position = new Vector3(transform.position.x, _runPoint.position.y, _runPoint.position.z);
        _runCorounine = StartCoroutine(RunOnGroundJob());
    }

    private void OnSplineCompleted()
    {
        StartCoroutine(JumpToNextBuildJob());
    }

    private void StartRunOnSpline(SplineContainer splineContainer, float timeOnSpline)
    {
        if (_runCorounine != null)
            StopCoroutine(_runCorounine);

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

    private void StartAnimJump(float time, float distance,  Vector3 targetPoint)
    {
        _animator.SetFloat(PlayerAnimHash.JumpSpeedAnim, _multipleForAnimSpeedJump / time);
        transform.LookAt(new Vector3(targetPoint.x, transform.position.y, targetPoint.z));

        if (distance <= _minDistanceForJumpAnim)
            return;
        else if (_minDistanceForJumpFlipAnim > distance)
            _animator.SetTrigger(PlayerAnimHash.ShortJump);
        else
            _animator.SetTrigger(PlayerAnimHash.JumpFlip);
    }

    private IEnumerator JumpToNextBuildJob()
    {
        if (_currentBuild.NextBuild == null)
        {
            JumpToGround();
            yield break;
        }
        _isGround = false;

        SplineContainer _targetSpline = FindNearestSpline(_currentBuild.NextBuild.SplineContainers);

        Vector3 startPointOnSpline = _targetSpline.transform.TransformPoint(_targetSpline.Spline[0].Position);
        float distanceX = Math.Abs(startPointOnSpline.x - transform.position.x);
        float distance = Vector3.Distance(transform.position, startPointOnSpline);
        float time = distanceX / (Speed * _multipleForJumpSpeed);
        JumpSpeed = distance / time;

        StartAnimJump(time, distance, startPointOnSpline);

        transform.DOMove(startPointOnSpline, time).SetEase(Ease.Linear);
        yield return new WaitForSeconds(time);

        _currentBuild = _currentBuild.NextBuild;
        _currentSplineIndex = Array.IndexOf(_currentBuild.SplineContainers, _targetSpline);

        Speed = Speed * _currentBuild.transform.lossyScale.x;
        StartRunOnSpline(_currentBuild.SplineContainers[_currentSplineIndex], 0);

        _isGround = true;

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

    private IEnumerator RunOnGroundJob()
    {
        while (true)
        {
            yield return null;
            transform.Translate(Vector3.right * _speed * Time.deltaTime, Space.World);
        }
    }
}
