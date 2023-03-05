using UnityEngine;
using UnityEngine.UI;

public class ScheduledEventButton : MonoBehaviour {
    private ScheduledEvent subject;
    public Image eventImage;
    public Image checkmarkImage;
    public Button button;
    private EventButtonManager manager;

    public ScheduledEvent GetSubject() {
        return subject;
    }

    private void Awake() {
        manager = FindObjectOfType<EventButtonManager>();
    }

    private void Start() {
        eventImage.sprite = manager.GetSprite(subject);
        if (!subject.IsCompleted()) {
            button.onClick.AddListener(OnButtonClick);
        }

        ShowCheckmarkIfCompleted();
    }

    private void OnButtonClick() {
        if (!subject.IsCompleted()) {
            manager.ShowActionWindowFor(subject);
        }
    }

    private void ShowCheckmarkIfCompleted() {
        var alpha = subject.IsCompleted() ? 0.75f : 0f;
        Utilities.SetImageAlpha(checkmarkImage, alpha);
    }

    public void SetSubject(ScheduledEvent scheduledEvent) {
        subject = scheduledEvent;
    }
}