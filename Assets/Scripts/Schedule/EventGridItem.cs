using System;
using UnityEngine;
using UnityEngine.UI;

public class EventGridItem : MonoBehaviour {
    
    private ScheduleEditor editor;
    public EventConfiguration.EventDefinition definition;
    public Image mainImage;
    public Image checkmark;
    public Button button;
    [HideInInspector]
    public bool selected;
    public EventConfirmationModal confirmationModal;
    public ScheduleController scheduleController;

    private void Start() {
        editor = FindObjectOfType<ScheduleEditor>();
        if (editor == null) {
            throw new Exception("ScheduleEditor must be present in scene for this component!");
        }
    }

    public void UpdateUI() {
        SetCheckmarkVisible(selected);
    }

    public void Reset() {
        selected = false;
        SetCheckmarkVisible(false);
    }

    public void Initialize() {
        if (definition == null) {
            throw new Exception("EventDefinition must be defined before invoking the initialization function!");
        }

        mainImage.sprite = definition.sprite;
        SetCheckmarkVisible(false);
        button.onClick.AddListener(OnClick);
    }

    private void SetCheckmarkVisible(bool visible) {
        checkmark.gameObject.SetActive(visible);
    }

    private void OnClick() {
        if (!selected && scheduleController.Contains(definition.id)) {
            confirmationModal.SetEventToConfirm(this);
            confirmationModal.Open();
            return;
        }
        selected = !selected;
        SetCheckmarkVisible(selected);
        if (selected) {
            editor.Add(definition.id);
        }
        else {
            editor.Remove(definition.id);
        }
    }
}