using UnityEngine;
using System.Collections.Generic;
using FMODUnity;
using FMOD.Studio;

/// <summary>
/// AudioManager handles audio playback.
/// </summary>
public class AudioManager : MonoBehaviour {
    private static readonly List<EventInstance> _eventInstances = new();
    public static AudioManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
            GameManager.OnGameStateChanged += TogglePauseGame;
        }
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

    public static void CleanUp() {
        foreach (EventInstance eventInstance in _eventInstances) {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
        _eventInstances.Clear();
    }

    private void TogglePauseGame(GameState state) {
        switch (state) {
            case GameState.Playing:
                // foreach (EventInstance eventInstance in _eventInstances) {
                //     eventInstance.setPaused(false);
                // }
                break;
            case GameState.Paused:
                // foreach (EventInstance eventInstance in _eventInstances) {
                //     eventInstance.setPaused(true);
                // }
                break;
        }
    }

    public void OnDestroy() {
        CleanUp();
        GameManager.OnGameStateChanged -= TogglePauseGame;
    }
}
