using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator weaponAnimator; // Weapon Animator
    [SerializeField] private float attackCooldown = 0.6f;
    private float lastAttackTime;
    [SerializeField] private Collider2D attackHitbox;
    [SerializeField] private float attackActiveTime = 0.6f; // seconds hitbox is active

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get keyboard input
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        movement = new Vector2(inputX, inputY).normalized;

        animator.SetFloat("InputX", inputX);
        animator.SetFloat("InputY", inputY);
        animator.SetBool("Moving", movement != Vector2.zero);

        if (Input.GetMouseButtonDown(0) && weaponAnimator != null)
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                weaponAnimator.SetTrigger("Attack");
                lastAttackTime = Time.time;

                if (attackHitbox != null)
                {
                    StartCoroutine(ActivateHitbox());
                }
            }
        }
    }

    private IEnumerator ActivateHitbox()
    {
        attackHitbox.enabled = true;
        yield return new WaitForSeconds(attackActiveTime);
        attackHitbox.enabled = false;
    }

    void FixedUpdate()
    {
        // Apply movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
