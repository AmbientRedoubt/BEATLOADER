using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

/// <summary>
/// NIGHTMARE SCRIPT AHHHHHHH
/// </summary>
public class PlayerSettingsManager : MonoBehaviour {
    private static Bus _masterBus;
    private static Bus _musicBus;
    private static Bus _SFXBus;
    private static float _masterVolume = 1f;
    private static float _musicVolume = 1f;
    private static float _SFXVolume = 1f;
    private static bool _screenShakeEnabled = true;
    private static bool _flashEffectsEnabled = true;
    private static bool _CRTModeEnabled = true;
    private static ScriptableRendererFeature _VHSProRendererFeature;

    public static float MasterVolume {
        get { return _masterVolume; }
        set {
            _masterVolume = value;
            _masterBus.setVolume(_masterVolume);
        }
    }

    public static float MusicVolume {
        get { return _musicVolume; }
        set {
            _musicVolume = value;
            _musicBus.setVolume(_musicVolume);
        }
    }

    public static float SFXVolume {
        get { return _SFXVolume; }
        set {
            _SFXVolume = value;
            _SFXBus.setVolume(_SFXVolume);
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

    public static bool CRTModeEnabled {
        get { return _CRTModeEnabled; }
        set {
            _CRTModeEnabled = value;
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
            InitialiseSettings();
        }
    }

    private void Start() {
        FindVHSProRendererFeature();
    }

    private void InitialiseSettings() {
        _masterBus = RuntimeManager.GetBus("bus:/");
        _masterBus.setVolume(_masterVolume);

        _musicBus = RuntimeManager.GetBus("bus:/Music");
        _musicBus.setVolume(_musicVolume);

        _SFXBus = RuntimeManager.GetBus("bus:/SFX");
        _SFXBus.setVolume(_SFXVolume);

        SetCRTMode(_CRTModeEnabled);
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

    private static void SetCRTMode(bool enabled) {
        if (_VHSProRendererFeature != null) {
            _VHSProRendererFeature.SetActive(enabled);
        }
    }
}
