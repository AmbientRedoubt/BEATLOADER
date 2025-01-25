using UnityEngine;

public class RhythmTrackManager : MonoBehaviour {
    public static float TrackStartTime { get; private set; }
    // Cannot serialise static properties so we're using instances to get a reference to the RhythmTrack (annoying!)
    [field: SerializeField] public RhythmTrack RhythmTrack { get; private set; }
    public static RhythmTrackManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        TrackStartTime = Time.time;
    }

    private void Update() {

    }
}
