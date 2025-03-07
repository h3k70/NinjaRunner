using UnityEngine;

public class LightningSlash : Skill
{
    [SerializeField] private LayerMask _layer;

    public override void Activate()
    {
        if (IsReady == false)
            return;

        //Physics.

        StartCooldown();
    }
}
