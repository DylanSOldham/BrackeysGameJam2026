using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    public Snake snake;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void TakeDamage(float amount)
    {
        snake.TakeDamage(amount / 2.0f);
    }
}
