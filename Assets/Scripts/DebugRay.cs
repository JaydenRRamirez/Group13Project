using UnityEngine;

public class DebugRay : MonoBehaviour
{
    public float rayLength = 50f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * rayLength, Color.red);
    }
}

