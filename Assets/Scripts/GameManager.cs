using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    public Camera playerCamera;
    public float rayDistance = 5f;
    public GameObject hoverUI;

    private GameObject currentTarget;

    void Update()
    {
        HandleHover();
        HandleClick();
    }

    void HandleHover()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("Interactable"))
            {
                if (hitObject != currentTarget)
                {
                    currentTarget = hitObject;
                    hoverUI.SetActive(true);
                }
            }
            else
            {
                ClearHover();
            }
        }
        else
        {
            ClearHover();
        }
    }

    void HandleClick()
    {
        if (Input.GetMouseButtonDown(0) && currentTarget != null)
        {
            // If the object has an Interactable component, tell it to react
            Interactable interactable = currentTarget.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.OnInteract();
            }
        }
    }

    void ClearHover()
    {
        if (currentTarget != null)
        {
            currentTarget = null;
            hoverUI.SetActive(false);
        }
    }
}