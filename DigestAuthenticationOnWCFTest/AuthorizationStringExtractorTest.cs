using System.ServiceModel.Channels;
using DigestAuthenticationOnWCF;
using NUnit.Framework;

namespace DigestAuthenticationOnWCFTest
{
   [TestFixture]
   public class AuthorizationStringExtractorTest
   {
      [Test]
      public void ShouldExtractAuthenticationStringFromMessageHeader()
      {
         using (Message testMessage = TestResources.CreateMessage())
         {
            AuthorizationStringExtractor extractor = new AuthorizationStringExtractor();
            string authenticationString;
            bool result = extractor.TryExtractAuthorizationHeader(testMessage, out authenticationString);
            Assert.That(result, Is.True);
            Assert.That(authenticationString, Is.EqualTo(TestResources.AuthenticationString));
         }
      }
   }
}
