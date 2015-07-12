//-----------------------------------------------------------------------
// <copyright file="MethodExtractor.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System.ServiceModel.Channels;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for extracting the HTTP method from the 
   /// incoming message.
   /// </summary>
   internal class MethodExtractor
   {
      private readonly string httpRequestMessagePropertyName = HttpRequestMessageProperty.Name;

      public virtual string Extract(Message message)
      {
         var requestMessageProperty = (HttpRequestMessageProperty)message.Properties[httpRequestMessagePropertyName];
         return requestMessageProperty.Method;
      }
   }
}