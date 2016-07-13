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

        public override AnswerBase CreateAnswer() => new MultiCriteriaAnswer(this);

        public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
    }

    public class MultiCriteriaOption
    {
        KeyValuePair<string, string> _pair;

        public MultiCriteriaOption(QuestionBase question, string key, string value)
        {
            _pair = new KeyValuePair<string, string>(key, value);
        }

        public string Key => _pair.Key;

        public string Value => _pair.Value;

        public string Title { get; set; }

        public bool Selected { get; set; }
    }
}
