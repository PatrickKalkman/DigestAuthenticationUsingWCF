//-----------------------------------------------------------------------
// <copyright file="DigestAuthenticationInterceptor.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System.ServiceModel.Channels;
using Microsoft.ServiceModel.Web;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class connects to the extension point of WCF, the RequestInterceptor. 
   /// RequestInterceptor is a class that processes each request that is sent 
   /// to the WCF service.
   /// </summary>
   internal class DigestAuthenticationInterceptor : RequestInterceptor
   {
      private readonly DigestAuthenticationManager authenticationManager;

      public DigestAuthenticationInterceptor(DigestAuthenticationManager authenticationManager) : base(false)
      {
         this.authenticationManager = authenticationManager;
      }

      public override void ProcessRequest(ref RequestContext requestContext)
      {
         if (requestContext == null || !authenticationManager.AuthenticateRequest(requestContext.RequestMessage))
         {
            requestContext = CreateAndSendInvalidAuthenticationRequest(requestContext);
         }
      }

      private RequestContext CreateAndSendInvalidAuthenticationRequest(RequestContext requestContext)
      {
         using (Message responseMessage = authenticationManager.CreateInvalidAuthenticationRequest(requestContext.RequestMessage))
         {
            requestContext.Reply(responseMessage);
            requestContext = null;
            return requestContext;
         }
      }
   }
}