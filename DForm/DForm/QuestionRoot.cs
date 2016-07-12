using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    public class QuestionRoot : QuestionFolder
    {
        Form _form;

        internal QuestionRoot(Form form)
        {
            _form = form;
        }

        public Form GetForm => _form;
    }
}
