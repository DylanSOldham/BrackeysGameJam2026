using UnityEngine;

public class SnakeTailCollider : MonoBehaviour
{
    void TakeDamage(float amount) // Do not remove, this is called via message
    {
        GameObject tailObject = transform.parent.transform.parent.gameObject;
        SnakeTail tail = tailObject.GetComponent<SnakeTail>();
        tail.TakeDamage(amount / 2.0f);
    }
}
