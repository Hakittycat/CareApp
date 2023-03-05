using System;
using System.Collections;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Authenticator : MonoBehaviour {
    public ApplicationState state;
    public PopupMenu popupMenu;
    public SceneLoader sceneLoader;
    public PersistentFormData persistentFormData;

    public void Authenticate(string username, string password) {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) {
            popupMenu.show("Username and password fields must be filled in!");
        }
        else {
            StartCoroutine(AuthenticateLogin(username, password));
        }
    }

    private IEnumerator AuthenticateLogin(string username, string password) {
        var body = new JObject(new JProperty("username", username), new JProperty("password", password));
        const string endpoint = WebRequestUtilities.URL + "/login";
        using var request = WebRequestUtilities.CreatePostRequest(endpoint, body);
        yield return request.SendWebRequest();
        if (request.responseCode == 200) {
            var response = JsonUtility.FromJson<RequestResponse>(request.downloadHandler.text);
            //if (response.success) {
            state.SetToken(response.token);
            WebRequestUtilities.token = response.token;
            state.username = username;
            WebRequestUtilities.username = username;
            sceneLoader.LoadScene("Home");
            persistentFormData.SetData("first_name", username);
            //}
            //else {
            //    popupMenu.show(response.message);
            //}
        }
        else {
            popupMenu.show("Login request failed!");
        }
    }

    [Serializable]
    private class RequestResponse {
        public bool success;
        public string message;
        public string token;

        public RequestResponse(bool success, string message, string token) {
            this.success = success;
            this.message = message;
            this.token = token;
        }
    }
}