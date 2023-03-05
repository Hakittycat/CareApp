using System.Collections.Generic;
using UnityEngine;

public class AnswerButtonGroup : MonoBehaviour, InputElement {
    private readonly List<AnswerButton> buttons = new List<AnswerButton>();
    public Color regular;
    public Color regularText;
    public Color highlighted;
    public Color highlightedText;
    private string answer;

    public void SetAnswer(string updated) {
        answer = updated;
        UpdateButtons();
    }

    public void Register(AnswerButton answerButton) {
        buttons.Add(answerButton);
    }

    private void UpdateButtons() {
        foreach (var answerButton in buttons) {
            if (answerButton.answer.Equals(answer)) {
                answerButton.SetBackgroundColor(highlighted);
                answerButton.SetTextColor(highlightedText);
            }
            else {
                answerButton.SetBackgroundColor(regular);
                answerButton.SetTextColor(regularText);
            }
        }
    }

    public void ClearInputValue() {
        answer = "";
        UpdateButtons();
    }

    public void SetInputValue(string value) {
        answer = value;
        UpdateButtons();
    }

    public string GetInputValue() {
        return answer;
    }
}