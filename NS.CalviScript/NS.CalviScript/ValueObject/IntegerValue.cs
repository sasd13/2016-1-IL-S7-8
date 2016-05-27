using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    public class IntegerValue : ValueBase
    {
        static IntegerValue M1 = new IntegerValue( -1 );
        static IntegerValue Zero = new IntegerValue( 0 );
        static IntegerValue One = new IntegerValue( 1 );
        static IntegerValue Two = new IntegerValue( 2 );
        static IntegerValue Three = new IntegerValue( 3 );

        static public IntegerValue Create( int v )
        {
            switch( v )
            {
                case -1: return M1;
                case 0: return Zero;
                case 1: return One;
                case 2: return Two;
                case 3: return Three;
                default: return new IntegerValue( v );
            }
        }

        private IntegerValue( int v )
        {
            Value = v;
        }

        public int Value { get; }

        public override bool IsTrue => Value >= 0;

        public override string ToString() => Value.ToString();

    }
}
