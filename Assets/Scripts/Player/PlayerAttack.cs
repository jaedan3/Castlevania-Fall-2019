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
            PlayAttackAnimation();
            StartCoroutine(FlashHitbox(duration));
        }
    }

    IEnumerator FlashHitbox(float seconds)
    {
        GameObject newHitBox = Instantiate(hitBox, transform.position + new Vector3(-attackReach * Mathf.Sign(transform.rotation.y - 0.5f), 0f, 0f), transform.rotation);
        newHitBox.GetComponent<AttackHitboxScript>().Initialize(gameObject, attackReach);
        yield return new WaitForSeconds(seconds);
        Destroy(newHitBox);
    }

    void PlayAttackAnimation()
    {
        anim.SetTrigger(attackHash);
    }
}
