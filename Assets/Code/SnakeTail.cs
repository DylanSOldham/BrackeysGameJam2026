using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    public Snake snake;
    private Animator animator;

    private float animationTimer = 0.0f;
    private Vector2 basePos;

    public AttackPhase phase = AttackPhase.Away;

    private const float SWEEP_ANIM_LEN = 0.66666f;
    private const float ENTER_ANIM_LEN = 0.5f;

    public enum AttackPhase
    {
        Away = 0,
        Intro = 1,
        Sweep = 2,
        Idle  = 3,
        Recede = 4,
    }

    void Start()
    {
        basePos = transform.position;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animationTimer += Time.deltaTime;

        switch (phase)
        {
            case AttackPhase.Intro:
                if (animationTimer >= ENTER_ANIM_LEN)
                {
                    transform.position = basePos + new Vector2(0.0f, -1.5f);
                    phase = AttackPhase.Sweep;
                    animationTimer = 0.0f;
                }
                break;
            case AttackPhase.Sweep:
                if (animationTimer >= SWEEP_ANIM_LEN)
                {
                    transform.position = basePos;
                    phase = AttackPhase.Idle;
                    animationTimer = 0.0f;
                }
                break;
            case AttackPhase.Idle:
                if (animationTimer >= 2.0f)
                {
                    phase = AttackPhase.Recede;
                    animationTimer = 0.0f;
                }
                break;
            case AttackPhase.Recede:
                if (animationTimer >= ENTER_ANIM_LEN)
                {
                    phase = AttackPhase.Away;
                    animationTimer = 0.0f;
                }
                break;
            case AttackPhase.Away:
                break;
        }

        animator.SetInteger("Phase", (int)phase);
    }

    void TakeDamage(float amount) // Do not remove, this is called via message
    {
        snake.TakeDamage(amount / 2.0f);
    }

    public void DoAttack()
    {
        animationTimer = 0.0f;
        basePos = transform.position;
        phase = AttackPhase.Intro;
        animator.SetInteger("Phase", (int)phase);
    }
}
