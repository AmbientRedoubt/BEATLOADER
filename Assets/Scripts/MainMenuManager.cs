using System;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {
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
