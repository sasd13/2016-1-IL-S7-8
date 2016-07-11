using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulaire
{
    public class OpenQuestion : Question
    {
        public bool AllowEmptyAnswer { get; set; }

        public override void Accept(IVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public override AnswerBase createAnswer()
        {
            throw new NotImplementedException();
        }
    }
}
