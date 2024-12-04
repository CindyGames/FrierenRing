using Io.AppMetrica;
using UnityEngine;

public static class AppMetricaActivator
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Activate()
    {
        AppMetrica.Activate(new AppMetricaConfig("2a17d42f-453f-409a-b6a5-27c7dd511a7f")
        {
            FirstActivationAsUpdate = !IsFirstLaunch(),
        });
    }

    private static bool IsFirstLaunch()
    {
        // Implement logic to detect whether the app is opening for the first time.
        // For example, you can check for files (settings, databases, and so on),
        // which the app creates on its first launch.
        return true;
    }
}
