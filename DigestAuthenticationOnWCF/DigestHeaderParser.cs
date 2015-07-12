//-----------------------------------------------------------------------
// <copyright file="DigestHeaderParser.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for parsing and creating the digest header that is available
   /// in the incoming message.
   /// </summary>
   internal class DigestHeaderParser
   {
      private readonly HeaderKeyValueSplitter keyValueSplitter;

      public DigestHeaderParser(HeaderKeyValueSplitter keyValueSplitter)
      {
         this.keyValueSplitter = keyValueSplitter;
      }

      public virtual DigestHeader Parse(string digestHeaderString)
      {
         string headerString = RemoveDigestFromHeaderString(digestHeaderString);
         var header = new DigestHeader();
         string[] keyValueCombinations = headerString.Split(',');
         foreach (string keyValueCombination in keyValueCombinations)
         {
            HeaderKeyValue headerKeyValue = keyValueSplitter.Split(keyValueCombination);
            header.Set(headerKeyValue);
         }

         return header;
      }

      private static string RemoveDigestFromHeaderString(string headerString)
      {
         return headerString.Replace("Digest", string.Empty);
      }
   }
}