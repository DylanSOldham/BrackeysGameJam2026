using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timeToLive = 2.0f;

    void Start()
    {
        
    }

    void Update()
    {
        timeToLive -= Time.deltaTime;
        if (timeToLive < 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            return;
        }
        else if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(5);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("SnakeBoss"))
        {
            collision.gameObject.SendMessage("TakeDamage", 5);
            Destroy(gameObject);
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
