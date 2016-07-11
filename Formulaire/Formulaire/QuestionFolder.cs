using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulaire
{
    public class QuestionFolder : QuestionBase
    {
        private readonly List<QuestionBase> _questions;

        public QuestionFolder(QuestionBase parent)
        {
            Parent = parent;
            _questions = new List<QuestionBase>();
        }

        public QuestionBase AddNewQuestion(string questionClass)
        {
            QuestionBase question = QuestionBaseFactory.create(questionClass, this);
            _questions.Add(question);
            question.Index = _questions.Count - 1;

            return question;
        }

        public bool Contains(QuestionBase question) => _questions.Contains(question);

        public override AnswerBase createAnswer()
        {
            throw new NotImplementedException();
        }

        public override void Accept(IVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
