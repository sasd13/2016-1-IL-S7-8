﻿using DForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    public class BinaryQuestion : MultiCriteriaQuestion
    {
        public BinaryQuestion() : base(false)
        {
            
        }

        public override AnswerBase CreateAnswer() => new BinaryAnswer(this);
    }
}
