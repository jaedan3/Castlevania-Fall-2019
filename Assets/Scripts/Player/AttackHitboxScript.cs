﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitboxScript : MonoBehaviour
{

    private GameObject player;
    private float distance;
    private SpriteRenderer RD;
    public void Initialize(GameObject target_player, float dist)
    {
        RD = GetComponent<SpriteRenderer>();
        RD.flipX = target_player.GetComponent<SpriteRenderer>().flipX;
        Debug.Log(target_player.GetComponent<SpriteRenderer>().flipX);
        player = target_player;
        distance = dist;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetDirection();
        transform.position = player.transform.position + new Vector3(distance, 0f, 0f);
    }

    private void GetDirection()
    {
        //this.GetComponent<PlayerMovement>
    }
}
