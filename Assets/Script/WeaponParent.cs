using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    private float rotationOffset = 0f;
    [SerializeField] private Transform weaponGraphics; // The object to flip (usually the child with the sprite)
    private Vector3 originalScale;

    void Start()
    {
        if (weaponGraphics != null)
            originalScale = weaponGraphics.localScale;
    }
    void Update()
    {
        if (Camera.main == null) return;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mouseWorldPos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset);
    }
}
