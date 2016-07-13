using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    public class MultiCriteriaAnswer : AnswerBase
    {
        readonly List<MultiCriteriaOption> _answers;

        public MultiCriteriaAnswer(QuestionBase question) : base(question)
        {
            _answers = new List<MultiCriteriaOption>();
        }

        public List<MultiCriteriaOption> Answers => _answers;
    }
}
