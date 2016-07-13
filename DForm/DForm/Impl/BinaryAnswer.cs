using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    public class BinaryAnswer : MultiCriteriaAnswer
    {
        public BinaryAnswer(QuestionBase question) : base(question)
        {
        }

        public MultiCriteriaOption Answer => Answers.Any() ? Answers.First() : null;
    }
}
