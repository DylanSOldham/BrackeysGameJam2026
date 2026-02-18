using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    public float moveSpeed = 50.0f;
    public float moveAcceleration = 15.0f;

    private InputAction moveAction;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 input;

    [Header("Sound Effects")]
    [SerializeField] private AudioLibrary.SFX PlayerMovement;
    [SerializeField] private float movementSoundCooldown = 0.4f;
    private float lastMovementSound;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastMovementSound = 0f;
        movementSoundCooldown = 0.4f;
    }

    void Update()
    {
        input = moveAction.ReadValue<Vector2>().normalized;

        animator.SetBool("moveRight", rigidBody.linearVelocityX > 0f);
        animator.SetBool("moveLeft", rigidBody.linearVelocityX < 0f);
        animator.SetBool("moveUp", rigidBody.linearVelocityY > 0f);
        animator.SetBool("moveDown", rigidBody.linearVelocityY < 0f);
        animator.SetBool("notMoving", (Mathf.Abs(rigidBody.linearVelocityX) + Mathf.Abs(rigidBody.linearVelocityY)) < 0.01f);

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("moveLeft") || stateInfo.IsName("idleLeft") || animator.GetBool("moveLeft"))
        {
            spriteRenderer.flipX = true;
        }
        else if (stateInfo.IsName("notMoving"))
        {

        } 
        else
        {
            spriteRenderer.flipX = false;
        }

    }

    void FixedUpdate()
    {
        rigidBody.AddForce(moveAcceleration * input);
        if (Mathf.Abs(input.x) < 0.01f)
        {
            rigidBody.linearVelocityX = 0.0f;
        }
        if (Mathf.Abs(input.y) < 0.01f) 
        {
            rigidBody.linearVelocityY = 0.0f;
        }
        rigidBody.linearVelocity = Vector2.ClampMagnitude(rigidBody.linearVelocity, moveSpeed);

        if((Mathf.Abs(rigidBody.linearVelocityX) + Mathf.Abs(rigidBody.linearVelocityY)) > 0.01f)
        {
            TryPlayMovement();
        }
    }

    private void TryPlayMovement()
    {
        if (Time.unscaledTime - lastMovementSound < movementSoundCooldown)
            return;

        lastMovementSound = Time.unscaledTime;
        PlaySound(PlayerMovement);
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
