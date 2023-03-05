using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class ApplicationState : MonoBehaviour {
    public string username;
    public string token;

    private void Awake() {
        var found = FindObjectOfType<ApplicationState>();
        if (found != null && found.gameObject != gameObject) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(this);
        }
    }

    public bool isLoggedIn() {
        return username != null && token != null;
    }

    public void SetToken(string newToken) {
        token = newToken;
    }

    public string GetToken() {
        return token;
    }

    public void ClearSessionData() {
        token = null;
        username = null;
        RemoveIfFound<PersistentFormData>();
        RemoveIfFound<PersistentScheduleData>();
    }

    private static void RemoveIfFound<T>() where T : MonoBehaviour {
        var instance = FindObjectOfType<T>();
        if (instance != null) {
            Destroy(instance.gameObject);
        }
    }
}