using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class NoteInput {
    [Tooltip("Time in seconds from the start of the song.")]
    public float Time;
    [Tooltip("Height of the note (+ve is up, -ve is down.)")]
    [Range(-5, 5)]
    public float Amplitude;
    [Tooltip("The input action that corresponds to this note.")]
    public InputActionReference InputAction;
}
