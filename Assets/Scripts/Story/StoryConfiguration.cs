using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryConfiguration : MonoBehaviour {
    [HideInInspector]
    public List<Story> stories;

    private void Start() {
        stories = GetComponentsInChildren<StoryDataContainer>()
            .Select(container => container.story)
            .ToList();
    }

    public Story GetStory(string id) {
        var found = stories.Find(story => story.id.Equals(id));
        if (found == null) {
            throw new Exception("Invalid story id was provided.");
        }

        return found;
    }

    [Serializable]
    public class Story {
        public string id;
        public string name;
        public List<StorySlide> slides;
    }

    [Serializable]
    public class StorySlide {
        public string text;
        public AudioClip audioClip;
        public Sprite sprite;
    }
}