using UnityEngine;
using FMODUnity;

public class MainMenuAudioManager : MonoBehaviour {
    [SerializeField] private EventReference _music;
    public static MainMenuAudioManager Instance { get; private set; }

    private void Start() {
        AudioManager.CreateAndAddEventInstance(_music).start();
    }

    public void PlayClickSound() {
        AudioManager.PlayOnClickSound();
    }

    private void OnDestroy() {
        AudioManager.CleanUp();
    }
}
