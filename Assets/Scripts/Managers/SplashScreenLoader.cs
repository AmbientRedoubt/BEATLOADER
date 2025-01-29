using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SplashScreenLoader : MonoBehaviour {
    [BankRef][SerializeField] private List<string> _banks = new();

    private void Start() {
        StartCoroutine(LoadGameAsync());
    }

    private IEnumerator LoadGameAsync() {
        AsyncOperation async = SceneLoader.LoadSceneAsync(Scene.MainMenu);
        // Don't let the scene start until all Studio Banks have finished loading
        async.allowSceneActivation = false;

        foreach (string bank in _banks) {
            RuntimeManager.LoadBank(bank, true);
        }

        while (!RuntimeManager.HaveAllBanksLoaded) { yield return null; }
        // Keep yielding the co-routine until all the sample data loading is done
        while (RuntimeManager.AnySampleDataLoading()) { yield return null; }

        // Wait for a few seconds to show logo before loading the next scene
        yield return new WaitForSeconds(2f);

        // Allow the scene to be activated. This means that any OnActivated() or Start()
        // methods will be guaranteed that all FMOD Studio loading will be completed and
        // there will be no delay in starting events
        async.allowSceneActivation = true;

        // Keep yielding the co-routine until scene loading and activation is done.
        while (!async.isDone) { yield return null; }
        DontDestroyOnLoad(Instantiate(Resources.Load("Managers")));
    }
}
