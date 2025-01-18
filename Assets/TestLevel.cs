using FMODUnity;
using UnityEngine;

public class TestLevel : MonoBehaviour {
    [SerializeField] private EventReference _music;
    public static TestLevel Instance { get; private set; }

    private void Start() {
        AudioManager.CreateAndAddEventInstance(_music).start();
    }

    private void OnDestroy() {
        AudioManager.CleanUp();
    }
}
