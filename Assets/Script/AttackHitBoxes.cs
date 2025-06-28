using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    public int damage = 1;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {   
                // Direction from player to enemy
                Vector2 knockbackDir = other.transform.position - playerTransform.position;
                enemy.TakeDamage(damage, knockbackDir);
            }
        }
    }
}

