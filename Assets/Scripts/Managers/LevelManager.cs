using UnityEngine;

[RequireComponent(typeof(LevelAudioManager))]
[RequireComponent(typeof(NoteManager))]
public class LevelManager : MonoBehaviour {
    private float _levelStartTime;
    [SerializeField] private Scene _nextScene;
    [SerializeField] private RhythmTrack _rhythmTrack;
    public static readonly int FrameRate = 12;
    public static float CurrentTime { get; private set; }

    private void Start() {
        _levelStartTime = Time.time;
    }

    private void Update() {
        if (GameManager.CurrentState == GameState.Paused || GameManager.CurrentState == GameState.GameOver) { return; }
        CurrentTime = Time.time - _levelStartTime;

        // Level completed
        if (CurrentTime >= _rhythmTrack.TrackLength) {
            Debug.Log("Current Time: " + CurrentTime);
            SceneLoader.LoadSceneLoadingScreenAsync(_nextScene);
        }
    }
}
