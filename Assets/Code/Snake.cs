using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public GameObject head;
    public GameObject tail;


    private BossPosition headPos = BossPosition.Back;
    private BossPosition tailPos = BossPosition.Away;

    private BossState state = BossState.BasicPattern;
    private Dictionary<int, Vector3> positions = new();

    private float headMoveTimer = 3.0f;

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
        positions[(int) BossPosition.Back] = new Vector2(0.0f, 2.5f);
        positions[(int) BossPosition.Left] = new Vector2(-4.0f, -2.0f);
        positions[(int) BossPosition.Right] = new Vector2(4.0f, -2.0f);
        positions[(int) BossPosition.Away] = new Vector2(1e5f, 0.0f);
    }

    void Update()
    {
        head.transform.localPosition = positions[(int)headPos];
        tail.transform.localPosition = positions[(int)tailPos];

        headMoveTimer -= Time.deltaTime;
        if (headMoveTimer < 0)
        {
            headPos = (BossPosition) Mathf.FloorToInt(Random.Range(0.0f, 3.0f));
            headMoveTimer = 3.0f;
        }

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

    }
}
