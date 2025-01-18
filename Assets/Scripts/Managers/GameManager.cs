using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>
/// GameManager handles the game state.
/// </summary>
public class GameManager : MonoBehaviour {
    public static GameState CurrentState { get; private set; }
    public static event Action<GameState> OnGameStateChanged;
    public static GameManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        // DisableMouseCursor();
        UpdateGameState(GameState.MainMenu);
    }

    private void DisableMouseCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UpdateGameState(GameState newState) {
        CurrentState = newState;

        switch (newState) {
            case GameState.MainMenu:
                break;
            case GameState.Playing:
                TogglePauseGame();
                break;
            case GameState.Paused:
                TogglePauseGame();
                break;
            case GameState.GameOver:
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }

    private void TogglePauseGame() {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
}

public enum GameState {
    MainMenu,
    Playing,
    Paused,
    GameOver
}