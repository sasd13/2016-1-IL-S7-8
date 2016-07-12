using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    class QuestionBaseFactory
    { 
        public static QuestionBase create(string mClass)
        {
            string fullName = mClass.Split(',')[0];
            Type type = Type.GetType(fullName);

            if (typeof(QuestionBase).IsAssignableFrom(type))
            {
                return (QuestionBase)Activator.CreateInstance(Type.GetType(fullName));
            }
            else
            {
                throw new ArgumentException("Invalid argument class: " + type);
            }
        }
    }
}
