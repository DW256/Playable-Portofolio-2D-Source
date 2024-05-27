using UnityEngine;

public class PlayerReadingState : PlayerBaseState
{
    public PlayerReadingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.InputReader.CloseEvent += Close;
    }

    public override void Exit()
    {
        stateMachine.InputReader.CloseEvent -= Close;
    }

    public override void Tick(float deltaTime)
    {
    }
    protected void Close()
    {
        stateMachine.ContentCanvas.SetActive(false);
        data = null;
        stateMachine.SwitchState(new PlayerIdleState(stateMachine));
    }
}