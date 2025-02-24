using System.Collections;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _attackDelay = 0.7f;
    [SerializeField] private Sword _sword;

    private bool _isReady = true;
    private Coroutine _cooldownCoroutine;

    public void EnableSwordCollider()
    {
        _sword.Activate();
    }

    public void OnAttack()
    {
        if (_isReady == false)
            return;

        _isReady = false;
        _animator.SetTrigger(PlayerAnimHash.Slash);

        _cooldownCoroutine = StartCoroutine(CooldownAttackJob());
    }

    private IEnumerator CooldownAttackJob()
    {
        yield return new WaitForSecondsRealtime(_attackDelay);
        _isReady = true;
    }
}
