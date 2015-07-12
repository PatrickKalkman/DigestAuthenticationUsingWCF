using DigestAuthenticationOnWCF;
using NUnit.Framework;
using Rhino.Mocks;
using System.Threading;

namespace DigestAuthenticationOnWCFTest
{
   [TestFixture]
   public class NonceGeneratorTest
   {
      [Test]
      public void ShouldGenerateNewNonce()
      {
         var repository = new MockRepository();
         var base64Converter = repository.Stub<Base64Converter>();
         var privateHashEncoder = repository.DynamicMock<PrivateHashEncoder>(null, null);
         using (repository.Record())
         {
            privateHashEncoder.Expect(hashEncoder => hashEncoder.Encode(string.Empty, string.Empty)).IgnoreArguments().Return("");
         }

         using (repository.Playback())
         {
            var nonceGenerator = new NonceGenerator(privateHashEncoder, base64Converter);
            nonceGenerator.Generate(string.Empty);
         }
         repository.VerifyAll();
      }

      [Test]
      public void ShouldGenerateNewNonceAfterSomeTime()
      {
         var nonceGenerator = new NonceGenerator(new PrivateHashEncoder(TestResources.PrivateKey, new MD5Encoder()),
                                                 new Base64Converter());
         string firstNonce = nonceGenerator.Generate(TestResources.IpAddress);
         Thread.Sleep(10);
         string secondNonce = nonceGenerator.Generate(TestResources.IpAddress);
         Assert.That(firstNonce, Is.Not.EqualTo(secondNonce));
      }
   }
}