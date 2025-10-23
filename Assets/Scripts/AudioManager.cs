using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    
    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake() {
        if (_instance != null && _instance != this) {
            UnityEngine.Debug.Log("More than one AudioManager detected in scene.");
            Destroy(this.gameObject); 
        }
        UnityEngine.Debug.Log("AudioManager assigned");
        _instance = this;      
    }

    public void PlayOneShot(EventReference sound, Vector3 pos) {
        // sound var passes in event from FMOD
        // pos var passes in a vector3 of the world position from which sound plays
        RuntimeManager.PlayOneShot(sound, pos);
    }
}
