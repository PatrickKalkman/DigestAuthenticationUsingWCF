using System;
using System.ServiceModel.Channels;
using DigestAuthenticationOnWCF;
using NUnit.Framework;
using Rhino.Mocks;

namespace DigestAuthenticationOnWCFTest
{
   [TestFixture]
   public class DigestAuthenticationManagerTest
   {
      MockRepository repository;

      [SetUp]
      public void SetUp()
      {
         repository = new MockRepository();
      }

      internal class DigestAuthenticationManagerFactoryDigestHeaderMock : DigestAuthenticationManagerFactory
      {
         public DigestAuthenticationManagerFactoryDigestHeaderMock(MockRepository repository) : base(repository) { }

         public override DigestHeaderExtractor CreateDigestHeaderExtractor()
         {
            return MockCreator<DigestHeaderExtractor>.Create(repository);
         }
      }

      [Test]
      public void ShouldAuthenticateIncomingRequestAndValidateAuthorizationHeader()
      {
         var managerFactory = new DigestAuthenticationManagerFactoryDigestHeaderMock(repository);
         var manager = managerFactory.Create();
         Message testMessage = TestResources.CreateMessage();

         using (repository.Record())
         {
            DigestHeader digestHeader;
            DigestHeaderExtractor digestHeaderExtractor = managerFactory.DigestHeaderExtractor;
            digestHeaderExtractor.Expect(extractor => extractor.TryExtract(testMessage, out digestHeader)).Return(false);
         }

         using (repository.Playback())
         {
            bool result = manager.AuthenticateRequest(testMessage);
            Assert.That(result, Is.False);
         }
         repository.VerifyAll();
      }

      internal class DigestAuthenticationManagerFactoryIpAddress : DigestAuthenticationManagerFactory
      {
         public DigestAuthenticationManagerFactoryIpAddress(MockRepository repository) : base(repository) { }

         public override IPAddressExtractor CreateIpAddressExtractor()
         {
            return MockCreator<IPAddressExtractor>.Create(repository);
         }
      }

      [Test]
      public void ShouldExtractIpAddressFromHeaderWhenHeaderIsExtracted()
      {
         var managerFactory = new DigestAuthenticationManagerFactoryIpAddress(repository);
         var manager = managerFactory.Create();
         Message testMessage = TestResources.CreateMessage();

         using (repository.Record())
         {
            DigestHeader digestHeader;
            DigestHeaderExtractor digestHeaderExtractor = managerFactory.DigestHeaderExtractor;
            digestHeaderExtractor.Expect(digestExtractor => digestExtractor.TryExtract(null, out digestHeader)).IgnoreArguments().Return(true);
            string extractedIpAddress;
            IPAddressExtractor ipAddressExtractor = managerFactory.IpAddressExtractor;
            ipAddressExtractor.Expect(extractor => extractor.TryExtractIpAddress(testMessage, out extractedIpAddress)).Return(false);
         }

         using (repository.Playback())
         {
            bool result = manager.AuthenticateRequest(testMessage);
            Assert.That(result, Is.False);
         }
         repository.VerifyAll();
      }

      internal class DigestAuthenticationManagerFactoryNonceValidator : DigestAuthenticationManagerFactory
      {
         public DigestAuthenticationManagerFactoryNonceValidator(MockRepository repository) : base(repository) { }

         public override NonceValidator CreateNonceValidator()
         {
            return MockCreator<NonceValidator>.Create(repository);
         }
      }

      [Test]
      public void ShouldValidateNonceFromHeader()
      {
         var managerFactory = new DigestAuthenticationManagerFactoryNonceValidator(repository);
         var manager = managerFactory.Create();
         Message testMessage = TestResources.CreateMessage();

         using (repository.Record())
         {
            DigestHeader digestHeader;
            DigestHeaderExtractor digestHeaderExtractor = managerFactory.DigestHeaderExtractor;
            digestHeaderExtractor.Expect(digestExtractor => digestExtractor.TryExtract(null, out digestHeader)).IgnoreArguments().Return(true).OutRef(TestResources.CreateHeader());
            string extractedIpAddress;
            IPAddressExtractor ipAddressExtractor = managerFactory.IpAddressExtractor;
            ipAddressExtractor.Expect(extractor => extractor.TryExtractIpAddress(testMessage, out extractedIpAddress)).
               Return(true).OutRef(TestResources.IpAddress);

            NonceValidator nonceValidator = managerFactory.NonceValidator;
            nonceValidator.Expect(validator => validator.Validate(TestResources.GeneratedValidNonce, TestResources.IpAddress)).Return(true);
            nonceValidator.Expect(validator => validator.IsStale(TestResources.GeneratedValidNonce)).Return(false);
         }

         using (repository.Playback())
         {
            bool result = manager.AuthenticateRequest(testMessage);
            Assert.That(result, Is.False);
         }
         repository.VerifyAll();
      }

      [Test]
      public void ShouldCreateInvalidAuthorizationRequest()
      {
         var managerFactory = new DigestAuthenticationManagerFactoryNonceValidator(repository);
         var manager = managerFactory.Create();
         Message testMessage = TestResources.CreateMessage(TestResources.IpAddress, 120);
         using (repository.Record())
         {
            var ipAddressExtractor = managerFactory.IpAddressExtractor;
            string ipAddress;
            ipAddressExtractor.Expect(extractor => extractor.TryExtractIpAddress(testMessage, out ipAddress)).Return(true).OutRef(TestResources.IpAddress);
            ResponseMessageFactory responseMessageFactory = managerFactory.Factory;
            DigestHeaderFactory digestHeaderGenerator = managerFactory.DigestHeaderGenerator;
            DigestHeader digestHeader = TestResources.CreateHeader();
            digestHeaderGenerator.Expect(headerGenerator => headerGenerator.Generate(TestResources.IpAddress)).Return(digestHeader);
            responseMessageFactory.Expect(factory => factory.CreateInvalidAuthorizationMessage(digestHeader));
         }

         using (repository.Playback())
         {
            manager.CreateInvalidAuthenticationRequest(testMessage);
         }
         repository.VerifyAll();
      }

      [Test]
      [ExpectedException(typeof(ArgumentException))]
      public void ShouldThrowWhenMessageDoesNotContainIPAddress()
      {
         var managerFactory = new DigestAuthenticationManagerFactoryNonceValidator(repository);
         var manager = managerFactory.Create();
         Message testMessage = TestResources.CreateMessage();
         using (repository.Record())
         {
            var ipAddressExtractor = managerFactory.IpAddressExtractor;
            string ipAddress;
            ipAddressExtractor.Expect(extractor => extractor.TryExtractIpAddress(testMessage, out ipAddress)).Return(false);
         }

         using (repository.Playback())
         {
            manager.CreateInvalidAuthenticationRequest(testMessage);
         }
         repository.VerifyAll();
      }


   }
}