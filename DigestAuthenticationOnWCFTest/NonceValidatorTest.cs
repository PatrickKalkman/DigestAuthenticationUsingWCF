using System;
using DigestAuthenticationOnWCF;
using NUnit.Framework;

namespace DigestAuthenticationOnWCFTest
{
   [TestFixture]
   public class NonceValidatorTest
   {
      [Test]
      public void ShouldPositivelyValidateNonceWhenNonceIsGeneratedUsingPrivateKey()
      {
         var md5Encoder = new MD5Encoder();
         var base64Converter = new Base64Converter();
         var privateHashEncoder = new PrivateHashEncoder(TestResources.PrivateKey, md5Encoder);

         var nonceTimeStampParser = new NonceTimeStampParser();
         var nonceValidator = new NonceValidator(privateHashEncoder, base64Converter, nonceTimeStampParser, 600);
         bool result = nonceValidator.Validate(TestResources.GeneratedValidNonce, TestResources.IpAddress);
         Assert.That(result, Is.True);
      }

      [Test]
      public void ShouldIndicateThatNonceIsStaleWithOldTimeStamp()
      {
         var md5Encoder = new MD5Encoder();
         var base64Converter = new Base64Converter();
         var privateHashEncoder = new PrivateHashEncoder(TestResources.PrivateKey, md5Encoder);

         var nonceTimeStampParser = new NonceTimeStampParser();
         var nonceValidator = new NonceValidator(privateHashEncoder, base64Converter, nonceTimeStampParser, 600);
         bool result = nonceValidator.IsStale(TestResources.GeneratedValidNonce);
         Assert.That(result, Is.True);
      }

      [Test]
      public void ShouldIndicateThatNonceIsNotStale()
      {
         var md5Encoder = new MD5Encoder();
         var base64Converter = new Base64Converter();
         var privateHashEncoder = new PrivateHashEncoder(TestResources.PrivateKey, md5Encoder);

         var nonceTimeStampParser = new NonceTimeStampParser();
         var nonceValidator = new NonceValidator(privateHashEncoder, base64Converter, nonceTimeStampParser, 600);
         var nonceGenerator = new NonceGenerator(privateHashEncoder, base64Converter);

         bool result = nonceValidator.IsStale(nonceGenerator.Generate(TestResources.IpAddress));
         Assert.That(result, Is.False);
      }
   }
}