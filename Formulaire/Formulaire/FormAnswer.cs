using System.Collections.Generic;

namespace Formulaire
{
    public class FormAnswer
    {
        readonly List<AnswerBase> _answers;
        
        internal FormAnswer(string uniqueName, Form form)
        {
            UniqueName = uniqueName;
            Form = form;
        }

        public string UniqueName { get; }

        private Form Form { get; }

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
            AnswerBase answer = new AnswerBase(question);
            _answers.Add(answer);

            return answer;
        }
    }
}