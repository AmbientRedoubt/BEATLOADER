// What the fuck man, WebGL either can't cache or dynamically recreates the buses
// I should've gone to bed hours ago
using FMODUnity;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class GameSettingsManager : MonoBehaviour
{
    private enum BusType
    {
        Master,
        Music,
        SFX
    }

    private static float _masterVolume = 1f;
    private static float _musicVolume = 1f;
    private static float _SFXVolume = 1f;
    private static bool _screenShakeEnabled = true;
    private static bool _flashEffectsEnabled = true;
    private static bool _CRTModeEnabled = true;
    private static ScriptableRendererFeature _VHSProRendererFeature;

    public static float MasterVolume
    {
        get { return _masterVolume; }
        set
        {
            _masterVolume = value;
            SetVolume(BusType.Master);
        }
    }

    public static float MusicVolume
    {
        get { return _musicVolume; }
        set
        {
            _musicVolume = value;
            SetVolume(BusType.Music);
        }
    }

    public static float SFXVolume
    {
        get { return _SFXVolume; }
        set
        {
            _SFXVolume = value;
            SetVolume(BusType.SFX);
        }
    }

    //TODO: Implement screen shake toggle
    public static bool ScreenShakeEnabled
    {
        get { return _screenShakeEnabled; }
        set
        {
            _screenShakeEnabled = value;
        }
    }

    public static bool FlashEffectsEnabled
    {
        get { return _flashEffectsEnabled; }
        set
        {
            _flashEffectsEnabled = value;
        }
    }

    public static bool CRTModeEnabled
    {
        get { return _CRTModeEnabled; }
        set
        {
            _CRTModeEnabled = value;
            SetCRTMode(_CRTModeEnabled);
        }
    }
    public static GameSettingsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            InitialiseSettings();
        }
    }

    private void Start()
    {
        FindVHSProRendererFeature();
    }

    private void InitialiseSettings()
    {
        SetVolume(BusType.Master);
        SetVolume(BusType.Music);
        SetVolume(BusType.SFX);

        SetCRTMode(_CRTModeEnabled);
    }

    private void FindVHSProRendererFeature()
    {
        UniversalRenderPipelineAsset urpAsset = GraphicsSettings.defaultRenderPipeline as UniversalRenderPipelineAsset;
        foreach (ScriptableRendererData renderer in urpAsset.rendererDataList.ToArray())
        {
            if (renderer is Renderer2DData renderer2DData)
            {
                foreach (ScriptableRendererFeature feature in renderer2DData.rendererFeatures)
                {
                    if (feature.name == "VHSPro")
                    {
                        _VHSProRendererFeature = feature;
                        break;
                    }
                }
            }
        }
    }

    private static void SetCRTMode(bool enabled)
    {
        if (_VHSProRendererFeature != null)
        {
            _VHSProRendererFeature.SetActive(enabled);
        }
    }

    private static void SetVolume(BusType busType)
    {
        switch (busType)
        {
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
