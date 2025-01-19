using System;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _settingsMenu;

    private void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
        GameManager.UpdateGameState(GameState.MainMenu);
    }

    private void Start() {
        _mainMenu.SetActive(true);
        _settingsMenu.SetActive(false);
    }

    public void OnStartButtonClicked() {
        GameManager.UpdateGameState(GameState.Playing);
        UnityEngine.SceneManagement.SceneManager.LoadScene("ZeroDay");
    }

    public void OnQuitButtonClicked() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void GameManagerOnStateChanged(GameState state) {
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }
}
