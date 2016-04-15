using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI2016.Dev.Tests
{
    [TestFixture]
    public class StreamTests
    {
        const string _inputFile = @"C:\Intech\2016-1\S7-8\2016-1-IL-S7-8\FirstSolution\ITI2016.Dev.Tests\StreamTests.cs";

        static string GetOutputFile( string originalFile, string suffixe )
        {
            string dir = Path.Combine( Path.GetDirectoryName( originalFile ), "TestFiles" );
            Directory.CreateDirectory( dir );
            return Path.Combine( dir, Path.GetFileName( originalFile ) + suffixe );
        }

        [TestCase( 1 )]
        [TestCase( 4 )]
        [TestCase( 3 )]
        [TestCase( 4 * 1024 )]
        public void reading_from_a_file_and_writing_to_another_one( int bufferSize )
        {
            string outputFile = GetOutputFile( _inputFile, ".out" );
            byte[] buffer = new byte[bufferSize];


            using( FileStream input = new FileStream( _inputFile, FileMode.Open, FileAccess.Read, FileShare.Read ) )
            using( FileStream output = new FileStream( outputFile, FileMode.Create, FileAccess.Write, FileShare.None ) )
            {
                int lenRead;
                while( (lenRead = input.Read( buffer, 0, buffer.Length )) > 0 )
                {
                    output.Write( buffer, 0, lenRead );
                }
            }

            CollectionAssert.AreEqual( File.ReadAllBytes( _inputFile ), File.ReadAllBytes( outputFile ) );
        }

        [TestCase( 1 )]
        [TestCase( 4 )]
        [TestCase( 3 )]
        [TestCase( 4 * 1024 )]
        public void reading_from_a_file_and_writing_to_two_outputs( int bufferSize )
        {
            string outputFile = GetOutputFile( _inputFile, ".out" );
            byte[] buffer = new byte[bufferSize];


            using( Stream input = new FileStream( _inputFile, FileMode.Open, FileAccess.Read, FileShare.Read ) )
            using( Stream out1 = new FileStream( outputFile + "1", FileMode.Create, FileAccess.Write, FileShare.None ) )
            using( Stream out2 = new FileStream( outputFile + "2", FileMode.Create, FileAccess.Write, FileShare.None ) )
            using( Stream output = new TeeStream( out1, out2 ) )
            {
                int lenRead;
                while( (lenRead = input.Read( buffer, 0, buffer.Length )) > 0 )
                {
                    output.Write( buffer, 0, lenRead );
                }
            }

            CollectionAssert.AreEqual( File.ReadAllBytes( _inputFile ), File.ReadAllBytes( outputFile + "1" ) );
            CollectionAssert.AreEqual( File.ReadAllBytes( _inputFile ), File.ReadAllBytes( outputFile + "2" ) );
        }

        [TestCase( 1 )]
        [TestCase( 4 )]
        [TestCase( 3 )]
        [TestCase( 4 * 1024 )]
        public void reading_from_a_file_and_writing_a_duplicate_and_a_compressed_version( int bufferSize )
        {
            string outputFile = GetOutputFile( _inputFile, ".out" );
            byte[] buffer = new byte[bufferSize];

            using( Stream input = new FileStream( _inputFile, FileMode.Open, FileAccess.Read, FileShare.Read ) )
            using( Stream out1 = new FileStream( outputFile, FileMode.Create, FileAccess.Write, FileShare.None ) )
            using( Stream out2 = new FileStream( outputFile + ".C", FileMode.Create, FileAccess.Write, FileShare.None ) )
            using( Stream zip = new GZipStream( out2, CompressionMode.Compress ) )
            using( Stream output = new TeeStream( out1, zip ) )
            {
                int lenRead;
                while( (lenRead = input.Read( buffer, 0, buffer.Length )) > 0 )
                {
                    output.Write( buffer, 0, lenRead );
                }
            }

            CollectionAssert.AreEqual( File.ReadAllBytes( _inputFile ), File.ReadAllBytes( outputFile ) );

            using( Stream input = File.OpenRead( outputFile + ".C" ) )
            using( Stream unzip = new GZipStream( input, CompressionMode.Decompress ) )
            using( Stream output = File.OpenWrite( outputFile + ".CD" ) )
            {
                unzip.CopyTo( output, 4 * 1024 );
            }
            CollectionAssert.AreEqual( File.ReadAllBytes( _inputFile ), File.ReadAllBytes( outputFile + ".CD" ) );

        }


        [TestCase( 1 )]
        [TestCase( 3 )]
        [TestCase( 13 )]
        [TestCase( 4 * 1024 )]
        public void KrabouilleStream_works( int bufferSize )
        {
            string kFile = GetOutputFile( _inputFile, ".K" );

            using( Stream input = File.OpenRead( _inputFile ) )
            using( Stream output = File.OpenWrite( kFile ) )
            using( Stream krab = new KrabouilleStream( output, KrabouilleMode.Krabouille, "My Secret..." ) )
            {
                input.CopyTo( krab, bufferSize );
            }
            CollectionAssert.AreNotEqual( File.ReadAllBytes( _inputFile ), File.ReadAllBytes( kFile ) );

            string kdFile = GetOutputFile( _inputFile, ".K.D" );

            using( Stream input = File.OpenRead( kFile ) )
            using( Stream deKrab = new KrabouilleStream( input, KrabouilleMode.UnKrabouille, "My Secret..." ) )
            using( Stream output = File.OpenWrite( kdFile ) )
            {
                deKrab.CopyTo( output, bufferSize );
            }

            CollectionAssert.AreEqual( File.ReadAllBytes( _inputFile ), File.ReadAllBytes( kdFile ) );
        }


    }
}
