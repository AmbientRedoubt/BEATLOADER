using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
// using FMOD;
using FMODUnity;
using FMOD.Studio;

/// <summary>
/// AudioManager handles audio playback.
/// </summary>
public class AudioManager : MonoBehaviour {
    private static readonly List<EventInstance> _eventInstances = new();
    // private EVENT_CALLBACK _eventCallback;
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

    public static void PlayOneShot(EventReference eventReference) {
        RuntimeManager.PlayOneShot(eventReference);
    }

    // Used for events that need to be stopped and released immediately after playing
    // In our case, this is used for the RhythmTrackNotes event
    // CATCH AND RELEASE 🦅
    public static void CreateAndReleaseEvent(EventReference eventReference, float eventLength) {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstance.start();
        Instance.StartCoroutine(Instance.StopEventAfterDuration(eventInstance, eventLength)); // Convert ms to seconds
        // Instance.StartCoroutine(Instance.StopEventAfterDuration(eventInstance, eventLength / 1000f)); // Convert ms to seconds
    }

    private IEnumerator StopEventAfterDuration(EventInstance eventInstance, float duration) {
        yield return new WaitForSeconds(duration);
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        eventInstance.release();
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
