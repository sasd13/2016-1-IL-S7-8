using DForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    public interface IVisitor<T>
    {
        T Visit(Form form);

        T Visit(QuestionFolder questionFolder);

        T Visit(BinaryQuestion binaryQuestion);

        T Visit(MultiChoiceQuestion multiChoiceQuestion);

        T Visit(OpenQuestion openQuestion);
    }

    public static class IVisitorExtensions
    {
        public static T Visit<T>(this IVisitor<T> @this, QuestionBase question)
        {
            return question.Accept(@this);
        }
    }
}
