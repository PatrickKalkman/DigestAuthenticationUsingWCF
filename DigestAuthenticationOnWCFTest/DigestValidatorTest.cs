using DigestAuthenticationOnWCF;
using NUnit.Framework;
using Rhino.Mocks;

namespace DigestAuthenticationOnWCFTest
{
   [TestFixture]
   public class DigestValidatorTest
   {
      [Test]
      public void ShouldUseDigestEncoderToEncodeAndCompareResponse()
      {
         var repository = new MockRepository();
         var encoders = repository.StrictMock<DigestEncoders>(new object[] {null});
         var digestHeader = new DigestHeader {Response = TestResources.GeneratedValidResponse, Qop = DigestQop.None};
         using (repository.Record())
         {
            encoders.Expect(enc => enc.GetEncoder(digestHeader.Qop)).Return(new DefaultDigestEncoder(new MD5Encoder()));             
         }

         using (repository.Playback())
         {
            var digestValidator = new DigestValidator(encoders);
            digestValidator.Validate(digestHeader, TestResources.Password);
         }
         repository.VerifyAll();
      }
   }
}