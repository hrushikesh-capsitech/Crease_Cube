using UnityEngine;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;

public class FireBaseManager : MonoBehaviour
{
    public static bool IsFirebaseReady { get; private set; }

    private static bool initialized;

    void Awake()
    {
        if (initialized)
        {
            Destroy(gameObject);
            return;
        }

        initialized = true;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

                IsFirebaseReady = true;

                Debug.Log("🔥 Firebase initialized successfully!");
                FirebaseAnalytics.LogEvent("firebase_initialized");   // test event
            }
            else
            {
                Debug.LogError("❌ Firebase dependency error: " + task.Result);
            }
        });
#else
        Debug.Log("ℹ Firebase disabled in Editor");
#endif
    }
}
