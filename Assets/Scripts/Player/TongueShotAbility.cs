using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueShotAbility : MonoBehaviour
{
    private Vector3 tongueDirection;
    private RaycastHit2D objHit;
    private Rigidbody2D rb2d;
    public float reach;
    public float pullPower;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        tongueDirection = ( new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) ).normalized;
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.DrawLine(transform.position, transform.position + ( tongueDirection * reach ) );
            objHit = Physics2D.Raycast(transform.position, tongueDirection, reach);
            if ( objHit.collider != null )
            {
                Debug.Log("Hit");
                if (objHit.collider.tag == "Wall") // collider is not the collider component, it is the object that was collided with
                {
                    Debug.Log("Hooked");
                    pullTo(tongueDirection);
                }
            }
        }
    }

    void pullTo(Vector3 destination){
        rb2d.velocity = destination * pullPower;
    }
}
