//-----------------------------------------------------------------------
// <copyright file="PrincipalAuthorizationPolicy.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Security.Principal;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class implements a principal authorization policy and is attached to 
   /// the incoming request when positively validated.
   /// </summary>
   internal class PrincipalAuthorizationPolicy : IAuthorizationPolicy
   {
      private readonly IPrincipal principal;

      private readonly string policyId = Guid.NewGuid().ToString();

      public PrincipalAuthorizationPolicy(IPrincipal principal)
      {
         this.principal = principal;
      }

      public ClaimSet Issuer
      {
         get { return ClaimSet.System; }
      }

      public string Id
      {
         get { return policyId; }
      }

      public bool Evaluate(EvaluationContext evaluationContext, ref object state)
      {
         if (evaluationContext != null)
         {
            evaluationContext.AddClaimSet(this, new DefaultClaimSet(Claim.CreateNameClaim(principal.Identity.Name)));
            evaluationContext.Properties["Identities"] = new List<IIdentity>(new[] { principal.Identity });
            evaluationContext.Properties["Principal"] = principal;
            return true;
         }

         return false;
      }
   }
}