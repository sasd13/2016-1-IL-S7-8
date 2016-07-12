using DForm;
using DForm.Questions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void CreateAnswers()
        {
            Form f = new Form();
            Assert.IsNull(f.Title);
            f.Title = "jj";
            Assert.AreEqual("jj", f.Title);

            FormAnswer a = f.FindOrCreateAnswer("Emilie");
            Assert.IsNotNull(a);
            FormAnswer b = f.FindOrCreateAnswer("Emilie");
            Assert.AreSame(a, b);

            Assert.AreEqual(1, f.AnswerCount);
            FormAnswer c = f.FindOrCreateAnswer("John Doe");
            Assert.AreNotSame(a, c);

            Assert.AreEqual("Emilie", a.UniqueName);
            Assert.AreEqual("John Doe", c.UniqueName);
        }

        [Test]
        public void CreateQuestionFolder()
        {
            Form f = new Form();
            f.Questions.Title = "HG67-Bis";
            Assert.AreEqual("HG67-Bis", f.Title);
            QuestionBase q1 = f.Questions.AddNewQuestion("DForm.QuestionFolder,DForm");
            QuestionBase q2 = f.Questions.AddNewQuestion("DForm.QuestionFolder,DForm");
            
            Assert.AreEqual(0, q1.Index);
            Assert.AreEqual(1, q2.Index);
            q2.Index = 0;
            Assert.AreEqual(0, q2.Index);
            Assert.AreEqual(1, q1.Index);
            q2.Parent = null;
            Assert.AreEqual(0, q1.Index);
            q2.Parent = q1;
            Assert.IsTrue(f.Questions.Contains(q1));
            Assert.IsTrue(f.Questions.Contains(q2));
        }

        [Test]
        public void LaTotale()
        {
            Form f = new Form();

            OpenQuestion qOpen = (OpenQuestion)f.Questions.AddNewQuestion("DForm.OpenQuestion");
            qOpen.Title = "First Question in the World!";
            qOpen.AllowEmptyAnswer = false;

            FormAnswer a = f.CreateAnswer("Emilie");
            AnswerBase theAnswerOfEmilieToQOpen = a.FindAnswer(qOpen);
            if (theAnswerOfEmilieToQOpen == null)
            {
                theAnswerOfEmilieToQOpen = a.AddAnswerFor(qOpen);
            }

            Assert.IsInstanceOfType(typeof(OpenAnswer), theAnswerOfEmilieToQOpen);

            OpenAnswer emilieAnswer = (OpenAnswer)theAnswerOfEmilieToQOpen;
            emilieAnswer.FreeAnswer = "I'm very happy to be here.";
        }

        [Test]
        public void DFormTree()
        {
            Form form = new Form();

            QuestionBase q1 = form.Questions.AddNewQuestion(typeof(BinaryQuestion).FullName);

            QuestionFolder q2 = (QuestionFolder) form.Questions.AddNewQuestion(typeof(QuestionFolder).FullName);
            QuestionBase q21 = q2.AddNewQuestion(typeof(BinaryQuestion).FullName);

            QuestionFolder q3 = (QuestionFolder) q2.AddNewQuestion(typeof(QuestionFolder).FullName);
            QuestionBase q31 = q3.AddNewQuestion(typeof(MultiChoiceQuestion).FullName);

            QuestionBase q4 = form.Questions.AddNewQuestion(typeof(MultiChoiceQuestion).FullName);

            q4.Parent = q3;
            q1.Parent = q2;
            q3.Parent = form.Questions;

            Assert.AreEqual(q4.Index, 1);
            Assert.AreEqual(q1.Index, 1);
            Assert.AreEqual(q2.Index, 0);
        }

        public void TestAnswers()
        {
            Form form = new Form();

            QuestionBase q1 = form.Questions.AddNewQuestion(typeof(BinaryQuestion).FullName);
            QuestionBase q2 = form.Questions.AddNewQuestion(typeof(OpenQuestion).FullName);

            AnswerBase a1 = q1.CreateAnswer();
            AnswerBase a2 = q2.CreateAnswer();

            Assert.IsInstanceOf(typeof(BinaryAnswer), a1);
            Assert.IsInstanceOf(typeof(MultiChoiceAnswer), a2);
        }
    }
}
