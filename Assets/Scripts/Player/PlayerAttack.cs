using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    public GameObject hitBox;
    public float attackReach;
    public float duration;
    private Animator anim;
    private int attackHash = Animator.StringToHash("Attack");
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(Input.GetAxis("Vertical"));
            if (Input.GetAxis("Vertical") > 0)
            {
                
                anim.SetTrigger("AttackUP");

                StartCoroutine(FlashHitbox(duration, new Vector3(0,0,90)));
            }
            //else if{ } ####################### PLACE HOLDER FOR ATTACKING DOWN
            else
            {
                PlayAttackAnimation();
                StartCoroutine(FlashHitbox(duration, new Vector3(0, GetComponent<SpriteRenderer>().flipX ? 180 : 0, 0)));

            }
        }
    }

    IEnumerator FlashHitbox(float seconds, Vector3 angle)
    {
        bool facingLeft = GetComponent<SpriteRenderer>().flipX;

        Vector3 DistForInit = new Vector3(attackReach * (facingLeft ? -1 : 1), 0, 0);
        yield return new WaitForSeconds(0.2f);
        GameObject newHitBox;
        if (angle.z > 0)
            {
            DistForInit = new Vector3(facingLeft ? 0.3f : 0.2f, attackReach, 0f);
            newHitBox = Instantiate(hitBox, transform.position +DistForInit, Quaternion.Euler(angle));

            }
        else
        {
            newHitBox = Instantiate(hitBox, transform.position + new Vector3(attackReach * (facingLeft ? -1 : 1), 0f, 0f), Quaternion.Euler(angle));

        }


        newHitBox.GetComponent<AttackHitboxScript>().Initialize(gameObject, DistForInit,GetComponent<Abilities>().Absorb);
        yield return new WaitForSeconds(seconds);
        Destroy(newHitBox);
    }

    void PlayAttackAnimation()
    {
        anim.SetTrigger(attackHash);
    }
}
