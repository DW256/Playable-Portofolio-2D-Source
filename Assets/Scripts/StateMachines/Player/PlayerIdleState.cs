using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    float coyoteTimer = 0f;
    private readonly int IdleHash = Animator.StringToHash("Idle");

    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.InteractEvent += Interact;
        stateMachine.InputReader.JumpPressEvent += OnJumpPress;
        stateMachine.Rigidbody.gravityScale = stateMachine.Stats.defaultGravityScale;

        stateMachine.Animator.Play(IdleHash);
    }

    public override void Exit()
    {

        stateMachine.InputReader.InteractEvent -= Interact;
        stateMachine.InputReader.JumpPressEvent -= OnJumpPress;
    }

    public override void Tick(float deltaTime)
    {
        coyoteTimer += Time.deltaTime;
        if (stateMachine.InputReader.MovementValue != Vector2.zero)
        {
            stateMachine.SwitchState(new PlayerRunState(stateMachine));
        }
        if (!isGrounded() && coyoteTimer >= stateMachine.Stats.coyoteTime)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }
        Move(deltaTime);
    }

    private void OnJumpPress()
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }
}
