using DigestAuthenticationOnWCF;
using NUnit.Framework;

namespace DigestAuthenticationOnWCFTest
{
   [TestFixture]
   public class MD5EncoderTest
   {
      [Test]
      public void ShouldCalculateMD5HashFromString()
      {
         var md5Encoder = new MD5Encoder();
         string result = md5Encoder.Encode("Hash It");
         Assert.That(result, Is.EqualTo("1deafc92595a02703f5736ecfb440563"));
      }

      [Test]
      public void ShouldEncodeRandomUnicodeString()
      {
         var md5Encoder = new MD5Encoder();
         string result = md5Encoder.Encode("\u0080\0\u0080\0\0");
         Assert.That(result, Is.EqualTo("3adae15227f3ce0b058bb526aeb3791b"));
      }
   }
}