using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGround : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    protected override void movement()
    {
        GetComponent<Rigidbody2D>().MovePosition(Vector3.MoveTowards(transform.position, 
        tracking(target), // Target
        speed)); // Speed
    }

    protected override void damaged()
    {
        int x = 2;
    }

    
}
