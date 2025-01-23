using UnityEngine;

public static class Bootstrapper {
#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadManagers() {
        Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("Managers")));
    }
#endif
}
