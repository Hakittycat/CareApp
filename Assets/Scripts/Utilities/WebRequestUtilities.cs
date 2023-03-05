using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;

public static class WebRequestUtilities {
    /* localhost */ public const string URL = "http://localhost:80";
    /* gcloud compute engine vm */ /*public const string URL = "http://34.86.27.110";*/

    public static string username = string.Empty;
    public static string token = string.Empty;
    private static byte[] getJsonBytes(JObject json) {
        
        return Encoding.UTF8.GetBytes(json.ToString());
    }

    public static void SetRequestHeaderToken(UnityWebRequest request, string token) {
        
        request.SetRequestHeader("Authorization", $"Bearer {token}");
    }

    public static UnityWebRequest CreateGetRequest(string url, [CanBeNull] string token) {
        var webRequest = new UnityWebRequest(url, "GET") {
            downloadHandler = new DownloadHandlerBuffer()
        };
        if (token != null) {
            webRequest.SetRequestHeader("Authorization", $"Bearer {token}");
        }

        return webRequest;
    }

    public static UnityWebRequest CreatePostRequest(string url, JObject body) {
        var webRequest = new UnityWebRequest(url, "POST") {
            downloadHandler = new DownloadHandlerBuffer(),
            uploadHandler = new UploadHandlerRaw(getJsonBytes(body))
        };
        webRequest.SetRequestHeader("Content-Type", "application/json");
        return webRequest;
    }
    
    public static bool IsSuccess(this UnityWebRequest request)
    {
        if (request.result == UnityWebRequest.Result.ConnectionError) { return false; }
 
        if (request.responseCode == 0) { return true; }
        if (request.responseCode == (long)System.Net.HttpStatusCode.OK) { return true; }
 
        return false;
    }
}
