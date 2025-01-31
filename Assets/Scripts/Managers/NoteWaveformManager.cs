using System.Collections.Generic;
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
    [SerializeField] private WaveformSO _waveforms;
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

        // Wait until the next frame
        if (_elapsedFrameTime >= _oneFrameTime) {
            _elapsedFrameTime -= _oneFrameTime;
            UpdateNotes();
            var waveform = InstantiateNewWaveform(2);
            _waveformObjects.Add(waveform);

            foreach (var obj in _waveformObjects) {
                Debug.Log(obj);
                Debug.Log(obj.transform.position);
                UpdatePosition(obj);
            }
        }

        // No more notes to instantiate
        if (_nextNoteIndex >= _rhythmTrack.NoteInputs.Length) { return; }

        NoteInput nextNote = _rhythmTrack.NoteInputs[_nextNoteIndex];
        // Check if we should instantiate the next note
        if (IsNoteWithinTelegraphWindow(nextNote)) {
            GameObject newNote = InstantiateNewNote(nextNote.Amplitude);
            _instantiatedNotes.Add(new InstantiatedNote { NoteObject = newNote, ExpiryTime = nextNote.Time });
            // Debug.Log($"Telegraph! Action: {nextNote.InputAction.name} at {_currentTime}");
            _nextNoteIndex++;
        }
    }

    private GameObject InstantiateNewWaveform(float amplitude) {
        Vector2 waveformPosition = new(_newGameObjectPositionX, amplitude);
        GameObject waveform = _waveforms.Waveforms[0].GameObjects[0];
        waveform = Instantiate(waveform, waveformPosition, Quaternion.identity, _waveformParent.transform);
        // GameObject waveform = _waveforms.Waveforms[0].Type == WaveformType.Up ? Instantiate(_waveforms.Waveforms[0].GameObjects[0], position, Quaternion.identity, _notesParent.transform) : Instantiate(_waveforms.Waveforms[0].GameObjects[1], position, Quaternion.identity, _notesParent.transform);
        // GameObject waveform = Instantiate(_notePrefab, position, Quaternion.identity, _notesParent.transform);
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
