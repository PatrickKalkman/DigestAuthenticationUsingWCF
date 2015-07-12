//-----------------------------------------------------------------------
// <copyright file="RequestInterceptorFactory.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System.Web.Security;
using Microsoft.ServiceModel.Web;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for creating the RequestInterceptor that attach the new
   /// functionality to WCF.
   /// </summary>
   public static class RequestInterceptorFactory
   {
      public static RequestInterceptor Create(string realm, string privateKey, MembershipProvider membershipProvider)
      {
         var base64Converter = new Base64Converter();
         var privateHashEncoder = new PrivateHashEncoder(privateKey, new MD5Encoder());
         var generator = new NonceGenerator(privateHashEncoder, base64Converter);
         var digestHeaderGenerator = new DigestHeaderFactory(generator, realm);
         var digestValidator = new DigestValidator(new DigestEncoders(new MD5Encoder()));
         var digestHeaderExtractor = new DigestHeaderExtractor(new AuthorizationStringExtractor(), new DigestHeaderParser(new HeaderKeyValueSplitter('=')), new MethodExtractor());
         var nonceValidator = new NonceValidator(privateHashEncoder, base64Converter, new NonceTimeStampParser(), 600);
         var digestAuthenticationManager = new DigestAuthenticationManager(
            new ResponseMessageFactory(),
            new IPAddressExtractor(),
            membershipProvider,
            digestHeaderExtractor,
            new ServiceSecurityContextFactory(new AuthorizationPolicyFactory()),
            nonceValidator,
            digestValidator,
            digestHeaderGenerator);
         return new DigestAuthenticationInterceptor(digestAuthenticationManager);
      }
   }
}