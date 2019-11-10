using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    CharacterController2D controller;
    PlayerMovement movement;
    PlayerHurtbox hurtbox;
    Animator animator;

    private bool lastUpdateGrounded;
    private bool lastUpdateNoKnockback;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<CharacterController2D>();
        movement = GetComponent<PlayerMovement>();
        hurtbox = GetComponent<PlayerHurtbox>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (hurtbox.knockbackTimer > 0 != lastUpdateNoKnockback)
        //{
            //if (hurtbox.knockbackTimer > 0)
            //{
                //animator.SetTrigger("JustKnockback");
            //}
        animator.SetBool("TakingKnockback", hurtbox.knockbackTimer > 0);
        //}

        if (controller.grounded != lastUpdateGrounded)
        {
            if (controller.grounded)
            {
                animator.SetTrigger("JustLanded");
            }
            else
            {
                //animator.SetTrigger("JustAirborn");
            }
            lastUpdateGrounded = controller.grounded;
        }
        animator.SetBool("Airborn", !controller.grounded);
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") < 0;
        }
        //if (controller.velocity.x < 0)
        //{
            //spriteRenderer.flipX = true;
        //}
        //else if (controller.velocity.x > 0)
        //{
            //spriteRenderer.flipX = false;
        //}

        animator.SetFloat("XSpeed", Mathf.Abs(controller.velocity.x));
        animator.SetFloat("YSpeed", Mathf.Abs(controller.velocity.y));
        animator.SetFloat("YVel", controller.velocity.y);
    }
}
