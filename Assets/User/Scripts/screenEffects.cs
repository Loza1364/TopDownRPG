using System.Collections;
using UnityEngine;

public class screenEffects : MonoBehaviour
{
    public bool start = false;
    public float duration = 1f;
    public AnimationCurve curve;

    private void Start()
    {
        start = true;
    }
    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }
    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float strength = curve.Evaluate(elapsed / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPosition;
    }
}
