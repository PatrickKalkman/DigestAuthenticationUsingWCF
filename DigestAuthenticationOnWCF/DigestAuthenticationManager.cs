//-----------------------------------------------------------------------
// <copyright file="DigestAuthenticationManager.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Web.Security;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class acts as the workhorse of the application. It validates the incoming messages, creates the required
   /// security context and sends invalid messages when the client does not authenticate properly.
   /// </summary>
   internal class DigestAuthenticationManager
   {
      private readonly ResponseMessageFactory responseMessageFactory;
      private readonly IPAddressExtractor ipAddressExtractor;
      private readonly MembershipProvider membershipProvider;
      private readonly DigestHeaderExtractor digestHeaderExtractor;
      private readonly ServiceSecurityContextFactory serviceSecurityContextFactory;
      private readonly NonceValidator nonceValidator;
      private readonly DigestValidator digestValidator;
      private readonly DigestHeaderFactory digestHeaderGenerator;

      public DigestAuthenticationManager(
         ResponseMessageFactory responseMessageFactory,
         IPAddressExtractor ipAddressExtractor,
         MembershipProvider membershipProvider,
         DigestHeaderExtractor digestHeaderExtractor,
         ServiceSecurityContextFactory serviceSecurityContextFactory,
         NonceValidator nonceValidator,
         DigestValidator digestValidator,
         DigestHeaderFactory digestHeaderGenerator)
      {
         this.responseMessageFactory = responseMessageFactory;
         this.ipAddressExtractor = ipAddressExtractor;
         this.membershipProvider = membershipProvider;
         this.digestHeaderExtractor = digestHeaderExtractor;
         this.serviceSecurityContextFactory = serviceSecurityContextFactory;
         this.nonceValidator = nonceValidator;
         this.digestValidator = digestValidator;
         this.digestHeaderGenerator = digestHeaderGenerator;
      }

      public virtual Message CreateInvalidAuthenticationRequest(Message requestMessage)
      {
         string ipAddress;
         if (ipAddressExtractor.TryExtractIpAddress(requestMessage, out ipAddress))
         {
            return responseMessageFactory.CreateInvalidAuthorizationMessage(digestHeaderGenerator.Generate(ipAddress));
         }

         throw new ArgumentException("Given message does not contain an ip address.");
      }

      internal virtual bool AuthenticateRequest(Message requestMessage)
      {
         DigestHeader digestHeader;
         if (digestHeaderExtractor.TryExtract(requestMessage, out digestHeader))
         {
            string ipAddress;
            if (ipAddressExtractor.TryExtractIpAddress(requestMessage, out ipAddress))
            {
               if (nonceValidator.Validate(digestHeader.Nonce, ipAddress))
               {
                  if (!nonceValidator.IsStale(digestHeader.Nonce))
                  {
                     MembershipUser membershipUser = membershipProvider.GetUser(digestHeader.UserName, false);
                     if (membershipUser != null)
                     {
                        if (digestValidator.Validate(digestHeader, membershipUser.GetPassword()))
                        {
                           AddSecurityContextToMessage(requestMessage, digestHeader.UserName);
                           return true;
                        }
                     }
                  }
               }
            }
         }

         return false;
      }

      private void AddSecurityContextToMessage(Message requestMessage, string userName)
      {
         if (requestMessage.Properties.Security == null)
         {
            requestMessage.Properties.Security = new SecurityMessageProperty();
         }

         requestMessage.Properties.Security.ServiceSecurityContext = serviceSecurityContextFactory.Create(userName);
      }
   }
}