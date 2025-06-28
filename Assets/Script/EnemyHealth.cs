using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private Rigidbody2D rb;

    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackDuration = 0.2f;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color hurtColor = Color.red;
    [SerializeField] private float flashDuration = 0.1f;

    [Header("Death Effect")]
    [SerializeField] private GameObject deathEffectPrefab; // Assign in Inspector

    private Color originalColor;
    private Coroutine knockbackCoroutine;


    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    public void TakeDamage(int amount, Vector2 knockbackDirection)
    {
        currentHealth -= amount;
        StartCoroutine(FlashHurt());
        Knockback(knockbackDirection);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Knockback(Vector2 direction)
    {
        if (rb != null)
        {
            if (knockbackCoroutine != null)
                StopCoroutine(knockbackCoroutine);

            knockbackCoroutine = StartCoroutine(KnockbackCoroutine(direction));
        }
    }

    private IEnumerator KnockbackCoroutine(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * knockbackForce;
        yield return new WaitForSeconds(knockbackDuration);
        rb.linearVelocity = Vector2.zero;
    }

    private IEnumerator FlashHurt()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = hurtColor;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor;
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");

        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
