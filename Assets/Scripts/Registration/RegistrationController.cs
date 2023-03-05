using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using Michsky.UI.ModernUIPack;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


public class RegistrationController : MonoBehaviour {
    private const string REGISTER_ENDPOINT = WebRequestUtilities.URL + "/register";
    public CanvasGroup canvasGroup;
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public NotificationManager notificationManager;
    public UnityEvent onRegistrationComplete;
    

    private string username = "";
    private string password = "";

    [HideInInspector]
    public string userType = "Patient";

    public string GetGeneratedUsername() {
        return username;
    }
    
    private void Start() {
        usernameField.readOnly = false;
        usernameField.onValueChanged.AddListener(x => UpdateUsername());
        passwordField.onValueChanged.AddListener(x => UpdatePassword());
    }

    public void WhenOpened() {
        StartCoroutine(FetchUsername());
    }

    private void SetInteractable(bool interactable) {
        canvasGroup.interactable = interactable;
    }

    public void SetUserType(string type) {
        userType = type;
    }

    private void UpdateUsername() {
        username = usernameField.text;
    }

    private void UpdatePassword() {
        password = passwordField.text;
    }


    public void Submit() {
        if (password.Length > 5) {
            StartCoroutine(SendRegistrationRequest());
        }
        else {
            notificationManager.description = "Password length does not meet requirement!";
            notificationManager.UpdateUI();
            notificationManager.OpenNotification();
        }
    }

    private IEnumerator FetchUsername() {
        //SetInteractable(false);
        var request = WebRequestUtilities.CreateGetRequest($"{REGISTER_ENDPOINT}/generateUsername", null);
        yield return request.SendWebRequest();
        var response = JsonUtility.FromJson<GenerateUsernameResponse>(request.downloadHandler.text);
        if (response != null) {
            username = response.username;
            usernameField.text = username;
        }

        SetInteractable(true);
    }

    private IEnumerator SendRegistrationRequest() {
        SetInteractable(false);
        Debug.Log(userType);
        var requestBody = new JObject {{"username", username}, {"password", password}, {"userRole", userType} };
        Debug.Log(requestBody);
        var request = WebRequestUtilities.CreatePostRequest(REGISTER_ENDPOINT, requestBody);

        yield return request.SendWebRequest();

        if (request.IsSuccess()) {
            Debug.Log("Shit worked!!!");
            onRegistrationComplete.Invoke();
        }
        else {
            notificationManager.description = "Failed to register user!";
            notificationManager.UpdateUI();
            notificationManager.OpenNotification();
        }
        SetInteractable(true);
    }

    private class GenerateUsernameResponse {
        public string username = "";
    }
}