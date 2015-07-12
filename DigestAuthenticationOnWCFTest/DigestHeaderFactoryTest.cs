using DigestAuthenticationOnWCF;
using NUnit.Framework;
using Rhino.Mocks;

namespace DigestAuthenticationOnWCFTest
{
   [TestFixture]
   public class DigestHeaderFactoryTest
   {
      [Test]
      public void Should_Generate_Response_Header_Based_On_Given_IPAddress()
      {
         var repository = new MockRepository();
         var nonceGenerator = repository.StrictMock<NonceGenerator>(null, null);
         using (repository.Record())
         {
            nonceGenerator.Expect(generator => generator.Generate(TestResources.IpAddress)).Return(TestResources.GeneratedValidNonce);
         }

         var digestHeaderGenerator = new DigestHeaderFactory(nonceGenerator, TestResources.Realm);
         using (repository.Playback())
         {
            DigestHeader digestHeader = digestHeaderGenerator.Generate(TestResources.IpAddress);
            Assert.That(digestHeader.Realm, Is.EqualTo(TestResources.Realm));
            Assert.That(digestHeader.Nonce, Is.EqualTo(TestResources.GeneratedValidNonce));
         }

         repository.VerifyAll();
      }
   }
}
