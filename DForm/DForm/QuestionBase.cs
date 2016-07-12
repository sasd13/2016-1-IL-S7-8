using DForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    public abstract class QuestionBase
    {
        QuestionFolder _parent;

        public QuestionBase Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                if (_parent != null)
                {
                    _parent.Children.Remove(this);
                }

                if (value != null)
                {
                    _parent = (QuestionFolder)value;
                    _parent.Children.Add(this);
                }
            }
        }

        public int Index
        {
            get
            {
                if (this is QuestionRoot)
                {
                    return 0;
                }

                return _parent.Children.IndexOf(this);
            }
            set
            {
                if (this is QuestionRoot)
                {
                    throw new InvalidOperationException("Index of type QuestionRoot cannot be changed");
                }

                _parent.Children.Remove(this);
                _parent.Children.Insert(value, this);
            }
        }

        public virtual string Title { get; set; }

        public abstract AnswerBase CreateAnswer();

        public virtual void Accept(IVisitor<Object> visitor)
        {
            throw new NotImplementedException();
        }
    }
}
