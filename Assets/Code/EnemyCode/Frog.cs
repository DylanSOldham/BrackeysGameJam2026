using UnityEditor.UI;
using UnityEngine;

public class Frog : Enemy
{
    public float attackDistance = 2f;
    public float attackCooldown = 2f;
    public float timeSinceLastAttack = 2f;
    public GameObject attackObject;
    public float projectileSpeed = 10f;

    protected override void HandleBehavior()
    {
        Vector2 difference = player.position - transform.position;

        float distance = difference.magnitude;

        if ((distance <= attackDistance) && (timeSinceLastAttack >= attackCooldown))
        {
            attack();
        }
        else
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if (distance > attackDistance)
        {
            Vector2 direction = Vector2.zero;

            if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
            {
                // Move horizontally only
                direction = new Vector2(Mathf.Sign(difference.x), 0);
            }
            else
            {
                // Move vertically only
                direction = new Vector2(0, Mathf.Sign(difference.y));
            }

            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        }

    }

    public void attack()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        GameObject projectile = Instantiate(
            attackObject,
            transform.position,
            rotation
        );

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * projectileSpeed;
    }
}
