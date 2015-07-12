//-----------------------------------------------------------------------
// <copyright file="ResponseMessageFactory.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System.Net;
using System.ServiceModel.Channels;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for creating the http response message when a 
   /// client does not send the appropriate authentication header.
   /// </summary>
   internal class ResponseMessageFactory
   {
      private const string DigestAuthenticationHeaderName = "WWW-Authenticate";

      internal virtual Message CreateInvalidAuthorizationMessage(DigestHeader digestHeader)
      {
         var responseMessage = Message.CreateMessage(MessageVersion.None, null);
         HttpResponseMessageProperty responseProperty = CreateResponseProperty(digestHeader);
         responseMessage.Properties.Add(HttpResponseMessageProperty.Name, responseProperty);
         return responseMessage;
      }

      private static HttpResponseMessageProperty CreateResponseProperty(DigestHeader digestHeader)
      {
         var responseProperty = new HttpResponseMessageProperty();
         responseProperty.StatusCode = HttpStatusCode.Unauthorized;
         responseProperty.Headers.Add(DigestAuthenticationHeaderName, digestHeader.GenerateHeaderString());
         return responseProperty;
      }
   }
}