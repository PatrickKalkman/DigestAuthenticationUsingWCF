using System;
using DigestAuthenticationOnWCF;
using NUnit.Framework;

namespace DigestAuthenticationOnWCFTest
{
   [TestFixture]
   public class NonceTimeStampParserTest
   {
      [Test]
      public void ShouldCorrectlyParseNonceTimeStap()
      {
         var nonceTimeStampParser = new NonceTimeStampParser();
         DateTime parsedDateTime = nonceTimeStampParser.Parse(TestResources.NonceTimeStampAsString);
         Assert.That(parsedDateTime, Is.EqualTo(TestResources.NonceTimeStampAsDateTime));
      }

      [Test]
      [ExpectedException(typeof (ArgumentException))]
      public void ShouldThrowWhenInvalidString()
      {
         var nonceTimeStampParser = new NonceTimeStampParser();
         nonceTimeStampParser.Parse("sdfsdf");
      }
   }
}