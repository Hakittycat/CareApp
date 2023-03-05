using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ScheduleController : MonoBehaviour {
    
    public UnityEvent onScheduleChange;
    private PersistentScheduleData scheduleData;

    private void Awake() {
        scheduleData = FindObjectOfType<PersistentScheduleData>();
    }

    public void SetSchedule(List<ScheduledEvent> events) {
        scheduleData.schedule = events;
        onScheduleChange.Invoke();
    }

    public bool Contains(string eventId) {
        return scheduleData.schedule.Any(item => item.eventId.Equals(eventId));
    }

    public void Remove(ScheduledEvent scheduledEvent) {
        scheduleData.schedule.Remove(scheduledEvent);
        onScheduleChange.Invoke();
    }

    public void Add(string eventId) {
        var item = new ScheduledEvent(eventId, scheduleData.schedule.Count, 0);
        scheduleData.schedule.Add(item);
        onScheduleChange.Invoke();
    }

    public void AddAll(List<string> ids) {
        var size = scheduleData.schedule.Count;
        for (var i = 0; i < ids.Count; i++) {
            var item = new ScheduledEvent(ids[i], size + i, 0);
            scheduleData.schedule.Add(item);
        }

        onScheduleChange.Invoke();
    }

    public void MarkComplete(ScheduledEvent scheduledEvent) {
        scheduledEvent.SetCompleted(1);
        onScheduleChange.Invoke();
    }

    public List<ScheduledEvent> GetSchedule() {
        return new List<ScheduledEvent>(scheduleData.schedule);
    }
}