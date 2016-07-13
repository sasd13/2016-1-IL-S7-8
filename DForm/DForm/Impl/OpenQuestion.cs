using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    public class OpenQuestion : QuestionBase
    {
        public OpenQuestion()
        {
            AllowEmptyAnswer = true;
        }

        public bool AllowEmptyAnswer { get; set; }

        public override AnswerBase CreateAnswer()
        {
            return new OpenAnswer(this);
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
