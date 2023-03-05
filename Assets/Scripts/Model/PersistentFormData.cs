using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PersistentFormData : MonoBehaviour {
    private readonly Dictionary<string, string> data = new Dictionary<string, string>();

    private void Awake() {
        Debug.Log("Hello I'm awake!!!");
        var instances = FindObjectsOfType<PersistentFormData>();
        if (instances.Length >= 2) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(this);
        }
    }

    public Dictionary<string, string> GetData() {
        return data;
    }

    public void SetData(string key, string value) {
        data[key] = value;
    }

    public bool HasValue(string key) {
        return data.ContainsKey(key) && data[key] != null && data[key] != "";
    }

    public int CountCompleted(IEnumerable<string> fields) {
        return fields.Count(HasValue);
    }

    public string GetValueWithFallback(string key, string fallback) {
        return data.ContainsKey(key) ? data[key] : fallback;
    }

    public bool IsYes(string key) {
        if (!HasValue(key)) return false;
        var value = data[key];
        return value.Equals("Yes") || value.Equals("yes");
    }

    public string GetValue(string key) {
        return data[key];
    }

    public bool isFilledIn(IEnumerable<string> fields) {
        return fields.All(HasValue);
    }
}