using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    private bool canDoubleJump = true;
    private readonly int FallHash = Animator.StringToHash("Fall");

    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        this.canDoubleJump = true;
    }

    public PlayerFallingState(PlayerStateMachine stateMachine, bool canDoubleJump) : base(stateMachine)
    {
        this.canDoubleJump = canDoubleJump;
    }

    public override void Enter()
    {
        stateMachine.Rigidbody.gravityScale = stateMachine.Stats.fallingGravityScale;

        stateMachine.InputReader.JumpPressEvent += OnJump;

        stateMachine.Animator.Play(FallHash);
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpPressEvent -= OnJump;
    }

    public override void Tick(float deltaTime)
    {
        Move(CalculateMovement(), deltaTime);

        //Front is wall, not grounded, direction inputted
        if (isWall(out float wallDir) && !isGrounded() && stateMachine.InputReader.MovementValue.x != 0f)
        {
            stateMachine.SwitchState(new PlayerWallSlide(stateMachine, wallDir));
        }
        //Ground detect
        if (isGrounded())
        {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }
    }

    private Vector2 CalculateMovement()
    {
        Vector2 movement = new Vector2(stateMachine.InputReader.MovementValue.x * stateMachine.Stats.speed, stateMachine.Rigidbody.velocity.y);
        //Debug.Log(movement);
        return movement;
    }

    private void OnJump()
    {
        if (canDoubleJump)
        {
            stateMachine.SwitchState(new PlayerDoubleJumpState(stateMachine));
        }
    }
}
