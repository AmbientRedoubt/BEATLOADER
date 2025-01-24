using System.ComponentModel;
using MilkShake;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputManager : MonoBehaviour {
    [Description("Time in seconds which the players input is still counted before/after the beat.")]
    [SerializeField] private float _inputWindow = 0.2f;
    [SerializeField] private ShakePreset _crashShake;
    [SerializeField] private ShakePreset _jumpShake;
    [SerializeField] private RhythmTrack _rhythmTrack;
    private float _trackStartTime;
    private int _nextInputIndex = 0;
    public static PlayerInputManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    private void Start() {
        _trackStartTime = Time.time;
    }

    private void Update() {
        // No more inputs to check
        if (_nextInputIndex >= _rhythmTrack.KeyInputs.Length) { return; }

        float currentTime = Time.time - _trackStartTime;
        KeyInput nextExpectedInput = _rhythmTrack.KeyInputs[_nextInputIndex];

        if (Mathf.Abs(nextExpectedInput.Time - currentTime) <= _inputWindow) {
            if (nextExpectedInput.InputAction.action.triggered) {
                Debug.Log($"Hit! Action: {nextExpectedInput.InputAction.name} at {currentTime}");
                _nextInputIndex++;
            }
        }

        else if (currentTime > nextExpectedInput.Time + _inputWindow) {
            Debug.Log($"Missed! Action: {nextExpectedInput.InputAction.name} at {currentTime}");
            _nextInputIndex++;
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
