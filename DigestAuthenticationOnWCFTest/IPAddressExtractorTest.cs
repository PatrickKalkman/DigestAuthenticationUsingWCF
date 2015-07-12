using System.ServiceModel.Channels;
using DigestAuthenticationOnWCF;
using NUnit.Framework;

namespace DigestAuthenticationOnWCFTest
{
   [TestFixture]
   public class IPAddressExtractorTest
   {
      [Test]
      public void Should_Extract_Valid_IpAddress_From_Incoming_WCF_Request()
      {
         var ipAddressExtractor = new IPAddressExtractor();
         using (Message message = TestResources.CreateMessage(TestResources.IpAddress, 1010))
         {
            string extractedIpAddress;
            bool result = ipAddressExtractor.TryExtractIpAddress(message, out extractedIpAddress);
            Assert.That(extractedIpAddress, Is.EqualTo(TestResources.IpAddress));
            Assert.That(result, Is.True);
         }
      }
   }
}