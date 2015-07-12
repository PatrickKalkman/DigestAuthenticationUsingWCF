using DigestAuthenticationOnWCF;
using NUnit.Framework;
using System;

namespace DigestAuthenticationOnWCFTest
{
   [TestFixture]
   public class DigestHeaderStringGeneratorTest
   {
      [Test]
      public void ShouldGenerateFullDigestResponseHeader()
      {
         var digestResponseHeaderGenerator = new DigestHeader();
         digestResponseHeaderGenerator.Realm = "MyRealm";
         digestResponseHeaderGenerator.Domain = "www.test.com www.semanticarchitecture.net";
         digestResponseHeaderGenerator.Nonce = "BABABABABABASD";
         digestResponseHeaderGenerator.Opaque = "3332211VVVV";
         digestResponseHeaderGenerator.Stale = false;
         digestResponseHeaderGenerator.Algoritm = DigestAlgorithm.MD5;
         digestResponseHeaderGenerator.Qop = DigestQop.Auth;
         string digestHeader = digestResponseHeaderGenerator.GenerateHeaderString();
         Assert.That(digestHeader,
                     Is.EqualTo(
                        "Digest realm=\"MyRealm\", domain=\"www.test.com www.semanticarchitecture.net\", nonce=\"BABABABABABASD\", opaque=\"3332211VVVV\", stale=\"false\", algorithm=\"MD5\", qop=\"auth\""));
      }

      [Test]
      public void ShouldGenerateMD5SessDigestResponseHeader()
      {
         var digestResponseHeaderGenerator = new DigestHeader();
         digestResponseHeaderGenerator.Realm = "MyRealm";
         digestResponseHeaderGenerator.Domain = "www.test.com www.semanticarchitecture.net";
         digestResponseHeaderGenerator.Nonce = "BABABABABABASD";
         digestResponseHeaderGenerator.Opaque = "3332211VVVV";
         digestResponseHeaderGenerator.Stale = false;
         digestResponseHeaderGenerator.Algoritm = DigestAlgorithm.MD5Sess;
         digestResponseHeaderGenerator.Qop = DigestQop.AuthInt;
         string digestHeader = digestResponseHeaderGenerator.GenerateHeaderString();
         Assert.That(digestHeader,
                     Is.EqualTo(
                        "Digest realm=\"MyRealm\", domain=\"www.test.com www.semanticarchitecture.net\", nonce=\"BABABABABABASD\", opaque=\"3332211VVVV\", stale=\"false\", algorithm=\"MD5-sess\", qop=\"auth-int\""));
      }

      [Test]
      public void ShouldGenerateDefaultDigestResponseHeader()
      {
         var digestResponseHeaderGenerator = new DigestHeader();
         digestResponseHeaderGenerator.Realm = "MyRealm";
         digestResponseHeaderGenerator.Nonce = "BABABABABABASD";
         string digestHeader = digestResponseHeaderGenerator.GenerateHeaderString();
         Assert.That(digestHeader,
                     Is.EqualTo("Digest realm=\"MyRealm\", nonce=\"BABABABABABASD\""));
      }

      [Test]
      [ExpectedException(typeof (InvalidOperationException))]
      public void ShouldThrowExceptionWhenNotAllMandatoryFieldsAreFilled()
      {
         var digestResponseHeaderGenerator = new DigestHeader();
         digestResponseHeaderGenerator.GenerateHeaderString();
      }
   }
}