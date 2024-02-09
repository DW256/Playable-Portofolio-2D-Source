using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using static UnityEngine.RuleTile.TilingRuleOutput;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(Vector2 motion, float deltaTime)
    {
        stateMachine.Rigidbody.velocity = motion;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector2.zero, deltaTime);
    }

    public override void LateTick()
    {
        
        //2D Sprite flipping
        float x = stateMachine.InputReader.MovementValue.x;
        if (x != 0f)
        {
            Vector3 theScale = stateMachine.transform.localScale;
            theScale.x = (x < 0f) ? -1f : 1f;
            stateMachine.transform.localScale = theScale;
        }
    }
}