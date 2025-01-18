using UnityEngine;
using System.Collections.Generic;
using FMODUnity;
using FMOD.Studio;

/// <summary>
/// AudioManager handles audio playback.
/// </summary>
public class AudioManager : MonoBehaviour {
    private static List<EventInstance> _eventInstances;
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

    private void Start() {
        _eventInstances = new List<EventInstance>();
    }

    private void Update() {

    }

    public static void PlayOneShot(EventReference eventInstance) {
        RuntimeManager.PlayOneShot(eventInstance);
    }

    public static EventInstance CreateEventInstance(EventReference eventReference) {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }

    public static EventInstance CreateAndAddEventInstance(EventReference eventReference) {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        _eventInstances.Add(eventInstance);
        return eventInstance;
    }
}
