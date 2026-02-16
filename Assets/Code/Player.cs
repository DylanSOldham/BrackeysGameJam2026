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
    }

    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>().normalized;
        if (input.magnitude < 0.01) {
            rigidBody.linearVelocity = Vector2.zero;
        } 
        else {
            rigidBody.AddForce(moveAcceleration * input);
            rigidBody.linearVelocity = Vector2.ClampMagnitude(rigidBody.linearVelocity, moveSpeed);
        }
    }
}
