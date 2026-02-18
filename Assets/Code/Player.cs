using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    public float moveSpeed = 50.0f;
    public float moveAcceleration = 15.0f;
    public GameObject bulletPrefab;

    private InputAction moveAction;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveInput;
    private Vector2 prevMoveInput;

    private InputAction shootAction;
    private Direction facing = Direction.Down;

    [Header("Sound Effects")]
    [SerializeField] private AudioLibrary.SFX PlayerMovement;
    [SerializeField] private float movementSoundCooldown = 0.4f;
    private float lastMovementSound;

    private enum Direction { Left, Right, Up, Down }

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Attack");
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastMovementSound = 0f;
        movementSoundCooldown = 0.4f;
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        if (Mathf.Abs(moveInput.x) < 0.01)
        {
            if (moveInput.y > 0.0)
            {
                facing = Direction.Up;
            }
            if (moveInput.y < 0.0)
            {
                facing = Direction.Down;
            }
        }

        if (Mathf.Abs(moveInput.y) < 0.01)
        {
            if (moveInput.x > 0.0)
            {
                facing = Direction.Right;
            }
            if (moveInput.x < 0.0)
            {
                facing = Direction.Left;
            }
        }

        if (Mathf.Abs(moveInput.x - prevMoveInput.x) > 0.4 && Mathf.Abs(moveInput.x) > 0.01)
        {
            facing = moveInput.x > 0.0 ? Direction.Right : Direction.Left;
        }
        else if (Mathf.Abs(moveInput.y - prevMoveInput.y) > 0.4 && Mathf.Abs(moveInput.y) > 0.01)
        {
            facing = moveInput.y > 0.0 ? Direction.Up : Direction.Down;
        }

        animator.SetBool("faceLeft", facing == Direction.Right);
        animator.SetBool("faceRight",  facing == Direction.Left);
        animator.SetBool("faceUp",    facing == Direction.Up);
        animator.SetBool("faceDown",  facing == Direction.Down);
        animator.SetBool("moving", rigidBody.linearVelocity.magnitude > 0.01f);
        spriteRenderer.flipX = facing == Direction.Left;

        if (shootAction.WasPerformedThisFrame()) {
            GameObject bullet = Instantiate(bulletPrefab);
            Vector2 forceDir = facing switch
            {
                Direction.Left => Vector2.left,
                Direction.Right => Vector2.right,
                Direction.Up => Vector2.up,
                _ => Vector2.down,
            };
            bullet.transform.position = transform.position;
            bullet.GetComponent<Rigidbody2D>().AddForce(50.0f * forceDir, ForceMode2D.Impulse);
        }

        prevMoveInput = moveInput;
    }

    void FixedUpdate()
    {
        rigidBody.AddForce(moveAcceleration * moveInput);
        if (Mathf.Abs(moveInput.x) < 0.01f)
        {
            rigidBody.linearVelocityX = 0.0f;
        }
        if (Mathf.Abs(moveInput.y) < 0.01f) 
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
