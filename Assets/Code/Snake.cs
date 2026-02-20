using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public GameObject headObj;
    public GameObject tailObj;

    private SnakeHead head;
    private SnakeTail tail;

    private BossPosition headPos = BossPosition.Back;
    private BossPosition tailPos = BossPosition.Away;

    private BossState state = BossState.BasicPattern;
    private Dictionary<int, Vector3> positions = new();

    private float headMoveTimer = 3.0f;
    private float tailAttackTimer = 2.0f;

    private float health = 20.0f;

    [Header("Enemy Hit SFX")]
    [SerializeField] private AudioLibrary.SFX snakeHit;

    enum BossPosition
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
        tail = tailObj.GetComponent<SnakeTail>();

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
            headMoveTimer = 3.0f;
        }

        tailAttackTimer -= Time.deltaTime;
        if (tailAttackTimer < 0)
        {
            if (tailPos == BossPosition.Away)
            {
                do
                {
                    tailPos =  (BossPosition)Mathf.FloorToInt(Random.Range(0.0f, 3.0f));
                    tailObj.transform.localPosition = positions[(int)tailPos];
                } while (tailPos == headPos);
                tail.DoAttack();
            } else
            {
                tailPos = BossPosition.Away;
                tailObj.transform.localPosition = positions[(int)tailPos];
            }
            tailAttackTimer = 2.0f;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (AudioManager.Instance != null)
        {
            AudioClip clip = AudioManager.Instance.audioLibrary.GetSFX(snakeHit);

            if (clip != null)
            {
                AudioManager.Instance.PlaySFX(clip);
            }
        }

        //updateHealthUI();

        Debug.Log(health);
        if (health <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
