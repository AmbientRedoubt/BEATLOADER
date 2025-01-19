using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

/// <summary>
/// NIGHTMARE SCRIPT AHHHHHHH
/// </summary>
public class PlayerSettingsManager : MonoBehaviour {
    [SerializeField] private static Bus _masterBus;
    [SerializeField] private static Bus _musicBus;
    [SerializeField] private static Bus _SFXBus;
    private static float _masterVolume;
    private static float _musicVolume;
    private static float _SFXVolume;
    private static bool _screenShakeEnabled;
    private static bool _flashEffectsEnabled;
    private static bool _CRTModeEnabled;
    private static ScriptableRendererFeature _VHSProRendererFeature;

    public static float MasterVolume {
        get { return _masterVolume; }
        set {
            _masterVolume = value;
            PlayerPrefs.SetFloat("MasterVolume", _masterVolume);
            PlayerPrefs.Save();
            _masterBus.setVolume(_masterVolume);
        }
    }

    public static float MusicVolume {
        get { return _musicVolume; }
        set {
            _musicVolume = value;
            PlayerPrefs.SetFloat("MusicVolume", _musicVolume);
            PlayerPrefs.Save();
            _musicBus.setVolume(_musicVolume);
        }
    }

    public static float SFXVolume {
        get { return _SFXVolume; }
        set {
            _SFXVolume = value;
            PlayerPrefs.SetFloat("SFXVolume", _SFXVolume);
            PlayerPrefs.Save();
            _SFXBus.setVolume(_SFXVolume);
        }
    }

    public static bool ScreenShakeEnabled {
        get { return _screenShakeEnabled; }
        set {
            _screenShakeEnabled = value;
            PlayerPrefs.SetInt("ScreenShakeEnabled", _screenShakeEnabled ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool FlashEffectsEnabled {
        get { return _flashEffectsEnabled; }
        set {
            _flashEffectsEnabled = value;
            PlayerPrefs.SetInt("FlashEffectsEnabled", _flashEffectsEnabled ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool CRTModeEnabled {
        get { return _CRTModeEnabled; }
        set {
            _CRTModeEnabled = value;
            PlayerPrefs.SetInt("CRTModeEnabled", _CRTModeEnabled ? 1 : 0);
            PlayerPrefs.Save();
            SetCRTMode(_CRTModeEnabled);
        }
    }
    public static PlayerSettingsManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    private void Start() {
        FindVHSProRendererFeature();
        LoadSavedVolumeSettings();
        LoadSavedToggleSettings();
    }

    private void FindVHSProRendererFeature() {
        UniversalRenderPipelineAsset urpAsset = GraphicsSettings.defaultRenderPipeline as UniversalRenderPipelineAsset;
        foreach (ScriptableRendererData renderer in urpAsset.rendererDataList.ToArray()) {
            if (renderer is Renderer2DData renderer2DData) {
                foreach (ScriptableRendererFeature feature in renderer2DData.rendererFeatures) {
                    if (feature.name == "VHSPro") {
                        _VHSProRendererFeature = feature;
                        break;
                    }
                }
            }
        }
    }

    private void LoadSavedVolumeSettings() {
        _masterBus = RuntimeManager.GetBus("bus:/");
        _masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1);
        _masterBus.setVolume(MasterVolume);

        _musicBus = RuntimeManager.GetBus("bus:/Music");
        _musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
        _musicBus.setVolume(MusicVolume);

        _SFXBus = RuntimeManager.GetBus("bus:/SFX");
        _SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1);
        _SFXBus.setVolume(SFXVolume);
    }

    private void LoadSavedToggleSettings() {
        ScreenShakeEnabled = PlayerPrefs.GetInt("ScreenShakeEnabled", 1) == 1;
        FlashEffectsEnabled = PlayerPrefs.GetInt("FlashEffectsEnabled", 1) == 1;

        CRTModeEnabled = PlayerPrefs.GetInt("CRTModeEnabled", 1) == 1;
        SetCRTMode(_CRTModeEnabled);
    }

    private static void SetCRTMode(bool enabled) {
        if (_VHSProRendererFeature != null) {
            _VHSProRendererFeature.SetActive(enabled);
        }
    }

    private void OnDestroy() {
        PlayerPrefs.Save();
    }
}
