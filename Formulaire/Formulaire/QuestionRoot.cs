using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulaire
{
    public class QuestionRoot : QuestionFolder
    {
        private Form _form;

        public QuestionRoot(QuestionBase question, Form form) : base(question)
        {
            _form = form;
        }

        public override string Title
        {
            get
            {
                return _form.Title;
            }
            set
            {
                _form.Title = value;
            }
        }

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
