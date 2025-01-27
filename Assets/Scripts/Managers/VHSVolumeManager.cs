using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// This class manages the enabling/disabling of the VHS effect
// Also disables the bloom since it causes weird banding otherwise
public class VHSVolumeManager : MonoBehaviour {
    [SerializeField] private Volume _volume;
    private static VHSProVolumeComponent _VHSVolumeComponent;
    private static Bloom _bloomVolumeComponent;

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
    }

    public static void ToggleVHSMode() {
        _VHSVolumeComponent.active = !_VHSVolumeComponent.active;
        _bloomVolumeComponent.active = !_bloomVolumeComponent.active;
    }
}
