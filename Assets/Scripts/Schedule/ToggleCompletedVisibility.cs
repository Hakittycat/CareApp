using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCompletedVisibility : MonoBehaviour {
    [Header("Resources")]
    public ScheduleView view;

    public Button trigger;
    public TextMeshProUGUI text;

    private void Start() {
        trigger.onClick.AddListener(ToggleVisibility);
    }

    private void ToggleVisibility() {
        view.showCompleted = !view.showCompleted;
        UpdateText();
        view.RedrawScheduleButtons();
    }

    private void UpdateText() {
        text.text = GetButtonText();
    }

    private string GetButtonText() {
        return view.showCompleted ? "Hide Completed" : "Show Completed";
    }
}