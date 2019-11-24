using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntBossEnemy : Enemy
{

    private Animator anim;
    private int eatAntHash = Animator.StringToHash("");
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected override void movement()
    {
        return;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Hit");
        if ( col.gameObject.GetComponent<EnemyGround>() != null )
        {
            anim.SetTrigger(eatAntHash);
        }
    }

}
