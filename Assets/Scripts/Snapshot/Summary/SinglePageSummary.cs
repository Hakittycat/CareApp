using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SinglePageSummary : MonoBehaviour {
    private QuestionnaireData questionnaireData;
    private PersistentFormData data;

    public void Awake() {
        data = FindObjectOfType<PersistentFormData>();
        questionnaireData = FindObjectOfType<QuestionnaireData>();
        Validator.CheckNotNull(data, "FormData missing!");
        Validator.CheckNotNull(questionnaireData, "Questionnaire missing!");
    }

    public void UpdateChildren() {
        foreach (var summary in GetComponentsInChildren<SectionSummary>()) {
            summary.UpdateSummaryData();
        }
    }

    public string GetAnswerSummaryFor(string sectionName) {
        var summary = new List<string>();
        var section = questionnaireData.FindSection(sectionName);
        if (section != null) {
            foreach (var question in section.questions) {
                if (IsButtonGroupInput(question.input)) {
                    if (!data.IsYes(question.key)) continue;
                    summary.Add(question.question.Replace("?", ""));
                }
                else {
                    if (data.HasValue(question.key)) {
                        summary.Add(data.GetValue(question.key));
                    }
                }
            }
        }

        var noResults = summary.Count == 0;
        return noResults ? "Unanswered." : string.Join(", ", summary);
    }

    private static bool IsButtonGroupInput(GameObject inputGameObject) {
        return inputGameObject.GetComponent<AnswerButtonGroup>() != null;
    }
}