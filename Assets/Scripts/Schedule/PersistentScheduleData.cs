using System.Collections.Generic;
using UnityEngine;

public class PersistentScheduleData : MonoBehaviour {
    public List<ScheduledEvent> schedule;

    private void Awake() {
        var found = FindObjectOfType<PersistentScheduleData>();
        if (found != null && found.gameObject != gameObject) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(this);
            schedule = new List<ScheduledEvent>();
        }
    }
}