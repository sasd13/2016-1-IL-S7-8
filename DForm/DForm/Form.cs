using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    public class Form
    {
        readonly List<FormAnswer> _formAnswers;

        public Form()
        {
            Questions = new QuestionRoot(this);
            _formAnswers = new List<FormAnswer>();
        }

        public QuestionRoot Questions { get; }

        public string Title
        {
            get
            {
                return Questions.Title;
            }
            set
            {
                Questions.Title = value;
            }
        }

        public int AnswerCount => _formAnswers.Count;

        public FormAnswer FindOrCreateAnswer(string username)
        {
            foreach(FormAnswer formAnswer in _formAnswers)
            {
                if (String.Equals(formAnswer.UniqueName, username))
                {
                    return formAnswer;
                }
            }

            return CreateAnswer(username);
        }

        public FormAnswer CreateAnswer(string username)
        {
            FormAnswer formAnswer = FormAnswerFactory.create(username, this);
            _formAnswers.Add(formAnswer);

            return formAnswer;
        }

        public T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
