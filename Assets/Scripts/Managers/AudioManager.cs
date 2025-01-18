using UnityEngine;
using System.Collections.Generic;
using FMODUnity;
using FMOD.Studio;
using UnityEditor;
using System;

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
        GameManager.OnGameStateChanged += TogglePauseGame;
        _eventInstances = new List<EventInstance>();
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

    private void TogglePauseGame(GameState state) {
        if (state == GameState.Paused) {
            foreach (EventInstance eventInstance in _eventInstances) {
                eventInstance.setPaused(true);
            }
        }

        else {
            foreach (EventInstance eventInstance in _eventInstances) {
                eventInstance.setPaused(false);
            }
        }
    }

    public void OnDestroy() {
        foreach (EventInstance eventInstance in _eventInstances) {
            eventInstance.release();
        }
        GameManager.OnGameStateChanged -= TogglePauseGame;
    }
}
