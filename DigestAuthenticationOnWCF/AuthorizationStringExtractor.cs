//-----------------------------------------------------------------------
// <copyright file="AuthorizationStringExtractor.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System.ServiceModel.Channels;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for extracting the authentication 
   /// string from the http message header.
   /// </summary>
   internal class AuthorizationStringExtractor
   {
      private const string AuthenticationHeaderName = "Authorization";

      internal virtual bool TryExtractAuthorizationHeader(Message message, out string authenticationString)
      {
         var requestMessageProperty = (HttpRequestMessageProperty)message.Properties[HttpRequestMessageProperty.Name];
         authenticationString = requestMessageProperty.Headers[AuthenticationHeaderName];
         return !string.IsNullOrEmpty(authenticationString);
      }
   }
}