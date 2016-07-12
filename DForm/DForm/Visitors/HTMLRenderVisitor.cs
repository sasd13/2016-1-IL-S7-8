using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    public class HTMLRenderVisitor : IVisitor<string>
    {
        public string Visit(Form form)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("<form>");

            if (form.Title != null)
            {
                builder.Append("<h1>");
                builder.Append(form.Title);
                builder.Append("</h1>");
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

            builder.Append("<div>");

            if (questionFolder.Title != null)
            {
                builder.Append("<h2>");
                builder.Append(questionFolder.Title);
                builder.Append("</h2>");
            }

            foreach (QuestionBase questionBase in questionFolder.Children)
            {
                builder.Append(questionBase.Accept(this));
            }
            builder.Append("</div>");

            return builder.ToString();
        }

        public string Visit(BinaryQuestion binaryQuestion)
        {
            StringBuilder builder = new StringBuilder();

            TryAppendTitleForQuestion(builder, binaryQuestion);
            
            foreach (MultiChoiceOption option in binaryQuestion.Options)
            {
                builder.Append("<input type='radio' name=");
                builder.Append(option.Name);
                builder.Append("' value='");
                builder.Append(option.Value);
                builder.Append("'>");
                builder.Append(option.Title);
            }

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

        public string Visit(MultiChoiceQuestion multiChoiceQuestion)
        {
            StringBuilder builder = new StringBuilder();

            TryAppendTitleForQuestion(builder, multiChoiceQuestion);

            builder.Append("<select");

            if (multiChoiceQuestion.AllowMultipleAnswers)
            {
                builder.Append(" multiple='multiple'");
            }

            foreach (MultiChoiceOption option in multiChoiceQuestion.Options)
            {
                builder.Append("<option name='");
                builder.Append(option.Name);
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

            return builder.ToString();
        }

        public string Visit(OpenQuestion openQuestion)
        {
            StringBuilder builder = new StringBuilder();

            TryAppendTitleForQuestion(builder, openQuestion);

            builder.Append("<textarea");

            if (!openQuestion.AllowEmptyAnswer)
            {
                builder.Append(" required='required'");
            }

            builder.Append(">");
            builder.Append("</textarea>");

            return builder.ToString();
        }
    }
}
