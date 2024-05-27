using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;
    protected ContentData data;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        stateMachine.CurrentState = this.GetType().Name;
    }

    protected void Move(Vector2 motion, float deltaTime)
    {
        stateMachine.Rigidbody.velocity = motion;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector2.zero, deltaTime);
    }

    protected bool isGrounded()
    {
        float distance = 0.1f;
        RaycastHit2D hit = Physics2D.BoxCast(stateMachine.Collider.bounds.center, stateMachine.Collider.bounds.size - new Vector3(0.1f, 0, 0), 0f, Vector2.down, distance, stateMachine.Stats.groundMask);

        Color color = hit.collider != null ? Color.green : Color.red;
        Debug.DrawRay(stateMachine.Collider.bounds.center + new Vector3(stateMachine.Collider.bounds.extents.x - 0.05f, 0), Vector2.down * (stateMachine.Collider.bounds.extents.y + distance), color);
        Debug.DrawRay(stateMachine.Collider.bounds.center - new Vector3(stateMachine.Collider.bounds.extents.x - 0.05f, 0), Vector2.down * (stateMachine.Collider.bounds.extents.y + distance), color);
        Debug.DrawRay(stateMachine.Collider.bounds.center - new Vector3(stateMachine.Collider.bounds.extents.x - 0.05f, stateMachine.Collider.bounds.extents.y + distance), Vector2.right * ((stateMachine.Collider.bounds.extents.x - 0.05f) * 2f), color);

        //Debug.Log(hit.collider);
        return hit.collider != null;
    }

    protected bool isWall(out float wallDir)
    {
        float distance = 0.3f;
        Vector2 facing = Vector2.right * stateMachine.transform.localScale.x;
        RaycastHit2D hit = Physics2D.Raycast(stateMachine.Collider.bounds.center, facing, distance, stateMachine.Stats.climbableMask);

        Color color = hit.collider != null ? Color.green : Color.red;
        Debug.DrawRay(stateMachine.Collider.bounds.center, facing * distance, color);

        wallDir = facing.x;
        //Debug.Log(hit.collider);
        return hit.collider != null;
    }

    private bool checkNpc(out ContentData data)
    {
        float distance = 0.5f;
        Vector2 facing = Vector2.right * stateMachine.transform.localScale.x;
        RaycastHit2D hit = Physics2D.Raycast(stateMachine.Collider.bounds.center, facing, distance, stateMachine.Stats.interactableMask);
        Color color = hit.collider != null ? Color.green : Color.red;
        Debug.DrawRay((Vector2)stateMachine.Collider.bounds.center + (Vector2.up * 0.2f), (facing * distance), color);

        if (hit.collider != null)
        {
            data = hit.collider.TryGetComponent<Npc>(out Npc npc) ? npc.ContentData : null;
        }
        else
        {
            data = null;
        }

        return data != null;
    }

    protected void Interact()
    {
        if (data != null)
        {
            Debug.Log(data);
            stateMachine.ContentCanvas.GetComponent<ContentReader>().contentData = data;
            stateMachine.ContentCanvas.SetActive(true);
            stateMachine.SwitchState(new PlayerReadingState(stateMachine));
        }
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

        if (checkNpc(out data))
        {
            stateMachine.InteractPrompt.SetActive(true);
        }
        else
        {
            stateMachine.InteractPrompt.SetActive(false);
        }
    }
}