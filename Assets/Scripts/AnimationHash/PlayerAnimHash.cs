using UnityEngine;

public static class PlayerAnimHash
{
    // triggers
    public static readonly int JumpFlip = Animator.StringToHash("JumpFlip");
    public static readonly int JumpStan = Animator.StringToHash("JumpStan");
    public static readonly int Slash = Animator.StringToHash("Slash");
    public static readonly int LongJump = Animator.StringToHash("LongJump");
    public static readonly int ShortJump = Animator.StringToHash("ShortJump");
    public static readonly int Die = Animator.StringToHash("Die");
    public static readonly int ShurikenJump = Animator.StringToHash("Shuriken");
    public static readonly int ShurikenEnd = Animator.StringToHash("ShurikenEnd");
    public static readonly int StartRun = Animator.StringToHash("StartRun");
    public static readonly int Revive = Animator.StringToHash("Revive");

    //float
    public static readonly int JumpSpeedAnim = Animator.StringToHash("JumpSpeedAnim");
    public static readonly int RunSpeedAnim = Animator.StringToHash("RunSpeedAnim");
}
