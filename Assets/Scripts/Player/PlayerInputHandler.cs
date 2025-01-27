using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour {
    private int _nextInputIndex = 0;
    [Tooltip("Time in seconds which the players input is still counted before/after the beat.")]
    [SerializeField] private float _inputWindow = 0.2f;
    [SerializeField] private bool _canQuickRestart = false;
    private RhythmTrack _rhythmTrack;

    private void Start() {
        _rhythmTrack = RhythmTrackManager.Instance.RhythmTrack;
    }

    private void Update() {
        if (GameManager.CurrentState == GameState.Paused) { return; }

        // No more inputs to check
        if (_nextInputIndex >= _rhythmTrack.NoteInputs.Length) { return; }

        float currentTime = Time.time - RhythmTrackManager.TrackStartTime;
        NoteInput nextExpectedNote = _rhythmTrack.NoteInputs[_nextInputIndex];

        if (Mathf.Abs(nextExpectedNote.Time - currentTime) <= _inputWindow) {
            if (nextExpectedNote.InputAction.action.triggered) {
                // Debug.Log($"Hit! Action: {nextExpectedNote.InputAction.name} at {currentTime}");
                PlayerEvents.OnNoteHit?.Invoke(nextExpectedNote);
                _nextInputIndex++;
            }
        }

        else if (currentTime > nextExpectedNote.Time + _inputWindow) {
            // Debug.Log($"Missed! Action: {nextExpectedNote.InputAction.name} at {currentTime}");
            PlayerEvents.OnNoteMiss?.Invoke();
            _nextInputIndex++;
        }
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
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            GameManager.UpdateGameState(GameState.Playing);
        }
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
