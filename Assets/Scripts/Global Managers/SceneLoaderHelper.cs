using UnityEngine;
using System.Collections;

/// <summary>
/// SceneLoaderHelper is a helper class for SceneLoader to load scenes asynchronously.
/// </summary>
public class SceneLoaderHelper : MonoBehaviour {
    public static SceneLoaderHelper Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    public IEnumerator LoadSceneAsync(Scene scene) {
        AsyncOperation async = SceneLoader.LoadSceneAsync(scene);
        async.allowSceneActivation = false;

        if (async.progress >= 0.9f) { async.allowSceneActivation = true; }
        while (!async.isDone) { yield return null; }
    }

}
