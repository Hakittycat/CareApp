using System;
using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using UnityEngine;

public class DateSelector : MonoBehaviour, InputElement {
    public CustomDropdown month;
    public CustomDropdown day;
    public CustomDropdown year;

    private void Awake() {
        SetDays(31);
        SetYears(1900, 2021);
    }

    private void SetDays(int amount) {
        day.dropdownItems.Clear();
        for (var i = 0; i < amount; i++) {
            day.CreateNewItemFast($"{i + 1}", null);
        }
    }

    private void SetYears(int start, int end) {
        for (var i = end; i > start; i--) {
            year.CreateNewItemFast($"{i}", null);
        }
    }

    private int GetIndex(IReadOnlyList<CustomDropdown.Item> items, string itemName) {
        for (var i = 0; i < items.Count; i++) {
            var item = items[i];
            if (item.itemName.Equals(itemName)) {
                return i;
            }
        }

        return 0;
    }

    public void ClearInputValue() {
        month.selectedItemIndex = 0;
        day.selectedItemIndex = 0;
        year.selectedItemIndex = 0;
    }

    public void SetInputValue(string value) {
        var parsed = value.Split('-');
        if (parsed.Length != 3) return;
        month.selectedText.text = parsed[0];
        day.selectedText.text = parsed[1];
        year.selectedText.text = parsed[2];
    }

    public string GetInputValue() {
        var monthText = month.selectedText.text;
        var dayText = day.selectedText.text;
        var yearText = year.selectedText.text;
        return $"{monthText}-{dayText}-{yearText}";
    }
}