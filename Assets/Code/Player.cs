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

        animator.SetBool("moveRight", rigidBody.linearVelocityX > 0f);
        animator.SetBool("moveLeft", rigidBody.linearVelocityX < 0f);
        animator.SetBool("moveUp", rigidBody.linearVelocityY > 0f);
        animator.SetBool("moveDown", rigidBody.linearVelocityY < 0f);
        animator.SetBool("notMoving", (Mathf.Abs(rigidBody.linearVelocityX) + Mathf.Abs(rigidBody.linearVelocityY)) < 0.01f);
        //animator.Set("MoveY", input.y);
        //animator.SetFloat("Speed", input.sqrMagnitude);

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


        //spriteRenderer.flipX = animator.GetBool("moveLeft");
        


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
