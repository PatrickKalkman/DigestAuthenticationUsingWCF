using DigestAuthenticationOnWCF;
using NUnit.Framework;
using Rhino.Mocks;

namespace DigestAuthenticationOnWCFTest
{
   [TestFixture]
   public class PrivateHashEncoderTest
   {
      [Test]
      public void ShouldEncodeDateTimeAndPrivateKey()
      {
         var repository = new MockRepository();
         var md5Encoder = repository.StrictMock<MD5Encoder>();
         using (repository.Record())
         {
            string expectedStringToEncode = TestResources.NonceTimeStampAsString + ":" + TestResources.IpAddress + ":" + TestResources.PrivateKey;
            md5Encoder.Expect(encoder => encoder.Encode(expectedStringToEncode)).Return(string.Empty);
         }

         using (repository.Playback())
         {
            var privateHashEncoder = new PrivateHashEncoder(TestResources.PrivateKey, md5Encoder);
            privateHashEncoder.Encode(TestResources.NonceTimeStampAsString, TestResources.IpAddress);
         }
      }

      [Test]
      public void ShouldReturnNullWhenDateTimeArgumentToEncodeIsNull()
      {
         var privateHashEncoder = new PrivateHashEncoder(TestResources.PrivateKey, null);
         var result = privateHashEncoder.Encode(null, TestResources.IpAddress);
         Assert.That(result, Is.Null);
      }
   }
}