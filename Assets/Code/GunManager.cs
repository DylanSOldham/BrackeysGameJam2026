using UnityEngine;
using UnityEngine.InputSystem;

public class GunAnimation : MonoBehaviour
{
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private Vector3 localOffset;

    [SerializeField] private Transform player;
    [SerializeField] private Animator playerAnimator;


    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        myAnimator.SetBool("moveDown", true);
        mySpriteRenderer.flipX = false;
        mySpriteRenderer.flipY = false;
        mySpriteRenderer.sortingOrder = 1;
        transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        localOffset = new Vector3(0.241f, -0.256f, 0);
        transform.position = player.position + localOffset;
    }

    private void Update()
    {
        bool moveLeft = playerAnimator.GetBool("moveLeft");
        bool moveRight = playerAnimator.GetBool("moveRight");
        bool moveUp = playerAnimator.GetBool("moveUp");
        bool moveDown = playerAnimator.GetBool("moveDown");

        if(moveLeft || moveRight || moveUp || moveDown)
        {
            Debug.Log("left: " + moveLeft);
            Debug.Log("right: " + moveRight);
            Debug.Log("Up: " + moveUp);
            Debug.Log("Down: " + moveDown);
        }

        // Reset all first
        myAnimator.SetBool("moveLeft", false);
        myAnimator.SetBool("moveRight", false);
        myAnimator.SetBool("moveUp", false);
        myAnimator.SetBool("moveDown", false);

        if (moveLeft)
        {
            myAnimator.SetBool("moveLeft", true);
            mySpriteRenderer.flipX = true;
            mySpriteRenderer.flipY = false;
            mySpriteRenderer.sortingOrder = 1;
            transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
            localOffset = new Vector3(-0.121f, -0.255f, 0);
        }
        else if (moveRight)
        {
            myAnimator.SetBool("moveRight", true);
            mySpriteRenderer.flipX = false;
            mySpriteRenderer.flipY = false;
            mySpriteRenderer.sortingOrder = 0;
            transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
            localOffset = new Vector3(0.232f, -0.235f, 0);
        }
        else if (moveDown)
        {
            myAnimator.SetBool("moveDown", true);
            mySpriteRenderer.flipX = false;
            mySpriteRenderer.flipY = false;
            mySpriteRenderer.sortingOrder = 1;
            transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            localOffset = new Vector3(0.241f, -0.256f, 0);
        }
        else if (moveUp)
        {
            myAnimator.SetBool("moveUp", true);
            mySpriteRenderer.flipX = false;
            mySpriteRenderer.flipY = true;
            mySpriteRenderer.sortingOrder = 0;
            transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            localOffset = new Vector3(-0.289f, -0.144f, 0);
        }

        transform.position = player.position + localOffset;
    }
}
