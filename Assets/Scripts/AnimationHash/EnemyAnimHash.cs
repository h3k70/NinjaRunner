using UnityEngine;

public class EnemyAnimHash
{
    // triggers
    public static readonly int Dead = Animator.StringToHash("Dead");
    public static readonly int Attack = Animator.StringToHash("Attack");
    public static readonly int Reset = Animator.StringToHash("Reset");
}
