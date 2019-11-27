using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenBee : Enemy
{
    private Vector3 direction;
    public float AirDrag;
    public int Delay;
    public bool ready;
    private int AttackCount = 3;
    private float Speed;
    bool DASH = false;
    public GameObject projectile;
    private int phase = 0;
    public float projSpeed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void movement()
    {
        transform.position = transform.position + direction * Speed;
    }

    Vector2 getDir()
    {
        return (target.transform.position - transform.position).normalized;
    }
    void dash()
    {
        Speed -= AirDrag;
        if(Speed < 0)
        {
            direction = getDir();
            if (AttackCount > 0)
            {
                AttackCount--;
                Speed = 0.8f;
                Debug.Log(AttackCount);
            }
            else
            {
                DASH = false;
                phase = 1;
            }
        }

    }
    void shoot()
    {
        for (int i = 0; i < 3; i++)
        {

            GameObject a = Instantiate(projectile, transform.position, Quaternion.identity);
            a.GetComponent<BeeShoot>().init(getDir() * projSpeed + new Vector2(0.02f * (i*i),0));

        }
        phase = 0;
    }

    private void FixedUpdate()
    {
        movement();
        GetComponent<SpriteRenderer>().flipX = getDir().x > 0 ? true : false;
        if (Delay == 0 && ready)
        {
            if (phase == 1)
            {
                shoot();
                Delay = 100;
            }
            else
            {
                DASH = true;
                AttackCount = 3;
                Delay = 250;
            }
        }
        else//############################### Delay after attack
        {
            Delay--;
            if (DASH)
            {
                dash();
            }
            else
            {

            }
        }
    }
}
