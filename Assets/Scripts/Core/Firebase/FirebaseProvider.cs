using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

namespace Core.Firebase
{
    public class FirebaseProvider
    {
        private const string GameOverEventName = "Game Over";
        private const string StartEventName = "Start";
        private const string ScoreParameterName = "S    core";

        public FirebaseProvider()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(OnDependencyReceived);
        }

        private void OnDependencyReceived(Task<DependencyStatus> task)
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
            FirebaseAnalytics.LogEvent(StartEventName);
        }

        public void LogDeathEvent(int finalScore = 0)
        {
            FirebaseAnalytics.LogEvent(GameOverEventName, new Parameter(ScoreParameterName, finalScore));
        }
    }
}
