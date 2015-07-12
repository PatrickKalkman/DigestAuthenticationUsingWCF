using System.ServiceModel.Channels;
using DigestAuthenticationOnWCF;
using NUnit.Framework;
using Rhino.Mocks;

namespace DigestAuthenticationOnWCFTest
{
   [TestFixture]
   public class DigestHeaderExtractorTest
   {
      [Test]
      public void ShouldParseAuthenticationStringWhenAuthorizationStringIsAvailable()
      {
         MockRepository repository = new MockRepository();

         DigestHeaderParser headerParser = MockCreator<DigestHeaderParser>.Stub(repository);
         MethodExtractor methodExtractor = MockCreator<MethodExtractor>.Stub(repository);

         Message testMessage = TestResources.CreateMessage();

         AuthorizationStringExtractor extractor;
         using (repository.Record())
         {
            headerParser.Expect(parser => parser.Parse(string.Empty)).IgnoreArguments().Return(TestResources.CreateHeader());

            extractor = MockCreator<AuthorizationStringExtractor>.Create(repository);
            string authenticationString;
            extractor.Expect(extr => extr.TryExtractAuthorizationHeader(testMessage, out authenticationString)).OutRef(TestResources.AuthenticationString).Return(true);
         }

         var digestHeaderExtractor = new DigestHeaderExtractor(extractor, headerParser, methodExtractor);
         using (repository.Playback())
         {
            DigestHeader digestHeader;
            bool result = digestHeaderExtractor.TryExtract(testMessage, out digestHeader);
            Assert.That(result, Is.True);
         }
      }
   }
}
