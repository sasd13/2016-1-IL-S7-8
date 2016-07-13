using DForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    public class QuestionFolder : QuestionBase
    {
        readonly List<QuestionBase> _questions;

        public QuestionFolder()
        {
            _questions = new List<QuestionBase>();
        }

        internal List<QuestionBase> Children => _questions;

        public QuestionBase AddNewQuestion(string questionClass)
        {
            QuestionBase question = QuestionBaseFactory.Create(questionClass);
            question.Parent = this;

            return question;
        }

        public bool Contains(QuestionBase question)
        {
            foreach (QuestionBase q in _questions)
            {
                if (q is QuestionFolder && ((QuestionFolder)q).Contains(question)) return true;
                else if (q.Equals(question)) return true;
            }

            return false;
        }

        public override AnswerBase CreateAnswer()
        {
            AnswerFolder answer = new AnswerFolder(this);
            
            foreach (QuestionBase question in _questions) answer.AddAnswerFor(question);

            return answer;
        }

        public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
    }
}
