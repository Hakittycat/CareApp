using System;
using System.Collections;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;

public class FormManager : MonoBehaviour {
    private ApplicationState state;
    private PersistentFormData formData;
    public UnityEvent onGetDataRequestComplete;
    
    private void Awake() {
        state = FindObjectOfType<ApplicationState>();
        formData = FindObjectOfType<PersistentFormData>();
    }

    /**
     * When first initialized only send LoadData request if the user has been logged in.
     */
    private void Start() {
        if (state != null && state.isLoggedIn()) {
            StartCoroutine(LoadData());
        }
    }

    public void UpdateFormData() {
        foreach (var pair in formData.GetData()) {
            UpdateAnswer(pair.Key, pair.Value);
        }
    }

    /**
     * Only send web request to update form data if the user is logged in.
     */
    public void UpdateAnswer(string question, string answer) {
        if (state != null && state.isLoggedIn()) {
            StartCoroutine(SendUpdateRequest(question, answer));
        }
    }

    private IEnumerator SendUpdateRequest(string question, string answer) {
        const string endpoint = WebRequestUtilities.URL + "/snapshot/update";
        var body = new JObject(new JProperty("answer", answer), new JProperty("question", question));
        using var request = WebRequestUtilities.CreatePostRequest(endpoint, body);
        WebRequestUtilities.SetRequestHeaderToken(request, state.token);
        yield return request.SendWebRequest();
    }

    private IEnumerator LoadData() {
        const string endpoint = WebRequestUtilities.URL + "/snapshot/data";
        using (var request = WebRequestUtilities.CreateGetRequest(endpoint, state.GetToken())) {
            yield return request.SendWebRequest();
            var response = JObject.Parse(request.downloadHandler.text);
            var results = response["results"]?.Value<JArray>() ?? new JArray();
            foreach (var result in results) {
                var question = result["question"]?.Value<string>();
                var answer = result["answer"]?.Value<string>();
                formData.SetData(question, answer);
            }
        }

        onGetDataRequestComplete.Invoke();
    }
}