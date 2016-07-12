using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm.Questions
{
    public class MultiChoiceAnswer : AnswerBase
    {
        readonly List<AnswerBase> _answers;

        public MultiChoiceAnswer(QuestionBase question) : base(question)
        {
            _answers = new List<AnswerBase>();
        }

        public List<AnswerBase> Answers => _answers;
    }
}
