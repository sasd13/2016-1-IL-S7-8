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

        public virtual QuestionBase Parent
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

                _parent = (QuestionFolder)value;

                if (value != null)
                {
                    _parent.Children.Add(this);
                }
            }
        }

        public virtual int Index
        {
            get
            {
                return _parent.Children.IndexOf(this);
            }
            set
            {
                _parent.Children.Remove(this);
                _parent.Children.Insert(value, this);
            }
        }

        public virtual string Title { get; set; }

        public abstract AnswerBase CreateAnswer();

        public virtual T Accept<T>(IVisitor<T> visitor)
        {
            throw new NotImplementedException();
        }
    }
}
