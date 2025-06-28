using System.Collections;
using UnityEngine;

public class PoisonPuddle : MonoBehaviour
{
    public float duration = 3f;
    public int damage = 1;
    public float damageInterval = 1f;

    private Coroutine damageCoroutine;

    private void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && damageCoroutine == null)
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                damageCoroutine = StartCoroutine(DamageOverTime(health));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    private IEnumerator DamageOverTime(PlayerHealth player)
    {
        while (player != null && player.gameObject.activeInHierarchy)
        {
            player.TakeDamage(damage);
            yield return new WaitForSeconds(damageInterval);
        }

        damageCoroutine = null;
    }
}