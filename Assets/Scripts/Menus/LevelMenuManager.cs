using System.Collections;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuManager : MonoBehaviour {
    [Header("FMOD UI Events")]
    [SerializeField] private EventReference _pauseSound;
    [SerializeField] private EventReference _unpauseSound;
    [SerializeField] private EventReference _countdownSound;
    [Header("UI Elements")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RawImage _backgroundImage;
    [SerializeField] private Outline _canvasOutline;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _countdownModal;
    [SerializeField] private TMP_Text _countdownText;
    private const int COUNTDOWN_DURATION = 3;
    private bool _isCountingDown = false;
    public static LevelMenuManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
    }

    private void Start() {
        _canvas.enabled = false;
        _backgroundImage.enabled = true;
        _canvasOutline.enabled = true;
        _pauseMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _countdownModal.SetActive(false);
        _gameOverMenu.SetActive(false);
    }

    private void GameManagerOnStateChanged(GameState state) {
        switch (state) {
            case GameState.Playing:
                // _canvas.enabled = false;
                break;
            case GameState.Paused:
                _canvas.enabled = true;
                _backgroundImage.enabled = true;
                _canvasOutline.enabled = true;
                _pauseMenu.SetActive(true);
                AudioManager.PlayOneShot(_pauseSound);
                break;
            case GameState.GameOver:
                _canvas.enabled = true;
                _backgroundImage.enabled = true;
                _canvasOutline.enabled = true;
                _gameOverMenu.SetActive(true);
                AudioManager.PlayOneShot(_pauseSound);
                break;
        }
    }

    public void OnResumeButtonClicked() {
        if (_isCountingDown) { return; }
        _isCountingDown = true;

        GameManager.ToggleMouseCursor(MouseState.Disabled);
        _countdownModal.SetActive(true);
        _pauseMenu.SetActive(false);
        _backgroundImage.enabled = false;
        _canvasOutline.enabled = false;
        AudioManager.PlayOneShot(_unpauseSound);
        StartCoroutine(CountdownToResume());
    }

    private IEnumerator CountdownToResume() {
        for (int i = COUNTDOWN_DURATION; i > 0; i--) {
            _countdownText.text = i.ToString();
            AudioManager.PlayOneShot(_countdownSound);
            yield return new WaitForSecondsRealtime(1f);
        }
        // Debug.Log("Resuming game...");
        _countdownModal.SetActive(false);
        _canvas.enabled = false;
        _isCountingDown = false;
        GameManager.UpdateGameState(GameState.Playing);
    }

    public void OnMainMenuButtonClicked() {
        GameManager.UpdateGameState(GameState.MainMenu);
        SceneLoader.LoadSceneLoadingScreenAsync(Scene.MainMenu);
    }

    public void OnRestartButtonClicked() {
        GameManager.UpdateGameState(GameState.Playing);
        SceneLoader.RestartScene();
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }
}
