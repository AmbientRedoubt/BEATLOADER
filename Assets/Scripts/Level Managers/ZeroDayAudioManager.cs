using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;

public class ZeroDayAudioManager : MonoBehaviour {
    [SerializeField] private EventReference _musicTrack;
    private EventInstance _musicTrackInstance;
    [SerializeField] private EventReference _musicBackingTrack;
    private EventInstance _musicBackingTrackInstance;
    public static ZeroDayAudioManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
    }

    private void Start() {
        _musicTrackInstance = AudioManager.CreateAndAddEventInstance(_musicTrack);
        _musicTrackInstance.start();
        _musicBackingTrackInstance = AudioManager.CreateAndAddEventInstance(_musicBackingTrack);
        _musicBackingTrackInstance.start();
        _musicBackingTrackInstance.setPaused(true);
    }

    private void GameManagerOnStateChanged(GameState state) {
        switch (state) {
            case GameState.Playing:
                Debug.Log("Playing");
                _musicTrackInstance.setPaused(false);
                _musicBackingTrackInstance.setPaused(true);
                break;
            case GameState.Paused:
                Debug.Log("Paused");
                _musicTrackInstance.setPaused(true);
                _musicBackingTrackInstance.setPaused(false);
                break;
            case GameState.GameOver:
                throw new NotImplementedException();
        }
    }

    public void PlayClickSound() {
        AudioManager.PlayOnClickSound();
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
        AudioManager.CleanUp();
    }
}
