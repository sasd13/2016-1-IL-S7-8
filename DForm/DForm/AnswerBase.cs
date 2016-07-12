using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    public class AnswerBase
    {
        public AnswerBase(QuestionBase question)
        {
            Question = question;
        }

        public QuestionBase Question { get; }

        public string Title { get; set; }
    }
}
