using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public Vector2 size = new Vector2(2, 2);
    public float minMovement;

    [HideInInspector]
    public bool grounded = false;
    [HideInInspector]
    public Vector3 velocity = Vector3.zero;

    private Collider2D[] nonAlloc = new Collider2D[1];


    // Call as many times as you want per frame, as opposed to regular Character Controller    
    public void Move(float delta = -1)
    {
        if (delta < 0) { delta = Time.fixedDeltaTime; } // Default args must be compile time constants.

        if (!grounded) { velocity += Vector3.down * 3 * delta; }

        if (velocity.sqrMagnitude <= minMovement * minMovement) { return; }

        transform.position += velocity * delta;

        if (velocity.y < 0)
        {
            collideBottom();
        }
        if (velocity.x < 0)
        {
            collideLeft();
            collideRight();
        }
        if (velocity.x > 0)
        {
            collideRight();
            collideLeft();
        }
        if (velocity.y > 0)
        {
            collideTop();
        }
    }

    private void collideBottom()
    {
        nonAlloc[0] = null;
        Physics2D.OverlapBoxNonAlloc(transform.position + Vector3.Scale(Vector2.down, this.size / 4), this.size / 2, 0, nonAlloc);
        if (nonAlloc[0] != null)
        {
            print(transform.position);
            transform.position += Vector3.up * (nonAlloc[0].bounds.max.y - (transform.position.y - this.size.y / 2));
            print(transform.position);
            grounded = true;
            velocity.y = 0;
        }
    }

    private void collideLeft()
    {
        nonAlloc[0] = null;
        Debug.Log(transform.position + Vector3.Scale(Vector2.left, this.size / 4));
        Physics2D.OverlapBoxNonAlloc(transform.position + Vector3.Scale(Vector2.left, this.size / 4), this.size / 2, 0, nonAlloc);
        if (nonAlloc[0] != null)
        {
            transform.position += Vector3.right * (nonAlloc[0].bounds.max.x - (transform.position.x - this.size.x / 2));
            velocity.x = 0;
        }
    }

    private void collideRight()
    {
        nonAlloc[0] = null;
        Physics2D.OverlapBoxNonAlloc(transform.position + Vector3.Scale(Vector2.right, this.size / 4), this.size / 2, 0, nonAlloc);
        if (nonAlloc[0] != null)
        {
            transform.position += Vector3.right * (nonAlloc[0].bounds.min.x - (transform.position.x + this.size.x / 2));
            velocity.x = 0;
        }
    }

    private void collideTop()
    {
        nonAlloc[0] = null;
        Physics2D.OverlapBoxNonAlloc(transform.position + Vector3.Scale(Vector2.up, this.size / 4), this.size / 2, 0, nonAlloc);
        if (nonAlloc[0] != null)
        {
            transform.position += Vector3.up * (nonAlloc[0].bounds.min.y - (transform.position.y + this.size.y / 2));
            velocity.y = 0;
        }
    }

}