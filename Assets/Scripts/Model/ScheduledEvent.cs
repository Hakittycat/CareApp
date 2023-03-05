using System;
using UnityEngine;

[Serializable]
public class ScheduledEvent {
    [HideInInspector] public string eventId;
    [HideInInspector] public int position;
    [HideInInspector] public int completed;

    public ScheduledEvent(string eventId, int position, int completed) {
        this.eventId = eventId;
        this.position = position;
        this.completed = completed;
    }

    public void SetCompleted(int complete) {
        completed = complete;
    }

    public string GetEventId() {
        return eventId;
    }

    public void SetPosition(int newPosition) {
        position = newPosition;
    }

    public int GetPosition() {
        return position;
    }

    public bool IsCompleted() {
        return completed == 1;
    }
}