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
        LoadScene(Scene.LoadingScreen);
        LoadSceneAsync(scene);
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
    ClockCycle,
    ShellInjection
}
