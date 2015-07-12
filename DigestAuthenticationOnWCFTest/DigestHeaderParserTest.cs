using DigestAuthenticationOnWCF;
using NUnit.Framework;

namespace DigestAuthenticationOnWCFTest
{
   [TestFixture]
   public class DigestHeaderParserTest
   {
      [Test]
      public void ShouldParseHeaderAndFillFields()
      {
         const string Header =
            "Digest realm=\"testrealm@host.com\",qop=\"auth,auth-int\",nonce=\"dcd98b7102dd2f0e8b11d0f600bfb0c093\",opaque=\"5ccc069c403ebaf9f0171e9517f40e41\"";
         DigestHeaderParser digestHeaderParser = CreateDigestHeaderParser();
         DigestHeader digestHeader = digestHeaderParser.Parse(Header);
         Assert.That(digestHeader.Realm, Is.EqualTo("testrealm@host.com"));
         Assert.That(digestHeader.Nonce, Is.EqualTo("dcd98b7102dd2f0e8b11d0f600bfb0c093"));
         Assert.That(digestHeader.Qop, Is.EqualTo(DigestQop.Auth));
         Assert.That(digestHeader.Opaque, Is.EqualTo("5ccc069c403ebaf9f0171e9517f40e41"));
      }

      [Test]
      public void ShouldParseUserNameAndResponse()
      {
         const string Header =
            "Digest username=\"Willem\",realm=\"mysite@site.com\",nonce=\"NjM0MzI2MjAyMTY3OTcsNjpBRjE1NTlBQkMwNjU2MTMxMzZDNjU1MTVDMDEwNEEyOA==\",uri=\"/Service.svc\",response=\"0fa5ded47c396ecb28c5c3a7a8a6ca42\"";
         DigestHeaderParser digestHeaderParser = CreateDigestHeaderParser();
         DigestHeader digestHeader = digestHeaderParser.Parse(Header);
         Assert.That(digestHeader.Realm, Is.EqualTo("mysite@site.com"));
         Assert.That(digestHeader.Nonce,
                     Is.EqualTo("NjM0MzI2MjAyMTY3OTcsNjpBRjE1NTlBQkMwNjU2MTMxMzZDNjU1MTVDMDEwNEEyOA=="));
         Assert.That(digestHeader.UserName, Is.EqualTo("Willem"));
         Assert.That(digestHeader.Uri, Is.EqualTo("/Service.svc"));
         Assert.That(digestHeader.Response, Is.EqualTo("0fa5ded47c396ecb28c5c3a7a8a6ca42"));
      }

      [Test]
      public void ShouldParseCNonceAndNonceCounter()
      {
         const string Header =
            "Digest username=\"Willem\",realm=\"mysite@site.com\",nonce=\"NjM0MzMwMzMzODE5NTMsNjo2Yzc0MThkMTEzN2IzMGZmODBiMjU3YzM4Y2Y3YTg3MA==\",uri=\"/Service.svc\",cnonce=\"4847b4c6baa838ed21969a50e03fd333\",nc=00000001,response=\"6d2c6ad771845799dbf3e4ed88b9d8a4\",qop=\"auth\"";
         DigestHeaderParser digestHeaderParser = CreateDigestHeaderParser();
         DigestHeader digestHeader = digestHeaderParser.Parse(Header);
         Assert.That(digestHeader.Cnonce, Is.EqualTo("4847b4c6baa838ed21969a50e03fd333"));
         Assert.That(digestHeader.NounceCounter, Is.EqualTo("00000001"));
         Assert.That(digestHeader.Qop, Is.EqualTo(DigestQop.Auth));
      }

      [Test]
      public void ShouldParseDigestQop()
      {
         const string Header =
            "Digest username=\"Willem\",realm=\"mysite@site.com\",nonce=\"NjM0MzMwMzMzODE5NTMsNjo2Yzc0MThkMTEzN2IzMGZmODBiMjU3YzM4Y2Y3YTg3MA==\",uri=\"/Service.svc\",cnonce=\"4847b4c6baa838ed21969a50e03fd333\",nc=00000001,response=\"6d2c6ad771845799dbf3e4ed88b9d8a4\",qop=\"auth-int\"";
         DigestHeaderParser digestHeaderParser = CreateDigestHeaderParser();
         DigestHeader digestHeader = digestHeaderParser.Parse(Header);
         Assert.That(digestHeader.Qop, Is.EqualTo(DigestQop.AuthInt));
      }

      private static DigestHeaderParser CreateDigestHeaderParser()
      {
         return new DigestHeaderParser(new HeaderKeyValueSplitter(TestResources.HeaderKeyValueSplitCharacter));
      }
   }
}