using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class PlayerController : MonoBehaviour {

    public Animator animator;
    public Transform characterTransform;
    public float moveSpeed = 10f;

    bool rightArrow;
    bool leftArrow;
    bool blocking;

    Rigidbody2D rb;
    Vector3 rightScale;
    Vector3 leftScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rightScale = characterTransform.localScale;
        leftScale = new Vector3(-characterTransform.localScale.x, characterTransform.localScale.y, characterTransform.localScale.z);
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
/*        rightArrow = Input.GetKey(KeyCode.RightArrow);
        leftArrow = Input.GetKey(KeyCode.LeftArrow);
        blocking = Input.GetKey(KeyCode.Z);*/

        rightArrow = CnInputManager.GetButton("RightArrow");
        leftArrow = CnInputManager.GetButton("LeftArrow");
        blocking = CnInputManager.GetButton("BlockBtn");


        if(CnInputManager.GetButtonDown("AttackBtn"))
        {
            animator.SetTrigger("attack");
        }

        animator.SetBool("blocking", blocking);

    }
}
