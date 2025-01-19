using UnityEngine;

public class AudioSettingsManager : MonoBehaviour {

    [SerializeField] private static float masterVolume { get; set; }
    [SerializeField] private static float musicVolume { get; set; }
    [SerializeField] private static float SFXVolume { get; set; }
    public static AudioSettingsManager Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    private void AssignBuses() {
        // _masterBus = RuntimeManager.GetBus(masterBusPath);
        // _musicBus = RuntimeManager.GetBus(musicBusPath);
        // _SFXBus = RuntimeManager.GetBus(SFXBusPath);
    }
}
