using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm.Tests
{
    class TestsHTMLVisitor
    {
        [TestCase(
            @"
            <form>
            </form>"
            )]
        public void RenderForm(string expectedOutput)
        {
            Form form = new Form();
            IVisitor<string> visitor = new HTMLRenderVisitor();

            string output = form.Accept(visitor);

            Assert.That(output, Is.EqualTo(expectedOutput.Trim().Replace("  ", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty)));
        }

        [TestCase(
            @"
            <form>
                <h3>Questionnaire</h3>
                <div>
                    <label>Commentaires</label>
                    <textarea></textarea>
                </div>
            </form>"
            )]
        public void RenderQuestion(string expectedOutput)
        {
            Form form = new Form();
            IVisitor<string> visitor = new HTMLRenderVisitor();

            form.Title = "Questionnaire";
            OpenQuestion q1 = (OpenQuestion)form.Questions.AddNewQuestion("DForm.OpenQuestion");
            q1.Title = "Commentaires";

            string output = form.Accept(visitor);

            Assert.That(output, Is.EqualTo(expectedOutput.Trim().Replace("  ", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty)));
        }

        [TestCase(
            @"
            <form>
                <h3>Questionnaire</h3>
                <div>
                    <h4>Partie 1</h4>
                    <div>
                        <h4>Partie 1.1</h4>
                        <div>
                            <label>Commentaires</label>
                            <textarea required='required'></textarea>
                        </div>
                    </div>
                </div>
                <div>
                    <label>Remarques</label>
                    <textarea></textarea>
                </div>
            </form>"
            )]
        public void RenderQuestionFolder(string expectedOutput)
        {
            Form form = new Form();
            IVisitor<string> visitor = new HTMLRenderVisitor();

            form.Title = "Questionnaire";
            QuestionFolder qf1 = (QuestionFolder)form.Questions.AddNewQuestion("DForm.QuestionFolder");
            qf1.Title = "Partie 1";

            QuestionFolder qf2 = (QuestionFolder)qf1.AddNewQuestion("DForm.QuestionFolder");
            qf2.Title = "Partie 1.1";
            OpenQuestion qo21 = (OpenQuestion)qf2.AddNewQuestion("DForm.OpenQuestion");
            qo21.Title = "Commentaires";
            qo21.AllowEmptyAnswer = false;

            OpenQuestion q2 = (OpenQuestion)form.Questions.AddNewQuestion("DForm.OpenQuestion");
            q2.Title = "Remarques";

            string output = form.Accept(visitor);

            Assert.That(output, Is.EqualTo(expectedOutput.Trim().Replace("  ", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty)));
        }
    }
}
