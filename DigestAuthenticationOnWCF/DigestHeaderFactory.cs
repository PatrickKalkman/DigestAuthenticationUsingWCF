//-----------------------------------------------------------------------
// <copyright file="DigestHeaderFactory.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for generating the digest header that is send to the 
   /// client from the server. The default algorithm return is Auth.
   /// </summary>
   internal class DigestHeaderFactory
   {
      private readonly NonceGenerator nonceGenerator;
      private readonly string realm;

      public DigestHeaderFactory(NonceGenerator nonceGenerator, string realm)
      {
         this.nonceGenerator = nonceGenerator;
         this.realm = realm;
      }

      public virtual DigestHeader Generate(string ipAddress)
      {
         var digestHeader = new DigestHeader();
         digestHeader.Realm = realm;
         digestHeader.Nonce = nonceGenerator.Generate(ipAddress);
         digestHeader.Qop = DigestQop.Auth;
         return digestHeader;
      }
   }
}