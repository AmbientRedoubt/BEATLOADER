using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

// The Unity events are changing the slider values, which causes their corresponding methods to be called
// So we skip their method calls by checking if the UI is being initialised

public class SettingsMenuManager : MonoBehaviour {
    private const int VOLUME_STEPS = 4;
    private bool _isInitialising = false;
    [SerializeField] private EventReference _settingsClickSound;
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;
    [SerializeField] private Toggle _screenShakeToggle;
    [SerializeField] private Toggle _flashEffectsToggle;
    [SerializeField] private Toggle _VHSModeToggle;

    private void Start() {
        InitialiseUI();
    }

    private void InitialiseUI() {
        _isInitialising = true;

        _masterVolumeSlider.value = GameSettingsManager.MasterVolume * VOLUME_STEPS;
        _musicVolumeSlider.value = GameSettingsManager.MusicVolume * VOLUME_STEPS;
        _sfxVolumeSlider.value = GameSettingsManager.SFXVolume * VOLUME_STEPS;

        _screenShakeToggle.isOn = GameSettingsManager.ScreenShakeEnabled;
        _flashEffectsToggle.isOn = GameSettingsManager.FlashEffectsEnabled;
        _VHSModeToggle.isOn = GameSettingsManager.VHSModeEnabled;

        _isInitialising = false;
    }

    public void PlaySettingsClickSound() {
        if (!_isInitialising) AudioManager.PlayOneShot(_settingsClickSound);
    }

    public void OnVolumeSliderUpdate(Slider slider) {
        if (_isInitialising) return;
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
        if (_isInitialising) return;
        switch (toggle.name) {
            case "ScreenShakeToggle":
                GameSettingsManager.ScreenShakeEnabled = toggle.isOn;
                break;
            case "FlashEffectsToggle":
                GameSettingsManager.FlashEffectsEnabled = toggle.isOn;
                break;
            case "VHSModeToggle":
                GameSettingsManager.VHSModeEnabled = toggle.isOn;
                break;
        }
    }
}
