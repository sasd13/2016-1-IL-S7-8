using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulaire
{
    public abstract class QuestionBase
    {
        public QuestionBase Parent { get; set; }

        public int Index { get; set; }

        public virtual string Title { get; set; }

        public abstract AnswerBase createAnswer();

        public abstract void Accept(IVisitor visitor);
    }
}
