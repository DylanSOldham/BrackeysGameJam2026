using UnityEngine;

public class FrogAttackManager : MonoBehaviour
{
    public float damage;
    public float lifetime = 0.75f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();

            if (player != null)
            {
                if(player.currentHealth > 0)
                {
                    player.takeDamage(damage);
                }
            }

            Destroy(gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {

        }
        else if (collision.CompareTag("Heart"))
        {

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
