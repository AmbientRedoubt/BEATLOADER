using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour {
    [SerializeField] private UIDocument _document;

    private void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
    }

    private void Update() {

    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }

    private void GameManagerOnStateChanged(GameState state) {
        throw new NotImplementedException();
    }
}
