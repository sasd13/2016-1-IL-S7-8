using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DForm.Questions;

namespace DForm.Visitors
{
    public class RenderVisitor : IVisitor<string>
    {
        public string Visit(MultiChoiceQuestion multiChoiceQuestion)
        {
            throw new NotImplementedException();
        }

        public string Visit(BinaryQuestion binaryQuestion)
        {
            throw new NotImplementedException();
        }
    }
}
