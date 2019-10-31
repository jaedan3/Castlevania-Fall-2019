using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovement : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    CharacterController2D controller;
    Animator animator;
    private bool lastUpdateGrounded;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //if (!Input.GetButtonDown("Fire3")) { return; }
        // By the way, this is all going to be replaced by a StateMachine, possibly.
        // But do keep it clean enough.
        if (controller.grounded)
        {
            controller.velocity.x = Input.GetAxisRaw("Horizontal") * 5;
            if (Input.GetButton("Jump")) {
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    controller.ignoringOneWayPlatforms = true;
                    controller.grounded = false;
                    //animator.SetTrigger("FallThrough");
                }
                else
                {
                    controller.applyLowGrav = true;
                    controller.grounded = false;
                    controller.velocity.y += 10;
                }
            }
        }
        else
        {
            controller.applyLowGrav = Input.GetButton("Jump") && controller.applyLowGrav;
            controller.velocity.x = Mathf.MoveTowards(controller.velocity.x, Input.GetAxis("Horizontal") * 5, 1f);
        }
        controller.Move(Time.fixedDeltaTime);
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
