using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm.Questions
{
    class AnswerFolder : AnswerBase
    {
        List<AnswerBase> _answers;

        public AnswerFolder(QuestionBase question) : base(question)
        {
            _answers = new List<AnswerBase>();
        }

        public void AddAnswer(AnswerBase answer)
        {
            _answers.Add(answer);
        }

        public void RemoveAnswer(AnswerBase answer)
        {
            _answers.Remove(answer);
        }
    }
}
