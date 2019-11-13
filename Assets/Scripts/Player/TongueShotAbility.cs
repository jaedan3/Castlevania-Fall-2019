using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueShotAbility : MonoBehaviour
{
    
    private RaycastHit2D objHit;
    private Rigidbody2D rb2d;
    private LineRenderer lr;
    private Vector3 tongueDirection;
    private Vector3 ogPosition;
    private Vector3 hitPosition;
    private Vector3[] points;
    private float distanceToTravel;
    private bool hooking;
    private float shortenDistance;
    public float reach;
    public float pullPower;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        points = new Vector3[2];
        lr.positionCount = 2;
        shortenDistance = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        tongueDirection = ( new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) ).normalized;
        RaycastHit2D HitWall = Physics2D.Raycast(transform.position, tongueDirection,1 );
        if (hooking)
        {
            CheckPosition();
            points[0] = transform.position;
            points[1] = hitPosition;
            lr.SetPositions(points); 
        }
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
        hitPosition = objHit.point;
        distanceToTravel = objHit.distance-shortenDistance;
        lr.enabled = true;
    }

    void CheckPosition(){
        if ( Vector3.Distance(ogPosition, transform.position) >= distanceToTravel )
        {
            rb2d.velocity = new Vector2(0f,0f);
            hooking = false;
            lr.enabled = false;
        }
    }

}
