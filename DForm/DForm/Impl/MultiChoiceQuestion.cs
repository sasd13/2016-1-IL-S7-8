using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    public class MultiChoiceQuestion : QuestionBase
    {
        readonly List<MultiChoiceOption> _options;

        public MultiChoiceQuestion() : this(true)
        {
            
        }

        public MultiChoiceQuestion(bool allowMultipleAnswers)
        {
            AllowMultipleAnswers = allowMultipleAnswers;
            _options = new List<MultiChoiceOption>();
        }

        public bool AllowMultipleAnswers { get; }

        public List<MultiChoiceOption> Options => _options;

        public override AnswerBase CreateAnswer()
        {
            return new MultiChoiceAnswer(this);
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class MultiChoiceOption
    {
        private KeyValuePair<string, string> _pair;

        public MultiChoiceOption(QuestionBase question)
        {
            _pair = new KeyValuePair<string, string>();
        }

        public string Name => _pair.Key;

        public string Value => _pair.Value;

        public string Title { get; set; }

        public bool Selected { get; set; }
    }
}
