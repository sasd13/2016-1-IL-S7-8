using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm.Questions
{
    public class MultiChoiceQuestion : QuestionBase
    {
        public MultiChoiceQuestion() : this(false)
        {
            
        }

        public MultiChoiceQuestion(bool allowMultipleAnswers)
        {
            IsMultipleAnswersAllowed = allowMultipleAnswers;
        }

        public bool IsMultipleAnswersAllowed { get; }

        public override AnswerBase CreateAnswer()
        {
            return new MultiChoiceAnswer(this);
        }

        public override void Accept(IVisitor<object> visitor)
        {
            throw new NotImplementedException();
        }
    }
}
