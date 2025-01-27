using UnityEngine;
using FMODUnity;

public class PlayerAudioHandler : MonoBehaviour {
    [SerializeField] private EventReference _jumpSound;
    [SerializeField] private EventReference _noteHitSound;
    [SerializeField] private EventReference _noteMissSound;
    [SerializeField] private EventReference _noteHoldSound;
    private NoteInput[] _rhythmTrackNotes;
    private int _noteID = 0;

    private void Awake() {
        PlayerEvents.OnNoteHit += OnNoteHit;
        PlayerEvents.OnNoteMiss += OnNoteMiss;
    }

    private void Start() {
        _rhythmTrackNotes = RhythmTrackManager.Instance.RhythmTrack.NoteInputs;
    }

    public void OnJump() {
        AudioManager.PlayOneShot(_jumpSound);
    }

    private void OnNoteHit() {
        AudioManager.PlayOneShot(_rhythmTrackNotes[_noteID].NoteSound);
        // AudioManager.PlayOneShot(_rhythmTrackNotes[RhythmTrackManager.NoteID].NoteType == NoteType.Hold ? _noteHoldSound : _noteHitSound);
        // AudioManager.PlayOneShot(_noteHitSound);
        Debug.Log($"Note Hit! Index: {_noteID}");
        _noteID++;
    }

    private void OnNoteMiss() {
        AudioManager.PlayOneShot(_noteMissSound);
        Debug.Log($"Note Missed! Index: {_noteID}");
        _noteID++;
    }

    private void OnDestroy() {
        PlayerEvents.OnNoteHit -= OnNoteHit;
        PlayerEvents.OnNoteMiss -= OnNoteMiss;
    }
}
