using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour {
    private const int VOLUME_STEPS = 4;
    [SerializeField] private EventReference _settingsClickSound;
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;
    [SerializeField] private Toggle _screenShakeToggle;
    [SerializeField] private Toggle _flashEffectsToggle;
    [SerializeField] private Toggle _crtModeToggle;

    private void Start() {
        _masterVolumeSlider.value = GameSettingsManager.MasterVolume * VOLUME_STEPS;
        _musicVolumeSlider.value = GameSettingsManager.MusicVolume * VOLUME_STEPS;
        _sfxVolumeSlider.value = GameSettingsManager.SFXVolume * VOLUME_STEPS;

        _screenShakeToggle.isOn = GameSettingsManager.ScreenShakeEnabled;
        _flashEffectsToggle.isOn = GameSettingsManager.FlashEffectsEnabled;
        _crtModeToggle.isOn = GameSettingsManager.CRTModeEnabled;
    }

    public void PlaySettingsClickSound() {
        AudioManager.PlayOneShot(_settingsClickSound);
    }

    public void OnVolumeSliderUpdate(Slider slider) {
        float normalisedVolume = slider.value / VOLUME_STEPS;
        switch (slider.name) {
            case "MasterVolumeSlider":
                GameSettingsManager.MasterVolume = normalisedVolume;
                break;
            case "MusicVolumeSlider":
                GameSettingsManager.MusicVolume = normalisedVolume;
                break;
            case "SFXVolumeSlider":
                GameSettingsManager.SFXVolume = normalisedVolume;
                break;
        }
    }

    public void OnOptionToggle(Toggle toggle) {
        switch (toggle.name) {
            case "ScreenShakeToggle":
                GameSettingsManager.ScreenShakeEnabled = toggle.isOn;
                break;
            case "FlashEffectsToggle":
                GameSettingsManager.FlashEffectsEnabled = toggle.isOn;
                break;
            case "CRTModeToggle":
                GameSettingsManager.CRTModeEnabled = toggle.isOn;
                break;
        }
    }

    private void GameManagerOnStateChanged(GameState state) {
    }
}
