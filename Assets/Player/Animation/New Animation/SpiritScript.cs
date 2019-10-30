using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritScript : MonoBehaviour
{
    Color alp;
    // Start is called before the first frame update
    void Start()
    {

        alp = GetComponent<SpriteRenderer>().color;
        alp.a = 255;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime);

        alp.a -= 5;
       // GetComponent<SpriteRenderer>().color = alp;
    }
}
