using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    public class OpenAnswer : AnswerBase
    {
        public OpenAnswer(QuestionBase question) : base(question)
        {
            
        }

        public string FreeAnswer { get; set; }
    }
}
