using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimeLogger : MonoBehaviour {
    private DateTime timestamp;
    private string tracked;

    private void Awake() {
        var instances = FindObjectsOfType<SceneTimeLogger>();
        if (instances.Length >= 2) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }

    private void Start() {
        timestamp = DateTime.Now;
        tracked = SceneManager.GetActiveScene().name;
    }

    private void Update() {
        var activeSceneName = SceneManager.GetActiveScene().name;
        if (activeSceneName.Equals(tracked)) return;
        LogViewTime(tracked);
        tracked = activeSceneName;
    }

    private int GetTimeElapsedInSeconds() {
        var now = DateTime.Now;
        var span = now.Subtract(timestamp);
        return Convert.ToInt32(span.TotalSeconds);
    }

    private void ResetTimeStamp() {
        timestamp = DateTime.Now;
    }

    private void LogViewTime(string sceneName) {
        var controller = FindObjectOfType<PersistentLoggingController>();
        if (controller == null) return;
        var elapsedTime = GetTimeElapsedInSeconds();
        controller.LogViewTime(sceneName, elapsedTime);
        ResetTimeStamp();
    }
}