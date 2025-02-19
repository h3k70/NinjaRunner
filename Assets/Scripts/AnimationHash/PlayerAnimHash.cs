using UnityEngine;

public static class PlayerAnimHash
{
    // triggers
    public static readonly int JumpFlip = Animator.StringToHash("JumpFlip");
    public static readonly int JumpStan = Animator.StringToHash("JumpStan");
    public static readonly int Slash = Animator.StringToHash("Slash");
    public static readonly int LongJump = Animator.StringToHash("LongJump");

    //float
    public static readonly int JumpSpeedAnim = Animator.StringToHash("JumpSpeedAnim");
    public static readonly int RunSpeedAnim = Animator.StringToHash("RunSpeedAnim");
}
