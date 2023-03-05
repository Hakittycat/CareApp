using System;
using TMPro;
using UnityEngine;

public class SectionSummary : MonoBehaviour {
    private SinglePageSummary singlePageSummary;
    public string sectionName;
    public TextMeshProUGUI label;
    public TextMeshProUGUI answers;

    private void Awake() {
        singlePageSummary = FindObjectOfType<SinglePageSummary>();
    }

    private void Start() {
        UpdateSummaryData();
    }

    public void UpdateSummaryData() {
        label.text = sectionName;
        answers.text = singlePageSummary.GetAnswerSummaryFor(sectionName);
    }
}