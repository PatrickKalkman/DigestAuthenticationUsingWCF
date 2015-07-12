//-----------------------------------------------------------------------
// <copyright file="DigestHeaderExtractor.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System.ServiceModel.Channels;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for extracting and decoding the 
   /// credentials from the encoded authorization string.
   /// </summary>
   internal class DigestHeaderExtractor
   {
      private readonly AuthorizationStringExtractor authorizationStringExtractor;
      private readonly DigestHeaderParser digestHeaderParser;
      private readonly MethodExtractor methodExtractor;

      internal DigestHeaderExtractor(
         AuthorizationStringExtractor authorizationStringExtractor, 
         DigestHeaderParser digestHeaderParser, 
         MethodExtractor methodExtractor)
      {
         this.authorizationStringExtractor = authorizationStringExtractor;
         this.digestHeaderParser = digestHeaderParser;
         this.methodExtractor = methodExtractor;
      }

      internal virtual bool TryExtract(Message requestMessage, out DigestHeader digestHeader)
      {
         string authenticationString;
         if (authorizationStringExtractor.TryExtractAuthorizationHeader(requestMessage, out authenticationString))
         {
            digestHeader = digestHeaderParser.Parse(authenticationString);
            digestHeader.Method = methodExtractor.Extract(requestMessage);
            return true;
         }

         digestHeader = null;
         return false;
      }
   }
}