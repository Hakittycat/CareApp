using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestionOverviewButton : MonoBehaviour, IPointerClickHandler {
    
    public OverviewButtonSettings buttonSettings;
    public Image image;
    public TextMeshProUGUI textLabel;
    public UnityEvent onClick;

    public void OnPointerClick(PointerEventData eventData) {
        onClick.Invoke();
    }

    public void SetLabelText(string label) {
        textLabel.text = label;
    }

    public void SetCompleted() {
        SetStatusImage(buttonSettings.checkmark, buttonSettings.complete);
    }

    public void SetIncomplete() {
        SetStatusImage(buttonSettings.warning, buttonSettings.incomplete);
    }

    private void SetStatusImage(Sprite sprite, Color color) {
        image.sprite = sprite;
        image.color = color;
    }
}