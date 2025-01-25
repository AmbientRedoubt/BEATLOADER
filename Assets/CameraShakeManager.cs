using UnityEngine;
using MilkShake;

public class CameraShakeManager : MonoBehaviour {
    [SerializeField] private ShakePreset _missShake;
    [SerializeField] private ShakePreset _upShake;
    [SerializeField] private ShakePreset _downShake;
    public static CameraShakeManager Instance { get; private set; }

    private void Awake() {
        PlayerEvents.OnNoteHit += UpShaker;
        PlayerEvents.OnNoteMiss += MissShaker;

        Instance = this;
    }

    public static void MissShaker() {
        Shaker.ShakeAll(Instance._missShake);
    }

    public static void UpShaker() {
        Shaker.ShakeAll(Instance._upShake);
    }

    public static void DownShaker() {
        Shaker.ShakeAll(Instance._downShake);
    }
}
