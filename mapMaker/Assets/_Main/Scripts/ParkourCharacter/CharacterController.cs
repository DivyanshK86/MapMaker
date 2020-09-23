using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

	public CharacterManager cm;
	public GroundCheckManager groundCheckManager;
	public WallStickCollider stickLeft;
	public WallStickCollider stickRight;
	public float movementSpeed = 1;
	public float jumpSpeed = 5;
	public float wallJumpSpeed_H;
	public float wallJumpSpeed_V;
	public float wallingNumbTime;
	public bool isWalling;
	public bool isNumb;
	public float maxFallVelocity;
	public float maxFallVelocity_Walling;


	[HideInInspector]
	public int moveDir;
	[HideInInspector]
	public Rigidbody2D rb;


	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (!isNumb)
			if (Input.GetKey(KeyCode.RightArrow))
			{
				moveDir = 1;
			}
			else if (Input.GetKey(KeyCode.LeftArrow))
			{
				moveDir = -1;
			}
			else
			{
				moveDir = 0;
			}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if(isWalling)
			{
				if (stickLeft.IsSticking())
				{
					rb.velocity = new Vector2(wallJumpSpeed_H, wallJumpSpeed_V);
					moveDir = 1;
				}
				if (stickRight.IsSticking())
				{
					rb.velocity = new Vector2(-wallJumpSpeed_H, wallJumpSpeed_V);
					moveDir = -1;
				}
				isNumb = true;
				Invoke("RemoveNumb", wallingNumbTime);
			}
			else if (groundCheckManager.IsOnGround())
				rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
		}

		if (!isNumb)
			rb.velocity = new Vector2(movementSpeed * moveDir, rb.velocity.y);

		if(isWalling)
		{
			if (rb.velocity.y < -maxFallVelocity_Walling)
				rb.velocity = new Vector2(rb.velocity.x, -maxFallVelocity_Walling);
		}
		else if(rb.velocity.y < -maxFallVelocity)
			rb.velocity = new Vector2(rb.velocity.x, -maxFallVelocity);

		isWalling = ((stickLeft.IsSticking() || stickRight.IsSticking()) && !groundCheckManager.IsOnGround() && rb.velocity.y < 0.068f);

		isSticking = stickLeft.IsSticking() || stickRight.IsSticking();
		onGrnd = !groundCheckManager.IsOnGround();
		vel = rb.velocity.y < 0.068f;
	}

	[Space]
	public bool isSticking;
	public bool onGrnd;
	public bool vel;

	void RemoveNumb()
	{
		isNumb = false;
	}
}
