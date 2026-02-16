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

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        input = moveAction.ReadValue<Vector2>().normalized;

        animator.SetBool("moveRight", input.x > 0f);
        animator.SetBool("moveLeft", input.x < 0f);
        animator.SetBool("isIdle", (Mathf.Abs(input.x) + Mathf.Abs(input.y)) < 0.01f);
        //animator.Set("MoveY", input.y);
        //animator.SetFloat("Speed", input.sqrMagnitude);
    }

    void FixedUpdate()
    {
        if (input.magnitude < 0.01f)
        {
            rigidBody.linearVelocity = Vector2.zero;
        }
        else
        {
            rigidBody.AddForce(moveAcceleration * input);
            rigidBody.linearVelocity = Vector2.ClampMagnitude(rigidBody.linearVelocity, moveSpeed);
        }

        
        spriteRenderer.flipX = animator.GetBool("moveLeft");
        

        //if(rigidBody.linearVelocity == Vector2.zero)
        //{
        //    animator.SetBool("isIdle", true);
        //    animator.SetBool("isRight", false);
        //}
        //else
        //{
        //    animator.SetBool("isIdle", false);
        //    animator.SetBool("isRight", true);
        //}
    }
}
