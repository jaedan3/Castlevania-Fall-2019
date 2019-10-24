using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public Vector2 size = new Vector2(2, 2);
    public float minMovement;
    public float upGravity = 8;
    public float downGravity = 20;

    [HideInInspector]
    public bool grounded = false;
    [HideInInspector]
    public Vector3 velocity = Vector3.zero;

    private Collider2D[] nonAlloc = new Collider2D[1];


    // Call as many times as you want per frame, as opposed to regular Character Controller    
    public void Move(float delta = -1)
    {
        if (delta < 0) { delta = Time.fixedDeltaTime; } // Default args must be compile time constants.

        if (grounded)
        {
            nonAlloc[0] = null;
            Physics2D.OverlapBoxNonAlloc(transform.position + Vector3.Scale(Vector2.down, this.size / 2), this.size / 2, 0, nonAlloc);
            if (nonAlloc[0] == null)
            {
                grounded = false;
            }
        }
        if (!grounded)
        {
            velocity += Vector3.down * delta * (velocity.y > 0 && Input.GetButton("Fire1") ? upGravity : downGravity);
        }

        if (velocity.sqrMagnitude <= minMovement * minMovement) { return; }

        transform.position += velocity * delta;

        if (velocity.y < 0)
        {
            CollideBottom();
        }
        if (velocity.x <= 0)
        {
            CollideLeft();
            CollideRight();
        }
        else
        {
            CollideRight();
            CollideLeft();
        }
        if (velocity.y > 0)
        {
            CollideTop();
        }
    }

    // CHEATING: ASSUMES THAT TILES ARE ONE UNITY UNIT WIDE >:))
    private void CollideBottom()
    {
        nonAlloc[0] = null;
        Physics2D.OverlapBoxNonAlloc(transform.position + Vector3.Scale(Vector2.down, this.size / 4), this.size / 2, 0, nonAlloc);
        if (nonAlloc[0] != null)
        {
            float my_bottom = transform.position.y - this.size.y / 2;
            transform.position += Vector3.up * (Mathf.Round(my_bottom) - my_bottom);

            //transform.position += Vector3.up * (nonAlloc[0].bounds.max.y - (transform.position.y - this.size.y / 2));
            print("landed");
            grounded = true;
            velocity.y = 0;
        }
    }

    private void CollideLeft()
    {
        nonAlloc[0] = null;
        Physics2D.OverlapBoxNonAlloc(transform.position + Vector3.Scale(Vector2.left, this.size / 4), this.size / 2, 0, nonAlloc);
        if (nonAlloc[0] != null)
        {
            float my_left = transform.position.x - this.size.x / 2;
            transform.position += Vector3.right * (Mathf.Round(my_left) - my_left);
            //transform.position += Vector3.right * (nonAlloc[0].bounds.max.x - (transform.position.x - this.size.x / 2));
            velocity.x = 0;
        }
    }

    private void CollideRight()
    {
        nonAlloc[0] = null;
        Physics2D.OverlapBoxNonAlloc(transform.position + Vector3.Scale(Vector2.right, this.size / 4), this.size / 2, 0, nonAlloc);
        if (nonAlloc[0] != null)
        {
            float my_right = transform.position.x + this.size.x / 2;
            transform.position += Vector3.right * (Mathf.Round(my_right) - my_right);
            velocity.x = 0;
        }
    }

    private void CollideTop()
    {
        nonAlloc[0] = null;
        Physics2D.OverlapBoxNonAlloc(transform.position + Vector3.Scale(Vector2.up, this.size / 4), this.size / 2, 0, nonAlloc);
        if (nonAlloc[0] != null)
        {
            float my_top = transform.position.y + this.size.y / 2;
            transform.position += Vector3.up * (Mathf.Round(my_top) - my_top);
            velocity.y = 0;
        }
    }

}