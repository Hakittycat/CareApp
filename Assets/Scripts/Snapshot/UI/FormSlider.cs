using System;
using UnityEngine;
using UnityEngine.UI;

public class FormSlider : MonoBehaviour {
    
    public string field;
    private PersistentFormData formData;
    private Slider slider;

    private void Start() {
        formData = FindObjectOfType<PersistentFormData>();
        slider = GetComponentInChildren<Slider>();
        if (slider == null) throw new Exception("FormSlider must contain slider child component!");
        if (formData.HasValue(field)) {
            slider.value = float.Parse(formData.GetValue(field));
        }
    }

    public void UpdateFormValue(float value) {
        formData.SetData(field, $"{value}");
    }
}
