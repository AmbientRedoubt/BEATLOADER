using System;
using UnityEngine;
using MilkShake;

public class CameraShakeManager : MonoBehaviour {
    private Action _onNoteHit;
    private Action _onNoteMiss;
    [SerializeField] private ShakePreset _missShake;
    [SerializeField] private ShakePreset _upShake;
    [SerializeField] private ShakePreset _downShake;
    public static CameraShakeManager Instance { get; private set; }

    private void Awake() {
        // Use lambdas to call Shake() with the appropriate ShakeType
        _onNoteHit = () => Shake(ShakeType.Up);
        _onNoteMiss = () => Shake(ShakeType.Miss);

        PlayerEvents.OnNoteHit += () => _onNoteHit();
        PlayerEvents.OnNoteMiss += () => _onNoteMiss();

        Instance = this;
    }

    public static void Shake(ShakeType shakeType) {
        if (!GameSettingsManager.ScreenShakeEnabled) return;

        switch (shakeType) {
            case ShakeType.Miss:
                Shaker.ShakeAll(Instance._missShake);
                break;
            case ShakeType.Up:
                Shaker.ShakeAll(Instance._upShake);
                break;
            case ShakeType.Down:
                Shaker.ShakeAll(Instance._downShake);
                break;
        }
    }

    private void OnDestroy() {
        PlayerEvents.OnNoteHit -= () => _onNoteHit();
        PlayerEvents.OnNoteMiss -= () => _onNoteMiss();
    }
}

public enum ShakeType {
    Miss,
    Up,
    Down
}
