using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public Vector2 size = new Vector2(2, 2);
    public float minMovement;
    public float upGravity = 8;
    public float downGravity = 32;
    public float maxFallSpeed = 60;

    [HideInInspector]
    public bool grounded = false;
    [HideInInspector]
    public bool applyLowGrav = false;
    [HideInInspector]
    public bool ignoringOneWayPlatforms = false;
    [HideInInspector]
    public Vector3 velocity = Vector3.zero;

    private bool notInOneWayPlatform = false;

    private int notIgnoreRaycast;
    private int oneWayMask;
    private int notOneWayMask;
    private Collider2D[] nonAlloc = new Collider2D[1];



    public void Start()
    {
        notIgnoreRaycast = Physics2D.AllLayers ^ (1 << LayerMask.NameToLayer("Ignore Raycast"));
        oneWayMask = (1 << LayerMask.NameToLayer("OneWayPlatform"));
        notOneWayMask = (Physics2D.AllLayers ^ oneWayMask) & notIgnoreRaycast;
    }

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
            velocity += Vector3.down * delta * (velocity.y > 0 && applyLowGrav ? upGravity : downGravity);
            if (velocity.y < -maxFallSpeed) { velocity += Vector3.up * (-maxFallSpeed - velocity.y); }
        }

        if (velocity.sqrMagnitude <= minMovement * minMovement) { return; }

        transform.position += velocity * delta; // Collide Point assumes point has moved before displacement.

        if (velocity.y < 0)
        {
            CollideBottom(-velocity * delta);
        }
        if (velocity.x < 0)
        {
            CollideLeft(-velocity * delta);
            CollideRight(-velocity * delta * 0.5f); // Fudging the numbers
        }
        else if (velocity.x > 0)
        {
            CollideRight(-velocity * delta);
            CollideLeft(-velocity * delta * 0.5f); // Again
        }
        else
        {
            CollideRight(-velocity * delta * 0.5f);
            CollideLeft(-velocity * delta * 0.5f); // Again
        }
        if (velocity.y > 0)
        {
            CollideTop(-velocity * delta);
        }

        if (this.ignoringOneWayPlatforms)
        {
            nonAlloc[0] = null;
            Physics2D.OverlapBoxNonAlloc(transform.position + Vector3.Scale(Vector2.down, this.size / 4), this.size / 2, 0, nonAlloc, oneWayMask);
            if (nonAlloc[0] == null)
            {
                this.ignoringOneWayPlatforms = false;
            }
        }
        else
        {
            nonAlloc[0] = null;
            Physics2D.OverlapBoxNonAlloc(transform.position + Vector3.Scale(Vector2.down, this.size / 4), this.size / 2, 0, nonAlloc, oneWayMask);
            if (nonAlloc[0] != null)
            {
                this.ignoringOneWayPlatforms = true;
            }
        }
    }

    private void CollideBottom(Vector2 displacement)
    {
        //if (!crossesInteger(transform.position.y, transform.position.y + displacement.y)) { return; }

        for (float i = -size.x/4f; i < size.x/4f; i += 0.1f)
        {
            CollidePoint((this.ignoringOneWayPlatforms ? notOneWayMask : notIgnoreRaycast),
                new Vector2(i, -size.y / 2), displacement, ResolveBottom);
        }
    }

    private void CollideLeft(Vector2 displacement)
    {
        //if (!crossesInteger(transform.position.x, transform.position.x + displacement.x)) { return; }

        for (float i = -size.y/4f; i < size.y/4f; i += 0.1f)
        {
            CollidePoint(notOneWayMask, new Vector2(-size.x / 2 , i), displacement, ResolveLeft);
        }
    }

    private void CollideRight(Vector2 displacement)
    {
        //if (!crossesInteger(transform.position.x, transform.position.x + displacement.x)) { return; }

        for (float i = -size.y/4f; i < size.y/4f; i += 0.1f)
        {
            CollidePoint(notOneWayMask, new Vector2(size.x / 2, i), displacement, ResolveRight);
        }
    }

    private void CollideTop(Vector2 displacement)
    {
        //if (!crossesInteger(transform.position.y, transform.position.y + displacement.y)) { return; }

        for (float i = -size.x/4f; i < size.x/4f; i += 0.1f)
        {
            CollidePoint(notOneWayMask, new Vector2(i, size.y / 2), displacement, ResolveTop);
        }
    }

    private void CollidePoint(int layerMask, Vector2 offset, Vector2 displacement, Action<Vector2> resolution)
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);

        nonAlloc[0] = null;
        Physics2D.OverlapAreaNonAlloc(position + offset, position + offset + displacement, nonAlloc, layerMask);
        if (nonAlloc[0] != null)
        {
            resolution(displacement);
        }
    }

    //// CHEATING: ASSUMES THAT TILES ARE ONE UNITY UNIT WIDE >:))
    private void ResolveBottom(Vector2 displacement)
    {
        float my_bottom = transform.position.y - this.size.y / 2;
        transform.position += Vector3.up * (Mathf.Round(my_bottom) - my_bottom + Physics2D.defaultContactOffset * 2);

        //transform.position += Vector3.up * (nonAlloc[0].bounds.max.y - (transform.position.y - this.size.y / 2));
        //print("landed");
        grounded = true;
        velocity.y = 0;
    }


    private void ResolveLeft(Vector2 displacement)
    {
        float my_left = transform.position.x - this.size.x / 2;
        transform.position += Vector3.right * (Mathf.Round(my_left) - my_left + Physics2D.defaultContactOffset * 2);
        //transform.position += Vector3.right * (nonAlloc[0].bounds.max.x - (transform.position.x - this.size.x / 2));
        velocity.x = 0;
    }

    private void ResolveRight(Vector2 displacement)
    {
        float my_right = transform.position.x + this.size.x / 2;
        transform.position += Vector3.right * (Mathf.Round(my_right) - my_right - Physics2D.defaultContactOffset * 2);
        velocity.x = 0;
    }

    private void ResolveTop(Vector2 displacement)
    {
        float my_top = transform.position.y + this.size.y / 2;
        transform.position += Vector3.up * (Mathf.Round(my_top) - my_top - Physics2D.defaultContactOffset * 2);
        velocity.y = 0;
    }

    //private bool crossesInteger(float a, float b)
    //{
    //    return (a - b) == ((a % 1) - (b % 1)); // Check if a and b are on the same tooth of a saw wave
    //}
}