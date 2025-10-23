using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System.Runtime.CompilerServices;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Absorption SFX")]
    [field: SerializeField] public EventReference absorption { get; private set; }

    private static FMODEvents _instance;
    public static FMODEvents Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            UnityEngine.Debug.Log("More than one AudioManager detected in scene.");
            Destroy(this.gameObject);
        }
        UnityEngine.Debug.Log("AudioManager assigned");
        _instance = this;
    }
}
