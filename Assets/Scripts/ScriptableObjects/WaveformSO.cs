using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveform", menuName = "Waveform")]
public class WaveformSO : ScriptableObject {
    public List<WaveformObject> Waveforms;
}

[System.Serializable]
public class WaveformObject {
    public int Length;
    public WaveformType Type;
    public List<GameObject> GameObjects;
}

public enum WaveformType {
    Up,
    Down
}
