using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    public class ErrorValue : ValueBase
    {
        public ErrorValue( string message )
        {
            Message = message ?? "An error occured.";
        }

        public override bool IsTrue => true;

        public string Message { get; }

        public override string ToString() => "Error: " + Message;

    }
}
