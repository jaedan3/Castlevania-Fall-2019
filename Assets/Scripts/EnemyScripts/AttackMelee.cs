using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMelee : MonoBehaviour
{
    public int HP = 100;
    Rigidbody2D rg;
    Animator animator;
    SpriteRenderer render;
    public Transform wallDetect;
    public float xComp = 3;
    bool knocked = false;
    void Start()
    {
        /////////////////////////////// GET COMPONENTS!
        rg = GetComponent<Rigidbody2D>();
        rg.freezeRotation = true;
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        ///////////////////////////////


        render.flipX = xComp > 0 ? true : false; //Sets the right direction
        //below will set the position of the raycaster according to the direction the ant is headed
        wallDetect.position = new Vector3(xComp > 0 ? transform.position.x + 0.75f : transform.position.x - 0.75f, wallDetect.position.y, wallDetect.position.z);
    }

    void KB(float dir)// Knock back
    {
        rg.velocity = new Vector2(dir * 3, 7);// Knocked back for 3 units back, 7 units up
        xComp = -(dir * 3); // moves towards the source of the damage
    }

    void movement()
    {
        rg.velocity = new Vector2(xComp, rg.velocity.y);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.collider.tag == "Attack") // When collided with an attack
        {
            if ((HP -= 20) > 0) // if HP is greator than 0, call KB(knockback)
            {
                KB(collision.collider.transform.position.x < transform.position.x ? 1 : -1);
                knocked = true;
                animator.SetBool("Air", true);
            }
            else    animator.SetTrigger("death");
        }
    }






    // Update is called once per frame
    void Update()
    {
        RaycastHit2D HitWall = Physics2D.Raycast(wallDetect.position,Vector2.down, 0.01f);
        if(HitWall.collider == true)
        {   
            xComp = -xComp;
            render.flipX = !render.flipX;
            wallDetect.position = new Vector3(xComp > 0 ? transform.position.x + 0.75f : transform.position.x - 0.75f, wallDetect.position.y, wallDetect.position.z);
        }
        if (!knocked)
        {
            movement();
        }
        else if (rg.velocity.y == 0)
        {
            knocked = false;
            animator.SetBool("Air", false);
            render.flipX = xComp > 0 ? true : false;
        }
    
    }
    
    
}
