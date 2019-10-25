using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovement : MonoBehaviour
{
    CharacterController2D controller;

    void Start()
    {
        controller = GetComponent<CharacterController2D>();
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
    }
}
