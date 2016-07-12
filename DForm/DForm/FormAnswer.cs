using System.Collections.Generic;

namespace DForm
{
    public class FormAnswer
    {
        Form _form;
        readonly List<AnswerBase> _answers;
        
        internal FormAnswer(string uniqueName, Form form)
        {
            UniqueName = uniqueName;
            _form = form;
            _answers = new List<AnswerBase>();
        }

        public string UniqueName { get; }

        private Form GetForm => _form;

        public AnswerBase FindAnswer(QuestionBase question)
        {
            foreach (AnswerBase answer in _answers)
            {
                if (answer.Question.Equals(question))
                {
                    return answer;
                }
            }

            return null;
        }

        public AnswerBase AddAnswerFor(QuestionBase question)
        {
            if (FindAnswer(question) != null)
            {
                return null;
            }

            AnswerBase answer = question.CreateAnswer();
            _answers.Add(answer);

            return answer;
        }

        internal AnswerBase RemoveAnswerOf(QuestionBase question)
        {
            AnswerBase answer = FindAnswer(question);

            if (answer != null)
            {
                _answers.Remove(answer);
            }

            return answer;
        }
    }
}