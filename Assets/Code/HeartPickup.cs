using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public float health = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("HITTING THE PLAYER");
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.gainHealth(health);
            }

            Destroy(gameObject);
        }
    }
}
