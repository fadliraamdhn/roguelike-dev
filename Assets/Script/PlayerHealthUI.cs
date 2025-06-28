using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    private Image fillImage;

    void Start()
    {
        fillImage = transform.Find("PlayerHealthbar/Fill")?.GetComponent<Image>();
        if (fillImage == null)
        {
            Debug.LogError("Fill image not found under PlayerHealthBar!");
        }
    }

    void Update()
    {
        if (playerHealth != null && fillImage != null)
        {
            fillImage.fillAmount = (float)playerHealth.CurrentHealth / playerHealth.maxHealth;
        }
    }
}
