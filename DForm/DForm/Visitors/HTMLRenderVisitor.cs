using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    public class HTMLRenderVisitor : IVisitor<string>
    {
        const string TAG_START = "<div>", TAG_END = "</div>";

        public string Visit(Form form)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("<form>");

            if (form.Title != null)
            {
                builder.Append("<h3>");
                builder.Append(form.Title);
                builder.Append("</h3>");
            }

            foreach (QuestionBase question in form.Questions.Children)
            {
                builder.Append(question.Accept(this));
            }
            
            builder.Append("</form>");

            return builder.ToString();
        }

        public string Visit(QuestionFolder questionFolder)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(TAG_START);

            if (questionFolder.Title != null)
            {
                builder.Append("<h4>");
                builder.Append(questionFolder.Title);
                builder.Append("</h4>");
            }

            foreach (QuestionBase question in questionFolder.Children)
            {
                builder.Append(question.Accept(this));
            }

            builder.Append(TAG_END);

            return builder.ToString();
        }

        public string Visit(BinaryQuestion binaryQuestion)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(TAG_START);

            TryAppendTitleForQuestion(builder, binaryQuestion);
            
            foreach (MultiCriteriaOption option in binaryQuestion.Options)
            {
                builder.Append("<input type='radio' name='");
                builder.Append(option.Key);
                builder.Append("' value='");
                builder.Append(option.Value);
                builder.Append("'>");
                builder.Append(option.Title);
            }

            builder.Append(TAG_END);

            return builder.ToString();
        }

        private void TryAppendTitleForQuestion(StringBuilder builder, QuestionBase question)
        {
            if (question.Title != null)
            {
                builder.Append("<label>");
                builder.Append(question.Title);
                builder.Append("</label>");
            }
        }

        public string Visit(MultiCriteriaQuestion multiChoiceQuestion)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(TAG_START);

            TryAppendTitleForQuestion(builder, multiChoiceQuestion);

            builder.Append("<select");

            if (multiChoiceQuestion.AllowMultipleAnswers)
            {
                builder.Append(" multiple='multiple'");
            }

            foreach (MultiCriteriaOption option in multiChoiceQuestion.Options)
            {
                builder.Append("<option name='");
                builder.Append(option.Key);
                builder.Append("' value='");
                builder.Append(option.Value);

                if (option.Selected)
                {
                    builder.Append(" selected='selected'");
                }
                builder.Append("'>");
                builder.Append(option.Title);
                builder.Append("</option>");
            }

            builder.Append("</select>");

            builder.Append(TAG_END);

            return builder.ToString();
        }

        public string Visit(OpenQuestion openQuestion)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(TAG_START);

            TryAppendTitleForQuestion(builder, openQuestion);

            builder.Append("<textarea");

            if (!openQuestion.AllowEmptyAnswer)
            {
                builder.Append(" required='required'");
            }

            builder.Append(">");
            builder.Append("</textarea>");

            builder.Append(TAG_END);

            return builder.ToString();
        }
    }
}
