using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackCD = 0.3f;
    private float ATTACKTIME = 0;
    public GameObject hitBox;
    public GameObject hitBoxUP;
    public float attackReach;
    public float duration;
    private Animator anim;
    private int attackHash = Animator.StringToHash("Attack");
    private int attackUpHash = Animator.StringToHash("AttackUP");
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K) && Time.time > ATTACKTIME)
        {
            ATTACKTIME = Time.time + attackCD;
            Debug.Log(Input.GetAxis("Vertical"));
            if (Input.GetAxis("Vertical") > 0)
            {
                PlayAttackAnimation(attackUpHash);
                StartCoroutine(FlashHitbox(duration, "UP"));
            }
            //else if{ } ####################### PLACE HOLDER FOR ATTACKING DOWN
            else
            {
                PlayAttackAnimation(attackHash);
                StartCoroutine(FlashHitbox(duration, "Normal"));

            }
        }
    }

    IEnumerator FlashHitbox(float seconds, string key)
    {
        bool facingLeft = GetComponent<SpriteRenderer>().flipX;

        Vector3 DistForInit = new Vector3(attackReach * (facingLeft ? -1 : 1), 0, 0);
        yield return new WaitForSeconds(0.2f);
        GameObject newHitBox;
        if (key == "UP")
            {
            DistForInit = new Vector3(facingLeft ? 0.3f : 0.2f, attackReach, 0f);
            newHitBox = Instantiate(hitBoxUP, transform.position +DistForInit, Quaternion.identity);

            newHitBox.GetComponent<SpriteRenderer>().flipX = facingLeft;
        }
        else
        {
            newHitBox = Instantiate(hitBox, transform.position + new Vector3(attackReach * (facingLeft ? -1 : 1), 0f, 0f), Quaternion.identity);

            newHitBox.GetComponent<SpriteRenderer>().flipX = facingLeft;
        }

        newHitBox.GetComponent<AttackHitboxScript>().Initialize(gameObject, DistForInit,GetComponent<Abilities>().Absorb);
        yield return new WaitForSeconds(seconds);
        Destroy(newHitBox);
    }

    void PlayAttackAnimation(int hash)
    {
        anim.SetTrigger(hash);
    }
}
