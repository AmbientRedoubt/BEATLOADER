using FMODUnity;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsMenuManager : MonoBehaviour {
    [SerializeField] private UIDocument _document;
    private Button _backButton;
    [field: SerializeField] private EventReference _clickSound;

    private void OnEnable() {
        _backButton = _document.rootVisualElement.Q("BackButton") as Button;
        _backButton.clicked += OnBackButtonClicked;
    }

    private void OnBackButtonClicked() {
        AudioManager.PlayOneShot(_clickSound);
    }

    private void GameManagerOnStateChanged(GameState state) {
    }
}
