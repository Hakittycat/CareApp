using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryControls : MonoBehaviour {
    public StoryManager storyManager;
    public Button next;
    public Button back;

    public void UpdateButtons() {
        UpdateNextButton();
        UpdateButton(back, storyManager.CanGoBack());
    }

    private void UpdateNextButton() {
        var label = storyManager.IsAtLastSlide() ? "COMPLETE" : "NEXT";
        next.GetComponentInChildren<TextMeshProUGUI>().text = label;
    }

    private static void UpdateButton(Selectable button, bool isEnabled) {
        const float faded = 0.75f;
        const float full = 1f;
        SetButtonAlpha(button, isEnabled ? full : faded);
        button.enabled = isEnabled;
    }

    private static void SetButtonAlpha(Selectable button, float alpha) {
        var modified = button.image.color;
        modified.a = alpha;
        button.image.color = modified;
    }
}