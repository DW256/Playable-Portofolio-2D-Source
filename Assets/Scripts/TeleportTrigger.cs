using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    public Transform target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.position = target.position;
    }
}