using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    public class BinaryAnswer : MultiChoiceAnswer
    {
        public BinaryAnswer(QuestionBase question) : base(question)
        {
        }

        public MultiChoiceOption Answer => Answers.Any() ? Answers.First() : null;
    }
}
