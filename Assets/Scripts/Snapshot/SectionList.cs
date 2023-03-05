using System;
using UnityEngine;
using UnityEngine.UI;

public class SectionList : MonoBehaviour {
    public WindowManager windowManager;
    public PromptController promptController;
    private PersistentFormData data;
    public QuestionnaireData questionnaireData;
    public GameObject questionnaireCardPrefab;
    public VerticalLayoutGroup contentList;
    public ImageSettings completed;
    public ImageSettings regular;

    private void Awake() {
        data = FindObjectOfType<PersistentFormData>();
    }

    private void Start() {
        questionnaireData.sections.ForEach(CreateQuestionnaireListElement);
    }

    public void UpdateListElements() {
        for (var i = 0; i < contentList.transform.childCount; i++) {
            var child = contentList.transform.GetChild(i);
            var component = child.GetComponent<QuestionnaireSectionCard>();
            if (component != null) {
                SetComponentData(component, questionnaireData.sections[i]);
            }
        }
    }

    private void CreateQuestionnaireListElement(QuestionnaireSection section) {
        var created = Instantiate(questionnaireCardPrefab, contentList.transform);
        var component = created.GetComponent<QuestionnaireSectionCard>();
        var button = created.GetComponent<Button>();
        SetComponentData(component, section);
        button.onClick.AddListener(() => promptController.SetSection(section.sectionName));
        button.onClick.AddListener(() => windowManager.OpenWindow("Form"));
    }

    private void SetComponentData(QuestionnaireSectionCard component, QuestionnaireSection section) {
        component.sectionTitle.text = section.sectionName;
        if (isCompleted(section)) {
            component.sectionStatus.text = "Complete";
            component.statusImage.sprite = completed.sprite;
            component.statusImage.color = completed.color;
        }
        else {
            var answered = data.CountCompleted(section.GetQuestionKeys());
            component.sectionStatus.text = $"{answered} of {section.questions.Count} completed";
            component.statusImage.sprite = regular.sprite;
            component.statusImage.color = regular.color;
        }
    }

    private bool isCompleted(QuestionnaireSection section) {
        return data.isFilledIn(section.GetQuestionKeys());
    }

    [Serializable]
    public class ImageSettings {
        public Sprite sprite;
        public Color color;
    }
}