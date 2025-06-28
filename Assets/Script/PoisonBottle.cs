using UnityEngine;

public class PoisonBottle : MonoBehaviour
{
    public GameObject poisonPuddlePrefab;
    public float lifetime = 1.5f; // how long before it hits the ground automatically

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CreatePuddle();
    }

    void CreatePuddle()
    {
        Instantiate(poisonPuddlePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
