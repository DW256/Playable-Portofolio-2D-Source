using Unity;
using UnityEngine;

public class PlayerDoubleJumpState : PlayerJumpState
{
    private readonly int JumpHash = Animator.StringToHash("Double Jump");
    public PlayerDoubleJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Rigidbody.gravityScale = stateMachine.Stats.jumpGravityScale;

        stateMachine.Rigidbody.velocity = Vector3.zero; //reset jump force
        stateMachine.Rigidbody.AddForce(new Vector2(0, CalculateJumpforce()), ForceMode2D.Impulse);
        //Debug.Log(CalculateJumpforce());
        //stateMachine.InputReader.JumpReleaseEvent += OnJumpRelease;

        stateMachine.Animator.Play(JumpHash);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.Rigidbody.velocity.y <= 0f) stateMachine.SwitchState(new PlayerFallingState(stateMachine, false));

        Move(CalculateMovement(), deltaTime);
    }

    public override void Exit()
    {
    }
}