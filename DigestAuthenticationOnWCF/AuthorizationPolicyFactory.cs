//-----------------------------------------------------------------------
// <copyright file="AuthorizationPolicyFactory.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System.IdentityModel.Policy;
using System.Security.Principal;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for creating a authorization policy based on the 
   /// given credentials.
   /// </summary>
   internal class AuthorizationPolicyFactory
   {
      public virtual IAuthorizationPolicy Create(string userName)
      {
         var genericIdentity = new GenericIdentity(userName);
         var genericPrincipal = new GenericPrincipal(genericIdentity, new string[] { });
         return new PrincipalAuthorizationPolicy(genericPrincipal);
      }
   }
}