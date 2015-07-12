//-----------------------------------------------------------------------
// <copyright file="IPAddressExtractor.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System.ServiceModel.Channels;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for extracting the ip-address from the header of the 
   /// incoming message.
   /// </summary>
   internal class IPAddressExtractor
   {
      public virtual bool TryExtractIpAddress(Message message, out string extractedIpAddress)
      {
         var remoteEndpointMessageProperty = (RemoteEndpointMessageProperty)message.Properties[RemoteEndpointMessageProperty.Name];
         extractedIpAddress = remoteEndpointMessageProperty.Address;
         return !string.IsNullOrEmpty(extractedIpAddress);
      }
   }
}