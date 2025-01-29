using UnityEngine;

[CreateAssetMenu(fileName = "NewRhythmTrack", menuName = "RhythmTrack")]
public class RhythmTrack : ScriptableObject {
    public float BPM;
    public NoteInput[] NoteInputs;
}
