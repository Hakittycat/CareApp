using System;
using Michsky.UI.ModernUIPack;
using UnityEngine;

public class YearDropdown : MonoBehaviour {

    public CustomDropdown yearDropdown;
    public int start;

    private void Awake() {
        var date = DateTime.Now;
        for (var i = date.Year; i > start; i--) {
            yearDropdown.CreateNewItemFast($"{i}", null);
        }
    }
}
