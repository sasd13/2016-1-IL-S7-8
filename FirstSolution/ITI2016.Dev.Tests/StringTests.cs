using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI2016.Dev.Tests
{
    [TestFixture]
    public class StringTests
    {
        [Test]
        public void string_encodings()
        {
            string s = "étجيكي";
            Encoding eUTF8 = Encoding.UTF8;
            byte[] bUTF8 = eUTF8.GetBytes( s );

            Encoding eUTF16 = Encoding.Unicode;
            byte[] bUTF16 = eUTF16.GetBytes( s );

            Encoding eUTF32 = Encoding.UTF32;
            byte[] bUTF32 = eUTF32.GetBytes( s );

            Encoding eUTF7 = Encoding.UTF7;
            byte[] bUTF7 = eUTF7.GetBytes( s );

            s = "été";
            bUTF7 = eUTF7.GetBytes( s );
            // send bUTF7
            string decoded = eUTF8.GetString( bUTF7 );
        }

        [Test]
        public void string_forms()
        {
            string s = "étäḝ";
                
            string sDenormalized = s.Normalize( NormalizationForm.FormD );

        }
    }
}
