using System;
using UnityEngine;

public class RhythmTrackManager : MonoBehaviour {
    public static float TrackStartTime { get; private set; }
    // Cannot serialise static properties so we're using instances to get a reference to the RhythmTrack (annoying!)
    private int _nextInputIndex = 0;
    [Tooltip("Time in seconds the next note appears before it's to be played.")]
    [SerializeField] private float _telegraphWindow = 2f;
    [SerializeField] private GameObject _notePrefab;
    [field: SerializeField] public RhythmTrack RhythmTrack { get; private set; }
    public static RhythmTrackManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        TrackStartTime = Time.time;
    }

    private void Update() {
        if (_nextInputIndex >= RhythmTrack.NoteInputs.Length) { return; }

        float currentTime = Time.time - TrackStartTime;
        NoteInput nextExpectedNote = RhythmTrack.NoteInputs[_nextInputIndex];
        float nextExpectedNoteAmplitude = nextExpectedNote.Amplitude;

        if (Mathf.Abs(nextExpectedNote.Time - currentTime) <= _telegraphWindow) {
            Instantiate(_notePrefab, new Vector3(0, nextExpectedNoteAmplitude, 0), Quaternion.identity);
            Debug.Log($"Telegraph! Action: {nextExpectedNote.InputAction.name} at {currentTime}");
            _nextInputIndex++;
        }
    }
}
