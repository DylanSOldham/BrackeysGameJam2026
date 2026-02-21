using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    public Snake snake;
    private Animator animator;

    private float animationTimer = 0.0f;
    private Vector2 basePos;

    private int currentFrame = 0;
    public AttackPhase phase = AttackPhase.Away;

    private const float SWEEP_ANIM_LEN = 0.66666f;
    private const int SWEEP_ANIM_FRAMES = 8;
    private const float ENTER_ANIM_LEN = 0.5f;
    private const int ENTER_ANIM_FRAMES = 6;

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
                if (animationTimer > SWEEP_ANIM_LEN)
                {
                    phase = AttackPhase.Sweep;
                    currentFrame = 0;
                    animationTimer = 0.0f;
                }
                break;
            case AttackPhase.Sweep:
                if (animationTimer > SWEEP_ANIM_LEN / SWEEP_ANIM_FRAMES)
                {
                    currentFrame += 1;
                    animationTimer = 0.0f;

                    float[] yPoses = {
                        0.0f, 0.0f, 0.5f, 1.5f,
                        1.5f, 1.5f, 1.0f, -0.5f
                    };

                    float[] xPoses = {
                        0.0f, -1.0f, -1.5f, -1.5f,
                        0.0f, 0.5f, 1.0f, 1.0f
                    };

                    float newX = basePos.x - xPoses[currentFrame % SWEEP_ANIM_FRAMES];
                    float newY = basePos.y - yPoses[currentFrame % SWEEP_ANIM_FRAMES];
                    transform.position = new Vector2(newX, newY);
                }

                if (currentFrame == SWEEP_ANIM_FRAMES - 1)
                {
                    transform.position = basePos;
                    phase = AttackPhase.Idle;
                    currentFrame = 0;
                    animationTimer = 0.0f;
                }
                break;
            case AttackPhase.Idle:
                if (animationTimer > 2.0f)
                {
                    phase = AttackPhase.Recede;
                    animationTimer = 0.0f;
                }
                break;
            case AttackPhase.Recede:
                if (animationTimer > ENTER_ANIM_LEN)
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
