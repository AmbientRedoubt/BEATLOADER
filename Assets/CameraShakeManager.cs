using UnityEngine;
using MilkShake;

public class CameraShakeManager : MonoBehaviour {
    [SerializeField] private ShakePreset _crashShake;
    [SerializeField] private ShakePreset _jumpShake;
    public static CameraShakeManager Instance { get; private set; }

    private void Awake() {
        PlayerEvents.OnNoteHit += JumpShaker;
        PlayerEvents.OnNoteMiss += CrashShaker;

        Instance = this;
    }
    public static void JumpShaker() {
        Shaker.ShakeAll(Instance._jumpShake);
    }

    public static void CrashShaker() {
        Shaker.ShakeAll(Instance._crashShake);
    }
}
