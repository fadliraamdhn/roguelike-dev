using UnityEngine;
using System.Collections;

public class EnemyThrow : MonoBehaviour
{
    public GameObject poisonBottlePrefab;
    public float throwCooldown = 3f;
    public float throwForce = 10f;
    public float throwAnimationDuration = 0.5f; // Set this to match your animation length

    private Transform player;
    private float lastThrowTime;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time >= lastThrowTime + throwCooldown)
        {
            StartCoroutine(ThrowAtPlayer());
            lastThrowTime = Time.time;
        }
    }

    IEnumerator ThrowAtPlayer()
    {
        if (player == null) yield break;

        if (animator != null)
            animator.SetTrigger("Throw");

        Vector2 direction = (player.position - transform.position).normalized;
        GameObject bottle = Instantiate(poisonBottlePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bottle.GetComponent<Rigidbody2D>();

        if (rb != null)
            rb.linearVelocity = direction * throwForce;

        // Wait for the animation to finish, then go back to Idle
        yield return new WaitForSeconds(throwAnimationDuration);

        if (animator != null)
            animator.SetTrigger("Idle");
    }
}
