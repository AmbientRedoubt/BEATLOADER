using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour {
    private float _currentTime;
    private float _noteUpdateRate;
    private int _nextNoteIndex = 0;
    private float _elapsedNoteTime = 0f;
    [SerializeField] private RhythmTrack _rhythmTrack;
    [SerializeField] private float _noteSpeed = 0.65f;
    [Tooltip("Time in seconds the next note appears before it's to be played.")]
    [SerializeField] private float _telegraphWindow = 3f;
    [Header("New Note Information")]
    [SerializeField] private float _newNotePositionX = 13f;
    [SerializeField] private GameObject _notesParent;
    [SerializeField] private GameObject _notePrefab;
    [SerializeField] private List<InstantiatedNote> _instantiatedNotes = new();

    private void Start() {
        _noteUpdateRate = 1f / LevelManager.FrameRate;
    }

    private void Update() {
        if (GameManager.CurrentState == GameState.Paused || GameManager.CurrentState == GameState.GameOver) { return; }

        _currentTime = LevelManager.CurrentTime;
        _elapsedNoteTime += Time.deltaTime;

        if (_elapsedNoteTime >= _noteUpdateRate) {
            _elapsedNoteTime -= _noteUpdateRate;
            UpdateNoteList();
        }

        // No more notes to instantiate
        if (_nextNoteIndex >= _rhythmTrack.NoteInputs.Length) { return; }

        NoteInput nextNote = _rhythmTrack.NoteInputs[_nextNoteIndex];
        float nextNoteAmplitude = nextNote.Amplitude;

        // Check if we should instantiate the next note
        if (IsNoteWithinTelegraphWindow(nextNote)) {
            GameObject newNote = InstantiateNewNote(nextNoteAmplitude);
            _instantiatedNotes.Add(new InstantiatedNote { NoteObject = newNote, ExpiryTime = nextNote.Time });
            // Debug.Log($"Telegraph! Action: {nextNote.InputAction.name} at {_currentTime}");
            _nextNoteIndex++;
        }
    }

    public void UpdateNoteList() {
        // Iterate backwards since removing elements from a list while iterating forwards causes iterator issues
        for (int i = _instantiatedNotes.Count - 1; i >= 0; i--) {
            InstantiatedNote note = _instantiatedNotes[i];
            if (_currentTime > note.ExpiryTime) {
                FadeAndDestroy(note.NoteObject);
                _instantiatedNotes.RemoveAt(i);
            }
            else {
                UpdateNotePosition(note.NoteObject);
            }
        }
    }

    private void UpdateNotePosition(GameObject noteObject) {
        Vector2 position = noteObject.transform.position;
        position.x -= _noteSpeed;
        noteObject.transform.position = position;
    }

    private GameObject InstantiateNewNote(float amplitude) {
        var notePosition = new Vector2(_newNotePositionX, amplitude);
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
