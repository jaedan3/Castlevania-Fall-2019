using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameObject hitBox;
    public float attackReach;
    public float duration;
    private Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            StartCoroutine(FlashHitbox(duration));
    }

    IEnumerator FlashHitbox(float seconds)
    {
        GameObject newHitBox = Instantiate(hitBox, transform.position + new Vector3(attackReach, 0f, 0f), transform.rotation);
        yield return new WaitForSeconds(seconds);
        Destroy(newHitBox);
    }
}
