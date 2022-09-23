using UnityEngine;
using System;
using Firebase.Analytics;
using Firebase;

#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

public class MenuPlayButton : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayGamebutton()
    {

#if UNITY_IOS
        Version currentVersion = new Version(UnityEngine.iOS.Device.systemVersion);
        Version ios14 = new Version("14.0");

        if (currentVersion >= ios14 && ATTrackingStatusBinding.GetAuthorizationTrackingStatus() ==
            ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            ATTrackingStatusBinding.RequestAuthorizationTracking();
        }
#endif

        GameDelegate gameDelegate = GameDelegate.SetGameController();
        SkillzCrossPlatform.LaunchSkillz(gameDelegate);

        audioSource.Stop();
        FirebaseAnalytics();
    }

    private void FirebaseAnalytics()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                var app = Firebase.FirebaseApp.DefaultInstance;

                // Log an event with no parameters.
                Firebase.Analytics.FirebaseAnalytics
                  .LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLogin);

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

}

