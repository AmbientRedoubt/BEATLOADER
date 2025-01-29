using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

[CustomEditor(typeof(RhythmTrack))]
public class RhythmTrackEditor : Editor {
    private SerializedProperty _bpmProp;
    private SerializedProperty _noteInputsProp;

    private void OnEnable() {
        _bpmProp = serializedObject.FindProperty("BPM");
        _noteInputsProp = serializedObject.FindProperty("NoteInputs");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_bpmProp);
        // SortNotesByTime();

        EditorGUILayout.LabelField("Note Inputs", EditorStyles.boldLabel);
        for (int i = 0; i < _noteInputsProp.arraySize; i++) {
            SerializedProperty noteInput = _noteInputsProp.GetArrayElementAtIndex(i);
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.LabelField($"Note {i + 1}", EditorStyles.boldLabel);

            SerializedProperty timeProp = noteInput.FindPropertyRelative("Time");
            SerializedProperty amplitudeProp = noteInput.FindPropertyRelative("Amplitude");
            SerializedProperty inputActionProp = noteInput.FindPropertyRelative("InputAction");
            SerializedProperty noteSoundProp = noteInput.FindPropertyRelative("NoteSound");

            EditorGUILayout.PropertyField(timeProp);
            EditorGUILayout.PropertyField(amplitudeProp);
            EditorGUILayout.PropertyField(inputActionProp);
            EditorGUILayout.PropertyField(noteSoundProp);

            if (GUILayout.Button("Remove Note")) {
                _noteInputsProp.DeleteArrayElementAtIndex(i);
                serializedObject.ApplyModifiedProperties();
                // SortNotesByTime();
                return;
            }
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(50);

        if (GUILayout.Button("Add Note")) {
            _noteInputsProp.arraySize++;
            SerializedProperty newNote = _noteInputsProp.GetArrayElementAtIndex(_noteInputsProp.arraySize - 1);

            newNote.FindPropertyRelative("Time").floatValue = 0f;
            newNote.FindPropertyRelative("Amplitude").floatValue = 0f;
            // newNote.FindPropertyRelative("InputAction").objectReferenceValue = null;

            // Apply changes first to avoid property reference issues
            serializedObject.ApplyModifiedProperties();
            // Directly modify the NoteInput in the target object
            RhythmTrack track = (RhythmTrack)target;

            // Set the note sound path here (CHANGE ME WHEN ADDING NEW TRACKS!)
            track.NoteInputs[track.NoteInputs.Length - 1].NoteSound.Path = "event:/Music/Awake/Notes/awake_note" + (track.NoteInputs.Length - 1 + 1);

            EditorUtility.SetDirty(track);
            serializedObject.Update();
            // SortNotesByTime();
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Sort Notes by Time")) {
            SortNotesByTime();
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void SortNotesByTime() {
        RhythmTrack track = (RhythmTrack)target;
        track.NoteInputs = track.NoteInputs.OrderBy(n => n.Time).ToArray();
        EditorUtility.SetDirty(track);
    }
}
