using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerHurtbox : MonoBehaviour
{
    public int maxHealth = 6;
    public float invincibilityPeriod = 1;
    public float knockbackPeriod = 0.5f;

    //Vector2 knockback;
    CharacterController2D controller;
    SpriteRenderer spriteRenderer;
    PlayerMovement playerMovement;

    public int health;
    // named different to prevent confusion.
    public float knockbackTimer = 0;
    public float invincibilityTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        controller = GetComponent<CharacterController2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (knockbackTimer > 0)
        {
            knockbackTimer -= Time.fixedDeltaTime;
        }
        playerMovement.airControl = knockbackTimer <= 0;

        if (invincibilityTimer <= 0)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, controller.size, 0, Physics2D.AllLayers);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.tag.Equals("Enemy") && !collider.GetComponent<Enemy>().dying)
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
            health--;///////////////////////////SIN ADDED THIS FOR HEALTH DISPLAY
            knockbackTimer = knockbackPeriod;
            controller.velocity = Vector3.up * 10;
            // assumes ant position is center of ant
            controller.velocity += Vector3.right * 5 * Mathf.Sign(this.transform.position.x - enemy.transform.position.x);
            controller.grounded = false;
        }
    }
}
