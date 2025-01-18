using UnityEngine;
using FMODUnity;
using FMOD.Studio;

/// <summary>
/// AudioManager handles audio playback.
/// </summary>
public class AudioManager : MonoBehaviour {
    public static AudioManager Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update() {

    }

    public static void PlayOneShot(EventReference eventInstance) {
        RuntimeManager.PlayOneShot(eventInstance);
    }
}
