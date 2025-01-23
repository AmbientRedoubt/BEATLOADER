using UnityEngine;
using System;

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
        }
    }

    private void Start() {
        // DisableMouseCursor();
        // UpdateGameState(GameState.MainMenu);
    }

    private static void ToggleMouseCursor(MouseState state) {
        if (state == MouseState.Enabled) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public static void UpdateGameState(GameState newState) {
        CurrentState = newState;

        switch (newState) {
            case GameState.MainMenu:
                Time.timeScale = 1;
                ToggleMouseCursor(MouseState.Enabled);
                break;
            case GameState.Playing:
                Time.timeScale = 1;
                ToggleMouseCursor(MouseState.Disabled);
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                ToggleMouseCursor(MouseState.Enabled);
                break;
            case GameState.GameOver:
                Time.timeScale = 1;
                ToggleMouseCursor(MouseState.Enabled);
                break;
        }
        OnGameStateChanged?.Invoke(newState);
        Debug.Log($"Game state changed to {newState}");
    }

    public static void TogglePauseGame() {
        if (CurrentState == GameState.Paused) {
            UpdateGameState(GameState.Playing);
        }
        else {
            UpdateGameState(GameState.Paused);
        }
    }
}

public enum GameState {
    MainMenu,
    Playing,
    Paused,
    GameOver
}

public enum MouseState {
    Enabled,
    Disabled
}