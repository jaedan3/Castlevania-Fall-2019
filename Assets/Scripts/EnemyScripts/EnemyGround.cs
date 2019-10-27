using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGround : Enemy
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(!stunned)
            movement();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.Equals(target))
            damaged();
    }

    protected override void movement()
    {
        rb2d.MovePosition(Vector3.MoveTowards(transform.position, 
            tracking(target), 
            speed));
    }

    protected override void damaged()
    {
        stunned = true;
        Debug.Log("What the fuck");
        rb2d.AddForce( -(transform.position.normalized - tracking(target).normalized) * pushBack );
    }

    
}
