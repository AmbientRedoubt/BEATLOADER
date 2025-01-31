using UnityEngine;

public class PlayerInputHandler : MonoBehaviour {
    private int _nextInputIndex = 0;
    [Tooltip("Time in seconds which the players input is still counted before/after the beat.")]
    [SerializeField] private float _inputWindow = 0.2f;
    [SerializeField] private bool _canQuickRestart = false;
    [SerializeField] private RhythmTrack _rhythmTrack;

    private void Update() {
        if (GameManager.CurrentState == GameState.Paused) { return; }

        // No more inputs to check
        if (_nextInputIndex >= _rhythmTrack.NoteInputs.Length) { return; }

        float currentTime = LevelManager.CurrentTime;
        NoteInput nextNote = _rhythmTrack.NoteInputs[_nextInputIndex];

        // Check if the player hit the note
        if (IsNoteHit(nextNote, currentTime)) {
            // Debug.Log($"Hit! Action: {nextNote.InputAction.name} at {currentTime}");
            PlayerEvents.OnNoteHit?.Invoke(nextNote);
            _nextInputIndex++;
        }

        // Missed the note
        else if (IsNoteMissed(nextNote, currentTime)) {
            // Debug.Log($"Missed! Action: {nextNote.InputAction.name} at {currentTime}");
            PlayerEvents.OnNoteMiss?.Invoke();
            _nextInputIndex++;
        }
    }

    private bool IsNoteHit(NoteInput noteInput, float currentTime) {
        bool isHit = Mathf.Abs(noteInput.Time - currentTime) <= _inputWindow;
        bool isCorrectInput = noteInput.InputAction.action.triggered;
        return isHit && isCorrectInput;
    }

    private bool IsNoteMissed(NoteInput noteInput, float currentTime) {
        return currentTime > noteInput.Time + _inputWindow;
    }

    private void OnUp() {
        // Debug.Log("Up");
        CameraShakeManager.Shake(ShakeType.Up);
        // CameraShakeManager.UpShaker();
    }

    private void OnDown() {
        // Debug.Log("Down");
        CameraShakeManager.Shake(ShakeType.Down);
    }

    private void OnSpace() {
        // Debug.Log("Space");
        CameraShakeManager.Shake(ShakeType.Miss);
    }

    private void OnEnter() {
        // Debug.Log("Enter");
        CameraShakeManager.Shake(ShakeType.Miss);
    }

    private void OnRestart() {
        if (_canQuickRestart) {
            SceneLoader.RestartScene();
            GameManager.UpdateGameState(GameState.Playing);
        }
    }

    private void OnPause() {
        //! NASTY HACK
        if (GameManager.CurrentState == GameState.Paused) {
            LevelMenuManager.Instance.OnResumeButtonClicked();
        }
        else {
            GameManager.TogglePauseGame();
        }
    }
}
