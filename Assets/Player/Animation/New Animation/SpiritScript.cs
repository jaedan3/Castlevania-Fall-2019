using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritScript : MonoBehaviour
{
    SpriteRenderer alp;
    // Start is called before the first frame update
    void Start()
    {

        alp = GetComponent<SpriteRenderer>();
        alp.color = new Color(1 , 1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime);
        alp.color = new Color(1f, 1f, 1f, alp.color.a - 0.015f);
        Destroy(gameObject, 3);
    }
}
