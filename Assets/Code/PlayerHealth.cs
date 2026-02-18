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

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxHealth = 4;
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        if(currentHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void takeDamage(float damage)
    {
        currentHealth -= damage;

        PlaySound(PlayerHit);

        StopAllCoroutines(); // prevents stacking flashes
        StartCoroutine(DamageFlash());
    }

    private IEnumerator DamageFlash()
    {
        spriteRenderer.color = Color.green;

        yield return new WaitForSeconds(1f);

        spriteRenderer.color = Color.red;
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
