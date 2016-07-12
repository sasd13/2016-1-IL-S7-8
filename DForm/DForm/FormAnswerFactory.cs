using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    class FormAnswerFactory
    {
        public static FormAnswer create(String username, Form form)
        {
            return new FormAnswer(username, form);
        }
    }
}
