using UnityEngine;

public class HoverUI : MonoBehaviour
{
    public Camera playerCamera;
    public float rayDistance = 5f;
    public GameObject hoverUI; // The UI element to toggle

    private GameObject currentTarget;

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Only react if the object has the "Interactable" tag
            if (hitObject.CompareTag("Interactable"))
            {
                if (hitObject != currentTarget)
                {
                    currentTarget = hitObject;
                    ShowUI(true);
                }
            }
            else
            {
                // Hit something else, hide UI if needed
                if (currentTarget != null)
                {
                    currentTarget = null;
                    ShowUI(false);
                }
            }
        }
        else
        {
            // Not hitting anything
            if (currentTarget != null)
            {
                currentTarget = null;
                ShowUI(false);
            }
        }
    }

    void ShowUI(bool state)
    {
        hoverUI.SetActive(state);
    }
}