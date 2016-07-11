using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulaire
{
    class QuestionBaseFactory
    {
        public static QuestionBase create(string questionClass, QuestionBase parent)
        {
            string className = questionClass.Split(',')[0];
            string nameSpace = questionClass.Split(',')[1];

            className = className.Split('.')[1];

            if (String.Equals(className, typeof(OpenQuestion).Name))
            {
                return new OpenQuestion();
            }
            else if (String.Equals(className, typeof(QuestionFolder).Name))
            {
                return new QuestionFolder(parent);

            }
            else
            {
                throw new ArgumentException("Your class type is goddamn wrong, buddy!");
            }
        }
    }
}
