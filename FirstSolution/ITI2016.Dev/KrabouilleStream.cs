using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI2016.Dev
{
    public enum KrabouilleMode
    {
        Krabouille,
        UnKrabouille
    }

    public class KrabouilleStream : Stream
    {
        readonly Stream _inner;
        readonly KrabouilleMode _mode;
        readonly byte[] _secret;
        long _position;

        public KrabouilleStream( Stream inner, KrabouilleMode mode, string secret )
        {
            if( inner == null ) throw new ArgumentNullException( nameof( inner ) );
            if( string.IsNullOrEmpty( secret ) ) throw new ArgumentException( "Must not be null nor empty.", nameof( secret ) );
            if( mode == KrabouilleMode.Krabouille )
            {
                if( !inner.CanWrite ) throw new ArgumentException( "Stream must be writable in Krabouille mode." );
            }
            else if( !inner.CanRead )
            {
                throw new ArgumentException( "Stream must be readable in UnKrabouille mode." );
            }
            _inner = inner;
            _mode = mode;
            _secret = Encoding.UTF7.GetBytes( secret );
        }

        public override bool CanRead => _mode == KrabouilleMode.UnKrabouille;

        public override bool CanSeek => false;

        public override bool CanWrite => _mode == KrabouilleMode.Krabouille;

        public override long Length
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public override long Position
        {
            get { return _position; }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override long Seek( long offset, SeekOrigin origin )
        {
            throw new NotSupportedException();
        }

        public override void SetLength( long value )
        {
            throw new NotSupportedException();
        }


        public override void Flush()
        {
        }

        public override int Read( byte[] buffer, int offset, int count )
        {
            throw new NotImplementedException();
        }

        public override void Write( byte[] buffer, int offset, int count )
        {
            throw new NotImplementedException();
        }
    }
}
