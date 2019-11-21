using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAir : Enemy
{
    private Vector3 Position;
    private Vector3 Velo = new Vector2(0, 0);
    private Animator animator;
    public float MaxSpeed;
    public float amp = 2;
    public float accel = 1;
    public float floating_rate = 2;
    public float DeathTimer;
  

    // Start is called before the first frame update
    void Start()
    {
        Position = transform.position;
        currentHealth = 20;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {

        if(stunCount > 0)
        {
            stunCount--;
        }
        else
        {
            movement();

        }
        Position += Velo;
        if(!dying)
            transform.position = new Vector2(Position.x, Position.y + amp * Mathf.Sin(Time.time * floating_rate));

    }


    protected override void movement()
    {
        Vector3 accelVector = (tracking(target) - Position).normalized * accel;
        Velo += accelVector;
        transform.rotation = Quaternion.Euler(0, accelVector.x < 0 ? 0 : 180, 0);
        if (MaxSpeed < Velo.magnitude)
        {
            Velo *= (MaxSpeed / Velo.magnitude);
        }

    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Attack" && !dying) // When collided with an attack
        {
            Debug.Log("DAMAGED");
            damage(20);
            if (currentHealth > 0 && !dying) // if HP is greator than 0, call KB(knockback)
            {
                Velo = new Vector2(col.gameObject.GetComponent<SpriteRenderer>().flipX ? -0.3f : 0.3f, Velo.y);///GETS KNOCKED BACK
                stunCount = 13;
            }
            else                //########################## DYING
            {
                spawnHeal();
                Random.InitState(System.DateTime.Now.Second);
                animator.SetTrigger("death");
                stunCount = 1000;
                Velo = Vector2.zero;/////########### DROPS when dead
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                GetComponent<BoxCollider2D>().isTrigger = false;
                Destroy(gameObject, DeathTimer);
                dying = true;

            }
        }
        else if (col.gameObject.tag == "Player" && !dying)
        {
            col.gameObject.GetComponent<PlayerHurtbox>().Response(this.gameObject);
        }
    }
}
