using UnityEngine;

[CreateAssetMenu(fileName = "NewRhythmTrack", menuName = "RhythmTrack")]
public class RhythmTrack : ScriptableObject {
    public string TrackName;
    public float BPM;
    public KeyInput[] KeyInputs;
}
