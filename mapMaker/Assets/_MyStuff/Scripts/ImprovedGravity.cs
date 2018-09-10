using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class ImprovedGravity : MonoBehaviour {

    Rigidbody2D rb;

    public Transform feetPos;
    public LayerMask ground;
    public float jumpForce = 5f;
    public float fallMultiplier = 2.5f;
    public float jumpTime;
    public bool enableJumpInput;

    bool isGrounded;
    bool isJumping;
    float checkRadius;
    float jumpTimeCounter;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        checkRadius = feetPos.GetComponent<CircleCollider2D>().radius;
        Destroy(feetPos.GetComponent<CircleCollider2D>());
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, ground);

        if (enableJumpInput)
            JumpInput();

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    void JumpInput()
    {
        if(CnInputManager.GetButtonDown("JumpBtn") && isGrounded)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if(CnInputManager.GetButton("JumpBtn") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
                isJumping = false;
        }

        if (CnInputManager.GetButtonUp("JumpBtn"))
            isJumping = false;
    }
}
