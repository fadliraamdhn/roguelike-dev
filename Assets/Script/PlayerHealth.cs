using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    [SerializeField] private float damageCooldown = 1f;
    private float lastDamageTime = -Mathf.Infinity;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (Time.time < lastDamageTime + damageCooldown)
            return;

        currentHealth -= damage;
        lastDamageTime = Time.time;

        // Trigger Camera Shake
        if (CameraShake.Instance != null)
            CameraShake.Instance.Shake();

        Debug.Log("Player took damage! Current HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log("Player died!");
        // Handle game over or respawn here
    }
    public int CurrentHealth => currentHealth;


}
