using DigestAuthenticationOnWCF;
using NUnit.Framework;

namespace DigestAuthenticationOnWCFTest
{
   [TestFixture]
   public class KeyValueSplitterTest
   {
      [Test]
      public void ShouldSplitKeyValueOnFirstAssignment()
      {
         const string GeneratedNonce = "nonce=\"NjM0MzI2MTgzNjc2NzUsOTpFRjE1RUZDNTgzQUZFMUYwNTg4QTgwRkIyQThDNjVENg==\"";
         var keyValueSplitter = new HeaderKeyValueSplitter(TestResources.HeaderKeyValueSplitCharacter);
         HeaderKeyValue pair = keyValueSplitter.Split(GeneratedNonce);
         Assert.That(pair.Key, Is.EqualTo("nonce"));
         Assert.That(pair.Value, Is.EqualTo("NjM0MzI2MTgzNjc2NzUsOTpFRjE1RUZDNTgzQUZFMUYwNTg4QTgwRkIyQThDNjVENg=="));
      }
   }
}