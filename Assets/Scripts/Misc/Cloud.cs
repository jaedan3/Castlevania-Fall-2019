using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float speed;
    public float upperLimit;
    public float lowerLimit;
    public float LeftLimit;
    public float RightLimit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + speed, transform.position.y, 0);
        if(transform.position.x > RightLimit)
        {
            transform.position = new Vector3(LeftLimit, Random.Range(lowerLimit, upperLimit), 0);
        }
        else if(transform.position.x < LeftLimit)
        {
            transform.position = new Vector3(RightLimit, Random.Range(lowerLimit, upperLimit), 0);
        }
    }
}
