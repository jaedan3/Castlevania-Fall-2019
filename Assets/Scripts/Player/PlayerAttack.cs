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
            }
            //else if{ } ####################### PLACE HOLDER FOR ATTACKING DOWN
            else
            {
                PlayAttackAnimation();
                StartCoroutine(FlashHitbox(duration));
            }
        }
    }

    IEnumerator FlashHitbox(float seconds)
    {
        yield return new WaitForSeconds(0.2f);
        GameObject newHitBox = Instantiate(hitBox, transform.position + new Vector3(attackReach * (GetComponent<SpriteRenderer>().flipX ? -1 : 1), 0f, 0f), transform.rotation);
        newHitBox.GetComponent<AttackHitboxScript>().Initialize(gameObject, attackReach * (GetComponent<SpriteRenderer>().flipX ? -1 : 1),GetComponent<Abilities>().Absorb);
        yield return new WaitForSeconds(seconds);
        Destroy(newHitBox);
    }

    void PlayAttackAnimation()
    {
        anim.SetTrigger(attackHash);
    }
}
