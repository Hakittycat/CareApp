using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class QuestionnaireSection {

    public string sectionName;
    public List<QuestionDetails> questions;
    

    public List<string> GetQuestionKeys() {
        return questions.Select(question => question.key).ToList();
    }

    public QuestionDetails GetQuestionAtPosition(int index) {
        return questions[index];
    }
    
    public List<QuestionDetails> getQuestionDetailsList() {
        return new List<QuestionDetails>(questions);
    }
}