using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue { get; private set; }
    public event Action JumpPressEvent;
    public event Action JumpReleaseEvent;
    public event Action InteractEvent;
    public event Action CloseEvent;

    private Controls controls;

    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnClose(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        CloseEvent?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        InteractEvent?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //if (!context.performed) return;

        if (context.started) //Jump key press
        {
            JumpPressEvent?.Invoke();
        }

        if (context.canceled) //Jump key release
        {
            JumpReleaseEvent?.Invoke();
        }

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
        //Debug.Log(MovementValue);
    }
}
