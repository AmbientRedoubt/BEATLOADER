using System.Collections;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour {
    [SerializeField] private EventReference _countdownSound;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RawImage _backgroundImage;
    [SerializeField] private Outline _canvasOutline;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _countdownModal;
    [SerializeField] private TMP_Text _countdownText;
    private const float COUNTDOWN_DURATION = 3f;
    public static PauseMenuManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
    }

    private void Start() {
        _canvas.enabled = false;
        _backgroundImage.enabled = true;
        _canvasOutline.enabled = true;
        _pauseMenu.SetActive(true);
        _countdownModal.SetActive(false);
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
                _countdownModal.SetActive(false);
                break;
        }
    }

    public void OnResumeButtonClicked() {
        _countdownModal.SetActive(true);
        _pauseMenu.SetActive(false);
        _backgroundImage.enabled = false;
        _canvasOutline.enabled = false;
        StartCoroutine(CountdownToResume());
    }

    private IEnumerator CountdownToResume() {
        for (float i = COUNTDOWN_DURATION; i > 0; i--) {
            _countdownText.text = i.ToString();
            AudioManager.PlayOneShot(_countdownSound);
            yield return new WaitForSecondsRealtime(1f);
        }
        Debug.Log("Resuming game...");
        _countdownModal.SetActive(false);
        _canvas.enabled = false;
        GameManager.UpdateGameState(GameState.Playing);
    }

    public void OnMainMenuButtonClicked() {
        GameManager.UpdateGameState(GameState.MainMenu);
        SceneLoader.LoadScene(SceneLoader.Scene.MainMenu);
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }
}
