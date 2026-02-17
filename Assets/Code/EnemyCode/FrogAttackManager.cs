using UnityEngine;

public class FrogAttackManager : MonoBehaviour
{
    public float damage = 1f;
    public float lifetime = 0.75f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("HITTING THE PLAYER");
            PlayerHealth player = collision.GetComponent<PlayerHealth>();

            if (player != null)
            {
                player.takeDamage(damage);
            }

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("NOT HITTING THE PLAYER");
            Destroy(gameObject);
        }
    }
}
