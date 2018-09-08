using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDumb : MonoBehaviour {

    public Animator animator;
    public Transform characterTransform;
    public float moveSpeed = 10f;
    public LayerMask hitSwitchDirection;

    Vector3 rightScale;
    Vector3 leftScale;
    Rigidbody2D rb;

    int moveDir = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rightScale = characterTransform.localScale;
        leftScale = new Vector3(-characterTransform.localScale.x, characterTransform.localScale.y, characterTransform.localScale.z);
        animator.SetBool("walking", true);
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, Vector2.right * moveDir, 0.4f, hitSwitchDirection);

        rb.velocity = new Vector2(moveSpeed * moveDir, rb.velocity.y);

        if (hit)
            moveDir *= -1;

        if(moveDir == 1)
            characterTransform.localScale = rightScale;
        else
            characterTransform.localScale = leftScale;
    }
}
