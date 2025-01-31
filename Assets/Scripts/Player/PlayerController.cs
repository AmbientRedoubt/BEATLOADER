using System;
using System.Collections.Generic;
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
    [SerializeField] private List<GameObject> _glitchEffects;

    private void Awake() {
        PlayerEvents.OnNoteMiss += TakeDamage;
    }

    public void TakeDamage() {
        if (_health > 0) {
            DisplayRandomGlitchEffect();
        }

        if (_health == 0) {
            GameManager.UpdateGameState(GameState.GameOver);
        }
        _health--;
    }

    /// <summary>
    /// Display two random glitch effects when the player takes damage.
    /// </summary>
    private void DisplayRandomGlitchEffect() {
        if (!GameSettingsManager.FlashEffectsEnabled) { return; }

        for (int i = 0; i < 2; i++) {
            var glitchEffect = _glitchEffects.Rand(); // Extension method
            _glitchEffects.Remove(glitchEffect);
            glitchEffect.SetActive(true);
        }
    }

    private void OnDestroy() {
        PlayerEvents.OnNoteMiss -= TakeDamage;
    }
}
