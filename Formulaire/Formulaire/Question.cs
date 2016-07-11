using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulaire
{
    abstract public class Question : QuestionBase
    {
        abstract public void Accept(IVisitor visitor);
    }
}
