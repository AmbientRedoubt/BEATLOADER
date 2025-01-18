using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour {
    [SerializeField] private UIDocument _document;
    public static PlayerController Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    private void OnEnable() {
        // GameManager.OnGameStateChanged += TogglePauseGame;
    }

    private void OnDisable() {
        // GameManager.OnGameStateChanged -= TogglePauseGame;
    }

    private void Start() {

    }

    private void Update() {

    }

    private void OnEscape() {
        GameManager.TogglePauseGame();
        if (GameManager.CurrentState == GameState.Paused) {
            _document.rootVisualElement.visible = true;
        }
        else {
            _document.rootVisualElement.visible = false;
        }
        Debug.Log("Escape pressed");
    }
}
