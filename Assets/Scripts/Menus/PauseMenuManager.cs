using System;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour {
    [SerializeField] private Canvas _canvas;

    private void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
    }

    private void Start() {
        _canvas.enabled = false;
    }

    private void GameManagerOnStateChanged(GameState state) {
        if (state == GameState.Paused) {
            _canvas.enabled = true;
        }
        else {
            _canvas.enabled = false;
        }
    }

    public void OnResumeButtonClicked() {
        GameManager.UpdateGameState(GameState.Playing);
    }

    public void OnMainMenuButtonClicked() {
        GameManager.UpdateGameState(GameState.MainMenu);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }
}
