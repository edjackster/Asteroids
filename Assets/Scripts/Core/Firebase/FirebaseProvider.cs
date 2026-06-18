using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseProvider
{
    public FirebaseProvider()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(OnDependencyRecieved);
    }

    private void OnDependencyRecieved(Task<DependencyStatus> task)
    {
        try
        {
            if (task.IsCompletedSuccessfully == false)
                throw new Exception("Could not resolve Firebase dependencies", task.Exception);
            
            var dependencyStatus = task.Result;
            
            if (dependencyStatus != DependencyStatus.Available)
                throw new Exception($"Could not resolve Firebase dependencies: {dependencyStatus}");
            
            LogLaunchEvent();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    private void LogLaunchEvent()
    {
        FirebaseAnalytics.LogEvent("Start");
    }

    public void LogDeathEvent(int finalScore = 0)
    {
        FirebaseAnalytics.LogEvent("Death", new Parameter("score", finalScore));
    }
}
