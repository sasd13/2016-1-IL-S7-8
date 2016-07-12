using DForm.Questions;
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
            QuestionBase question = QuestionBaseFactory.create(questionClass);
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
            AnswerFolder answerFolder = new AnswerFolder(this);
            
            foreach (QuestionBase question in _questions)
            {
                answerFolder.AddAnswer(question.CreateAnswer());
            }

            return answerFolder;
        }

        public override void Accept(IVisitor<object> visitor)
        {
            foreach (QuestionBase question in _questions)
            {
                question.Accept(visitor);
            }
        }
    }
}
