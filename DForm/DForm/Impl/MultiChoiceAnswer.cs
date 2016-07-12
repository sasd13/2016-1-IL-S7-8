using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    public class MultiChoiceAnswer : AnswerBase
    {
        readonly List<MultiChoiceOption> _answers;

        public MultiChoiceAnswer(QuestionBase question) : base(question)
        {
            _answers = new List<MultiChoiceOption>();
        }

        public List<MultiChoiceOption> Answers => _answers;
    }
}
