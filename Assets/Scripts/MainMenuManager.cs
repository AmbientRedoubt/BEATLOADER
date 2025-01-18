using System;
using FMODUnity;
using UnityEngine;
using UnityEngine.UIElements;


public class MainMenuManager : MonoBehaviour {
    [SerializeField] private UIDocument _document;
    private Button _startButton;
    private Button _settingsButton;
    private Button _creditsButton;
    private Button _quitButton;
    [field: SerializeField] private EventReference _clickSound;

    private void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
    }
    private void OnEnable() {
        _startButton = _document.rootVisualElement.Q("StartButton") as Button;
        _startButton.clicked += OnStartButtonClicked;
        _settingsButton = _document.rootVisualElement.Q("SettingsButton") as Button;
        _settingsButton.clicked += OnSettingsButtonClicked;
        _creditsButton = _document.rootVisualElement.Q("CreditsButton") as Button;
        _creditsButton.clicked += OnCreditsButtonClicked;
        _quitButton = _document.rootVisualElement.Q("QuitButton") as Button;
        _quitButton.clicked += OnQuitButtonClicked;
    }

    private void OnStartButtonClicked() {
        AudioManager.PlayOneShot(_clickSound);
        // GameManager.Instance.UpdateGameState(GameState.Playing);
    }

    private void OnSettingsButtonClicked() {
        AudioManager.PlayOneShot(_clickSound);
        throw new NotImplementedException();
    }


    private void OnCreditsButtonClicked() {
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

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }

    private void GameManagerOnStateChanged(GameState state) {
        Debug.Log($"Game state changed to {state}");
        // throw new NotImplementedException();
    }
}
