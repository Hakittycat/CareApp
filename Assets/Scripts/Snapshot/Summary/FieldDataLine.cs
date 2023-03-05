using System;
using TMPro;
using UnityEngine;

public class FieldDataLine : MonoBehaviour {
    [Header("The field value in the snapshot form.")]
    public string field;

    [Header("Resources")]
    public TextMeshProUGUI valueTextField;

    private PersistentFormData formData;

    private void Awake() {
        formData = FindObjectOfType<PersistentFormData>();
    }

    private void Start() {
        UpdateField();
    }

    public void UpdateField() {
        if (formData == null) return;
        if (!formData.HasValue(field)) {
            valueTextField.text = "Unanswered";
        }
        else {
            var value = formData.GetValue(field);
            valueTextField.text = value;
        }
    }
}