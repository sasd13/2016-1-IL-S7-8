using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    public class OpenQuestion : QuestionBase
    {
        public bool AllowEmptyAnswer { get; set; }

        public override AnswerBase CreateAnswer()
        {
            return new OpenAnswer(this);
        }
    }
}
