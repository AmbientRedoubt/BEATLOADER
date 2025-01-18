using System;
using FMODUnity;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    [field: SerializeField] private EventReference _clickSound;

    private void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
        GameManager.UpdateGameState(GameState.MainMenu);
    }

    public void OnStartButtonClicked() {
        AudioManager.PlayOneShot(_clickSound);
        GameManager.UpdateGameState(GameState.Playing);
        UnityEngine.SceneManagement.SceneManager.LoadScene("TestLevel");
    }

    public void OnSettingsButtonClicked() {
        AudioManager.PlayOneShot(_clickSound);
        throw new NotImplementedException();
    }

    public void OnQuitButtonClicked() {
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
