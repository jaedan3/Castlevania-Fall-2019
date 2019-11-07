using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitboxScript : MonoBehaviour
{
    public bool absorbing;
    private GameObject player;
    private Vector3 distance;
    private SpriteRenderer RD;
    public void Initialize(GameObject target_player, Vector3 distVect, bool absorb)
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
        transform.position = player.transform.position + distance;
    }

    private void GetDirection()
    {
    }
}
