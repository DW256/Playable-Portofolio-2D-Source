using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    float jumpTime = 0f;

    private readonly int JumpHash = Animator.StringToHash("Jump");

    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Rigidbody.gravityScale = stateMachine.Stats.jumpGravityScale;

        stateMachine.Rigidbody.AddForce(new Vector2(0, CalculateJumpforce()), ForceMode2D.Impulse);

        stateMachine.InputReader.JumpReleaseEvent += OnJumpRelease;

        stateMachine.Animator.Play(JumpHash);
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpReleaseEvent -= OnJumpRelease;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.Rigidbody.velocity.y <= 0f) stateMachine.SwitchState(new PlayerFallingState(stateMachine));

        jumpTime += Time.deltaTime;

        Move(CalculateMovement(), deltaTime);
    }

    protected void OnJumpRelease() //early drop
    {
        //Debug.Log($"Jump time : {jumpTime}");
        if (jumpTime < stateMachine.Stats.jumpButtonTime)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }
    }

    protected Vector2 CalculateMovement()
    {
        Vector2 movement = new Vector2(stateMachine.InputReader.MovementValue.x * stateMachine.Stats.speed, stateMachine.Rigidbody.velocity.y);
        //Debug.Log(movement);
        return movement;
    }

    protected float CalculateJumpforce()
    {
        return Mathf.Sqrt(stateMachine.Stats.jumpHeight * -2 * (Physics2D.gravity.y * stateMachine.Rigidbody.gravityScale));
    }
}
