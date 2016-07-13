using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    public class MultiCriteriaQuestion : QuestionBase
    {
        readonly List<MultiCriteriaOption> _options;

        public MultiCriteriaQuestion() : this(true)
        {
            
        }

        public MultiCriteriaQuestion(bool allowMultipleAnswers)
        {
            AllowMultipleAnswers = allowMultipleAnswers;
            _options = new List<MultiCriteriaOption>();
        }

        public bool AllowMultipleAnswers { get; }

        public List<MultiCriteriaOption> Options => _options;

        public override AnswerBase CreateAnswer()
        {
            return new MultiCriteriaAnswer(this);
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class MultiCriteriaOption
    {
        private KeyValuePair<string, string> _pair;

        public MultiCriteriaOption(QuestionBase question)
        {
            _pair = new KeyValuePair<string, string>();
        }

        public string Name => _pair.Key;

        public string Value => _pair.Value;

        public string Title { get; set; }

        public bool Selected { get; set; }
    }
}
