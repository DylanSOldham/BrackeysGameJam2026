using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public GameObject headObj;
    public GameObject tailObj;

    private SnakeHead head;
    private SnakeTail tail;
    private SpriteRenderer headSpriteRenderer;
    private SpriteRenderer tailSpriteRenderer;

    private BossPosition headPos = BossPosition.Back;
    public  BossPosition tailPos = BossPosition.Left;

    private BossState state = BossState.BasicPattern;
    private Dictionary<int, Vector3> positions = new();

    static private float tailCooldown = 2.0f;

    private float headMoveTimer = 3.0f;
    private float tailAttackTimer = tailCooldown;

    private float maxHealth = 150.0f;
    private float currentHealth = 20.0f;
    public SnakeHealthUI healthUI;

    [Header("Enemy Hit SFX")]
    [SerializeField] private AudioLibrary.SFX snakeHit;

    public enum BossPosition
    {
        Back = 0,
        Left = 1,
        Right = 2,
        Away = 3,
    }

    enum BossState
    {
        Intro,
        BasicPattern,
        TornadoDance,
    }

    void Start()
    {
        head = headObj.GetComponent<SnakeHead>();
        headSpriteRenderer = headObj.GetComponent<SpriteRenderer>();
        tail = tailObj.GetComponent<SnakeTail>();
        tailSpriteRenderer = tailObj.GetComponent<SpriteRenderer>();

        positions[(int) BossPosition.Back] = new Vector2(0.0f, 2.5f);
        positions[(int) BossPosition.Left] = new Vector2(-4.0f, -2.0f);
        positions[(int) BossPosition.Right] = new Vector2(4.0f, -2.0f);
        positions[(int) BossPosition.Away] = new Vector2(1e5f, 0.0f);

        headObj.transform.localPosition = positions[(int)headPos];
        tailObj.transform.localPosition = positions[(int)tailPos];
    }

    void Update()
    {
        switch (state)
        {
            case BossState.Intro:
                break;
            case BossState.BasicPattern:
                BasicPattern();
                break;
            case BossState.TornadoDance:
                break;
        }
    }

    void BasicPattern()
    {
        headMoveTimer -= Time.deltaTime;
        if (headMoveTimer < 0)
        {
            if (headPos == BossPosition.Away)
            {
                do
                {
                    headPos = (BossPosition)Mathf.FloorToInt(Random.Range(0.0f, 3.0f));
                    headObj.transform.localPosition = positions[(int)headPos];
                } while (headPos == tailPos);
            }
            else
            {
                headPos = BossPosition.Away;
                headObj.transform.localPosition = positions[(int)headPos];
            }
            headSpriteRenderer.flipX = headPos == BossPosition.Right;
            headMoveTimer = 3.0f;
        }

        if (tail.phase == SnakeTail.AttackPhase.Away && tailPos != BossPosition.Away)
        {
            tailPos = BossPosition.Away;
            tailObj.transform.localPosition = positions[(int)tailPos];
            tailAttackTimer = 2.0f;
        } 
        tailAttackTimer -= Time.deltaTime;
        if (tailAttackTimer < 0)
        {
            if (tail.phase == SnakeTail.AttackPhase.Away)
            {
                do
                {
                    tailPos = (BossPosition)Mathf.FloorToInt(Random.Range(0.0f, 3.0f));
                    tailObj.transform.localPosition = positions[(int)tailPos];
                } while (tailPos == headPos);
                tail.DoAttack();

                tailSpriteRenderer.flipX = tailPos == BossPosition.Right;
                tailAttackTimer = 3.0f;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (AudioManager.Instance != null)
        {
            AudioClip clip = AudioManager.Instance.audioLibrary.GetSFX(snakeHit);

            if (clip != null)
            {
                AudioManager.Instance.PlaySFX(clip);
            }
        }
        if (healthUI != null) 
        {
            float percentHp = currentHealth / maxHealth;
            healthUI.setSlider(percentHp);
        }

        Debug.Log(currentHealth);
        if (currentHealth <= 0.0f)
        {
            if(healthUI != null)
            {
                healthUI.setTransition();
            }
            Destroy(gameObject);

        }
    }
}
