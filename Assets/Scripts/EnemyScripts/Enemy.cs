using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : MonoBehaviour
{

    public Attack attackModule;
    public int currentHealth;
    public float speed;
    public GameObject target;
    protected int maxHealth;
    

    protected abstract void movement();
    protected abstract void damaged();

    public Vector3 tracking(GameObject target)
    {
        return target.GetComponent<Transform>().position;
    }
    public Rigidbody2D get_rigid_body_2d(GameObject target)
    {
        return target.GetComponent<Rigidbody2D>();
    }
}
