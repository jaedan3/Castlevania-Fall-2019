using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    CharacterController2D controller;
    PlayerMovement movement;
    Animator animator;

    private bool lastUpdateGrounded;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<CharacterController2D>();
        movement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.grounded != lastUpdateGrounded)
        {
            if (controller.grounded)
            {
                animator.SetTrigger("JustLanded");
            }
            else
            {
                animator.SetTrigger("JustAirborn");
            }
            lastUpdateGrounded = controller.grounded;
        }
        if (controller.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (controller.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        animator.SetFloat("XSpeed", Mathf.Abs(controller.velocity.x));
        animator.SetFloat("YSpeed", Mathf.Abs(controller.velocity.y));
        animator.SetFloat("YVel", controller.velocity.y);
    }
}
