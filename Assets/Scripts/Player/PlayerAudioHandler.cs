using UnityEngine;
using FMODUnity;
using System.Collections.Generic;

public class PlayerAudioHandler : MonoBehaviour {
    [SerializeField] private EventReference _jumpSound;
    [SerializeField] private EventReference _noteHitSound;
    [SerializeField] private EventReference _noteMissSound;
    [SerializeField] private EventReference _noteHoldSound;
    [SerializeField] private List<EventReference> _glitchSFXList;

    private void Awake() {
        PlayerEvents.OnNoteHit += OnNoteHit;
        PlayerEvents.OnNoteMiss += OnNoteMiss;
    }

    public void OnJump() {
        AudioManager.PlayOneShot(_jumpSound);
    }

    private void OnNoteHit(NoteInput noteInput) {
        AudioManager.PlayOneShot(noteInput.NoteSound);
        // AudioManager.PlayOneShot(_rhythmTrackNotes[RhythmTrackManager.NoteID].NoteType == NoteType.Hold ? _noteHoldSound : _noteHitSound);
        // AudioManager.PlayOneShot(_noteHitSound);
        // Debug.Log($"Note Hit! Note: {noteInput.NoteSound}");
    }

    private void OnNoteMiss() {
        EventReference glitchSFX = _glitchSFXList.Rand();
        AudioManager.PlayOneShot(glitchSFX);
        _glitchSFXList.Remove(glitchSFX);
        // AudioManager.PlayOneShot(_noteMissSound);
        // Debug.Log("Note Missed!");
    }

    private void OnDestroy() {
        PlayerEvents.OnNoteHit -= OnNoteHit;
        PlayerEvents.OnNoteMiss -= OnNoteMiss;
    }
}
