using DigestAuthenticationOnWCF;
using NUnit.Framework;

namespace DigestAuthenticationOnWCFTest
{
   [TestFixture]
   public class DigestEncodersTest
   {
      [Test]
      public void Should_Return_Right_Type_Of_Encoder_Based_On_Given_Qop()
      {
         var digestEncoders = new DigestEncoders(new MD5Encoder());
         DigestEncoderBase digestEncoder = digestEncoders.GetEncoder(DigestQop.None);
         Assert.That(digestEncoder.GetType(), Is.EqualTo(typeof (DefaultDigestEncoder)));
         digestEncoder = digestEncoders.GetEncoder(DigestQop.Auth);
         Assert.That(digestEncoder.GetType(), Is.EqualTo(typeof(AuthDigestEncoder)));
      }
   }
}
