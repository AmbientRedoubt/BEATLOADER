using UnityEngine;
using FMODUnity;

public class PlayerAudioHandler : MonoBehaviour {
    [SerializeField] private EventReference _jumpSound;
    [SerializeField] private EventReference _noteHitSound;
    [SerializeField] private EventReference _noteMissSound;
    [SerializeField] private EventReference _noteHoldSound;

    private void Awake() {
        PlayerEvents.OnNoteHit += OnNoteHit;
        PlayerEvents.OnNoteMiss += OnNoteMiss;
    }

    public void OnJump() {
        AudioManager.PlayOneShot(_jumpSound);
    }

    private void OnNoteHit() {
        AudioManager.PlayOneShot(_noteHitSound);
    }

    private void OnNoteMiss() {
        AudioManager.PlayOneShot(_noteMissSound);
    }

    private void OnDestroy() {
        PlayerEvents.OnNoteHit -= OnNoteHit;
        PlayerEvents.OnNoteMiss -= OnNoteMiss;
    }
}
