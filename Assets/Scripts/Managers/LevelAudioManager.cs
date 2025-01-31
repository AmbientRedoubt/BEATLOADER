using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class LevelAudioManager : MonoBehaviour {
    [SerializeField] private EventReference _clickSound;
    [Header("FMOD Music Events")]
    [SerializeField] private EventReference _musicBackingTrack;
    [SerializeField] private EventReference _musicPausedTrack;
    private EventInstance _musicBackingTrackInstance;
    private EventInstance _musicPausedTrackInstance;
    public static LevelAudioManager Instance { get; private set; }

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
        _musicBackingTrackInstance = AudioManager.CreateAndAddEventInstance(_musicBackingTrack);
        _musicPausedTrackInstance = AudioManager.CreateAndAddEventInstance(_musicPausedTrack);
        _musicBackingTrackInstance.start();
        _musicPausedTrackInstance.start();
        _musicPausedTrackInstance.setPaused(true);
    }

    private void GameManagerOnStateChanged(GameState state) {
        switch (state) {
            case GameState.Playing:
                Debug.Log("Playing");
                _musicBackingTrackInstance.setPaused(false);
                _musicPausedTrackInstance.setPaused(true);
                break;
            case GameState.Paused:
                Debug.Log("Paused");
                _musicBackingTrackInstance.setPaused(true);
                _musicPausedTrackInstance.setPaused(false);
                break;
            case GameState.GameOver:
                Debug.Log("Game Over");
                _musicBackingTrackInstance.setPaused(true);
                _musicPausedTrackInstance.setPaused(true);
                break;
        }
    }

    public void PlayClickSound() {
        AudioManager.PlayOneShot(_clickSound);
    }

    private void OnDestroy() {
        AudioManager.CleanUp();
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }
}
