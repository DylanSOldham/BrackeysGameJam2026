using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    public Snake snake;
    private Animator animator;

    private float animationTimer = 0.0f;
    private Vector2 basePos;

    private int currentFrame = 0;
    private AttackPhase phase = AttackPhase.Idle;

    private const float SWEEP_ANIM_LEN = 0.66666f;
    private const int SWEEP_ANIM_FRAMES = 8;

    enum AttackPhase
    {
        Intro = 0,
        Sweep = 1,
        Idle  = 2,
    }

    void Start()
    {
        basePos = transform.position;

        animator = GetComponent<Animator>();
        animator.SetBool("Attacking", true);
    }

    void Update()
    {
        if (phase == AttackPhase.Intro)
        {
            animationTimer = 0.0f;
            basePos = transform.position;
            phase = AttackPhase.Sweep;
            animator.SetBool("Attacking", true);
        }
        if (phase == AttackPhase.Sweep)
        {
            animationTimer += Time.deltaTime;
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
                currentFrame = 0;
                transform.position = basePos;
                animator.SetBool("Attacking", false);
                phase = AttackPhase.Idle;
            }
        }
    }

    void TakeDamage(float amount) // Do not remove, this is called via message
    {
        snake.TakeDamage(amount / 2.0f);
    }

    public void DoAttack()
    {
        phase = AttackPhase.Intro;
    }
}
