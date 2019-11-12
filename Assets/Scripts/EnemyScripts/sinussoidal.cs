using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sinussoidal : MonoBehaviour
{
    
    private Vector3 Position;
    private Vector3 Velo = new Vector2(0, 0);
    private bool dying;
    public float MaxSpeed;
    public float amp = 2;
    public float accel = 1;
    public float floating_rate = 2;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Position = transform.position;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        transform.position = new Vector2(Position.x,Position.y + amp * Mathf.Sin(Time.time * floating_rate));
        setMovement();
        Position += Velo;
        
    }
    void setMovement()
    {
        Vector3 accelVector = (player.transform.position - Position).normalized * accel;
        Debug.Log(accelVector);
        Velo += accelVector;
        Debug.Log(accelVector + "<- accel and velo->" +Velo);
        transform.rotation = Quaternion.Euler(0, accelVector.x < 0 ? 0:180, 0);
        if (MaxSpeed < Velo.magnitude)
        {
            Velo *= (MaxSpeed / Velo.magnitude);
        }
    
    }
    

}
