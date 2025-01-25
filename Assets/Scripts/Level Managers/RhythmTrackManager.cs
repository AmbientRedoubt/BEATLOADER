using System;
using System.Collections.Generic;
using UnityEngine;

public class RhythmTrackManager : MonoBehaviour {
    public static float TrackStartTime { get; private set; }
    // Cannot serialise static properties so we're using instances to get a reference to the RhythmTrack (annoying!)
    private float _currentTime;
    private int _nextInputIndex = 0;
    private float _timer = 0f;
    private float _timeInterval;
    [SerializeField] private int _frameRate;
    [SerializeField] private float _noteSpeed = 0.4f;
    [SerializeField] private float _telegraphWindow = 2f;
    [Tooltip("Time in seconds the next note appears before it's to be played.")]
    [SerializeField] private GameObject _notesParent;
    [SerializeField] private GameObject _notePrefab;
    [field: SerializeField] public RhythmTrack RhythmTrack { get; private set; }
    [SerializeField] private List<InstantiatedNote> _notes = new();
    public static RhythmTrackManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        TrackStartTime = Time.time;
        _timeInterval = 1f / _frameRate;
    }

    private void Update() {
        if (GameManager.CurrentState == GameState.Paused) { return; }

        _currentTime = Time.time - TrackStartTime;
        _timer += Time.deltaTime;

        if (_timer >= _timeInterval) {
            _timer -= _timeInterval;
            UpdateNoteList();
        }

        if (_nextInputIndex >= RhythmTrack.NoteInputs.Length) { return; }
        NoteInput nextExpectedNote = RhythmTrack.NoteInputs[_nextInputIndex];
        float nextExpectedNoteAmplitude = nextExpectedNote.Amplitude;

        if (Mathf.Abs(nextExpectedNote.Time - _currentTime) <= _telegraphWindow) {
            GameObject note = Instantiate(_notePrefab, new Vector3(13, nextExpectedNoteAmplitude, 0), Quaternion.identity, _notesParent.transform);
            _notes.Add(new InstantiatedNote { NoteObject = note, Time = nextExpectedNote.Time });
            Debug.Log($"Telegraph! Action: {nextExpectedNote.InputAction.name} at {_currentTime}");
            _nextInputIndex++;
        }
    }

    private void UpdateNoteList() {
        // Iterate backwards since removing elements from a list while iterating forwards causes iterator issues
        for (int i = _notes.Count - 1; i >= 0; i--) {
            InstantiatedNote note = _notes[i];

            if (_currentTime > note.Time) {
                Destroy(note.NoteObject);
                _notes.RemoveAt(i);
            }

            else {
                Vector3 position = note.NoteObject.transform.position;
                position.x -= _noteSpeed;
                note.NoteObject.transform.position = position;
            }
        }
    }
}
