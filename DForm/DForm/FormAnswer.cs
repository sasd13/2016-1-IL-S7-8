using System.Collections.Generic;

namespace DForm
{
    public class FormAnswer
    {
        Form _form;
        AnswerFolder _answer;
        
        public FormAnswer(string uniqueName, Form form)
        {
            UniqueName = uniqueName;
            _form = form;
            _answer = new AnswerFolder(form.Questions);
        }

        public string UniqueName { get; }

        private Form Form => _form;

        public AnswerBase FindAnswer(QuestionBase question)
        {
            return _answer.FindAnswerFor(question);
        }

        public AnswerBase AddAnswerFor(QuestionBase question)
        {
            return _answer.AddAnswerFor(question);
        }

        public AnswerBase RemoveAnswerOf(QuestionBase question)
        {
            return _answer.RemoveAnswerOf(question);
        }
    }
}