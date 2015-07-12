//-----------------------------------------------------------------------
// <copyright file="DigestValidator.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for validating the digest from the incoming message.
   /// </summary>
   internal class DigestValidator
   {
      private readonly DigestEncoders digestEncoders;

      public DigestValidator(DigestEncoders digestEncoders)
      {
         this.digestEncoders = digestEncoders;
      }

      public bool Validate(DigestHeader digestHeader, string password)
      {
         DigestEncoderBase digestEncoder = digestEncoders.GetEncoder(digestHeader.Qop);
         string expectedResponse = digestEncoder.Encode(digestHeader, password);
         return expectedResponse == digestHeader.Response;
      }
   }
}