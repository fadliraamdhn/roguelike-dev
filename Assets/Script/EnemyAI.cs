using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    private Transform player;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // Flip based on horizontal direction
        if (direction.x > 0.01f)
            spriteRenderer.flipX = false;  // Facing right
        else if (direction.x < -0.01f)
            spriteRenderer.flipX = true;   // Facing left
    }
}
