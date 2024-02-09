using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{

    private readonly int IdleHash = Animator.StringToHash("Idle");

    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.JumpPressEvent += OnJumpPress;


        stateMachine.Animator.Play(IdleHash);
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpPressEvent -= OnJumpPress;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue!= Vector2.zero){
            stateMachine.SwitchState(new PlayerRunState(stateMachine));
        }
        Move(deltaTime);

    }

    private void OnJumpPress()
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }
}
