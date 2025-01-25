using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerAudioHandler))]
[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerMovementController))]
public class PlayerController : MonoBehaviour {
    [SerializeField] private SpriteRenderer _playerSprite;
    [SerializeField] private PlayerAudioHandler _playerAudio;
    [SerializeField] private PlayerInputHandler _playerInput;
    [SerializeField] private PlayerMovementController _playerMovement;
    public static PlayerController Instance { get; private set; }

    private void Start() {

    }
}
