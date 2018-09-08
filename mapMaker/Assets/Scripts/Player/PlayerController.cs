using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Animator animator;
    public Transform feetPos;
    public Transform characterTransform;
    public float moveSpeed = 10f;
    public float jumpForce = 5f;
    public float fallMultiplier = 2.5f;
    public float jumpTimeCounter;
    public float jumpTime;
    public LayerMask ground;

    Rigidbody2D rb;

    bool rightArrow;
    bool leftArrow;
    bool blocking;
    bool jumpBtn;

    bool isGrounded;
    bool isJumping;
    float checkRadius;

    Vector3 rightScale;
    Vector3 leftScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rightScale = characterTransform.localScale;
        leftScale = new Vector3(-characterTransform.localScale.x, characterTransform.localScale.y, characterTransform.localScale.z);
        checkRadius = feetPos.GetComponent<CircleCollider2D>().radius;
        Destroy(feetPos.GetComponent<CircleCollider2D>());
    }

    void FixedUpdate()
    {

        if (rightArrow)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            characterTransform.localScale = rightScale;
            animator.SetBool("walking", true);
        }
        if (leftArrow)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            characterTransform.localScale = leftScale;
            animator.SetBool("walking", true);
        }

        if(rightArrow && leftArrow || !rightArrow && !leftArrow || blocking)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetBool("walking", false);
        }


            
    }

    void Update()
    {
        rightArrow = Input.GetKey(KeyCode.RightArrow);
        leftArrow = Input.GetKey(KeyCode.LeftArrow);
        blocking = Input.GetKey(KeyCode.Z);
        jumpBtn = Input.GetKeyDown(KeyCode.Space);
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, ground);

        if(Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("attack");
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if(Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
                isJumping = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
            isJumping = false;

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        animator.SetBool("blocking", blocking);

    }
}
