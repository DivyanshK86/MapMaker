using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateButton : MonoBehaviour {

    public Animator animator;
    public LayerMask activators;
    public bool redoAnimate;
    public bool canBePressedAgain;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(((1<<other.gameObject.layer) & activators) != 0)
        {
            animator.SetBool("pressed",true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(((1<<other.gameObject.layer) & activators) != 0)
        {
            if(redoAnimate)
                animator.SetBool("pressed",false);
        }
    }
}
