using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerHurtbox : MonoBehaviour
{
    int maxHealth = 6;
    float invincibilityPeriod = 1;

    //Vector2 knockback;
    CharacterController2D controller;
    SpriteRenderer spriteRenderer;

    private int health;
    // named different to prevent confusion.
    private float invincibilityTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        controller = GetComponent<CharacterController2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (invincibilityTimer <= 0)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, controller.size, 0, Physics2D.AllLayers);
            foreach (Collider2D collider in colliders)
            {
                print(collider.gameObject.name);
                if (collider.gameObject.tag.Equals("Enemy"))
                {
                    Response(collider.gameObject);
                    break;
                }
            }
        }
        else
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            invincibilityTimer -= Time.fixedDeltaTime;
            if (invincibilityTimer <= 0)
            {
                spriteRenderer.enabled = true;
            }
        }
    }

    public void Response(GameObject enemy)
    {
        if (invincibilityTimer <= 0)
        {
            invincibilityTimer = invincibilityPeriod;
        }
    }
}
