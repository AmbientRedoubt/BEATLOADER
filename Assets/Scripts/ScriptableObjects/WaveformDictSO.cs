using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveformDict", menuName = "WaveformDict")]
public class WaveformDictSO : ScriptableObject {
    public List<GameObject> WaveformDict;
}

[System.Serializable]
public class WaveformDict {
    public Dictionary<WaveformType, List<GameObject>> waveforms;
}
