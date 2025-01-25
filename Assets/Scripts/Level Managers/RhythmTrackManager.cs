using UnityEngine;

public class RhythmTrackManager : MonoBehaviour {
    public static float TrackStartTime { get; private set; }
    [SerializeField] private RhythmTrack _rhythmTrack;

    private void Start() {
        TrackStartTime = Time.time;
    }

    private void Update() {

    }
}
