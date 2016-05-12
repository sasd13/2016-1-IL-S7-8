using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI2016.Dev
{
    public class BlackHoleStream : Stream
    {
        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => true;

        public override long Length => long.MaxValue;

        public override long Position { get; set; }

        public override void Flush()
        {
        }

        public override long Seek( long offset, SeekOrigin origin )
        {
            switch( origin )
            {
                case SeekOrigin.Begin:
                    Position = offset;
                    break;
                case SeekOrigin.Current:
                    Position += offset;
                    break;
                case SeekOrigin.End:
                    Position = long.MaxValue + offset;
                    break;
                default:
                    break;
            }
            return Position;
        }

        public override void SetLength( long value )
        {
            throw new NotSupportedException();
        }

        public override int Read( byte[] buffer, int offsetInBuffer, int count )
        {
            for( int i = 0; i < count; ++i ) buffer[offsetInBuffer + i] = 0;
            Position += count;
            return count;
        }

        public override void Write( byte[] buffer, int offsetInBuffer, int count )
        {
            Position += count;
        }
    }
}
