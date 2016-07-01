using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    public class RefValue : ValueBase
    {
        public RefValue( ValueBase v )
        {
            RealValue = v;
        }

        public ValueBase RealValue { get; set; }

        public override bool IsTrue => RealValue.IsTrue;

        public override string ToString() => "ref " + RealValue.ToString();

    }
}
