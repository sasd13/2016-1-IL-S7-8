using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulaire
{
    public class Form
    {
        private readonly List<FormAnswer> _formAnswers;

        public Form()
        {
            Questions = new QuestionRoot(this);
        }

        public QuestionRoot Questions { get; }

        public string Title { get; set; }

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
            FormAnswer formAnswer = new FormAnswer(username, this);
            _formAnswers.Add(formAnswer);

            return formAnswer;
        }
    }

}
