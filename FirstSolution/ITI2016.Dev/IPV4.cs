using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI2016.Dev
{

    public struct IPV4
    {
        readonly int _address;

        public IPV4( int ipAddress )
        {
            _address = ipAddress;
        }

        public IPV4( byte hiByte, byte hiLoByte, byte loHiByte, byte loByte )
        {
            _address = (hiByte << 24) | (hiLoByte << 16) | (loHiByte << 8) | loByte;
        }

        public int this[int i]
        {
            get
            {
                if( i < 0 || i > 3 ) throw new IndexOutOfRangeException();
                return (_address >> (i * 8)) & 0xFF;
            }
        }

        public IPV4 SetByte( int index, byte value )
        {
            if( index < 0 | index > 3 ) throw new IndexOutOfRangeException();
            index <<= 3;
            return new IPV4( (_address & ~(0xFF << index)) | (value << index) );
        }

        public IPV4 ClearByte( int index )
        {
            if( index < 0 | index > 3 ) throw new IndexOutOfRangeException();
            return new IPV4( _address & ~(0xFF << (index * 8)) );
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

}

