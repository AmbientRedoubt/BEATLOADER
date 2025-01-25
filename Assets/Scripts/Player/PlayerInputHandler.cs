using UnityEngine;
using MilkShake;

public class PlayerInputHandler : MonoBehaviour {
    private int _nextInputIndex = 0;
    [Tooltip("Time in seconds which the players input is still counted before/after the beat.")]
    [SerializeField] private float _inputWindow = 0.2f;
    [SerializeField] private ShakePreset _crashShake;
    [SerializeField] private ShakePreset _jumpShake;
    [SerializeField] private RhythmTrack _rhythmTrack;

    private void Start() {
    }

    private void Update() {
        // No more inputs to check
        if (_nextInputIndex >= _rhythmTrack.KeyInputs.Length) { return; }

        float currentTime = Time.time - RhythmTrackManager.TrackStartTime;
        KeyInput nextExpectedInput = _rhythmTrack.KeyInputs[_nextInputIndex];

        if (Mathf.Abs(nextExpectedInput.Time - currentTime) <= _inputWindow) {
            if (nextExpectedInput.InputAction.action.triggered) {
                Debug.Log($"Hit! Action: {nextExpectedInput.InputAction.name} at {currentTime}");
                _nextInputIndex++;
                PlayerEvents.OnNoteHit?.Invoke();
            }
        }

        else if (currentTime > nextExpectedInput.Time + _inputWindow) {
            Debug.Log($"Missed! Action: {nextExpectedInput.InputAction.name} at {currentTime}");
            _nextInputIndex++;
            PlayerEvents.OnNoteMiss?.Invoke();
        }
    }

    private void OnUp() {
        // Debug.Log("Up");
        Shaker.ShakeAll(_jumpShake);
    }

    private void OnDown() {
        // Debug.Log("Down");
        Shaker.ShakeAll(_jumpShake);
    }

    private void OnSpace() {
        // AudioManager.PlayOneShot(Instance._jumpSound);
        // PlayerAudio.OnJump();
        // Debug.Log("Space");
        Shaker.ShakeAll(_jumpShake);
    }

    private void OnEnter() {
        // AudioManager.PlayOneShot(Instance._attackSound);
        // Debug.Log("Enter");
        Shaker.ShakeAll(_crashShake);
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
