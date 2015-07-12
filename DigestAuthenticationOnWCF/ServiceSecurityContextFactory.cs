//-----------------------------------------------------------------------
// <copyright file="ServiceSecurityContextFactory.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using System.IdentityModel.Policy;
using System.ServiceModel;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for creating the service security context that is attached
   /// to the incoming message after it is positively validated.
   /// </summary>
   internal class ServiceSecurityContextFactory
   {
      private readonly AuthorizationPolicyFactory authorizationPolicyFactory;

      public ServiceSecurityContextFactory(AuthorizationPolicyFactory authorizationPolicyFactory)
      {
         this.authorizationPolicyFactory = authorizationPolicyFactory;
      }

      internal ServiceSecurityContext Create(string userName)
      {
         var authorizationPolicies = new List<IAuthorizationPolicy>();
         authorizationPolicies.Add(authorizationPolicyFactory.Create(userName));
         return new ServiceSecurityContext(authorizationPolicies.AsReadOnly());
      }
   }
}