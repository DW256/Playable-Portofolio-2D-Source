using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{

    private readonly int RunHash = Animator.StringToHash("Run");

    public PlayerRunState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.JumpPressEvent += OnJumpPress;


        stateMachine.Animator.Play(RunHash);
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpPressEvent -= OnJumpPress;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }
        Move(CalculateMovement(), deltaTime);
    }

    private void OnJumpPress()
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }

    private Vector2 CalculateMovement()
    {
        Vector2 movement = new Vector2(stateMachine.InputReader.MovementValue.x * stateMachine.Stats.speed, 0);
        //Debug.Log(movement);
        return movement;
    }
}
