using System;
using System.Collections.Generic;
using UnityEngine;

public class EventConfiguration : MonoBehaviour {
    public List<EventDefinition> presets;

    public EventDefinition GetDefinition(string id) {
        var def = presets.Find(image => image.id.Equals(id));
        if (def == null) {
            throw new Exception($"There is not event defined for id: {id}");
        }

        return def;
    }

    [Serializable]
    public class EventDefinition {
        public string id;
        public Sprite sprite;
    }
}