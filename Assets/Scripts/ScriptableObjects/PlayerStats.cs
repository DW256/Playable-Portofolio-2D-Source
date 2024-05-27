using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    public float speed;
    
    //jump-thingy
    public float jumpHeight;
    public float jumpButtonTime;
    public float wallJumpAmplifier;
    public float wallJumpLockInputTime;
    public float coyoteTime;

    //gravity-thingy
    public float defaultGravityScale;
    public float jumpGravityScale;
    public float fallingGravityScale;
    public float wallSlideGravityScale;

    //Layer Thingy
    public LayerMask climbableMask;
    public LayerMask groundMask;
    public LayerMask interactableMask;
}
