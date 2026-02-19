using System.Collections;
using UnityEngine;
using static AudioLibrary;

public abstract class Enemy : MonoBehaviour
{
    [Header("Base Stats")]
    public float maxHealth = 10f;
    public float moveSpeed = 3f;
    public float damage = 0.5f;

    [Header("Enemy Hit SFX")]
    [SerializeField] private AudioLibrary.SFX EnemyHit;

    protected float currentHealth;
    protected Transform player;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public GameObject regainHeart;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        HandleBehavior();
    }

    protected abstract void HandleBehavior();

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;

        StopAllCoroutines(); // prevents stacking flashes
        StartCoroutine(DamageFlash());

        if (currentHealth <= 0)
        {
            SpawnHeart();
            Die();
        }
    }

    private void SpawnHeart()
    {
        if(regainHeart != null)
        {
            GameObject projectile = Instantiate(
            regainHeart,
            transform.position,
            transform.rotation
        );
        }
    }

    private IEnumerator DamageFlash()
    {
        if (AudioManager.Instance != null)
        {
            AudioClip clip = AudioManager.Instance.audioLibrary.GetSFX(EnemyHit);

            if (clip != null)
            {
                AudioManager.Instance.PlaySFX(clip);
            }
        }

        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        spriteRenderer.color = Color.white;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
