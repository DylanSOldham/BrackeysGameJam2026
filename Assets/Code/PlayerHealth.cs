using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    private SpriteRenderer spriteRenderer;

    [Header("Sound Effects")]
    [SerializeField] private AudioLibrary.SFX PlayerHit;
    [SerializeField] private AudioLibrary.SFX PlayerRegainHP;

    public HeartUI heartUI;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxHealth = 9f;
        currentHealth = 3f;
        heartUI.setHearts(this);
    }

    private void FixedUpdate()
    {
        if(currentHealth <= 0f)
        {
            //Destroy(gameObject);
            //Game Over set active
        }
    }

    public void gainHealth(float health)
    {
        currentHealth += health;

        heartUI.setHearts(this);

        PlaySound(PlayerRegainHP);
    }

    public void takeDamage(float damage)
    {
        currentHealth -= damage;

        heartUI.setHearts(this);

        PlaySound(PlayerHit);

        StopAllCoroutines(); // prevents stacking flashes
        StartCoroutine(DamageFlash());
    }

    private IEnumerator DamageFlash()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = Color.pink;
    }

    private void PlaySound(AudioLibrary.SFX sfx)
    {
        if (AudioManager.Instance == null) return;

        AudioClip clip = AudioManager.Instance.audioLibrary.GetSFX(sfx);

        if (clip != null)
        {
            AudioManager.Instance.PlaySFX(clip);
        }
    }

}
