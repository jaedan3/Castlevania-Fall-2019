using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckedIn : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public int healAmount;
    private bool expired = false;
    // Start is called before the first frame update

    private void Awake()
    {
        GetComponent<ParticleSystem>().Play(true);
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
        if ((transform.position - player.transform.position).magnitude < 0.1) 
        {
            GetComponent<ParticleSystem>().Play(false); //STOPS EMITTING THE EXTRA BLODD
            if (!expired)
            {
                player.GetComponent<PlayerHurtbox>().health += healAmount;
                expired = true;
            }
            Destroy(gameObject,0.5f);//DESTROYS AFTER A DELAY SO THE PARTICLES DOES NOT DISAPPEAR IMMEDIATELY
        }
    }
}
