using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public string CurrentState { get; set; }
    [field: SerializeField] public LayerMask LayerMask { get; private set; }
    [field: SerializeField] public PlayerStats Stats {  get; private set; }
    [field: SerializeField] public InputReader InputReader {  get; private set; }
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
    [field: SerializeField] public Collider2D Collider { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public GameObject InteractPrompt { get; private set; }
    [field: SerializeField] public GameObject ContentCanvas { get; private set; }

    private void Start()
    {
        ForceIdleState();
    }

    public void ForceIdleState()
    {
        SwitchState(new PlayerIdleState(this));
    }
}
