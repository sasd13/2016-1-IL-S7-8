using DForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm.Questions
{
    public class BinaryQuestion : QuestionBase
    {
        public override AnswerBase CreateAnswer()
        {
            return new BinaryAnswer(this);
        }
    }
}
