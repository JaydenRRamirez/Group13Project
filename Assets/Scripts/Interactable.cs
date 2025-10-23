using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour
{
    public float shrinkSpeed = 2f; // Higher = faster

    private bool isShrinking = false;

    public void OnInteract()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.absorption, transform.position);
        if (!isShrinking)
        {
            StartCoroutine(ShrinkAndDeactivate());
        }
    }

    IEnumerator ShrinkAndDeactivate()
    {
        isShrinking = true;
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = Vector3.zero;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * shrinkSpeed;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }

        // Ensure final scale is exactly zero
        transform.localScale = Vector3.zero;

        // Deactivate the whole object (parent) so it no longer interacts
        gameObject.SetActive(false);
    }
}
