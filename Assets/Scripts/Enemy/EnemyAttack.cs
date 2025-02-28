using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Player _player;
    private bool _isCanAttack;
    private float _damage;

    public Player Target { get { return _player; } set { _player = value; } }
    public bool IsCanAttack { get => _isCanAttack; set => _isCanAttack = value; }
    public float Damage { get => _damage; set => _damage = value; }

    public void AnimEventAttack()
    {
        if (_isCanAttack)
            Target.TakeDamage(_damage);
    }
}
