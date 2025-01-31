using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveform", menuName = "Waveform")]
public class WaveformSO : ScriptableObject {
    public WaveformType Type;
    public List<GameObject> Waveforms;
}

public enum WaveformType {
    ThreeDown,
    ThreeUp,
    FourDown,
    FourUp,
    FiveDown,
    FiveUp,
    SevenDown,
    SevenUp
}
