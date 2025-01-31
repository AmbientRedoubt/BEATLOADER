using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveform", menuName = "Waveform")]
public class WaveformsSO : ScriptableObject {
    public List<WaveformObject> Waveforms;
}

[System.Serializable]
public class WaveformObject {
    public int Length; // Length of the waveform
    public WaveformType Type; // The type of waveform (Up/Down)
    public List<GameObject> GameObjects; // List of game objects associated with this waveform type
}

public enum WaveformType {
    Up,
    Down
}
