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
        //if (controller.grounded)
        //{
            controller.velocity.x = Input.GetAxis("Horizontal") * 5;
        //}
        //if (Input.GetButtonDown("Fire1"))
        //{
            controller.Move(Time.fixedDeltaTime);
        //}
    }
}
