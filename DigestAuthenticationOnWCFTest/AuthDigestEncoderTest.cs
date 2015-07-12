using System;
using DigestAuthenticationOnWCF;
using NUnit.Framework;

namespace DigestAuthenticationOnWCFTest
{
   [TestFixture]
   public class DigestEncoderBaseTest
   {
      [Test]
      public void When_Qop_Is_Auth_Digest_Should_Be_Encoded_Using_CNonce_NounceCounter_Qop()
      {
         var digestHeader = new DigestHeader();
         digestHeader.Method = "GET";
         digestHeader.Uri = "/Service.svc";
         digestHeader.Response = "6d2c6ad771845799dbf3e4ed88b9d8a4";
         digestHeader.UserName = "Willem";
         digestHeader.Realm = "mysite@site.com";
         digestHeader.Nonce = "NjM0MzMwMzMzODE5NTMsNjo2Yzc0MThkMTEzN2IzMGZmODBiMjU3YzM4Y2Y3YTg3MA==";
         digestHeader.Qop = DigestQop.Auth;
         digestHeader.Cnonce = "4847b4c6baa838ed21969a50e03fd333";
         digestHeader.NounceCounter = "00000001";

         DigestEncoderBase digestEncoder = new AuthDigestEncoder(new MD5Encoder());
         string result = digestEncoder.Encode(digestHeader, TestResources.Password);
         Assert.That(result, Is.EqualTo(digestHeader.Response));
      }


      [Test]
      public void When_Qop_Is_None_Digest_Should_Be_Encoded_Using_RFC2069()
      {
         var digestHeader = new DigestHeader();
         digestHeader.Method = "GET";
         digestHeader.Uri = "/Service.svc";
         digestHeader.Response = "0fa5ded47c396ecb28c5c3a7a8a6ca42";
         digestHeader.UserName = "Willem";
         digestHeader.Realm = "mysite@site.com";
         digestHeader.Nonce = "NjM0MzI2MjAyMTY3OTcsNjpBRjE1NTlBQkMwNjU2MTMxMzZDNjU1MTVDMDEwNEEyOA==";
         digestHeader.Qop = DigestQop.None;

         DigestEncoderBase digestValidator = new DefaultDigestEncoder(new MD5Encoder());
         string response = digestValidator.Encode(digestHeader, TestResources.Password);
         Assert.That(response, Is.EqualTo(digestHeader.Response));
      }
   }
}