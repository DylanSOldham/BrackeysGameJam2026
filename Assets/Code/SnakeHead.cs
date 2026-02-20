using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public Snake snake;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TakeDamage(float amount) // Do not remove, this is called via message
    {
        snake.TakeDamage(amount);
    }
}
