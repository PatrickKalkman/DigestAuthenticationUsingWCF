using System;
using System.ServiceModel.Channels;
using DigestAuthenticationOnWCF;
using NUnit.Framework;

namespace DigestAuthenticationOnWCFTest
{
   [TestFixture]
   public class MethodExtractorTest
   {
      [Test]
      public void Should_Extract_Http_Method_From_Message_Header()
      {
         var methodExtractor = new MethodExtractor();
         using (Message testMessage = TestResources.CreateMessage())
         {
            string method = methodExtractor.Extract(testMessage);
            Assert.That(method, Is.EqualTo("GET"));
         }
      }
   }
}
