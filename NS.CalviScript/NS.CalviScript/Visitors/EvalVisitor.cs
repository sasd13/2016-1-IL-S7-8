using System;
using System.Collections.Generic;

namespace NS.CalviScript
{
    public class EvalVisitor : StandardVisitor
    {
        readonly Dictionary<string, int> _globalContext;

        public EvalVisitor( Dictionary<string, int> globalContext )
        {
            _globalContext = globalContext;
        }


    }
}
