using System;
using FMODUnity;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuManager : MonoBehaviour {
    [SerializeField] private UIDocument _document;
    private Button _resumeButton;
    private Button _settingsButton;
    private Button _quitButton;
    [field: SerializeField] private EventReference _clickSound;

    private void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
    }

    private void OnEnable() {
        _resumeButton = _document.rootVisualElement.Q("ResumeButton") as Button;
        _resumeButton.clicked += OnResumeButtonClicked;
        _settingsButton = _document.rootVisualElement.Q("SettingsButton") as Button;
        _settingsButton.clicked += OnSettingsButtonClicked;
        _quitButton = _document.rootVisualElement.Q("QuitButton") as Button;
        _quitButton.clicked += OnQuitButtonClicked;
    }

    private void OnResumeButtonClicked() {
        AudioManager.PlayOneShot(_clickSound);
        _document.rootVisualElement.visible = false;
        GameManager.UpdateGameState(GameState.Playing);
    }

    private void OnSettingsButtonClicked() {
        AudioManager.PlayOneShot(_clickSound);
        throw new NotImplementedException();
    }

    private void OnQuitButtonClicked() {
        AudioManager.PlayOneShot(_clickSound);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    private void GameManagerOnStateChanged(GameState state) {
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }
}
