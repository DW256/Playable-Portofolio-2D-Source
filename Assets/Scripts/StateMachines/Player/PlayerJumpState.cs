using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{

    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.JumpPressEvent += OnJumpPress;
        stateMachine.InputReader.JumpReleaseEvent += OnJumpRelease;
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpPressEvent -= OnJumpPress;
        stateMachine.InputReader.JumpReleaseEvent -= OnJumpRelease;
    }

    public override void Tick(float deltaTime)
    {


        Move(CalculateMovement(), deltaTime);
    }

    private void OnJumpRelease() //early drop
    {
    
    }

    private void OnJumpPress() //double jump
    {
    
    }

    private Vector2 CalculateMovement()
    {
        Vector2 movement = new Vector2(stateMachine.InputReader.MovementValue.x * stateMachine.Stats.speed, stateMachine.Stats.jumpSpeed);
        //Debug.Log(movement);
        return movement;
    }

}
