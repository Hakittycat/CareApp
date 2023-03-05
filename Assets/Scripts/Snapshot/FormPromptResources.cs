using System;
using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using TMPro;
using UnityEngine;

public class FormPromptResources : MonoBehaviour {
    public ProgressBar progressBar;
    public TextMeshProUGUI question;
    public TextMeshProUGUI description;
    public ButtonManagerBasic nextButton;
    public ButtonManagerBasic backButton;
    public List<GameObject> inputElements = new List<GameObject>();

    private void HideAllInputElements() {
        inputElements.ForEach(element => element.SetActive(false));
    }

    public void SetQuestion(string text) {
        question.text = text;
    }

    public void SetDescription(string text) {
        description.text = text;
    }

    public void SetProgress(float current, float max) {
        progressBar.currentPercent = (current / max) * 100;
        progressBar.UpdateUI();
    }

    public void ClearInputs() {
        inputElements.ForEach(element => element.GetComponent<InputElement>().ClearInputValue());
    }

    public void SetAnswer(string value) {
        var input = GetVisibleInputElement();
        if (input != null) {
            input.GetComponent<InputElement>().SetInputValue(value);
        }
    }

    private GameObject GetVisibleInputElement() {
        return inputElements.Find(inputElement => inputElement.activeSelf);
    }

    public void ShowInputElement(GameObject inputElement) {
        HideAllInputElements();
        inputElement.SetActive(true);
    }
}