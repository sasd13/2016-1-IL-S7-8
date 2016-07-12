using DForm.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    public interface IVisitor<T> where T : class
    {
        T Visit(BinaryQuestion binaryQuestion);

        T Visit(MultiChoiceQuestion multiChoiceQuestion);
    }
}
