using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeMagnitude = 0.2f;

    private Vector3 originalPosition;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        originalPosition = transform.localPosition;
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude;
            transform.localPosition = originalPosition + randomOffset;
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
