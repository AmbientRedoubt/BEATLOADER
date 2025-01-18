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

    private static void ToggleMouseCursor() {
        if (CurrentState != GameState.Playing) {
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
        ToggleMouseCursor();

        switch (newState) {
            case GameState.MainMenu:
                break;
            case GameState.Playing:
                Time.timeScale = 1;
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                break;
            case GameState.GameOver:
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