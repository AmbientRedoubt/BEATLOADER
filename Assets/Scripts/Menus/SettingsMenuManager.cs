using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour {
    private const int VOLUME_STEPS = 4;
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;
    [SerializeField] private Toggle _screenShakeToggle;
    [SerializeField] private Toggle _flashEffectsToggle;
    [SerializeField] private Toggle _crtModeToggle;

    public void Start() {
        _masterVolumeSlider.value = PlayerSettingsManager.MasterVolume * VOLUME_STEPS;
        _musicVolumeSlider.value = PlayerSettingsManager.MusicVolume * VOLUME_STEPS;
        _sfxVolumeSlider.value = PlayerSettingsManager.SFXVolume * VOLUME_STEPS;

        _screenShakeToggle.isOn = PlayerSettingsManager.ScreenShakeEnabled;
        _flashEffectsToggle.isOn = PlayerSettingsManager.FlashEffectsEnabled;
        _crtModeToggle.isOn = PlayerSettingsManager.CRTModeEnabled;
    }

    public void PlaySettingsClickSound() {
        AudioManager.PlayOnSettingsChange();
    }

    public void OnVolumeSliderUpdate(Slider slider) {
        float normalisedVolume = slider.value / VOLUME_STEPS;
        switch (slider.name) {
            case "MasterVolumeSlider":
                PlayerSettingsManager.MasterVolume = normalisedVolume;
                break;
            case "MusicVolumeSlider":
                PlayerSettingsManager.MusicVolume = normalisedVolume;
                break;
            case "SFXVolumeSlider":
                PlayerSettingsManager.SFXVolume = normalisedVolume;
                break;
        }
    }

    public void OnOptionToggle(Toggle toggle) {
        switch (toggle.name) {
            case "ScreenShakeToggle":
                PlayerSettingsManager.ScreenShakeEnabled = toggle.isOn;
                break;
            case "FlashEffectsToggle":
                PlayerSettingsManager.FlashEffectsEnabled = toggle.isOn;
                break;
            case "CRTModeToggle":
                PlayerSettingsManager.CRTModeEnabled = toggle.isOn;
                break;
        }
    }

    private void GameManagerOnStateChanged(GameState state) {
    }
}
