using UnityEngine.SceneManagement;

public static class SceneLoader {
    public enum Scene {
        SplashScreen,
        MainMenu,
        ZeroDay,
        ClockCycle
    }

    public static void LoadScene(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }
}
