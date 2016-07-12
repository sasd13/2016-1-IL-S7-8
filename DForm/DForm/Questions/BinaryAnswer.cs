using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm.Questions
{
    public class BinaryAnswer : AnswerBase
    {
        public BinaryAnswer(QuestionBase question) : base(question)
        {
        }

        public bool Value { get; set; }
    }
}
