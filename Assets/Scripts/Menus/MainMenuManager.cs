using System;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _creditsMenu;

    private void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
        GameManager.UpdateGameState(GameState.MainMenu);
    }

    private void Start() {
        _mainMenu.SetActive(true);
        _settingsMenu.SetActive(false);
        _creditsMenu.SetActive(false);
    }

    public void OnStartButtonClicked() {
        GameManager.UpdateGameState(GameState.Playing);
        SceneLoader.LoadSceneLoadingScreenAsync(Scene.ZeroDay);
    }

    public void OnQuitButtonClicked() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        // Application.OpenURL("https://ambientredoubt.itch.io/beatloader");
        Application.Quit();
    }

    private void GameManagerOnStateChanged(GameState state) {
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }
}
