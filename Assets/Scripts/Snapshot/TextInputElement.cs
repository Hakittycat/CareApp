using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class TextInputElement : MonoBehaviour, InputElement {
    private TMP_InputField inputField;

    private void Awake() {
        inputField = GetComponent<TMP_InputField>();
    }

    public void ClearInputValue() {
        if (inputField == null) return;
        inputField.text = "";
    }

    public void SetInputValue(string value) {
        inputField.text = value;
    }

    public string GetInputValue() {
        return inputField.text;
    }
}