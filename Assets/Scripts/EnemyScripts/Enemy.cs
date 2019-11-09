using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : MonoBehaviour
{

    // public Attack attackModule;
    public int currentHealth;
    public float speed;
    public float pushBack;
    public GameObject target;
    protected int HP;
    protected Rigidbody2D rb2d;
    protected bool stunned;
    
    protected abstract void movement();

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public Vector3 tracking(GameObject target)
    {
        return target.GetComponent<Transform>().position;
    }
    public Rigidbody2D get_rigid_body_2d(GameObject target)
    {
        return target.GetComponent<Rigidbody2D>();
    }

    protected int damage(int power)
    {
        return currentHealth-=power;
    }


}
