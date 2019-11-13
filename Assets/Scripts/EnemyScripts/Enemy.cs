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
    protected int stunCount = 25;
    public bool dying = false;
    public GameObject healPrefab;
    
    protected abstract void movement();

    void Start()
    {
        healPrefab = GameObject.Find("Health Globe");
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

    protected void spawnHeal()
    {
        GameObject healthGlobe = Instantiate(healPrefab, transform.position, Quaternion.identity); //SPAWNS THE HEALTH GLOBE
        healthGlobe.GetComponent<SuckedIn>().player = target;
    }
}
