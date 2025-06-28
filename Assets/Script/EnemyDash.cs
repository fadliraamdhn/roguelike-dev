using System.Collections;
using UnityEngine;

public class EnemyDash : MonoBehaviour
{
    public float detectionRange = 5f;
    public float dashSpeed = 10f;
    public float windUpTime = 0.3f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 2f;
    public int damage = 1;

    private Transform player;
    private Rigidbody2D rb;
    private bool isDashing = false;
    private bool canDash = true;

    [SerializeField] private Animator animator; // <-- Drag your Animator here in the inspector

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isDashing && canDash && Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            StartCoroutine(DashAttack());
        }
    }

    private IEnumerator DashAttack()
    {
        canDash = false;
        
        isDashing = true;

        if (animator != null)
            animator.SetTrigger("Dash"); // Create "Dash" trigger in Animator

        Vector2 dashDirection = (player.position - transform.position).normalized;
        rb.linearVelocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        rb.linearVelocity = Vector2.zero;
        isDashing = false;

        if (animator != null)
            animator.SetTrigger("Idle"); // Optional: revert to idle if needed

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDashing && other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}
