using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class NoteWaveformManager : MonoBehaviour {
    private float _currentTime;
    private float _oneFrameTime;
    private int _nextNoteIndex = 0;
    private float _elapsedFrameTime = 0f;
    [SerializeField] private RhythmTrack _rhythmTrack;
    [SerializeField] private float _speed = 0.65f;
    [Tooltip("Time in seconds the next note appears before it's to be played.")]
    [SerializeField] private float _telegraphWindow = 3f;
    [Header("Waveform Information")]
    [SerializeField] private List<WaveformSO> _waveformsSO;
    [SerializeField] private GameObject _waveformParent;
    [SerializeField] private List<GameObject> _waveformObjects = new();
    [Header("New Note Information")]
    [SerializeField] private float _newGameObjectPositionX = 13f;
    [SerializeField] private GameObject _notesParent;
    [SerializeField] private GameObject _notePrefab;
    [SerializeField] private List<InstantiatedNote> _instantiatedNotes = new();

    private void Start() {
        _oneFrameTime = 1f / LevelManager.FrameRate;
    }

    private void Update() {
        if (GameManager.CurrentState == GameState.Paused || GameManager.CurrentState == GameState.GameOver) { return; }

        _currentTime = LevelManager.CurrentTime;
        _elapsedFrameTime += Time.deltaTime;

        NoteInput nextNote = _rhythmTrack.NoteInputs[_nextNoteIndex];

        // TryInstantiateWaveform();

        // Wait until the next frame
        if (_elapsedFrameTime >= _oneFrameTime) {
            _elapsedFrameTime -= _oneFrameTime;
            UpdateNotes();
            UpdateWaveforms();
        }

        // No more notes to instantiate
        if (_nextNoteIndex >= _rhythmTrack.NoteInputs.Length) { return; }

        // Check if we should instantiate the next note
        if (IsNoteWithinTelegraphWindow(nextNote)) {
            GameObject newNote = InstantiateNewNote(nextNote.Amplitude);
            _instantiatedNotes.Add(new InstantiatedNote { NoteObject = newNote, ExpiryTime = nextNote.Time });
            // Debug.Log($"Telegraph! Action: {nextNote.InputAction.name} at {_currentTime}");
            _nextNoteIndex++;
        }
    }

    private void TryInstantiateWaveform() {
        NoteInput[] nextSevenNotes = _rhythmTrack.NoteInputs.Next(_nextNoteIndex, 7);
        if (nextSevenNotes.Length == 0) { return; }

        NoteInput nextNote = nextSevenNotes[0];

        foreach (NoteInput note in _rhythmTrack.NoteInputs) {
            float nextNoteTelegraphTime = note.Time - _currentTime - _telegraphWindow - _oneFrameTime;
            if (nextNoteTelegraphTime < 0) { continue; }


            float nextNoteAmplitude = note.Amplitude;

            Debug.Log($"Next note time: {nextNoteTelegraphTime}, Frame time x 7: {_oneFrameTime * 7}");

            switch (nextNoteTelegraphTime) {
                case float time when time > _oneFrameTime * 7:
                    return;

                case float time when time < _oneFrameTime * 3 && nextNoteAmplitude < 0:
                    Debug.Log("Instantiate waveform 3 down");
                    _waveformObjects.Add(InstantiateNewWaveform(_waveformsSO[0].Waveforms));
                    return;

                case float time when time < _oneFrameTime * 3 && nextNoteAmplitude >= 0:
                    Debug.Log("Instantiate waveform 3 up");
                    _waveformObjects.Add(InstantiateNewWaveform(_waveformsSO[1].Waveforms));
                    return;

                case float time when time < _oneFrameTime * 4 && nextNoteAmplitude < 0:
                    Debug.Log("Instantiate waveform 4 down");
                    _waveformObjects.Add(InstantiateNewWaveform(_waveformsSO[2].Waveforms));
                    return;

                case float time when time < _oneFrameTime * 4 && nextNoteAmplitude >= 0:
                    Debug.Log("Instantiate waveform 4 up");
                    _waveformObjects.Add(InstantiateNewWaveform(_waveformsSO[3].Waveforms));
                    return;

                case float time when time < _oneFrameTime * 5 && nextNoteAmplitude < 0:
                    Debug.Log("Instantiate waveform 5 down");
                    _waveformObjects.Add(InstantiateNewWaveform(_waveformsSO[4].Waveforms));
                    return;

                case float time when time < _oneFrameTime * 5 && nextNoteAmplitude >= 0:
                    Debug.Log("Instantiate waveform 5 up");
                    _waveformObjects.Add(InstantiateNewWaveform(_waveformsSO[5].Waveforms));
                    return;

                case float time when time < _oneFrameTime * 7 && nextNoteAmplitude < 0:
                    Debug.Log("Instantiate waveform 7 down");
                    _waveformObjects.Add(InstantiateNewWaveform(_waveformsSO[6].Waveforms));
                    return;

                case float time when time < _oneFrameTime * 7 && nextNoteAmplitude >= 0:
                    Debug.Log("Instantiate waveform 7 up");
                    _waveformObjects.Add(InstantiateNewWaveform(_waveformsSO[7].Waveforms));
                    return;
            }
        }
    }

    private GameObject InstantiateNewWaveform(List<GameObject> waveforms) {
        Vector2 waveformPosition = new(_newGameObjectPositionX, 0);
        GameObject waveform = waveforms.Rand();
        waveform = Instantiate(waveform, waveformPosition, Quaternion.identity, _waveformParent.transform);
        return waveform;
    }

    public void UpdateNotes() {
        // Iterate backwards since removing elements from a list while iterating forwards causes iterator issues
        for (int i = _instantiatedNotes.Count - 1; i >= 0; i--) {
            InstantiatedNote note = _instantiatedNotes[i];
            if (_currentTime > note.ExpiryTime) {
                FadeAndDestroy(note.NoteObject);
                _instantiatedNotes.RemoveAt(i);
            }
            else {
                UpdatePosition(note.NoteObject);
            }
        }
    }

    public void UpdateWaveforms() {
        for (int i = _waveformObjects.Count - 1; i >= 0; i--) {
            GameObject waveform = _waveformObjects[i];
            if (waveform.transform.position.x < -10) {
                Destroy(waveform);
                _waveformObjects.RemoveAt(i);
            }
            else {
                UpdatePosition(waveform);
            }
        }
    }

    // I should've lerped this on second thought
    private void UpdatePosition(GameObject gameObject) {
        Vector2 position = gameObject.transform.position;
        position.x -= _speed;
        gameObject.transform.position = position;
    }

    private GameObject InstantiateNewNote(float amplitude) {
        Vector2 notePosition = new(_newGameObjectPositionX, amplitude);
        GameObject note = Instantiate(_notePrefab, notePosition, Quaternion.identity, _notesParent.transform);
        return note;
    }

    private bool IsNoteWithinTelegraphWindow(NoteInput noteInput) {
        return Mathf.Abs(noteInput.Time - _currentTime) <= _telegraphWindow;
    }

    private void FadeAndDestroy(GameObject noteObject) {
        // Destroying the gameObject is handled by the NoteController animation Event
        noteObject.GetComponent<NoteController>().FadeOut();
    }
}
