using UnityEngine;

public class AudioSettingsManager : MonoBehaviour {

    public static AudioSettingsManager Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void AssignBuses() {
        // _masterBus = RuntimeManager.GetBus(masterBusPath);
        // _musicBus = RuntimeManager.GetBus(musicBusPath);
        // _SFXBus = RuntimeManager.GetBus(SFXBusPath);
        // _UIBus = RuntimeManager.GetBus(UIBusPath);
    }
}
