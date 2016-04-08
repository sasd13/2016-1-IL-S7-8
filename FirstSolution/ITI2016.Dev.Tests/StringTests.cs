using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static Regex _regex = new Regex(
                      "^[\\w-]+:\\s*<?(?<mail>([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}" +
                      "\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-z" +
                      "A-Z]{2,4}|[0-9]{1,3}))>?",
                RegexOptions.IgnoreCase
                | RegexOptions.Multiline
                | RegexOptions.ExplicitCapture
                | RegexOptions.CultureInvariant
                | RegexOptions.Compiled
                );

        [Test]
        public void testing_regex()
        {
            Match m = _regex.Match( @"Return-Path: <test@example.com>
X-Original-To: test@example.com
Delivered-To: test@example.com
Received: from [127.0.0.1] (127-0-0-1-dynip.superkabel.de [127.0.0.1])
by example (Postfix) with ESMTPSA id FFFFFFFFFF
for <test@example.com>; Thu, 23 Jun 2011 11:26:44 +0200 (CEST)
Message-ID: <FFFFFFFF.5555555@example.com>
Date: Thu, 23 Jun 2011 11:26:29 +0200
From: Example Sender <test@example.com>
User-Agent: Mozilla/5.0 Gecko/20110616 Thunderbird/3.1.11
MIME-Version: 1.0
To: Example Receiver <test@example.com>
Subject: HTML Mail with embedded picture" );
            while( m.Success )
            {
                m = m.NextMatch();
            }

        }

    }
}
