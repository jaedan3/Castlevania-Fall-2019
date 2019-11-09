using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitboxScript : MonoBehaviour
{
    public bool absorbing;
    private GameObject player;
    private Vector3 distance;
    private SpriteRenderer RD;

    private int destroyCount = 0;
    public void Initialize(GameObject target_player, Vector2 distVect, bool absorb)
    {
        absorbing = absorb;
        RD = GetComponent<SpriteRenderer>();
        player = target_player;
        distance = distVect;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetDirection();
    }
    
    private void GetDirection()
    {
        RD.flipX = player.GetComponent<SpriteRenderer>().flipX;
        transform.position = new Vector3(player.transform.position.x + (player.GetComponent<SpriteRenderer>().flipX ? -distance.x : distance.x),player.transform.position.y+ distance.y);
    }
    private void FixedUpdate()
    {
        destroyCount++;
        if (destroyCount > 10)
            Destroy(gameObject);
    }
}
