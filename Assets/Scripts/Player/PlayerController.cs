using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerAudioHandler))]
[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerMovementController))]
public class PlayerController : MonoBehaviour {
    [SerializeField] private PlayerAudioHandler _playerAudio;
    [SerializeField] private PlayerInputHandler _playerInput;
    [SerializeField] private PlayerMovementController _playerMovement;
    [SerializeField] private int _health = 3;
    public static PlayerController Instance { get; private set; }

    private void Awake() {
        PlayerEvents.OnNoteMiss += TakeDamage;
        Instance = this;
    }

    private void Start() {

    }

    public void TakeDamage() {
        _health--;

        if (_health <= 0) {
            GameManager.UpdateGameState(GameState.GameOver);
        }
    }

    private void OnDestroy() {
        PlayerEvents.OnNoteMiss -= TakeDamage;
    }

}
