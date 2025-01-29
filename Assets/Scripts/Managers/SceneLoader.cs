using UnityEngine.SceneManagement;
using UnityEngine;

public static class SceneLoader {
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
        Debug.Log($"Loading loading screen: {Time.deltaTime}");
        LoadScene(Scene.LoadingScreen);
        // I don't like this, but it's the only way to get coroutines to work in a static context
        SceneLoaderHelper.Instance.StartCoroutine(SceneLoaderHelper.Instance.LoadSceneAsync(scene));
        Debug.Log($"Loaded {scene}: {Time.deltaTime}");
        UnloadSceneAsync(Scene.LoadingScreen);
        Debug.Log($"Unloaded loading screen: {Time.deltaTime}");
    }

    public static void UnloadSceneAsync(Scene scene) {
        SceneManager.UnloadSceneAsync(scene.ToString());
    }

    public static void RestartScene() {
        LoadSceneLoadingScreenAsync((Scene)System.Enum.Parse(typeof(Scene), SceneManager.GetActiveScene().name));
    }
}

/// <summary>
/// Scene enum to represent all scenes in the game.
/// </summary>
public enum Scene {
    SplashScreen,
    LoadingScreen,
    MainMenu,
    ZeroDay,
    ClockCycle
}
