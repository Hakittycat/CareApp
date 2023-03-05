using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour {
    private WindowManager windowManager;
    public AudioSource source;
    public StoryConfiguration configuration;
    public StoryBookResources resources;
    public StoryConfiguration.Story selected;
    public UnityEvent onSlideChange;
    public int index;

    private void Awake() {
        windowManager = FindObjectOfType<WindowManager>();
    }

    public void SetStory(string storyName) {
        index = 0;
        selected = configuration.GetStory(storyName);
        resources.title.text = selected.name;
        UpdateStorySlide();
        onSlideChange.Invoke();
    }

    private void UpdateSlide() {
        var slide = selected.slides[index];
        resources.slide.sprite = slide.sprite;
        resources.text.text = slide.text;
    }

    public void RepeatAudioClip() {
        source.Stop();
        source.Play();
    }

    public void OnClickNextSlide() {
        if (IsAtLastSlide()) {
            windowManager.OpenWindow("Completed");
            return;
        }

        index++;
        UpdateStorySlide();
        onSlideChange.Invoke();
    }

    private void UpdateStorySlide() {
        source.Stop();
        UpdateSlide();
        InitializeThenStartAudio();
    }

    public void OnClickPreviousSlide() {
        if (!CanGoBack()) return;
        index--;
        UpdateStorySlide();
        onSlideChange.Invoke();
    }

    public bool CanGoBack() {
        return index > 0;
    }

    public bool IsAtLastSlide() {
        return index == selected.slides.Count - 1;
    }

    public bool CanGoNext() {
        return index < selected.slides.Count - 1;
    }

    private void InitializeThenStartAudio() {
        var slide = selected.slides[index];
        source.clip = slide.audioClip;
        source.Play();
    }


    [Serializable]
    public class StoryBookResources {
        public TextMeshProUGUI title;
        public Image slide;
        public TextMeshProUGUI text;
        public TextMeshProUGUI buttonText;
    }
}