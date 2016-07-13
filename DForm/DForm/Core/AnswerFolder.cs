﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    public class AnswerFolder : AnswerBase
    {
        readonly List<AnswerBase> _answers;

        public AnswerFolder(QuestionBase question) : base(question)
        {
            _answers = new List<AnswerBase>();
        }

        public bool Contains(AnswerBase answer)
        {
            foreach (AnswerBase a in _answers)
            {
                if (a is AnswerFolder && ((AnswerFolder)a).Contains(answer)) return true;
                else if (a.Equals(answer)) return true;
            }

            return false;
        }

        public AnswerBase FindAnswerFor(QuestionBase question)
        {
            AnswerBase answer;

            foreach (AnswerBase a in _answers)
            {
                if (a is AnswerFolder)
                {
                    answer = ((AnswerFolder)a).FindAnswerFor(question);
                    if (answer != null) return answer;
                }
                else if (a.Question.Equals(question)) return a;
            }

            return null;
        }

        public AnswerBase AddAnswerFor(QuestionBase question)
        {
            if (FindAnswerFor(question) != null)
            {
                return null;
            }

            AnswerBase answer = question.CreateAnswer();
            _answers.Add(answer);

            return answer;
        }

        public AnswerBase RemoveAnswerOf(QuestionBase question)
        {
            AnswerBase answer = null;

            foreach (AnswerBase a in _answers)
            {
                if (a is AnswerFolder)
                {
                    answer = ((AnswerFolder)a).RemoveAnswerOf(question);
                    if (answer != null) return answer;
                }
                else if (a.Question.Equals(question))
                {
                    answer = a;
                    _answers.Remove(answer);
                    return answer;
                }
            }

            return answer;
        }
    }
}
