using System;
using FMODUnity;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour {
    [SerializeField] private UIDocument _document;
    private Button _startButton;
    private Button _settingsButton;
    private Button _quitButton;
    [field: SerializeField] private EventReference _clickSound;

    private void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
        GameManager.UpdateGameState(GameState.MainMenu);
    }

    private void OnEnable() {
        _startButton = _document.rootVisualElement.Q("StartButton") as Button;
        _startButton.clicked += OnStartButtonClicked;
        _settingsButton = _document.rootVisualElement.Q("SettingsButton") as Button;
        _settingsButton.clicked += OnSettingsButtonClicked;
        _quitButton = _document.rootVisualElement.Q("QuitButton") as Button;
        _quitButton.clicked += OnQuitButtonClicked;
    }

    private void OnStartButtonClicked() {
        AudioManager.PlayOneShot(_clickSound);
        GameManager.UpdateGameState(GameState.Playing);
        UnityEngine.SceneManagement.SceneManager.LoadScene("TestLevel");
    }

    private void OnSettingsButtonClicked() {
        AudioManager.PlayOneShot(_clickSound);
        throw new NotImplementedException();
    }

    private void OnQuitButtonClicked() {
        AudioManager.PlayOneShot(_clickSound);
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
