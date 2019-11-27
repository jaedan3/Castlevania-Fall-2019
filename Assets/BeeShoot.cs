using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeShoot : Enemy
{
    public Vector3 dir;
    Animator animator;
    public void init(Vector2 direction)
    {
        dir = direction;
        animator = GetComponent<Animator>();
        GetComponent<SpriteRenderer>().flipX = dir.x > 0 ? true : false;
        Destroy(gameObject, 5);
        dying = false;
    }
    // Update is called once per frame
    override protected void movement()
    {

    }
    void Update()
    {
        transform.position += dir;   
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Attack" && !dying) // When collided with an attack
        {
            animator.SetTrigger("death");
            dir = Vector2.zero;/////########### DROPS when dead
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<BoxCollider2D>().isTrigger = false;
            Destroy(gameObject, 0.5f);
            dying = true;
        }
    }
}
