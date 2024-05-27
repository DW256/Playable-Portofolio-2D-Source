using UnityEngine;

public class PlayerWallJumpState : PlayerJumpState
{
    float forcedDir;
    float lockInputTime;
    private readonly int JumpHash = Animator.StringToHash("Jump");

    public PlayerWallJumpState(PlayerStateMachine stateMachine, float wallDir) : base(stateMachine)
    {
        this.forcedDir = wallDir * -1 * stateMachine.Stats.wallJumpAmplifier;
    }

    public override void Enter()
    {
        stateMachine.Rigidbody.gravityScale = stateMachine.Stats.jumpGravityScale;

        stateMachine.Rigidbody.AddForce(new Vector2(forcedDir, CalculateJumpforce()), ForceMode2D.Impulse);

        //stateMachine.InputReader.JumpReleaseEvent += OnJumpRelease;

        stateMachine.Animator.Play(JumpHash);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.Rigidbody.velocity.y <= 0f) stateMachine.SwitchState(new PlayerFallingState(stateMachine));

        lockInputTime += Time.deltaTime;

        if (lockInputTime > stateMachine.Stats.wallJumpLockInputTime)
        {
            Move(CalculateMovement(), deltaTime);
        }
    }
}