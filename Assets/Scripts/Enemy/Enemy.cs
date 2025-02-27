using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _head;
    [SerializeField] private GameObject _particleHead;
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyAttack _attack;

    private float _hightForJumpHead = 3f;
    private float _jumpHeadDuration = 1f;
    private float _attackDistanceX = 2f;
    private float _attackDistanceY = 1f;
    private Player _target;
    private Coroutine _attackCoroutine;

    public Action Die;

    public void Init(Player target)
    {
        _target = target;
        _attack.Target = target;
    }

    public void Activate()
    {
        gameObject.SetActive(true);

        _animator.SetTrigger(EnemyAnimHash.Reset);
        _attack.IsCanAttack = true;

        _head.transform.localScale = Vector3.one;
        _particleHead.SetActive(false);
        _particleHead.transform.localPosition = Vector3.zero;
        _particleHead.transform.localRotation = Quaternion.identity;

        _attackCoroutine = StartCoroutine(AttackJob());
    }

    public void TakeDamage()
    {
        StopCoroutine(_attackCoroutine);
        _attack.IsCanAttack = false;
        _animator.SetTrigger(EnemyAnimHash.Dead);

        _head.transform.localScale = Vector3.zero;
        _particleHead.SetActive(true);
        _particleHead.transform.DOJump(transform.position, _hightForJumpHead, 1, _jumpHeadDuration);
        _particleHead.transform.DORotateQuaternion(Quaternion.Euler(-32, -171, 84), _jumpHeadDuration);
    }

    private IEnumerator AttackJob()
    {
        while (transform.position.x - _target.transform.position.x > _attackDistanceX)
        {
            yield return null;
        }

        if (Math.Abs(transform.position.y - _target.transform.position.y) <= _attackDistanceY)
            _animator.SetTrigger(EnemyAnimHash.Attack);
    }
}
