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
            GameManager.OnGameStateChanged += TogglePauseGame;
        }
    }

    private void Start() {
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

    public static void CleanUpOne(EventInstance eventInstance, FMOD.Studio.STOP_MODE stopMode) {
        if (stopMode == FMOD.Studio.STOP_MODE.IMMEDIATE) {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        else if (stopMode == FMOD.Studio.STOP_MODE.ALLOWFADEOUT) {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        eventInstance.release();
    }

    public static void CleanUp() {
        // Debug.Log("Cleaning up audio");
        if (_eventInstances.Count == 0) return;
        foreach (EventInstance eventInstance in _eventInstances) {
            CleanUpOne(eventInstance, FMOD.Studio.STOP_MODE.IMMEDIATE);
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
        foreach (EventInstance eventInstance in _eventInstances) {
            eventInstance.release();
        }
        GameManager.OnGameStateChanged -= TogglePauseGame;
    }
}
