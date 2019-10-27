using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMelee : MonoBehaviour
{
    public int HP = 100;
    Rigidbody2D rg;
    Animator animator;
    SpriteRenderer render;
    public float xComp = 3;
    bool knocked = false;
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        rg.freezeRotation = true;
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        render.flipX = xComp > 0 ? true : false;
    }
    void KB(float dir)
    {
        rg.velocity = new Vector2(dir * 3, 7);
        xComp = -(dir * 3);
    }

    void movement()
    {
        rg.velocity = new Vector2(xComp, rg.velocity.y);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")//// BOUND OFF THE WALL
        {
            render.flipX = !render.flipX;
            xComp = -xComp;
        }
        else if (collision.collider.tag == "Attack")
        {
            if (HP > 0)
            {
                KB(collision.collider.transform.position.x < transform.position.x ? 1 : -1);
                knocked = true;
                animator.SetBool("Air", true);
                HP -= 20;
            }
            else    animator.SetTrigger("death");
        }
    }






    // Update is called once per frame
    void Update()
    {
        if (!knocked)
        {
            movement();
        }
        else if(rg.velocity.y == 0)
        {
            knocked = false;
            animator.SetBool("Air", false);
            render.flipX = xComp > 0 ? true : false;
        }
    
    }
    
    
}
