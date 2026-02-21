using UnityEngine;

public class SnakeTailCollider : MonoBehaviour
{
    void TakeDamage(float amount) // Do not remove, this is called via message
    {
        GameObject tailObject = transform.parent.transform.parent.gameObject;
        SnakeTail tail = tailObject.GetComponent<SnakeTail>();
        tail.TakeDamage(amount / 2.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("BAH");
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("HIT THE PLAYER");
            PlayerHealth player = collision.collider.GetComponent<PlayerHealth>();
            player.takeDamage(1.5f);
            return;
        }
    }
}
