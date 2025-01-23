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
    public static PlayerInputManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    private void OnEnable() {
        // GameManager.OnGameStateChanged += TogglePauseGame;
    }

    private void OnDisable() {
        // GameManager.OnGameStateChanged -= TogglePauseGame;
    }

    private void Start() {

    }

    private void Update() {

    }

    private void OnUp() {
        Debug.Log("Up");
        Shaker.ShakeAll(_jumpShake);
    }

    private void OnDown() {
        Debug.Log("Down");
        Shaker.ShakeAll(_jumpShake);
    }

    private void OnSpace() {
        // AudioManager.PlayOneShot(Instance._jumpSound);
        Debug.Log("Space");
        Shaker.ShakeAll(_jumpShake);
    }

    private void OnAttack() {
        // Likely we won't need this action but keeping for testing purposes
        // AudioManager.PlayOneShot(Instance._attackSound);
        Debug.Log("Attack");
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
