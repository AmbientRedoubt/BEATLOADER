using System;

// Struct for storing player events
public struct PlayerEvents {
    // Input Events
    public static Action<NoteInput> OnNoteHit;
    public static Action OnNoteMiss;
}
