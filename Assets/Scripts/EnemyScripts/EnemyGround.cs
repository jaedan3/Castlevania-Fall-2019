using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGround : Enemy
{
    // Rigidbody2D rg;
    Animator animator;
    SpriteRenderer render;
    // private Transform wallDetect;
    public float xComp = 3;
    bool knocked = false;
    private bool dying = false;
    void Start()
    {
        /////////////////////////////// GET COMPONENTS!
        currentHealth = 100;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        ///////////////////////////////


        render.flipX = xComp > 0 ? true : false; //Sets the right direction
        //below will set the position of the raycaster according to the direction the ant is headed
        // transform.position = new Vector3(xComp > 0 ? transform.position.x + 0.75f : transform.position.x - 0.75f, transform.position.y, transform.position.z);
    }

    void KB(float dir)// Knock back
    {
        rb2d.velocity = new Vector2(dir * 3, 7);// Knocked back for 3 units back, 10 units up
        Debug.Log(rb2d.velocity);
        xComp = -(dir * 3); // moves towards the source of the damage
    }

    protected override void movement()
    {
        rb2d.velocity = new Vector2(xComp, rb2d.velocity.y);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Attack" && !dying) // When collided with an attack
        {

            damage(20);
            if (currentHealth > 0 && !dying) // if HP is greator than 0, call KB(knockback)
            {
                KB(col.gameObject.GetComponent<SpriteRenderer>().flipX ? -1 : 1);
                knocked = true;
                animator.SetBool("Air", true);
            }
            else                //########################## DYING
            {

                Random.InitState(System.DateTime.Now.Second);  //######    Random Seed with current time in seconds
                animator.SetTrigger(Random.Range(0, 1.0f) >= 0.5f ? "death" : "death2");   // 50 50 chance to play either animation
                Destroy(gameObject, 1.5f);
                dying = true;
                xComp = 0;

            }
        }
        else if (col.gameObject.tag == "Player" && !dying)
        {
            print("hit thingy");
            col.gameObject.GetComponent<PlayerHurtbox>().Response(this.gameObject);
        }
    }



    // Update is called once per frame
    void Update()
    {

        RaycastHit2D HitWall = Physics2D.Linecast(transform.position, new Vector2(transform.position.x + Mathf.Sign(xComp) , transform.position.y-0.5f));
        if(HitWall.collider == true && HitWall.collider.tag != "Attack" && !dying)
        {
            xComp = -xComp;
            render.flipX = !render.flipX;
        }
        if (!(knocked || dying))
        {
            movement();
        }
        else if (rb2d.velocity.y == 0)
        {
            knocked = false;
            animator.SetBool("Air", false);
            render.flipX = xComp > 0 ? true : false;
        }
        
    }
    
    

    
}
