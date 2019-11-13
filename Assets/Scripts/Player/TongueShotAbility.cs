using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueShotAbility : MonoBehaviour
{
    private Vector3 tongueDirection;
    private RaycastHit2D objHit;
    private Rigidbody2D rb2d;
    private Vector3 ogPosition;
    private float distanceToTravel;
    private bool hooking;
    private float shortenDistance;
    public float reach;
    public float pullPower;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        shortenDistance = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {

        tongueDirection = ( new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) ).normalized;
        RaycastHit2D HitWall = Physics2D.Raycast(transform.position, tongueDirection,1 );
        if (hooking){ CheckPosition(); }
        /*if (HitWall.collider == true)
        {
            rb2d.velocity = Vector2.zero;
        }*/
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.DrawLine(transform.position, transform.position + ( tongueDirection * reach ) );
            objHit = Physics2D.Raycast(transform.position, tongueDirection, reach);
            if ( objHit.collider != null )
            {
                Debug.Log("Hit");
                if (objHit.collider.tag == "Wall" && !hooking) // collider is not the collider component, it is the object that was collided with
                {
                    Debug.Log("Hooked");
                    pullTo(tongueDirection);
                }
            }
        }
    }

    void pullTo(Vector3 destination){
        hooking = true;
        rb2d.velocity = destination * pullPower;
        ogPosition = transform.position;
        distanceToTravel = objHit.distance-shortenDistance;
    }

    void CheckPosition(){
        if ( Vector3.Distance(ogPosition, transform.position) >= distanceToTravel )
        {
            rb2d.velocity = new Vector2(0f,0f);
            hooking = false;
        }
    }

}
