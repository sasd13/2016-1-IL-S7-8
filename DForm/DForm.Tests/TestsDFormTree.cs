using DForm;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm.Tests
{
    [TestFixture]
    public class TestsDFormTree
    {
        [Test]
        public void DFormTree()
        {
            Form form = new Form();

            QuestionBase q1 = form.Questions.AddNewQuestion(typeof(BinaryQuestion).FullName);

            QuestionFolder q2 = (QuestionFolder) form.Questions.AddNewQuestion(typeof(QuestionFolder).FullName);
            QuestionBase q21 = q2.AddNewQuestion(typeof(BinaryQuestion).FullName);

            QuestionFolder q3 = (QuestionFolder) q2.AddNewQuestion(typeof(QuestionFolder).FullName);
            QuestionBase q31 = q3.AddNewQuestion(typeof(MultiCriteriaQuestion).FullName);

            QuestionBase q4 = form.Questions.AddNewQuestion(typeof(MultiCriteriaQuestion).FullName);

            q4.Parent = q3;
            q1.Parent = q2;
            q3.Parent = form.Questions;

            Assert.AreEqual(q4.Index, 1);
            Assert.AreEqual(q1.Index, 1);
            Assert.AreEqual(q2.Index, 0);
        }

        public void TypeOfAnswers()
        {
            Form form = new Form();

            QuestionBase q1 = form.Questions.AddNewQuestion(typeof(BinaryQuestion).FullName);
            QuestionBase q2 = form.Questions.AddNewQuestion(typeof(OpenQuestion).FullName);

            AnswerBase a1 = q1.CreateAnswer();
            AnswerBase a2 = q2.CreateAnswer();

            Assert.IsInstanceOf(typeof(BinaryAnswer), a1);
            Assert.IsInstanceOf(typeof(MultiCriteriaAnswer), a2);
        }

        [Test]
        public void ContentOfAnswers()
        {
            Form form = new Form();

            QuestionBase q1 = form.Questions.AddNewQuestion(typeof(BinaryQuestion).FullName);
            QuestionBase q2 = form.Questions.AddNewQuestion(typeof(OpenQuestion).FullName);

            AnswerBase a1 = q1.CreateAnswer();
            AnswerBase a2 = q2.CreateAnswer();

            Assert.IsInstanceOf(typeof(BinaryAnswer), a1);
            Assert.IsNotInstanceOf(typeof(MultiCriteriaAnswer), a2);
        }
    }
}
