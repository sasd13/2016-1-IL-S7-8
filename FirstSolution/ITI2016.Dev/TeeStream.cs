using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI2016.Dev
{
    public class TeeStream : Stream
    {
        readonly Stream _stream1;
        readonly long _p1;
        readonly Stream _stream2;
        readonly long _p2;
        long _position;

        public TeeStream( Stream stream1, Stream stream2 )
        {
            if( stream1 == null ) throw new ArgumentNullException( nameof( stream1 ) );
            if( stream2 == null ) throw new ArgumentNullException( nameof( stream2 ) );
            if( !stream1.CanWrite ) throw new ArgumentException( "Stream must be writeable!", nameof( stream1 ) );
            if( !stream2.CanWrite ) throw new ArgumentException( "Stream must be writeable!", nameof( stream2 ) );
            _stream1 = stream1;
            _stream2 = stream2;
            if( _stream1.CanSeek && _stream2.CanSeek )
            {
                _p1 = _stream1.Position;
                _p2 = _stream2.Position;
            }
        }

        public override bool CanRead => false;

        public override bool CanSeek => _stream1.CanSeek && _stream2.CanSeek;

        public override bool CanWrite => true;

        public override long Length
        {
            get { throw new NotSupportedException(); }
        }

        public override long Position
        {
            get { return _position; }
            set { Seek( value, SeekOrigin.Begin ); }
        }

        public override void Flush()
        {
            _stream1.Flush();
            _stream2.Flush();
        }

        public override int Read( byte[] buffer, int offset, int count )
        {
            throw new NotSupportedException();
        }

        public override long Seek( long offset, SeekOrigin origin )
        {
            if( !CanSeek ) throw new NotSupportedException();
            switch( origin )
            {
                case SeekOrigin.Begin:
                    _stream1.Seek( offset + _p1, SeekOrigin.Begin );
                    _stream2.Seek( offset + _p2, SeekOrigin.Begin );
                    _position = offset;
                    break;
                case SeekOrigin.Current:
                    _stream1.Seek( offset, SeekOrigin.Current );
                    _stream2.Seek( offset, SeekOrigin.Current );
                    _position += offset;
                    break;
                default:
                    Debug.Assert( origin == SeekOrigin.End );
                    throw new NotSupportedException();
            }
            return Position;
        }

        public override void SetLength( long value )
        {
            throw new NotImplementedException();
        }

        public override void Write( byte[] buffer, int offset, int count )
        {
            _stream1.Write( buffer, offset, count );
            _stream2.Write( buffer, offset, count );
        }
    }
}
