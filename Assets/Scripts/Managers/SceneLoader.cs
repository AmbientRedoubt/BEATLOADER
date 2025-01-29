using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public static class SceneLoader {
    private static SceneLoaderHelper _helper;

    // Static constructor to create a MonoBehaviour helper object, which can initiate a Coroutine
    // Need to check Application is playing to avoid creating a GameObject in edit mode
    static SceneLoader() {
        if (Application.isPlaying && _helper == null) {
            _helper = new GameObject("SceneLoaderHelper").AddComponent<SceneLoaderHelper>();
            Object.DontDestroyOnLoad(_helper);
        }
    }

    public static void LoadScene(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }

    public static void LoadSceneAdditive(Scene scene) {
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Additive);
    }

    public static AsyncOperation LoadSceneAsync(Scene scene) {
        return SceneManager.LoadSceneAsync(scene.ToString());
    }

    public static void LoadSceneLoadingScreenAsync(Scene scene) {
        if (!Application.isPlaying) return;
        Debug.Log("Loading " + scene + Time.deltaTime);
        LoadScene(Scene.LoadingScreen);
        _helper.StartCoroutine(_helper.LoadSceneAsync(scene));
        Debug.Log("Loaded " + scene + Time.deltaTime);
        UnloadSceneAsync(Scene.LoadingScreen);
        Debug.Log("Unloaded " + Scene.LoadingScreen + Time.deltaTime);
    }

    public static void UnloadSceneAsync(Scene scene) {
        SceneManager.UnloadSceneAsync(scene.ToString());
    }

    public static void RestartScene() {
        LoadSceneLoadingScreenAsync((Scene)System.Enum.Parse(typeof(Scene), SceneManager.GetActiveScene().name));
    }

    public static void ClearHelper() {
        _helper = null;
    }
}

// MonoBehaviour helper class to allow SceneLoader to call StartCoroutine
public class SceneLoaderHelper : MonoBehaviour {

    public IEnumerator LoadSceneAsync(Scene scene) {
        AsyncOperation async = SceneLoader.LoadSceneAsync(scene);
        async.allowSceneActivation = false;

        if (async.progress >= 0.9f) { async.allowSceneActivation = true; }
        while (!async.isDone) { yield return null; }
    }

    private void OnDestroy() {
        SceneLoader.ClearHelper();
    }
}

public enum Scene {
    SplashScreen,
    LoadingScreen,
    MainMenu,
    ZeroDay,
    ClockCycle
}
