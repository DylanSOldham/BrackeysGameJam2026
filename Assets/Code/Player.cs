using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 50.0f;
    public float moveAcceleration = 15.0f;

    InputAction moveAction;
    Rigidbody2D rigidBody;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.linearDamping = 0.0f;
    }

    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        rigidBody.AddForce(moveAcceleration * input);
        rigidBody.linearVelocity = Vector2.ClampMagnitude(rigidBody.linearVelocity, moveSpeed);
    }
}
