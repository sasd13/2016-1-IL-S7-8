using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    public class UndefinedValue : ValueBase
    {
        public static UndefinedValue Default = new UndefinedValue();

        private UndefinedValue()
        {
        }

        public override bool IsTrue => false;

        public override string ToString() => "undefined";

    }
}
