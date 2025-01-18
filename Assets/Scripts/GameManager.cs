using UnityEngine;
using System;

/// <summary>
/// GameManager handles the game state.
/// </summary>
public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }
    public static event Action<GameState> OnGameStateChanged;
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
        UpdateGameState(GameState.MainMenu);
    }

    private void Update() {

    }

    public void UpdateGameState(GameState newState) {
        CurrentState = newState;

        switch (newState) {
            case GameState.MainMenu:
                break;
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }
}

public enum GameState {
    MainMenu,
    Playing,
    Paused,
    GameOver
}