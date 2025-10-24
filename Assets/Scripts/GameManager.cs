using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    [SerializeField] private Volume globalVolume;
    private Vignette vignette;

    public Camera playerCamera;
    public float rayDistance = 5f;
    public GameObject hoverUI;

    public int wispsAbsorbed = 0;

    private GameObject currentTarget;

    void Start()
    {
        // Try to find the vignette override
        if (globalVolume != null && globalVolume.profile.TryGet(out vignette))
        {
            Debug.Log("Vignette found!");
        }
        else
        {
            Debug.LogWarning("No Vignette found on Volume Profile!");
        }
    }
    
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
            Interactable interactable = currentTarget.GetComponent<Interactable>();

            if (interactable != null)
            {
                Vector3 playerPos = playerCamera.transform.position;
                interactable.OnInteract();
                wispsAbsorbed++;

                // Safety check
                if (vignette != null)
                {
                    SetVignetteIntensity(vignette.intensity.value + 0.1f);
                }
            }
        }
    }

    public void SetVignetteIntensity(float intensity)
    {
        if (vignette != null)
        {
            vignette.intensity.value = Mathf.Clamp01(intensity);
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