using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlide : PlayerBaseState
{
    private readonly int WallHash = Animator.StringToHash("Wall Jump");
    private float wallDir;
    public PlayerWallSlide(PlayerStateMachine stateMachine, float inputValueX) : base(stateMachine)
    {
        this.wallDir = inputValueX;
    }

    public override void Enter()
    {
        stateMachine.Rigidbody.velocity = Vector3.zero;//stop character movement
        stateMachine.Rigidbody.gravityScale = stateMachine.Stats.wallSlideGravityScale;

        stateMachine.InputReader.JumpPressEvent += OnJump;

        stateMachine.Animator.Play(WallHash);
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpPressEvent -= OnJump;
    }

    public override void Tick(float deltaTime)
    {
        //Debug.Log(wallDir);
        //Exit wall-ing
        if (wallDir != stateMachine.InputReader.MovementValue.x)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }
        //End wall-ing
        if (isGrounded() || !isWall(out wallDir))
        {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }

    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerWallJumpState(stateMachine,wallDir));
    }

}
