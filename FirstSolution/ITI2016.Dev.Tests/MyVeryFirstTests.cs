using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI2016.Dev.Tests
{

    [TestFixture]
    public class MyVeryFirstTests
    {
        [Test]
        public void Console_is_available_in_a_test_method()
        {
            Console.WriteLine("Hello World!");
        }

        [Test]
        public void Basic_numeric_types_manipulations()
        {
            // Arrange
            byte b1 = 76;
            byte b2 = 32;

            // Act
            byte b3 = (byte)(b1 + b2);

            // Assert
            Assert.That(b3, Is.EqualTo(103));
        }


        [TestCase(130, -126)]
        [TestCase(131, -125)]
        [TestCase(132, -124)]
        [TestCase(100, 100)]
        public void How_signed_integers_work(byte positive, sbyte negative)
        {
            // Arrange
            byte b = positive;

            // Act
            sbyte sb = (sbyte)b;

            // Assert
            Assert.That(sb, Is.EqualTo(negative));
        }

        public struct IPV4
        {
            readonly int _address;

            public IPV4(int ipAddress)
            {
                _address = ipAddress;
            }

            public IPV4(byte hiByte, byte hiLoByte, byte loHiByte, byte loByte)
            {
                _address = (hiByte << 24) | (hiLoByte << 16) | (loHiByte << 8) | loByte;
            }

            public int this[int i]
            {
                get
                {
                    if (i < 0 || i > 3) throw new IndexOutOfRangeException();
                    return (_address >> (i * 8)) & 0xFF;
                }
            }

            public IPV4 SetByte(int index, byte value)
            {
                if (index < 0 | index > 3) throw new IndexOutOfRangeException();
                index <<= 3;
                return new IPV4((_address & ~(0xFF << index)) | (value << index));
            }

            public IPV4 ClearByte(int index)
            {
                if (index < 0 | index > 3) throw new IndexOutOfRangeException();
                return new IPV4(_address & ~(0xFF << (index * 8)));
            }

            // Version 0:
            //public override string ToString()
            //{
            //    return this[3].ToString() + '.' + this[2].ToString() + '.' + this[1].ToString() + '.' + this[0].ToString();
            //}

            // Version 1:
            //public override string ToString()
            //{
            //    return string.Format( "{0}.{1}.{2}.{3}", this[3], this[2], this[1], this[0] );
            //}

            // Version 2:
            //public override string ToString()
            //{
            //    return $"{this[3]}.{this[2]}.{this[1]}.{this[0]}";
            //}

            // Version 3:
            public override string ToString() => $"{this[3]}.{this[2]}.{this[1]}.{this[0]}";
        }

        [Test]
        public void manipulating_IPV4_addresses()
        {
            IPV4 a = new IPV4(76678976);
            IPV4 b = new IPV4(250, 89, 43, 210);
            Assert.That(b[0], Is.EqualTo(210));
            Assert.That(b[1], Is.EqualTo(43));
            Assert.That(b[2], Is.EqualTo(89));
            Assert.That(b[3], Is.EqualTo(250));
            Assert.That(b.ToString(), Is.EqualTo("250.89.43.210"));
        }

        [TestCase(0, "250.89.43.0")]
        [TestCase(1, "250.89.0.210")]
        [TestCase(2, "250.0.43.210")]
        [TestCase(3, "0.89.43.210")]
        public void clearing_bytes_in_an_IPV4_addresse(int index, string expected)
        {
            IPV4 a = new IPV4(250, 89, 43, 210);
            IPV4 a1 = a.ClearByte(index);
            Assert.That(a1.ToString(), Is.EqualTo(expected));
        }

        [TestCase(0, 67, "250.89.43.67")]
        [TestCase(1, 255, "250.89.255.210")]
        [TestCase(2, 34, "250.34.43.210")]
        [TestCase(3, 128, "128.89.43.210")]
        public void setting_bytes_in_an_IPV4_addresse(int index, byte value, string expected)
        {
            IPV4 a = new IPV4(250, 89, 43, 210);
            IPV4 a1 = a.SetByte(index, value);
            Assert.That(a1.ToString(), Is.EqualTo(expected));
        }

        [Test]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void check_that_index_is_controlled()
        {
            IPV4 a = new IPV4();
            int x = a[5];
        }
    }
}
