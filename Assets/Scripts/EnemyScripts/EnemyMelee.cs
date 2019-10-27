using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public int HP = 100;
    Rigidbody2D rg;
    public float Xforce = 10, Yforce;
    void movement()
    {
        rg.velocity = new Vector2(Xforce, rg.velocity.y);
    }
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        rg.freezeRotation = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
            Xforce = -Xforce;

    }

    // Update is called once per frame
    void Update()
    {
        movement();
    
    }
    
}
