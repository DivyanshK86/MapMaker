using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour {

	public CharacterManager cm;
	public Animator anim;
	public Transform skin;

	void Update()
	{
		anim.SetBool("isRunning", Mathf.Abs(cm.characterController.moveDir) > 0);
		if (cm.characterController.moveDir != 0 && !cm.characterController.isWalling)
			skin.localScale = new Vector3(cm.characterController.moveDir, skin.localScale.y, skin.localScale.z);

		if (cm.characterController.groundCheckManager.IsOnGround())
		{
			anim.SetInteger("jumpState", 0);
			anim.SetBool("isWalling", false);
		}
		else if (cm.characterController.isWalling)
		{
			anim.SetBool("isWalling", true);
		}
		else if (cm.characterController.rb.velocity.y > 0.3f)
		{
			anim.SetInteger("jumpState", 1);
			anim.SetBool("isWalling", false);
		}
		else if (cm.characterController.rb.velocity.y < 0.3f && cm.characterController.rb.velocity.y > -0.3f)
		{
			anim.SetInteger("jumpState", 2);
			anim.SetBool("isWalling", false);
		}
		else if (cm.characterController.rb.velocity.y < -0.3f)
		{
			anim.SetInteger("jumpState", 3);
			anim.SetBool("isWalling", false);
		}
	}
}
