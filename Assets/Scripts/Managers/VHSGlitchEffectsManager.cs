using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// This class manages the enabling/disabling of the VHS effect
// Also disables the bloom since it causes weird banding otherwise
// Also the glitch effect as well
public class VHSGlitchEffectsManager : MonoBehaviour {
    [SerializeField] private Volume _volume;
    [SerializeField] private GameObject _glitchEffect;
    private static VHSProVolumeComponent _VHSVolumeComponent;
    private static Bloom _bloomVolumeComponent;
    public static VHSGlitchEffectsManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        _volume.profile.TryGet(out _VHSVolumeComponent);
        _volume.profile.TryGet(out _bloomVolumeComponent);

        if (GameSettingsManager.VHSModeEnabled) {
            _VHSVolumeComponent.active = true;
            _bloomVolumeComponent.active = true;
        }
        else {
            _VHSVolumeComponent.active = false;
            _bloomVolumeComponent.active = false;
        }

        if (_glitchEffect == null) { return; }
        if (GameSettingsManager.FlashEffectsEnabled) {
            _glitchEffect.SetActive(true);
        }
        else {
            _glitchEffect.SetActive(false);
        }
    }

    public static void ToggleVHSMode() {
        _VHSVolumeComponent.active = !_VHSVolumeComponent.active;
        _bloomVolumeComponent.active = !_bloomVolumeComponent.active;
    }

    public static void ToggleGlitchEffect() {
        if (Instance._glitchEffect == null) { return; }
        Instance._glitchEffect.SetActive(!Instance._glitchEffect.activeSelf);
    }
}
