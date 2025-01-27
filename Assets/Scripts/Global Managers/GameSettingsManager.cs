// What the fuck man, WebGL either can't cache or dynamically recreates the buses
// I should've gone to bed hours ago
using FMODUnity;
using UnityEngine;

public class GameSettingsManager : MonoBehaviour {
    private enum BusType {
        Master,
        Music,
        SFX
    }

    private static float _masterVolume = 1f;
    private static float _musicVolume = 1f;
    private static float _SFXVolume = 1f;
    private static bool _screenShakeEnabled = true;
    private static bool _flashEffectsEnabled = true;
    private static bool _VHSModeEnabled = true;

    public static float MasterVolume {
        get { return _masterVolume; }
        set {
            _masterVolume = value;
            SetVolume(BusType.Master);
        }
    }

    public static float MusicVolume {
        get { return _musicVolume; }
        set {
            _musicVolume = value;
            SetVolume(BusType.Music);
        }
    }

    public static float SFXVolume {
        get { return _SFXVolume; }
        set {
            _SFXVolume = value;
            SetVolume(BusType.SFX);
        }
    }

    //TODO: Implement screen shake toggle
    public static bool ScreenShakeEnabled {
        get { return _screenShakeEnabled; }
        set {
            _screenShakeEnabled = value;
        }
    }

    public static bool FlashEffectsEnabled {
        get { return _flashEffectsEnabled; }
        set {
            _flashEffectsEnabled = value;
        }
    }

    public static bool VHSModeEnabled {
        get { return _VHSModeEnabled; }
        set {
            _VHSModeEnabled = value;
            VHSVolumeManager.ToggleVHSMode();
        }
    }

    public static GameSettingsManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
            InitialiseSettings();
        }
    }

    private void InitialiseSettings() {
        SetVolume(BusType.Master);
        SetVolume(BusType.Music);
        SetVolume(BusType.SFX);
    }

    private static void SetVolume(BusType busType) {
        switch (busType) {
            case BusType.Master:
                RuntimeManager.GetBus("bus:/").setVolume(_masterVolume);
                break;
            case BusType.Music:
                RuntimeManager.GetBus("bus:/Music").setVolume(_musicVolume);
                break;
            case BusType.SFX:
                RuntimeManager.GetBus("bus:/SFX").setVolume(_SFXVolume);
                break;
        }
    }
}
