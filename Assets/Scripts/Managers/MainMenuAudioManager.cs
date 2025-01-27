using UnityEngine;
using FMODUnity;

public class MainMenuAudioManager : MonoBehaviour {
    [SerializeField] private EventReference _music;
    [SerializeField] private EventReference _clickSound;

    private void Start() {
        AudioManager.CreateAndAddEventInstance(_music).start();
    }

    public void PlayClickSound() {
        AudioManager.PlayOneShot(_clickSound);
    }

    private void OnDestroy() {
        AudioManager.CleanUp();
    }
}
