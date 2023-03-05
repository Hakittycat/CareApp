using System.Collections;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class PersistentLoggingController : MonoBehaviour {
    private const string LOG_CLICK_ENDPOINT = WebRequestUtilities.URL + "/log/click";
    private const string LOG_VIEW_ENDPOINT = WebRequestUtilities.URL + "/log/view";
    private ApplicationState state;

    private void Awake() {
        state = FindObjectOfType<ApplicationState>();
        var instances = FindObjectsOfType<PersistentLoggingController>();
        if (instances.Length >= 2) {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
    }

    private string GetUsername() {
        if (state == null) return "guest-fallback-id";
        if (state.username != null && !state.username.Equals("")) {
            return state.username;
        }

        return "fallback-id";
    }

    public void LogClick(string data) {
        StartCoroutine(LogClickCoroutine(data));
    }

    public void LogViewTime(string scene, int viewTime) {
        StartCoroutine(LogViewTimeCoroutine(scene, viewTime));
    }

    private IEnumerator LogViewTimeCoroutine(string scene, int viewTime) {
        var body = new JObject {{"user", GetUsername()}, {"viewing", scene}, {"timeSpent", viewTime}};
        var request = WebRequestUtilities.CreatePostRequest(LOG_VIEW_ENDPOINT, body);
        yield return request.SendWebRequest();
    }

    private IEnumerator LogClickCoroutine(string data) {
        var body = new JObject {{"user", GetUsername()}, {"button", data}};
        var request = WebRequestUtilities.CreatePostRequest(LOG_CLICK_ENDPOINT, body);
        yield return request.SendWebRequest();
    }
}