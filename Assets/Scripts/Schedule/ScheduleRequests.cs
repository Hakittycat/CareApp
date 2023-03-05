using Newtonsoft.Json.Linq;
using UnityEngine.Networking;

public static class ScheduleRequests {
    public static UnityWebRequest GetSchedule(string token) {
        const string endpoint = WebRequestUtilities.URL + "/schedule/get";
        return WebRequestUtilities.CreateGetRequest(endpoint, token);
    }

    public static UnityWebRequest SaveSchedule(string token, PatientSchedule schedule) {
        const string endpoint = WebRequestUtilities.URL + "/schedule/save";
        var payload = JObject.FromObject(schedule);
        return CreatePost(endpoint, payload, token);
    }

    private static UnityWebRequest CreatePost(string endpoint, JObject payload, string token) {
        var request = WebRequestUtilities.CreatePostRequest(endpoint, payload);
        WebRequestUtilities.SetRequestHeaderToken(request, token);
        return request;
    }
}