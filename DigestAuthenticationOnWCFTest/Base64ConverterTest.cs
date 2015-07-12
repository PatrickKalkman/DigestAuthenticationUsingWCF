using DigestAuthenticationOnWCF;
using NUnit.Framework;

namespace DigestAuthenticationOnWCFTest
{
   [TestFixture]
   public class Base64ConverterTest
   {
      [Test]
      public void ShouldDecodeBase64String()
      {
         const string Base64EncodedData = "SGVsbG8gQmFzZTY0";
         const string DecodedData = "Hello Base64";

         var base64Converter = new Base64Converter();
         string result = base64Converter.Decode(Base64EncodedData);

         Assert.That(DecodedData, Is.EqualTo(result));
      }

      [Test]
      public void ShouldReturnNullWhenNullIsGivenToDecode()
      {
         var base64Converter = new Base64Converter();
         string result = base64Converter.Decode(null);
         Assert.That(result, Is.EqualTo(null));
      }

      [Test]
      public void ShouldBase64EncodeARandomString()
      {
         var base64Converter = new Base64Converter();
         string result = base64Converter.Encode("Hello Base64");
         Assert.That(result, Is.EqualTo("SGVsbG8gQmFzZTY0"));
      }

      [Test]
      public void ShouldReturnNullWhenNullIsGivenToEncode()
      {
         var base64Converter = new Base64Converter();
         string result = base64Converter.Encode(null);
         Assert.That(result, Is.EqualTo(null));
      }
   }
}