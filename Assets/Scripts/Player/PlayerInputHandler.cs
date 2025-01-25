using UnityEngine;

public class PlayerInputHandler : MonoBehaviour {
    private int _nextInputIndex = 0;
    [Tooltip("Time in seconds which the players input is still counted before/after the beat.")]
    [SerializeField] private float _inputWindow = 0.2f;
    private RhythmTrack _rhythmTrack;

    private void Start() {
        _rhythmTrack = RhythmTrackManager.Instance.RhythmTrack;
    }

    private void Update() {
        // No more inputs to check
        if (_nextInputIndex >= _rhythmTrack.NoteInputs.Length) { return; }

        float currentTime = Time.time - RhythmTrackManager.TrackStartTime;
        NoteInput nextExpectedNote = _rhythmTrack.NoteInputs[_nextInputIndex];

        if (Mathf.Abs(nextExpectedNote.Time - currentTime) <= _inputWindow) {
            if (nextExpectedNote.InputAction.action.triggered) {
                Debug.Log($"Hit! Action: {nextExpectedNote.InputAction.name} at {currentTime}");
                _nextInputIndex++;
                PlayerEvents.OnNoteHit?.Invoke();
            }
        }

        else if (currentTime > nextExpectedNote.Time + _inputWindow) {
            Debug.Log($"Missed! Action: {nextExpectedNote.InputAction.name} at {currentTime}");
            _nextInputIndex++;
            PlayerEvents.OnNoteMiss?.Invoke();
        }
    }

    private void OnUp() {
        // Debug.Log("Up");
        CameraShakeManager.JumpShaker();
    }

    private void OnDown() {
        // Debug.Log("Down");
        CameraShakeManager.JumpShaker();
    }

    private void OnSpace() {
        // Debug.Log("Space");
        CameraShakeManager.JumpShaker();
    }

    private void OnEnter() {
        // Debug.Log("Enter");
        CameraShakeManager.CrashShaker();
    }

    private void OnPause() {
        //! NASTY HACK
        if (GameManager.CurrentState == GameState.Paused) {
            PauseMenuManager.Instance.OnResumeButtonClicked();
        }
        else {
            GameManager.TogglePauseGame();
        }
    }
}
