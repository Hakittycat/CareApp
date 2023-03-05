using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestionnaireData : MonoBehaviour {
    
    public List<QuestionnaireSection> sections;

    public QuestionnaireSection FindSection(string sectionName) {
        return sections.Find(section => section.sectionName.Equals(sectionName));
    }

    public IEnumerable<QuestionDetails> GetAllQuestions() {
        Debug.Log("Get all questions");
        return sections.SelectMany(section => section.questions);
    }
}