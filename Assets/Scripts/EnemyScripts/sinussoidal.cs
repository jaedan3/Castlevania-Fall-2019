using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sinussoidal : MonoBehaviour
{

    private Vector2 Position;
    public float amp = 2;
    public float floating_rate = 2;
    // Start is called before the first frame update
    void Start()
    {
        Position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Position.x,Position.y + amp * Mathf.Sin(Time.time * floating_rate));
    }
}
