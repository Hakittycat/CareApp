using Michsky.UI.ModernUIPack;
using UnityEngine;

public class FormDropdown : MonoBehaviour {
    public string field;
    public PersistentFormData formData;
    [HideInInspector] public CustomDropdown dropdown;
    private bool hasTimedOut;
    private int timeToWait;

    private void Awake() {
        dropdown = GetComponent<CustomDropdown>();
        formData = FindObjectOfType<PersistentFormData>();
        AddChangeListener();
    }

    private void AddChangeListener() {
        dropdown.dropdownEvent.AddListener(selectedIndex => {
            var chosen = dropdown.dropdownItems[selectedIndex];
            if (chosen != null) {
                formData.SetData(field, chosen.itemName);
            }
        });
    }

    private void LoadDropdownValue() {
        if (formData.HasValue(field)) {
            dropdown.selectedText.text = formData.GetValue(field);
        }
    }
}