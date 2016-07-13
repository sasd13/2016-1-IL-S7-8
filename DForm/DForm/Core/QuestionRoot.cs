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

        public override QuestionBase Parent
        {
            get
            {
                return null;
            }

            set
            {
                throw new InvalidOperationException();
            }
        }

        public override int Index
        {
            get
            {
                return 0;
            }

            set
            {
                throw new InvalidOperationException();
            }
        }
    }
}
